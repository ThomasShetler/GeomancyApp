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


def split_list_phrase(text: str) -> list[str]:
    """Split a comma / and / or list into discrete items."""
    text = text.strip().rstrip(".")
    if not text:
        return []

    parts = [p.strip() for p in re.split(r",\s*", text) if p.strip()]
    if len(parts) == 1:
        multi = re.split(r"\s+(?:and|or)\s+", parts[0])
        return [s.strip() for s in multi if s.strip()]

    items: list[str] = []
    for part in parts:
        cleaned = re.sub(r"^(?:and|or)\s+", "", part, flags=re.I).strip()
        if cleaned:
            items.append(cleaned)
    return items


def parse_question_involves(sig: str) -> tuple[list[str], list[str]]:
    """Return (question_involves items, leftover additional detail sentences)."""
    sig = (sig or "").strip()
    if not sig:
        return [], []

    m = re.match(r"^The question involves\s+(.+)$", sig, re.I | re.S)
    if not m:
        return [], [s.strip() for s in re.split(r"(?<=\.)\s+", sig) if s.strip()]

    body = m.group(1).strip()
    sentences = re.split(r"(?<=\.)\s+", body)
    list_sentence = sentences[0]
    extras = [s.strip() for s in sentences[1:] if s.strip()]
    items = split_list_phrase(list_sentence)
    # Normalize awkward "or when ..." list tails into clean bullets.
    cleaned_items = []
    for item in items:
        if item.lower().startswith("when the true question is deliberately hidden"):
            cleaned_items.append("cases where the true question is deliberately hidden")
        else:
            cleaned_items.append(item)
    return cleaned_items, extras


def filter_caution_notes(notes: str) -> list[str]:
    """Remove Rubeus/Cauda/Populus caution sentences; keep rejoice + craft notes."""
    text = (notes or "").strip()
    if not text:
        return []

    # Drop known caution sentences (may span multiple sentences).
    text = re.sub(
        r"Traditionally,\s*if the figure Rubeus appears here.*?(?=Mercury traditionally|\Z)",
        "",
        text,
        flags=re.I | re.S,
    )
    text = re.sub(
        r"If Rubeus is in the Eleventh house and Populus in the First[^.]*\.",
        "",
        text,
        flags=re.I,
    )
    text = re.sub(r"\s+", " ", text).strip()
    if not text:
        return []

    return [s.strip() for s in re.split(r"(?<=\.)\s+", text) if s.strip()]


def transform_house(h: dict) -> dict:
    governs = list(h.get("governs") or [])
    question_involves, sig_extras = parse_question_involves(h.get("significator_of_quesited_when", ""))
    additional = filter_caution_notes(h.get("notes", ""))
    additional = sig_extras + additional

    return {
        "id": h["id"],
        "ordinal": HOUSE_ORDINALS.get(h["id"], h.get("house", "")),
        "governs": governs,
        "question_involves": question_involves,
        "additional_details": additional,
        "example_questions": h.get("example_questions", []),
        "source": {
            "work": LICENSE_META["work"],
            "chapter": "Chapter 6 — The Twelve Houses",
            "pages": "103–111",
            "attribution": ATTRIBUTION,
        },
    }


def transform_houses(house_json: dict) -> dict:
    houses = house_json["HouseData"]["Houses"]
    greer_houses = [transform_house(h) for h in houses]
    return {
        "GreerHouseData": {
            "schema_version": 2,
            "license": {**LICENSE_META, "approved_pages": "103–111"},
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
