# Releases

## v1.0.7 — Classical house reference & wiki revamp

**Released:** July 2026  
**Live app:** <https://geofancy.up.railway.app>

Feature and QA release building on the classical house corpus, figure slot rewrites, and workspace detail panel scope labels.

### Highlights

- **Classical house corpus.** All twelve houses rewritten with traditional governs lists, `read_when` triggers, `classical_note` routing sentences, and misreading guards (especially Eighth = death and other's substance, not modern Scorpio drift).
- **Figure slot text.** All sixteen figures updated: `in_houses` and `in_court_roles` aligned to the same house doctrine; Eighth-house intimacy/joint-resource phrasing removed.
- **Greer reference overlay.** Licensed Greer figure and house translations in Court & Houses with **Off / Alongside / Greer-primary** modes, structured house fields (including Critical Context flags), and attribution in the detail panel.
- **Workspace detail panel.** Clear **Figure ·** / **House ·** / **Court ·** scope labels; house summary cards with Keep in mind and Watch; full house Read When and Reading Craft sections.
- **Wiki house pages revamp.** Glossary index with filter, essence previews, and topic chips; article pages with summary card, house pager, and full reference stack.
- **Mothers casting walkthrough.** Desktop side-panel dot casting aligned to the shield chart; mobile walkthrough parity (tapping, completion flow, scrollable figure detail); chart seed restore prompt on return visits.
- **Reference integrity tests.** `HouseDirectoryContentTests`, `ReferenceIntegrityTests`, and repaired aspect/perfection tests guard corpus quality in CI.

### Implementation notes

- **Assemblies aligned to v1.0.7.** `Directory.Build.props`, `GeofancyVersion.Display`, and legacy `AssemblyInfo.cs` updated in lockstep.
- **Branches:** `master` = production; `web-app` = integration / testing.

---

## v1.0.6 — Production deploy & detail panel styling

**Released:** May 2026  
**Live app:** <https://geofancy.up.railway.app>

Patch release after **v1.0.5** — fixes production parity with the tested `web-app` branch (detail panel styling and static asset delivery on Railway).

### Highlights

- **Client detail panel CSS on production.** `FigureDetailPanel`, chart surfaces, and the interactive casting walkthrough live in `GeomancyWebUI.Client`. Their scoped stylesheet is now linked explicitly in `App.razor` (`GeomancyWebUI.Client.bundle.scp.css`) instead of relying only on `@import` inside `GeomancyWebUI.styles.css`, which mobile browsers often skip.
- **Stable Client bundle in Docker.** The publish step emits a fingerprinted `GeomancyWebUI.Client.*.bundle.scp.css`; the `Dockerfile` and MSBuild targets copy it to a stable filename so prod and `@import` both resolve.
- **Static asset cache busting.** `theme.js`, `clipboard.js`, and main stylesheets use `?v={version}` via `Branding.AssetUrl`; production sends `Cache-Control: no-cache` on `.js` and `.css` so phones pick up deploys after merges.
- **Deploy documentation.** `DEPLOY.md` adds test-vs-prod Railway checklist and troubleshooting for unstyled detail panels and stale caches.

### Implementation notes

- **Assemblies aligned to v1.0.6.** `Directory.Build.props`, `GeofancyVersion.Display`, and legacy `AssemblyInfo.cs` updated in lockstep.
- **Branches:** `master` = production; `web-app` = integration / testing.

---

## v1.0.5 — Production mobile stability

**Released:** May 2026  
**Live app:** <https://geofancy.up.railway.app>

Production release after **v1.0.4** — promotes the **phone-validated `web-app` line** (bisected from baseline `6733e19` through **v1.0.2**) and drops the **v1.0.3 / v1.0.4** layout experiments that were not needed on real devices.

### Highlights

- **Mobile verified.** Cherry-picked **1.0.0 → 1.0.2** changes tested on a physical phone; **prerender**, logo, SEO, and footer/workspace scroll fixes all passed. Skipped **v1.0.3** (`mobile-workspace.css`) and **v1.0.4** (footer/skip revert) commits — issues they targeted were not reproduced on the bisected branch.
- **Figure detail navigation.** Sticky section chips on mobile now scroll the correct panel (`.mobile-panel-body`) via `geofancyScrollToId` in `clipboard.js`.
- **Site chrome retained.** Global footer on content pages, `PageSeo` / sitemap / Search Console file, square caduceus branding, desktop workspace scroll chain, and **Cast a chart!** dialog outside the landing flex tree.
- **No global mobile-workspace stylesheet.** Removed `mobile-workspace.css` link and file so `/mobile` uses the scoped layout that passed phone testing.

### Implementation notes

- **Assemblies aligned to v1.0.5.** `Directory.Build.props`, `GeofancyVersion.Display`, and legacy `AssemblyInfo.cs` updated in lockstep.
- **Branches:** `master` = production; `web-app` = integration / testing.

---

## v1.0.4 — UI stability restore (pre-footer layout) — superseded

**Released:** May 2026 (replaced by **v1.0.5** on production)

Stability patch that attempted to revert footer/skip-link regressions while keeping branding and SEO. Superseded because the bisected **v1.0.2** line plus scroll fix already behaved correctly on phones without these changes.

---

## v1.0.3 — Mobile workspace layout fix — superseded

**Released:** May 2026 (replaced by **v1.0.5** on production)

Introduced global `mobile-workspace.css` and skip-link layout tweaks. Superseded — not shipped in **v1.0.5**.

---

## v1.0.2 — Brand emblem & app icons

**Released:** May 2026  
**Live app:** <https://geofancy.up.railway.app>

Patch release after **v1.0.1** — new square caduceus-on-shield branding across the web app.

### Highlights

- **New logo.** Square **1024×1024** emblem (caduceus on a starry heraldic shield) replaces the previous wordmark asset on the home page, nav brand, and footer.
- **App icons.** Regenerated **favicon** (32×32) and **apple-touch-icon** (180×180); cache-busted static URLs via `Branding` helpers.
- **Icon styling.** Restored rounded app-icon treatment (18% radius, shadow) on hero, sidebar, and footer marks.
- **Load reliability.** Root-relative logo URLs, asset preload, and interactive **prerender** so the emblem appears before the Blazor circuit connects.

### Implementation notes

- **Assemblies aligned to v1.0.2.** `Directory.Build.props`, `GeofancyVersion.Display`, and legacy `AssemblyInfo.cs` updated in lockstep.

---

## v1.0.1 — SEO, About polish & workspace UX

**Released:** May 2026  
**Live app:** <https://geofancy.up.railway.app>

Patch release after **v1.0.0** — polish and discoverability, no chart-engine changes.

### Highlights

- **Search & sharing.** Per-page SEO via `PageSeo` (titles, descriptions, canonical URLs, Open Graph, Twitter cards); **`robots.txt`** and **`sitemap.xml`**; JSON-LD on the home page. **Google Search Console** verification file at `/googleaad6cadf84b5b2c5.html`.
- **About page.** Refreshed copy for the wiki, interactive walkthrough, and 1.0 feature set; trust bullets aligned with the landing page; explore links and clearer corpus/AI sections.
- **Site chrome.** Global footer on content pages (home, wiki, about); **hidden on workspace, mobile, and interactive wiki** so chart flows stay full-screen.
- **Desktop workspace fix.** Restored viewport height chain so Perfections, Court & Houses, and Way of Points **list and detail panels scroll** again inside tabs (regression from footer layout work).

### Implementation notes

- **Assemblies aligned to v1.0.1.** `Directory.Build.props`, `GeofancyVersion.Display`, and legacy `AssemblyInfo.cs` updated in lockstep.
- **Branches:** `master` = production; `web-app` = integration / testing.

---

## v1.0.0 — Official Release

**Released:** May 2026  
**Live app:** <https://geofancy.up.railway.app>

**Geofancy 1.0.0** is the first **official** public release of the web app: a complete geomancy workspace with a live reference wiki, interactive casting walkthroughs, polished mobile and desktop layouts, and the full interpretation stack practitioners have been using through the 0.1.x–0.2.x stable line.

### Highlights

- **Geomancy Wiki.** Live **Figure** and **House** glossaries backed by the same corpus as the workspace detail panels; how-tos for **generating figures** and **casting a shield chart**; an **interactive shield walkthrough** with step-by-step derivation animations; a **figure dot practice caster** on the generate-figures page.
- **Casting entry & onboarding.** **Cast a chart!** on the home page opens a three-way dialog: open the workspace with Mothers ready, mark the four Mothers in-app, or start the interactive shield walkthrough. Mobile and desktop paths share one device-detection rule.
- **Desktop & mobile workspaces.** Wide desktop layout with tabbed perfections, Way of Points (including **Classic Way of Points / Way of the Light**), court & houses, share links, and JSON export. Thumb-friendly **mobile workspace** with mothers drawer, chart overlay, and casting-shell parity with the wiki.
- **Share & archive.** **Share Chart** copies a readable `?seed=` URL; phones opening `/workspace?seed=…` redirect to `/mobile?seed=…`. **Download JSON** and **Copy JSON** on **Lots & Other** export the full reading (`schemaVersion: 1`).
- **Presentation.** Light and dark themes with per-circuit persistence; landing and footer surfaces show **Official** channel and **v1.0.0**.

### What's new since v0.2.2

Production **`master`** shipped **v0.2.2** (workspace, share/export, WoP, mobile shell, theme fix). **v1.0.0** merges **29 commits** from **`web-app`** (~**133 files**, **+18,968 / −2,864** lines vs `master`). The bulk of the delta is new **wiki** and **interactive casting** UI, a **JSON-backed figure corpus**, and a redesigned **figure detail** / **perfections** experience—not a rewrite of chart math (still `Geomancy.Core` + in-process handlers).

### Technical changes (comprehensive)

#### Data layer & `Geomancy.Core`

| Change | Detail |
|--------|--------|
| **Corpus externalized** | Removed four `FigureCorpus.Part*.cs` compile-time blobs (~1,200 lines). Figure text now loads from **`databank/FigureCorpus/`** (`Figures.json`, `NewAndImprovedFigures.json`, `Figures.schema.json`). |
| **`FigureCorpusLoader`** | New loader with validation; shared by Core, API handlers, and Blazor. |
| **Reference JSON moved** | House, court, and Way of Points directory data relocated under **`databank/`** (`HouseData.json`, `CourtData.json`, `ElementData.json`, `PathTypeData.json`). Loaders updated in `HouseDirectoryLoader`, `WayOfPointsDirectoryLoader`. |
| **Tests** | `FigureCorpusLoaderTests` in `GeomancyUnitTesting`. |
| **Models** | `FigureData` / `TraditionalSourceEntry` extended for richer corpus fields; API contract models adjusted. |

*Commits:* `243b828`, `1099fb4`, `8d96e5c`

#### Workspace — figure detail & perfections

| Change | Detail |
|--------|--------|
| **Figure detail panel** | Moved to **`GeomancyWebUI.Client`** (`FigureDetailPanel.razor` + scoped CSS). Card-based layout, circular figure emblem in header, restored **master house** and **court** reference stacks, pattern-based navigation, polished traditional sources block. |
| **Perfections tab** | `CompactPerfectionListRow`, **`PerfectionIcon`** (SVG), subsection grouping; redundant “By Aspect” subgroup removed; aspects nested under Perfections / Denials. |
| **Aspect UI** | `AspectDirectionHelper` — dexter/sinister direction arrows in list + detail; `PerfectionDetailPanel` styling refresh. |
| **Stage-scoped court/houses** | `StageScopedCourtAndHousesTable` for walkthrough-aligned house tables. |
| **Helpers** | `FigureDisplayHelper`, `FigureMarking`, `PerfectionIconHelper` centralized display logic. |
| **Dead code removed** | Legacy server-side `Workspace/FigureDetailPanel.razor` deleted after client migration. |

*Commits:* `21e11c0`, `d220f2d`, `a0228e8`, `1099fb4`, `3a5350a`, `9ccb5dc`, `b59c2e1`, `ef67f7d`, `3cfd0e1`, `8d96e5c`

#### Geomancy Wiki (new site area)

| Route | Status | Implementation |
|-------|--------|----------------|
| `/wiki` | Live | Hub with `WikiTopicCard` (Live / Coming soon badges) |
| `/wiki/glossary/figures`, `/wiki/glossary/figures/{slug}` | Live | Filterable glossary, `WikiFigureSlug`, article + `WikiFigureCorpusSections` |
| `/wiki/glossary/houses`, `/wiki/glossary/houses/{id}` | Live | House glossary + articles, `WikiHouseReferenceStack` |
| `/wiki/how-to/generate-figures` | Live | Prose + **`WikiFigurePracticeSection`** + **`FigureDotCaster`** |
| `/wiki/how-to/shield-chart` | Live | Structure guide + static walkthrough copy |
| `/wiki/how-to/shield-chart/interactive` | Live | Full **`ChartCastingWalkthrough`** host |
| `/wiki/how-to/generate-figures/interactive` | Redirect | Server redirect → shield interactive |
| `/wiki/how-to/house-chart`, `/wiki/how-to/use-the-app` | Draft | `WikiPlaceholderBody` outlines only |
| `/wiki/methods/perfections-and-aspects`, `/wiki/methods/way-of-points` | Draft | Outlines only |

**New wiki components:** `WikiArticleShell`, `WikiFigureHeader` (Pattern mini-card), `WikiFigureDataPanel`, `ShieldChartStructureGuide`, `ShieldChartSectionDemo`, **`wiki.css`** (~2,400 lines).

*Commits:* `d3556ec`, `6c3d8b6`, `2b3850b`, `16cfe3f`, `e67e310`

#### Interactive casting & walkthrough

| Component | Role |
|-----------|------|
| **`ChartCastingWalkthrough.razor`** | ~2,550 lines — tabbed stages (Mothers → Daughters → Nieces → Witnesses → Judge → Reconciler), slot targeting, mobile drawer phases, chart preview events. |
| **`DaughterGenerationVisualizer.razor`** | Animated daughter derivation between shield rows. |
| **`ChartSurface.razor`** | Extended highlights, empty cells, aria labels, walkthrough host modes (`ChartCastingHostMode`, `ShieldSectionFocus`, `ShieldRowVisibility`). |
| **`FigureDotCaster.razor`** | Canvas dot marking, pairing animation, mobile tap targets; **`figure-dot-caster.css`**. |
| **`CastChartEntryDialog`** | Home-page three-path entry (workspace / mothers setup / learn). |
| **`WorkspaceEntryQuery`** | Shared `?setup=mothers` path builder for workspace + mobile. |

*Commits:* `2e2bdab`, `4698471`, `ad99d29`, `436c0a0`, `560b931`, `6733e19`

#### Mobile workspace & `CastingMobileShell`

| Change | Detail |
|--------|--------|
| **`CastingMobileShell.razor`** | Shared mobile casting chrome (drawer, chart scaling, mothers strip) used by wiki interactive host and **`/mobile`** mothers setup. |
| **Mothers setup on phone** | `/mobile` mothers flow uses same shell as wiki; chart drawer preview during dot casting; drawer scaling + Mother strip fixes. |
| **CSS** | `casting-mobile-shell.css`, mobile workspace drawer rules in `MobileWorkspace.razor.css`. |
| **Wiki mobile** | `wiki-interactive-shell--mobile` layout locks in `MainLayout` (100dvh, footer hidden on mobile shell pages). |

*Commits:* `4cdc2aa`, `9a2c643`, `41772b9`, `0bd7a58`, `3cc7a8a`, `60dda05`, `560b931`

#### Chart surface, clipboard & deploy

- **`chart-surface.css`** — layout tokens for shield grid and walkthrough overlays.
- **`clipboard.js`** — `downloadTextFile` + clipboard fallback (share/export from 0.2.x, still used).
- **`Dockerfile` / `.csproj`** — copy `databank/` into publish output so corpus JSON is available in the Railway container.
- **`InProcessGeomancyService` / `GeomancyApiService`** — wiki and workspace call same handlers; glossary pages use directory + figure APIs.

#### Site shell, landing & v1.0.0 polish (`cb01cc7`)

- **`GeofancyVersion`:** `1.0.0`, channel **Official**; `Directory.Build.props` + three `AssemblyInfo.cs` aligned.
- **Landing (`Home.razor`):** Wiki + interactive CTAs, 1.0.0 release copy, feature cards updated.
- **`SiteFooter`:** Global footer (version, changelog, quick links); hidden on full-screen mobile shell routes.
- **`NavMenu`:** Home → Wiki → smart **Workspace** button (`geofancyDeviceIsMobile`) → Mobile workspace → About.
- **404 / Error pages**, skip-link, default meta description, **`DEPLOY.md`** documents **`master`** = prod / **`web-app`** = testing.
- **`ShieldChartStructureGuide`:** Links to workspace + interactive walkthrough (not legacy `/chart`).

### New & renamed projects paths (reference)

```
databank/FigureCorpus/          # JSON corpus + schema
databank/HouseAndCourtDirectory/
databank/WayOfPointsDirectory/
GeomancyWebUI.Client/Components/
  ChartCastingWalkthrough.razor
  DaughterGenerationVisualizer.razor
  FigureDotCaster.razor
  CastingMobileShell.razor
  FigureDetailPanel.razor       # authoritative detail UI
GeomancyWebUI/Components/
  Wiki/                         # article shell, corpus sections, topic cards
  Pages/Wiki*.razor             # routes listed above
  CastChartEntryDialog.razor
  Layout/SiteFooter.razor
GeomancyWebUI/wwwroot/
  wiki.css, casting-mobile-shell.css, figure-dot-caster.css
```

### Full commit log (v0.2.2 → v1.0.0)

| Commit | Summary |
|--------|---------|
| `243b828` | Move static corpus and reference data into `databank/` JSON |
| `21e11c0` | Aspect UI: dexter/sinister arrows, detail panel polish |
| `d220f2d` | Fix perfections list house labels; tighter rows |
| `a0228e8` | Compact perfections panel with SVG icons and subsections |
| `1099fb4` | Merge improved figure corpus; redesign figure detail panel |
| `3a5350a` | Figure detail navigation + visual styling |
| `9ccb5dc` | Restore house/court reference UI in figure detail |
| `b59c2e1` | Remove redundant “By Aspect” subgroup |
| `8d96e5c` | Traditional sources styling in figure detail |
| `ef67f7d` | Nest aspects under Perfections and Denials |
| `3cfd0e1` | Circular figure emblem in detail panel header |
| `d3556ec` | Mobile-friendly wiki; live figure & house glossaries |
| `6c3d8b6` | Figure wiki pages; pattern-based selector |
| `2b3850b` | Pattern mini-card on wiki figure header |
| `16cfe3f` | Fix clipped dots in Pattern mini-card |
| `e67e310` | Enlarge Pattern mini-card emblem |
| `2e2bdab` | Interactive figure casting + shield walkthrough |
| `ad99d29` | Figure dot caster: canvas marking, pairing animation |
| `436c0a0` | Dot caster mobile tap accuracy and layout |
| `4698471` | Cast-chart entry dialog + interactive walkthrough route |
| `4cdc2aa` | Mobile chart drawer for mothers setup preview |
| `9a2c643` | Mobile mothers drawer scaling + Mother strip |
| `41772b9` | Mobile mothers casting UX in drawer |
| `0bd7a58` | Mobile wiki casting shell; mothers drawer QoL |
| `3cc7a8a` | `/mobile` mothers setup uses `CastingMobileShell` |
| `60dda05` | Wiki shield chart scaling on narrow viewports |
| `560b931` | Shield derivation visualizer: court animations, mobile scaling |
| `6733e19` | Generate-figures wiki: practice caster + corpus panel |
| `cb01cc7` | **v1.0.0** — version bump, landing, footer, nav, copy polish |
| `1f1a424` | Expand v1.0.0 release notes (technical changelog) |

### Stability (carried from v0.2.1–0.2.2)

- **Theme state** scoped per Blazor Server circuit so nav toggle and workspace stay in sync.
- **Interactive render boundary** fixed: `@rendermode InteractiveServer` on the `Router` subtree only (no illegal `RenderFragment` across wrappers).
- **Mobile routing** uses a **768px** breakpoint so modest desktop widths are not forced to `/mobile`.

### Implementation notes

- **Assemblies aligned to v1.0.0.** `Directory.Build.props`, `GeofancyVersion.Display`, and legacy `AssemblyInfo.cs` updated in lockstep.
- **Branches:** `master` = production (Railway deploy); `web-app` = integration / testing.

### Known limitations

- No server-side saved charts yet — use **share links** and **JSON export** to keep readings.
- Legacy WinForms desktop does not reflect the full web corpus layout; the web app is the recommended surface.
- **Aspect analysis** under **Lots & Other** remains **experimental** (superseded by the Perfections tab for primary reading).
- Desktop workspace enforces a minimum width (~1380px); very narrow viewports use the mobile layout or chart drawer scaling.
- Some wiki topics (house-chart how-to, app guide, perfections & WoP method articles) remain **Coming soon** outlines.

---

## v0.2.2 — Blazor render boundary & theme circuit

**Released:** May 2026  
**Live app:** <https://geofancy.up.railway.app>

Patch release fixing a **production crash** after v0.2.1 and completing the **theme + interactive** story.

### Fixes

- **Startup / every request crash.** An `InteractiveShell` wrapper used `@rendermode InteractiveServer` with **`ChildContent`** from `MainLayout`. Blazor cannot serialize **`RenderFragment`** across that boundary, which threw `System.InvalidOperationException` on each load. The shell was removed; **`@rendermode InteractiveServer`** now lives on **`Routes.razor`** around the **`Router`**, so layout + pages share **one** interactive subtree without illegal parameters.
- **Theme state across nav + sidebar.** With a single circuit scope from `Routes`, **`ThemeService` (Scoped)** is the **same instance** for the nav toggle and workspace pages—matching v0.2.1’s intent without the broken wrapper pattern.

### Implementation notes

- **Assemblies aligned to v0.2.2.** `Directory.Build.props`, `GeofancyVersion.Display`, and legacy `AssemblyInfo.cs` updated in lockstep.

---

## v0.2.1 — Theme persistence & mobile routing

**Released:** May 2026  
**Live app:** <https://geofancy.up.railway.app>

Patch release addressing **server-side theme state** and **over-eager mobile routing**.

### Fixes

- **Light/dark theme.** `ThemeService` is now **Scoped** to each Blazor Server **circuit** instead of **Singleton**. A singleton shared every user’s in-memory theme; another tab or visitor could overwrite your toggle and make the UI “revert.” Scoped matches one theme state per browser connection.
- **Desktop sent to `/mobile`.** “Mobile viewport” used **`max-width: 900px`**, which matched many **desktop** setups (tiled windows, modest laptop widths). Detection now uses **`768px`**, and the **`/workspace` → `/mobile`** redirect in `App.razor` calls the same **`geofancyDevice.isMobile()`** helper as **Cast a chart** so one rule drives both paths.

### Implementation notes

- **Assemblies aligned to v0.2.1.** Same three-way bump as prior releases (`Directory.Build.props`, `GeofancyVersion.Display`, legacy `AssemblyInfo.cs`).

---

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
