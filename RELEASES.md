# Releases

## v0.2.0 — Web milestone (stable line)

**Released:** May 2026  
**Live app:** <https://geofancy.up.railway.app>

**Geofancy 0.2.0** is the current **stable** web line: assemblies and the home-page version chip drop the `-beta` prerelease suffix while shipping everything accumulated across the 0.1.x beta series — Way of Points depth (including classical **Way of the Light** / Classic Way of Points), mobile workspace polish, shareable chart URLs + JSON export, dark-mode refinements, About/trust copy, and corpus tone updates.

### Highlights

- **Way of Points.** Element summaries, path mechanism detail, composition-aware verdict copy, and Via Ignis **Way of the Light** spotlight with naming and rules aligned to core logic.
- **Mobile workspace.** Tab-aware hints, chart-drawer layout discipline, and clearer onboarding around the expanded chart.
- **Share & archive.** Seed-based **Share Chart** links (desktop + `/mobile` redirect) and timestamped **JSON export** / copy from **Lots & Other** (`schemaVersion: 1`).
- **Presentation.** Improved Way-of-Light amber accents in dark mode; landing **Stable** channel badge matches non-prerelease semver.

### Implementation notes

- **Assemblies aligned to v0.2.0.** `Directory.Build.props` drives SDK-style `0.2.0` / `0.2.0.0`; legacy WinForms projects match via `AssemblyInfo.cs`; UI reads `GeofancyVersion.Display` and `GeofancyVersion.Channel` (**Stable**).

### Known limitations

Same as v0.1.0-beta — charts still reset on full refresh; legacy WinForms trail the web corpus layout; aspect analysis remains partly experimental. See **v0.1.0-beta — Public Beta** below.

---

## v0.1.3-beta — Way of Light, Mobile Polish & WoP UX

**Released:** May 2026  
**Live app:** <https://geofancy.up.railway.app>

Focus release on **Way of Points**: classical **Way of the Light** on Via Ignis is surfaced end-to-end with clearer naming (**Classic Way of Points / Way of the Light**), a condensed spotlight in the detail panel, and better readability of amber tags in **dark mode**. Mobile workspace gets tab-aware hints, tighter chart-drawer layout, and small copy and corpus-tone polish.

### Highlights

- **Way of the Light (Classic Way of Points).** When Fire forms exactly one strong path on singly active head-line fire, the app marks that path and shows structured detail: naming vs the four elemental ways, rule checklist aligned with core logic, and reading cues scoped to this classical highlight — without duplicating generic Via Ignis glossary when the spotlight is active.
- **Way of Points verdict line.** The summary header reflects chart composition using strong vs passive path totals across elements.
- **Mechanism card only on paths.** Selecting a whole-element Way summary no longer shows the path mechanism strip (avoids reading as “not established” when the way is open).
- **Mobile workspace.** Welcome flow hints vary by active tab; layout height is locked so tabs do not scroll under the chart handle; expanded chart sheet and collapse hints are tightened; optional chart-area hint for orientation.
- **Dark mode.** Way-of-Light chips, header pill, list ribbon, row highlight, and **Light** badge use higher-contrast light-on-amber styling.

### Fixes

- **WoP element summary** no longer shows the mechanism graph until a specific path row is selected.

### Implementation notes

- WoP UI lives under `GeomancyWebUI/Components/Workspace/` (`WayOfPointsListTab`, `WayOfPointsDetailPanel`, scoped CSS). Classical marking remains in `Geomancy.Core/WayOfPoints.cs` (`MarkClassicalWayOfLightIfApplicable`).
- **Assemblies aligned to v0.1.3-beta.** SDK-style builds take version from `Directory.Build.props`; legacy WinForms projects use matching `AssemblyInfo.cs`; the home-page chip reads `GeofancyVersion.Display`.

### Known limitations

Same as v0.1.2-beta — see below.

---

## v0.1.2-beta — Share Links & JSON Export

**Released:** May 2026
**Live app:** <https://geofancy.up.railway.app>

A workflow release focused on getting readings *out* of the app: share a chart with one click, archive a reading as a JSON file, or paste it straight into another tool. No more re-entering the four Mothers by hand to compare notes with a teacher or a study group.

### Highlights

