# Cherry-pick the next bisect commit onto web-app and force-push for Railway testing.
# Run from repo root. Requires clean working tree on branch web-app.

$ErrorActionPreference = "Stop"
$RepoRoot = Split-Path $PSScriptRoot -Parent
Set-Location $RepoRoot

$Queue = @(
    @{ Hash = "cb01cc7"; Summary = "1.0.0 prep: footer, skip link, home/nav" },
    @{ Hash = "1f1a424"; Summary = "Expand v1.0.0 RELEASES changelog" },
    @{ Hash = "9d4c127"; Summary = "RELEASES table tweak" },
    @{ Hash = "e54fff6"; Summary = "SEO, PageSeo, footer, workspace scroll" },
    @{ Hash = "9afbef8"; Summary = "Google Search Console verification" },
    @{ Hash = "ff6dcce"; Summary = "v1.0.1 version bump" },
    @{ Hash = "b89ba6e"; Summary = "Logo + prerender + branding" },
    @{ Hash = "5057ef3"; Summary = "Square logo asset" },
    @{ Hash = "5fc1737"; Summary = "v1.0.2 version bump" },
    @{ Hash = "ce6c84e"; Summary = "mobile-workspace.css + skip link" },
    @{ Hash = "43d3a04"; Summary = "v1.0.3 version bump" },
    @{ Hash = "211cf0f"; Summary = "v1.0.4 layout revert attempt" }
)

$StatePath = Join-Path $RepoRoot ".bisect-progress.json"
$Baseline = "6733e19"

function Get-State {
    if (Test-Path $StatePath) {
        return Get-Content $StatePath -Raw | ConvertFrom-Json
    }
    return [PSCustomObject]@{ nextIndex = 0; applied = @() }
}

$branch = (git rev-parse --abbrev-ref HEAD).Trim()
if ($branch -ne "web-app") {
    Write-Error "Checkout web-app first (current: $branch)"
}

if (git status --porcelain --untracked-files=no) {
    Write-Error "Working tree has uncommitted changes to tracked files. Commit or stash first."
}

$head = (git rev-parse --short HEAD).Trim()
if ($head -ne $Baseline -and -not (Test-Path $StatePath)) {
    Write-Warning "HEAD is $head, not baseline $Baseline. .bisect-progress.json will drive the queue."
}

$state = Get-State
$idx = [int]$state.nextIndex

if ($idx -ge $Queue.Count) {
    Write-Host "All $($Queue.Count) bisect commits already applied."
    Write-Host "web-app HEAD: $(git log -1 --oneline)"
    exit 0
}

$item = $Queue[$idx]
Write-Host "Applying step $($idx + 1)/$($Queue.Count): $($item.Hash) - $($item.Summary)"

git cherry-pick $item.Hash
if ($LASTEXITCODE -ne 0) {
    Write-Error "Cherry-pick failed. Resolve conflicts, then: git cherry-pick --continue"
}

$state.nextIndex = $idx + 1
$state.applied += $item.Hash
$state | ConvertTo-Json | Set-Content $StatePath -Encoding utf8

Write-Host "Pushing origin/web-app..."
git push --force origin web-app

Write-Host ""
Write-Host "Deployed step $($idx + 1): $($item.Hash)"
Write-Host "Test on your phone (see docs/mobile-regression-bisect.md), then run again or reset."
