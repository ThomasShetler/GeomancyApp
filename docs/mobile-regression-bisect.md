# Mobile regression bisect (`web-app` test branch)

**Known good baseline:** `6733e19` — *Refocus generate-figures wiki with practice caster and corpus panel*

**Goal:** Cherry-pick post-baseline commits onto `web-app` one at a time and test on a **real phone** (not only desktop mobile emulation) after each Railway deploy.

## Phone test checklist (2–3 min)

1. **Home** → tap **Cast a chart!** → dialog appears, all three options respond.
2. **`/mobile`** → tab strip styled; chart handle works; open Perfections list; tap a row → detail; back works.
3. **`/mobile?mothers=1`** (or Mothers setup from cast flow) → dot caster taps register.

If any step fails, **stop** — the commit just applied is the regression (or part of it).

## Commit queue (oldest → newest)

| Step | Commit | Summary | Status |
|------|--------|---------|--------|
| 0 | `6733e19` | Baseline (good) | ✅ current start |
| 1 | `cb01cc7` | 1.0.0 prep: footer, home/nav (skip link removed) | pass |
| 2 | `1f1a424` | Expand v1.0.0 RELEASES changelog | pass |
| 3 | `9d4c127` | RELEASES table tweak | pass |
| 4 | `e54fff6` | SEO, PageSeo, footer, workspace scroll CSS | pass |
| 5 | `9afbef8` | Google Search Console HTML | pass |
| 6 | `ff6dcce` | v1.0.1 version bump only | pass |
| 7 | `b89ba6e` | Logo + prerender + branding URLs | pass |
| 8 | `5057ef3` | Square logo asset | pass |
| 9 | `5fc1737` | v1.0.2 version bump only | pass |
| 10 | `ce6c84e` | mobile-workspace.css, skip link layout | skipped |
| 11 | `43d3a04` | v1.0.3 version bump only | skipped |
| 12 | `211cf0f` | v1.0.4 layout revert attempt | skipped |

**Outcome:** Bisect stopped after step 9. Production promoted to **`master` as v1.0.5** (merge `web-app`, drop v1.0.3/1.0.4 experiments).

**Skipped on test branch** (merge commits / master-only doc): `b49d908`, `61ad746`, `7f608e3`, `789a1a1`, `a985e45`, `52abe2f`

## Commands

Apply **next** commit (from repo root, on `web-app`):

```powershell
.\scripts\Advance-BisectCommit.ps1
```

Reset test branch back to baseline:

```powershell
git checkout web-app
git reset --hard 6733e19
git push --force origin web-app
```

## Recording a failure

When step **N** breaks mobile, note commit hash from the table and stop. Fix or revert that change before re-applying later steps.
