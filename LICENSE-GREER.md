# Greer Reference License

Licensed excerpts from *The Art and Practice of Geomancy* by John Michael Greer are distributed separately from the Geofancy corpus (CC BY-NC 4.0).

## Permission

Permission granted **June 10, 2026** by **Red Wheel/Weiser LLC** to **Thomas Shetler** for use in **Geofancy Web App, C# code lib** (digital application format).

Permission letter: [licenses/Greer-RedWheelWeiser-2026-06-10.pdf](Greer-RedWheelWeiser-2026-06-10.pdf)

## Approved material

| Content | Book pages |
|---------|------------|
| Chapter 3 — Geomantic Figure Interpretations | 39–64 |
| Excerpt on the Twelve Houses | 103–111 |
| Chart cautions intro (Rubeus/Cauda/Populus) | ~101–102 (included per project scope) |

## Data files

- `databank/FigureCorpus/GreersFigures.json`
- `databank/HouseAndCourtDirectory/GreersHouseData.json`

Source extraction: git commit `a217c5f` (figure definitions) and `ab4526e` (house directory mapping), via `scripts/extract-greer-from-git.py`.

Raw OCR reference files (`GreersFigureReferences`, `GreersHouseReference`) are kept as provenance only and are **not** loaded at runtime.

## Required attribution

Whenever Greer prose is displayed in the application, the following credit line must appear:

> Material excerpted from *The Art and Practice of Geomancy* © 2009, John Michael Greer with permission from Red Wheel/Weiser LLC. Newburyport, MA www.redwheelweiser.com

This string is defined in code as `GreerLicenseConstants.Attribution`.

## Scope

This permission applies only to the designated digital application use. It does not extend the CC BY-NC Geofancy corpus license to Greer content, and Greer content must not be merged into `Figures.json` or `HouseData.json`.

© 2009 John Michael Greer. Used by permission of Red Wheel/Weiser LLC.
