<p align="center">
  <img src="assets/logo.png" alt="Geofancy logo" width="220" />
</p>

<h1 align="center">Geofancy</h1>

<p align="center">
  <em>The most advanced geomancy software available, built with users in mind by a devoted practitioner — powered by the Geofancy engine.</em>
</p>


**Live app:** <https://geofancy.up.railway.app>

> **Official release (v1.0.7).** Geofancy is a production-ready web workspace with a live geomancy wiki, classical house reference, licensed Greer's references, interactive casting walkthroughs, and the full perfections and Way of Points stack. Bug reports and feedback are welcome on [GitHub Issues](https://github.com/ThomasShetler/GeomancyApp/issues).

---

## What Geofancy does

Geofancy is a complete digital tool for traditional Western geomancy — the Renaissance art of casting and interpreting charts of sixteen elemental figures. The app generates a full chart from a question, calculates the Mothers, Daughters, Nieces, Witnesses, Judge, and Reconciler, and walks the practitioner through:

- The four classical **perfections** (Occupation, Translation, Mutation, Conjunction) and their supporting and opposing aspects
- The **Way of Points** for tracing the path of an outcome through the chart
- A complete **Court and Houses** reference panel with original interpretive corpus for every figure in every house and court position
- A printable, responsive chart view with traditional shield notation

It is meant for both the working practitioner and the serious student.

## Features

- **Two workspaces.** Wide desktop layout and mobile-first flow with chart drawer, mothers casting shell, and walkthrough that opens your chart on `/mobile` when you finish on a phone.
- **Original corpus.** Figure, house, court, and slot text written from primary sources and practice — now aligned to classical house doctrine with Read When, Reading Craft, and misreading guards.
- **Greer's references.** Optional licensed overlay from *The Art and Practice of Geomancy* (2009) in Court & Houses — Off, Alongside, Override, or Greer only — with attribution and Critical Context flags.
- **Geomancy Wiki.** Live figure and house glossaries, filterable house index, classical articles, casting how-tos, and interactive shield walkthrough.
- **Mothers casting walkthrough.** Desktop side-panel dot casting aligned to the shield chart; mobile parity with scrollable figure detail during stage review.
- **Live perfections engine.** Querent / Quesited selection with every perfection, modifier, and aspect populated with practitioner-facing tips.
- **Way of Points analyzer.** Trace elemental paths, path types, break conditions, and Classic Way of Points / Way of the Light.
- **House & court inspector.** Scope-labeled detail panels with house summary cards, elemental analysis, traditional imagery, and fully cited sources.
- **Share, export & restore.** Seed URLs, JSON export, and locally saved Mothers with a restore prompt on return visits.
- **Light and dark modes.** Glanceable, accessible.
- **Auto-routing.** Phones opening `/workspace?seed=…` redirect to `/mobile?seed=…` while preserving the seed.

## Try it

Open <https://geofancy.up.railway.app> on any device. **Cast a chart!** opens the workspace (or an interactive shield walkthrough). Browse the **[geomancy wiki](https://geofancy.up.railway.app/wiki)** for glossaries and how-tos.

## Geomancy in one paragraph

Geomancy is a divinatory system that emerged in the medieval Islamic world and reached Europe in the twelfth century, where it flourished alongside astrology through the Renaissance. The practitioner generates sixteen binary marks — traditionally by striking the earth, today by any reliable randomization — which are organized into four "Mothers." From the Mothers, four Daughters, four Nieces, two Witnesses, a Judge, and a Reconciler are derived by simple geomantic addition. The resulting chart is then read against the houses of horary astrology to answer the question. Geofancy automates the mechanical steps so the practitioner can give full attention to interpretation.

## Tech stack

- **Frontend:** Blazor Server with InteractiveServer rendering, Blazor WebAssembly client where appropriate
- **Backend:** ASP.NET Core 8 minimal APIs, in-process service mode for the deployed app
- **Domain libraries:** `Geomancy.Core`, `Geomancy.Api.Contracts`, `Geomancy.Api.Handlers` (all .NET Standard 2.0 for cross-target compatibility)
- **Legacy desktop:** WinForms (.NET Framework 4.8) shell, retained for offline practitioner use
- **Hosting:** Railway, Linux container, multi-stage Dockerfile

See [DEPLOY.md](DEPLOY.md) for deployment specifics.

## Repository layout

```
Geomancy.Core/                 Domain logic — figures, houses, chart math, perfections, corpus
Geomancy.Api.Contracts/        DTOs and JSON loaders shared across runtimes
Geomancy.Api.Handlers/         Stateless API handlers used by both web and self-host
GeomancyAPI/                   Legacy .NET Framework 4.8 self-host API (optional)
GeomancyApp/                   Legacy WinForms desktop app
GeomancyWebUI/                 The web app
  GeomancyWebUI/                 Server project, hosting + controllers + pages
  GeomancyWebUI.Client/          WASM client project, models + services
databank/                      Static reference data (figure corpus, houses, courts, Way of Points)
  FigureCorpus/                  All 16 geomantic figures (Figures.json)
  HouseAndCourtDirectory/        House and court reference JSON
  WayOfPointsDirectory/          Way of Points configuration JSON
```

## Local development

The web app is the active surface. To run it locally:

```bash
cd GeomancyWebUI/GeomancyWebUI
dotnet run
```

Then open the URL printed in the console. The default configuration uses the in-process service implementation, so no separate API server is required.

## Reporting issues

Please open a [GitHub issue](https://github.com/ThomasShetler/GeomancyApp/issues) for bugs or feature requests. Include the question or chart that produced the unexpected output if you can — it helps reproduce the problem quickly.

## Acknowledgments

Geofancy stands on the shoulders of the geomantic tradition. The corpus draws on, and credits, the public-domain works of:

- **Cornelius Agrippa**, *Three Books of Occult Philosophy* (1531; English 1651)
- **Pseudo-Agrippa**, *Of Geomancy* (Fourth Book of Occult Philosophy, 1655)
- **Christopher Cattan**, *The Geomancie of Maister Christopher Cattan* (1558; English 1591)
- **Robert Fludd**, *De Geomantia* in *Utriusque Cosmi Historia* (1617)
- **John Heydon**, *Theomagia, or the Temple of Wisdome* (1664)
- **Franz Hartmann**, *The Principles of Astrological Geomancy* (1889)

Thanks also to the modern practitioners whose teaching shaped the broader revival of the art. None of their work is reproduced here, but the conversation they kept alive made this project possible.

## License

Geofancy is **proprietary, source-available** software.

- **Source code** is licensed under the [PolyForm Noncommercial License 1.0.0](LICENSE). You may read, study, and modify it for personal and noncommercial purposes; any commercial use requires a separate license from the author.
- **Interpretive corpus** (the prose content of `databank/FigureCorpus/Figures.json`, `databank/HouseAndCourtDirectory/*.json`, and `databank/WayOfPointsDirectory/*.json`) is licensed separately under [Creative Commons Attribution-NonCommercial 4.0 International](LICENSE-CORPUS.md).

See [NOTICE.md](NOTICE.md) for the plain-English summary.

For a commercial license — including SaaS reselling, white-label deployments, or inclusion of the engine or corpus in a paid product — please contact:

**Thomas Shetler** · thomas.ja.shetler@gmail.com · Portland, OR, USA

© 2026 Thomas Shetler. All rights reserved beyond the licenses above.
