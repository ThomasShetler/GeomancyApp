#!/usr/bin/env python3
"""Extract licensed Greer reference JSON from git history sources.

Figures: a217c5f GeomancyApp/FigureData.cs (JGM Art and Practice of Geomancy, Ch. 3)
Houses:  ab4526e HouseAndCourtDirectory/HouseData.json (Greer Ch. 6 mapping)
"""
import json
import re
import subprocess
import sys
from pathlib import Path

REPO = Path(__file__).resolve().parents[1]
FIGURE_COMMIT = "a217c5f"
FIGURE_PATH = "GeomancyApp/FigureData.cs"
HOUSE_COMMIT = "ab4526e"
HOUSE_PATH = "HouseAndCourtDirectory/HouseData.json"

ATTRIBUTION = (
    "Material excerpted from The Art and Practice of Geomancy © 2009, "
    "John Michael Greer with permission from Red Wheel/Weiser LLC. "
    "Newburyport, MA www.redwheelweiser.com"
)

LICENSE_META = {
    "work": "The Art and Practice of Geomancy",
    "author": "John Michael Greer",
    "publisher": "Red Wheel/Weiser LLC",
    "permission_date": "2026-06-10",
    "attribution": ATTRIBUTION,
}

FIGURE_ORDER = [
    "Puer", "Amissio", "Albus", "Populus", "Fortuna Major", "Conjunctio",
    "Puella", "Rubeus", "Acquisitio", "Carcer", "Tristitia", "Laetitia",
    "Cauda Draconis", "Caput Draconis", "Fortuna Minor", "Via",
]

HOUSE_ORDINALS = {
    1: "First", 2: "Second", 3: "Third", 4: "Fourth", 5: "Fifth", 6: "Sixth",
    7: "Seventh", 8: "Eighth", 9: "Ninth", 10: "Tenth", 11: "Eleventh", 12: "Twelfth",
}

HOUSE_NAME_TO_ID = {
    "first": 1, "second": 2, "third": 3, "fourth": 4, "fifth": 5, "sixth": 6,
    "seventh": 7, "eighth": 8, "ninth": 9, "tenth": 10, "eleventh": 11, "twelfth": 12,
}


def git_show(commit: str, path: str) -> str:
    result = subprocess.run(
        ["git", "show", f"{commit}:{path}"],
        cwd=REPO,
        capture_output=True,
        text=True,
        check=True,
    )
    return result.stdout


def parse_figure_name(raw: str) -> tuple[str, str]:
    m = re.match(r"^(.+?)\s*\((.+)\)$", raw.strip())
    if m:
        return m.group(1).strip(), m.group(2).strip()
    return raw.strip(), ""


def house_name_to_id(name: str) -> int:
    return HOUSE_NAME_TO_ID.get(name.strip().lower(), 0)


def parse_figure_cs(content: str) -> list[dict]:
    figures = []
    case_pattern = re.compile(r'case\s+"([^"]+)":\s*\n\s*return\s+new\s+FigureData\s*\{', re.MULTILINE)
    prop_pattern = re.compile(r'(\w+)\s*=\s*"((?:[^"\\]|\\.)*)"', re.DOTALL)

    matches = list(case_pattern.finditer(content))
    for i, match in enumerate(matches):
        case_name = match.group(1)
        start = match.end()
        end = matches[i + 1].start() if i + 1 < len(matches) else content.find("default:", start)
        block = content[start:end]

        props = {}
        for prop_match in prop_pattern.finditer(block):
            key, val = prop_match.group(1), prop_match.group(2)
            props[key] = val.replace("\\\"", "\"").replace("\\n", "\n")

        raw_name = props.get("Name", case_name.title())
        name, english = parse_figure_name(raw_name)
        figure_id = str(FIGURE_ORDER.index(name) + 1) if name in FIGURE_ORDER else "0"

        figures.append({
            "figure_id": figure_id,
            "name": name,
            "english_name": english,
            "other_names": props.get("OtherNames", ""),
            "keyword": props.get("Keyword", ""),
            "quality": props.get("Quality", ""),
            "planet": props.get("Planet", ""),
            "sign": props.get("Sign", ""),
            "imagery": props.get("Imagery", ""),
            "strong_house": props.get("StrongHouse", ""),
            "strong_house_id": house_name_to_id(props.get("StrongHouse", "")),
            "weak_house": props.get("WeakHouse", ""),
            "weak_house_id": house_name_to_id(props.get("WeakHouse", "")),
            "outer_el": props.get("OuterEl", ""),
            "inner_el": props.get("InnerEl", ""),
            "fire_element": props.get("FireElement", ""),
            "air_element": props.get("AirElement", ""),
            "water_element": props.get("WaterElement", ""),
            "earth_element": props.get("EarthElement", ""),
            "anatomy": props.get("Anatomy", ""),
            "body_type": props.get("BodyType", ""),
            "character_type": props.get("CharacterType", ""),
            "colors": props.get("Colors", ""),
            "commentary": props.get("Commentary", ""),
            "divinatory_meaning": props.get("DivinatoryMeaning", ""),
            "source": {
                "work": LICENSE_META["work"],
                "chapter": "Chapter 3 — The Geomantic Figures",
                "pages": "39–64",
                "attribution": ATTRIBUTION,
            },
        })

    figures.sort(key=lambda f: int(f["figure_id"]))
    return figures