- **Shareable chart links.** A new **Share Chart** button on every workspace (`/workspace`, `/mobile`, and the legacy `/chart`) copies a self-contained URL like `https://geofancy.up.railway.app/workspace?seed=2122.1212.2222.1122` to the clipboard. Anyone who opens that URL lands on the same chart on first paint — no flash of the default reading, no extra clicks. The seed format is fully readable (four 4-digit clusters, one per Mother) and round-trips through any chat / email / browser without escaping.
- **Mobile-aware.** Phones that open a `/workspace?seed=…` link are auto-redirected to `/mobile?seed=…` while preserving the seed, so a single canonical link works for everyone.
- **Download JSON on Lots & Other.** A new export card pinned to the top of the **Lots & Other** tab streams the full reading — the four Mothers, the resolved chart, the perfection analysis (if a querent and quesited are picked on the Perfections tab), the Way of Points result, and the legacy aspect analysis — into a single timestamped file like `geofancy-chart-20260510-103213.json`. The payload includes the share URL and seed alongside the data so the file can also reproduce the chart by URL.
- **Copy JSON for paste-into-anything use.** A second button next to *Download JSON* copies the same payload to the clipboard — for users who want to drop the reading straight into an editor, a notebook, a chat, or another tool without the file roundtrip. Both buttons emit byte-identical output.
- **Versioned export schema.** Files are stamped with `schema: "geofancy.chart-export"` and `schemaVersion: 1` so future format evolution is clean.
- **Forgiving share parser.** Malformed `?seed=…` values silently fall back to the default chart instead of erroring out, so a tampered or truncated URL never throws in front of a user.
- **Assemblies aligned to v0.1.2-beta.** Every DLL — SDK and legacy alike — now reports `FileVersion=0.1.2.0`, `ProductVersion=0.1.2-beta`, `ProductName=Geofancy`. The home-page version chip and release card update automatically via `GeofancyVersion`.

### Implementation notes

- New `ChartSeedCodec` in `GeomancyWebUI.Client.Services` is the single source of truth for encode / decode / build-share-path. Both workspace pages and the legacy `/chart` page route through it, so the seed format is identical everywhere.
- Browser-side helper `downloadTextFile(name, contents, mime)` was added to `clipboard.js` and works in every modern browser; `copyToClipboard` now also has a `document.execCommand('copy')` fallback for older Safari and insecure contexts.
- Both share and export paths are no-network operations once the workspace is loaded — they round-trip locally.

### Known limitations

Same as v0.1.1-beta — see below.

---

## v0.1.1-beta — Mobile Mother Strip Polish

**Released:** May 2026
**Live app:** <https://geofancy.up.railway.app>

A small polish release that finalizes the mobile Mother input experience and rounds out the v0.1.0-beta launch with proper version metadata baked into every assembly.

### Highlights

- **Mobile Mother input strip is below the chart now.** When the chart drawer opens the chart stays the visual focus and the four input cells fall naturally under the user's thumb.
- **Compact, house-named cells.** The four cells were tightened (smaller tap rows, lighter padding) and now display the Latin **house names** — *Genitor · Fratres · Lucrum · Vita* — left to right, matching the chart's Mother reading order. Each cell still drives the same state as the in-chart Mother inputs and triggers the same regeneration pipeline.
- **Bottom breathing room.** The chart drawer now reserves a small gap below the input strip so it isn't flush against the screen edge.
- **Reclaimed chart room.** With the strip slimmer, the chart's responsive `--chart-fit` budget got back ~2 rem of vertical headroom, giving the diagram more room on shorter phones.
- **Assemblies aligned to v0.1.1-beta.** Every DLL — SDK and legacy alike — now reports `FileVersion=0.1.1.0`, `ProductVersion=0.1.1-beta`, `ProductName=Geofancy`. The home-page version chip and release card update automatically via `GeofancyVersion`.

### Fixes

- Hover-scale transforms on the Mother strip's tap rows are suppressed (they fought touch interaction on phones).

### Known limitations

Same as v0.1.0-beta — see below.

---

## v0.1.0-beta — Public Beta

**Released:** May 2026
**Live app:** <https://geofancy.up.railway.app>

The first public release of **Geofancy**, a digital tool for traditional Western geomancy. Cast a chart from a question and walk through the full reading: Mothers, Daughters, Nieces, Witnesses, Judge, Reconciler, twelve houses, perfections, aspects, and the Way of Points — with an original interpretive corpus written from primary sources for every figure in every slot.

The chart engine, perfection analyzer, Way of Points module, and the entire interpretive corpus are production-ready. The surrounding app continues to evolve, which is why this release is tagged **beta**.

### Highlights

- **Two purpose-built workspaces.** A wide desktop layout that gives the chart and details room to breathe, and a mobile-first layout where the chart fills the screen and tabs collapse into focused detail panels. Phones are auto-routed to the mobile workspace.
- **Mobile Mother input strip.** A row of four full-width tap targets — labeled Fourth, Third, Second, and First Mother — sits above the chart inside the mobile drawer. Each cell drives the same state as the in-chart Mother inputs, giving thumbs a comfortable place to set lines without zooming in on the small chart.
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
