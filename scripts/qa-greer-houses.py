#!/usr/bin/env python3
"""QA: compare structured GreersHouseData against ab4526e source HouseData."""
import json
import re
import subprocess
from pathlib import Path

REPO = Path(__file__).resolve().parents[1] if (Path(__file__).name == "qa-greer-houses.py") else Path(r"c:\Users\tommy\Geomancy App\GeomancyApp")


def norm(s: str) -> str:
    return re.sub(r"\s+", " ", (s or "").strip().lower())


def tokens(text: str) -> set[str]:
    return set(re.findall(r"[a-z0-9']{4,}", norm(text)))


def is_caution(s: str) -> bool:
    n = norm(s)
    keys = [
        "rubeus appears here",
        "cauda draconis appears here",
        "populus is in the first and rubeus",
        "rubeus is in the eleventh house and populus",
        "fabricated a fake question",
        "fake question to test",
        "made up a fake question",
    ]
    return any(k in n for k in keys)


def main() -> None:
    current = json.loads(
        (REPO / "databank/HouseAndCourtDirectory/GreersHouseData.json").read_text(encoding="utf-8")
    )
    src = json.loads(
        subprocess.check_output(
            ["git", "show", "ab4526e:HouseAndCourtDirectory/HouseData.json"],
            cwd=REPO,
        ).decode("utf-8")
    )

    # Also reconstruct prior blob description for houses (what UI used to show)
    # from pre-split committed version if available
    try:
        old = json.loads(
            subprocess.check_output(
                ["git", "show", "HEAD:databank/HouseAndCourtDirectory/GreersHouseData.json"],
                cwd=REPO,
            ).decode("utf-8")
        )
    except subprocess.CalledProcessError:
        old = None

    all_ok = True
    for h in src["HouseData"]["Houses"]:
        hid = h["id"]
        cur = next(x for x in current["GreerHouseData"]["houses"] if x["id"] == hid)
        src_governs = h.get("governs") or []
        sig = (h.get("significator_of_quesited_when") or "").strip()
        notes = (h.get("notes") or "").strip()
        issues = []

        if src_governs != cur.get("governs"):
            issues.append(f"GOVERNS mismatch src={src_governs!r} cur={cur.get('governs')!r}")

        note_sents = [s.strip() for s in re.split(r"(?<=\.)\s+", notes) if s.strip()] if notes else []
        sig_sents = [s.strip() for s in re.split(r"(?<=\.)\s+", sig) if s.strip()] if sig else []
        add_blob = norm(" ".join(cur.get("additional_details") or []))
        qi_blob = norm(" ".join(cur.get("question_involves") or []))

        if sig.startswith("The question involves"):
            body = sig[len("The question involves ") :].strip()
            parts = re.split(r"(?<=\.)\s+", body)
            list_part = parts[0]
            extras = parts[1:]
            # Known readability rewrites of awkward "or when ..." list tails.
            qi_aliases = {
                "cases where the true question is deliberately hidden": (
                    "when the true question is deliberately hidden"
                ),
            }
            for item in cur.get("question_involves") or []:
                expected = qi_aliases.get(norm(item), norm(item))
                if expected not in norm(list_part) and norm(item) not in norm(list_part):
                    issues.append(f"QI item not found in source list: {item!r}")
            # word coverage from list into QI (ignore connector words)
            stop = {
                "involves",
                "question",
                "the",
                "and",
                "with",
                "from",
                "that",
                "this",
                "when",
                "into",
                "over",
                "between",
                "or",
                "cases",
                "where",
            }
            # Count aliased rewrite words toward coverage.
            qi_cov = qi_blob
            for rewritten, original in qi_aliases.items():
                if rewritten in qi_cov:
                    qi_cov = qi_cov.replace(rewritten, original)
            lost = sorted((tokens(list_part) - stop) - tokens(qi_cov))
            if lost:
                issues.append(f"QI word loss from list sentence: {', '.join(lost)}")
            for e in extras:
                if norm(e) not in add_blob:
                    issues.append(f"SIG trailing sentence missing from additional: {e!r}")
        else:
            for s in sig_sents:
                if norm(s) not in add_blob:
                    issues.append(f"SIG sentence missing from additional: {s!r}")

        for s in note_sents:
            if is_caution(s):
                # should not leak caution content into structured fields
                for field_name, blob in (
                    ("additional", add_blob),
                    ("question_involves", qi_blob),
                    ("governs", norm(" ".join(cur.get("governs") or []))),
                ):
                    # only flag if distinctive caution phrases remain
                    if any(
                        k in blob
                        for k in (
                            "not being honest",
                            "won't listen",
                            "already decided",
                            "fake question",
                            "test or trick",
                        )
                    ):
                        issues.append(f"CAUTION leaked into {field_name}: {s[:80]!r}...")
                continue
            if norm(s) not in add_blob:
                issues.append(f"NOTE sentence missing from additional: {s!r}")

        # Compare against previous description blob if present
        if old and old.get("GreerHouseData", {}).get("houses"):
            old_h = next(
                (x for x in old["GreerHouseData"]["houses"] if x["id"] == hid),
                None,
            )
            if old_h and old_h.get("description"):
                old_desc = old_h["description"]
                # Strip leading "The Nth house traditionally governs X." wrapper and caution text
                residual = old_desc
                # Remove governs joined form
                if src_governs:
                    governs_text = "; ".join(src_governs)
                    residual = residual.replace(
                        f"The {cur['ordinal']} house traditionally governs {governs_text}.",
                        "",
                        1,
                    )
                # Remove known caution blobs from house 1/11 notes that were in chart_cautions not description
                # For description comparison: content words from residual should appear in QI+additional
                residual_words = tokens(residual) - {
                    "traditionally",
                    "governs",
                    "house",
                    "first",
                    "second",
                    "third",
                    "fourth",
                    "fifth",
                    "sixth",
                    "seventh",
                    "eighth",
                    "ninth",
                    "tenth",
                    "eleventh",
                    "twelfth",
                    "question",
                    "involves",
                }
                # strip caution words that shouldn't be required
                caution_words = tokens(
                    "rubeus cauda draconis populus honest listen decided fabricated fake trick divinatory"
                )
                # For houses 1/11, old description may NOT have included caution notes (extract skipped notes for 1,11)
                keep = residual_words - caution_words
                structured_blob = qi_blob + " " + add_blob + " " + " ".join(cur.get("governs") or [])
                # Map known readability rewrites back to source phrasing for coverage.
                structured_blob = structured_blob.replace(
                    "cases where the true question is deliberately hidden",
                    "when the true question is deliberately hidden",
                )
                have = tokens(structured_blob)
                lost_old = sorted(keep - have)
                # Filter very common glue
                lost_old = [w for w in lost_old if w not in {"also", "always", "holds", "acts", "never", "when", "cases", "where"}]
                if lost_old:
                    issues.append(f"OLD_DESC word loss vs structured: {', '.join(lost_old[:25])}")

        print("=" * 64)
        print(f"HOUSE {hid} {cur['ordinal']}")
        print(f"  governs={len(cur.get('governs') or [])} qi={len(cur.get('question_involves') or [])} add={len(cur.get('additional_details') or [])}")
        if issues:
            all_ok = False
            print("  ISSUES:")
            for i in issues:
                print(f"    - {i}")
        else:
            print("  OK — no content loss detected vs source")

    print("=" * 64)
    print("PASS" if all_ok else "FAIL — see issues above")


if __name__ == "__main__":
    main()