def build_house_description(house: dict) -> str:
    ordinal = HOUSE_ORDINALS.get(house["id"], house.get("house", ""))
    governs = house.get("governs", [])
    governs_text = "; ".join(governs) if governs else ""
    sig = house.get("significator_of_quesited_when", "")
    parts = [f"The {ordinal} house traditionally governs {governs_text}."]
    if sig:
        parts.append(sig)
    notes = house.get("notes", "")
    if notes and house["id"] not in (1, 11):
        parts.append(notes)
    return " ".join(parts)


def build_chart_cautions(houses: list[dict]) -> str:
    h1 = next((h for h in houses if h["id"] == 1), {})
    h11 = next((h for h in houses if h["id"] == 11), {})
    parts = [
        "In some books on geomancy, you'll encounter the claim that the figures Rubeus and "
        "Cauda Draconis are unspeakably bad omens if they appear in the First house of a geomantic chart. "
        "When this happens, the geomancer is supposed to stop the divination, destroy the chart, and wait "
        "at least two hours before trying again.",
        h1.get("notes", ""),
        h11.get("notes", ""),
        "Still, it's not necessary to stop a reading if any of these signs appear. If you feel comfortable "
        "doing so, you might mention the traditional meaning of the Figure and ask the querent if it might "
        "have any bearing on the reading. If you are doing a reading for yourself, confront yourself directly: "
        "Are you being honest with yourself? Will you listen if the reading tells you your preconceptions are "
        "wrong? Is the question you're asking what you really want to know about?",
    ]
    return " ".join(p for p in parts if p)


def transform_houses(house_json: dict) -> dict:
    houses = house_json["HouseData"]["Houses"]
    greer_houses = []
    for h in houses:
        greer_houses.append({
            "id": h["id"],
            "ordinal": HOUSE_ORDINALS.get(h["id"], h.get("house", "")),
            "description": build_house_description(h),
            "example_questions": h.get("example_questions", []),
            "source": {
                "work": LICENSE_META["work"],
                "chapter": "Chapter 6 — The Twelve Houses",
                "pages": "103–111",
                "attribution": ATTRIBUTION,
            },
        })
    return {
        "GreerHouseData": {
            "schema_version": 1,
            "license": {**LICENSE_META, "approved_pages": "103–111 (plus chart cautions intro)"},
            "chart_cautions": build_chart_cautions(houses),
            "houses": greer_houses,
        }
    }


def main() -> int:
    figure_cs = git_show(FIGURE_COMMIT, FIGURE_PATH)
    figures = parse_figure_cs(figure_cs)
    if len(figures) != 16:
        print(f"ERROR: expected 16 figures, got {len(figures)}", file=sys.stderr)
        return 1

    figure_out = {
        "GreerFigureCorpus": {
            "schema_version": 1,
            "license": {**LICENSE_META, "approved_pages": "39–64"},
            "figures": figures,
        }
    }

    house_json = json.loads(git_show(HOUSE_COMMIT, HOUSE_PATH))
    house_out = transform_houses(house_json)

    figure_path = REPO / "databank" / "FigureCorpus" / "GreersFigures.json"
    house_path = REPO / "databank" / "HouseAndCourtDirectory" / "GreersHouseData.json"
    figure_path.write_text(json.dumps(figure_out, indent=2, ensure_ascii=False) + "\n", encoding="utf-8")
    house_path.write_text(json.dumps(house_out, indent=2, ensure_ascii=False) + "\n", encoding="utf-8")

    print(f"Wrote {len(figures)} figures -> {figure_path}")
    print(f"Wrote {len(house_out['GreerHouseData']['houses'])} houses -> {house_path}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
