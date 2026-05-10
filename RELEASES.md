# Releases

## v0.1.0-beta — Public Beta

**Released:** May 2026
**Live app:** <https://geofancy.up.railway.app>

The first public release of **Geofancy**, a digital tool for traditional Western geomancy. Cast a chart from a question and walk through the full reading: Mothers, Daughters, Nieces, Witnesses, Judge, Reconciler, twelve houses, perfections, aspects, and the Way of Points — with an original interpretive corpus written from primary sources for every figure in every slot.

The chart engine, perfection analyzer, Way of Points module, and the entire interpretive corpus are production-ready. The surrounding app continues to evolve, which is why this release is tagged **beta**.

### Highlights

- **Two purpose-built workspaces.** A wide desktop layout that gives the chart and details room to breathe, and a mobile-first layout where the chart fills the screen and tabs collapse into focused detail panels. Phones are auto-routed to the mobile workspace.
- **Original interpretive corpus.** Every interpretive line — figure data, house data, court placements, and the contextual "this figure in this slot" blurbs (64 in total) — was written from primary sources (Agrippa, Cattan, Fludd, Heydon, Hartmann) and the author's practice. No third-party reference material is reproduced.
- **Themed card-based detail panel.** Contextual in-slot callout, split favorable / unfavorable cards, person and body correspondences, house affinity pills, themed imagery cards, and a citation footer with sources.
- **Perfection analyzer.** Querent / Quesited selection with per-perfection commentary covering Occupation, Conjunction, Translation, Mutation, Mutual Reception, and the supporting and denying aspects.
- **Way of Points module.** Element and path-type explanations with structured, scannable tips.
- **Light and dark modes** that follow your choice and persist across sessions.
- **Apple home-screen ready.** PWA-style favicon, apple-touch-icon, and dark theme color so it lives nicely on a phone's home screen.

### What's in the workspace

| Tab | What you get |
| --- | --- |
| **Chart** | Shield and twelve-house diagrams with interactive figures |
| **Court & Houses** | Directory-backed detail panels for every figure in every house and court position |
| **Perfections & Aspects** | Querent / Quesited selection, perfection list with definitions, and structured tips |
| **Way of Points** | Element traces, path-type explanations, and interpretive notes |

### Tech stack

- **Web app:** Blazor (Server + WebAssembly) on .NET 8, deployed to Railway via Docker.
- **Domain logic & API contracts:** Three .NET Standard 2.0 libraries (`Geomancy.Core`, `Geomancy.Api.Contracts`, `Geomancy.Api.Handlers`) shared between the modern web app and the legacy WinForms client.
- **Legacy desktop:** The original WinForms / .NET Framework 4.8 client is preserved for offline use and references the same `Geomancy.Core` so chart math is the single source of truth.
- **Local dev:** Either-or — the Blazor app runs against an in-process minimal API by default, or can switch to the F4.8 self-hosted API via `appsettings.json`.

### Known limitations

- Charts are not yet persisted across sessions; refreshing the page resets the workspace.
- The legacy WinForms desktop app remains available for offline use but does not yet reflect the full corpus rewrite or the new card layout. The web app is the recommended surface.
- The Aspect Analysis page is marked **experimental** in places where its interpretive layer is still being expanded.
- Mobile chart minimum render size is enforced at 435 × 435 to preserve diagram integrity; on very narrow screens the chart drawer scales rather than reflows.

### License

Geofancy is **proprietary, source-available** software distributed under two complementary licenses:

- **Source code** — [PolyForm Noncommercial License 1.0.0](LICENSE). Read, study, and modify for personal and noncommercial purposes; commercial use requires a separate license from the author.
- **Interpretive corpus** — [Creative Commons Attribution-NonCommercial 4.0 International](LICENSE-CORPUS.md). Cite, quote, and build on the prose freely in noncommercial educational settings, with attribution.

See [NOTICE.md](NOTICE.md) for a plain-English summary. For commercial licensing, contact **Thomas Shetler** at thomas.ja.shetler@gmail.com.

### Acknowledgments

Built on the public-domain works of Agrippa, Cattan, Fludd, Heydon, and Hartmann. With gratitude to the modern practitioners who kept the art alive long enough for a digital tool to make sense.

### Feedback

This is a beta. Bug reports, interpretive corrections, and feature suggestions are very welcome — please open an issue at <https://github.com/ThomasShetler/GeomancyApp/issues>.
