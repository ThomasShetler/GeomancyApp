using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeomancyApp;

namespace GeomancyUnitTesting
{
    [TestClass]
    public class PerfectionCalculatorTests
    {
        private static readonly string[] UniqueFigureNames =
        {
            "Via", "Populus", "Conjunctio", "Carcer", "Amissio", "Albus",
            "Puer", "Rubeus", "Acquisitio", "Laetitia", "Puella", "Tristitia"
        };

        private static HouseChart ChartWithUniqueFigures()
        {
            var chart = new HouseChart();
            for (int i = 0; i < 12; i++)
                chart.SetHouseFigure(i + 1, UniqueFigureNames[i]);
            return chart;
        }

        private static void AssertAspectGeometry(int fromHouse, int toHouse, AspectType expectedAspect, string expectedDirection)
        {
            var (aspect, direction) = GeomanticAspects.GetAspectWithDirection(fromHouse, toHouse);
            Assert.AreEqual(expectedAspect, aspect, $"Aspect type from {fromHouse} to {toHouse}");
            Assert.AreEqual(expectedDirection, direction, $"Direction from {fromHouse} to {toHouse}");
        }

        private static void AssertResultMatchesEngine(PerfectionResult result)
        {
            Assert.IsTrue(
                result.Mode == PerfectionType.Aspect
                || (result.Mode == PerfectionType.Company && result.BaseMode == PerfectionType.Aspect),
                "Expected a direct or company-mediated cast aspect result.");
            Assert.IsTrue(result.AspectFromHouse > 0 && result.AspectToHouse > 0,
                "Aspect cast houses must be set on engine results.");

            var (expectedAspect, expectedDirection) =
                GeomanticAspects.GetAspectWithDirection(result.AspectFromHouse, result.AspectToHouse);

            Assert.AreEqual(expectedAspect, result.AspectBetweenSignificators,
                $"Aspect type mismatch for cast {result.AspectFromHouse}→{result.AspectToHouse}");
            Assert.AreEqual(expectedDirection, result.AspectDirection,
                $"Direction mismatch for cast {result.AspectFromHouse}→{result.AspectToHouse}");
        }

        private static void AssertRecordMatchesEngine(AspectRecord record)
        {
            Assert.IsTrue(record.FromHouse > 0 && record.ToHouse > 0,
                "UI aspect records must carry explicit from/to houses.");

            var (expectedAspect, expectedDirection) =
                GeomanticAspects.GetAspectWithDirection(record.FromHouse, record.ToHouse);

            Assert.AreEqual(expectedAspect, record.AspectType,
                $"UI record aspect mismatch for {record.FromHouse}→{record.ToHouse}");
            Assert.AreEqual(expectedDirection, record.Direction,
                $"UI record direction mismatch for {record.FromHouse}→{record.ToHouse}");

            bool expectedMajor = expectedDirection == "Dexter" || expectedAspect == AspectType.Opposition;
            Assert.AreEqual(expectedMajor, record.IsMajorAspect,
                $"IsMajorAspect mismatch for {record.FromHouse}→{record.ToHouse}");
        }

        // ── Figure identity (master fix) ─────────────────────────────────────

        [TestMethod]
        public void FigureNameHelper_FortunaMajorAndMinorAreDistinct()
        {
            Assert.AreNotEqual(
                FigureNameHelper.Root("Fortuna Major"),
                FigureNameHelper.Root("Fortuna Minor"));
            Assert.AreNotEqual(
                FigureNameHelper.Root("Fortuna Major (Greater Fortune)"),
                FigureNameHelper.Root("Fortuna Minor"));
        }

        [TestMethod]
        public void Find_DoesNotTreatFortunaMajorAndMinorAsOccupation()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Fortuna Major");
            chart.SetHouseFigure(7, "Fortuna Minor");

            var result = PerfectionCalculator.Find(chart, 1, 7);
            Assert.AreNotEqual(PerfectionType.Occupation, result.Mode,
                "Fortuna Major and Fortuna Minor must not collapse to the same root.");
        }

        // ── Aspect geometry oracle ───────────────────────────────────────────

        [TestMethod]
        public void GetAspectWithDirection_KnownHousePairs()
        {
            AssertAspectGeometry(1, 3, AspectType.Sextile, "Sinister");
            AssertAspectGeometry(1, 11, AspectType.Sextile, "Dexter");
            AssertAspectGeometry(1, 4, AspectType.Square, "Sinister");
            AssertAspectGeometry(1, 10, AspectType.Square, "Dexter");
            AssertAspectGeometry(1, 5, AspectType.Trine, "Sinister");
            AssertAspectGeometry(1, 9, AspectType.Trine, "Dexter");
            AssertAspectGeometry(1, 7, AspectType.Opposition, "Opposition");
            AssertAspectGeometry(1, 2, AspectType.None, "None");
        }

        // ── Engine → UI parity ─────────────────────────────────────────────────

        [TestMethod]
        public void TranslationAspect_EngineAndUiRecordsShareCastGeometry()
        {
            // Querent Via in house 1; duplicate in house 3 aspects quesited house 5 (sinister sextile).
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(3, "Via");
            chart.SetHouseFigure(5, "Populus");

            var engineResults = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true);
            var aspectResults = engineResults.Where(r => r.Mode == PerfectionType.Aspect).ToList();

            Assert.IsTrue(aspectResults.Count >= 1, "Expected at least one translation aspect.");
            foreach (var aspect in aspectResults)
                AssertResultMatchesEngine(aspect);

            var analysis = PerfectionCalculator.AnalyzePerfections(chart, 1, 5);
            var uiAspects = analysis.PositiveAspects.Concat(analysis.NegativeAspects).ToList();

            Assert.IsTrue(uiAspects.Count >= 1, "UI should surface the same translation aspect(s).");
            foreach (var record in uiAspects)
                AssertRecordMatchesEngine(record);

            // The favorable translation we set up must appear with exact cast houses.
            Assert.IsTrue(uiAspects.Any(r =>
                r.FromHouse == 3 && r.ToHouse == 5
                && r.AspectType == AspectType.Sextile && r.Direction == "Sinister"),
                "UI must show house 3 casting sinister sextile to house 5, not querent/quesited defaults.");
        }

        [TestMethod]
        public void TranslationSquare_EngineStoresCastFromTranslatorHouse()
        {
            // Quesited Populus in house 7; duplicate in house 10 aspects querent house 1 (sinister square).
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(7, "Populus");
            chart.SetHouseFigure(10, "Populus");

            var engineResults = PerfectionCalculator.Find(chart, 1, 7, returnAllModes: true);
            var square = engineResults.FirstOrDefault(r =>
                r.Mode == PerfectionType.Aspect && r.AspectBetweenSignificators == AspectType.Square);

            Assert.IsNotNull(square, "Expected a translation square.");
            Assert.AreEqual(10, square.AspectFromHouse);
            Assert.AreEqual(1, square.AspectToHouse);
            Assert.AreEqual("Sinister", square.AspectDirection);

            var analysis = PerfectionCalculator.AnalyzePerfections(chart, 1, 7);
            var uiSquare = analysis.NegativeAspects.FirstOrDefault(r => r.AspectType == AspectType.Square);

            Assert.IsNotNull(uiSquare);
            Assert.AreEqual(10, uiSquare.FromHouse);
            Assert.AreEqual(1, uiSquare.ToHouse);
            Assert.AreEqual("Sinister", uiSquare.Direction);
            Assert.IsFalse(analysis.Denials.Any(d => d.Mode == PerfectionType.Aspect),
                "Standalone denial aspects must not also appear under Denials.");
        }

        [TestMethod]
        public void DenialAspect_NotDuplicatedInDenialsAndNegativeAspects()
        {
            // Only a translation square — no classical perfection.
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Conjunctio");
            chart.SetHouseFigure(7, "Populus");
            chart.SetHouseFigure(10, "Populus");

            var analysis = PerfectionCalculator.AnalyzePerfections(chart, 1, 7);

            Assert.AreEqual(1, analysis.NegativeAspects.Count(a => a.AspectType == AspectType.Square));
            Assert.AreEqual(0, analysis.Denials.Count(d => d.Mode == PerfectionType.Aspect));
            Assert.IsTrue(analysis.Denials.Any(d => d.Mode == PerfectionType.None),
                "Impedition remains under Denials.");

            int listedUnfavorable =
                analysis.Denials.Where(d => d.Mode != PerfectionType.Aspect)
                    .Sum(d => PerfectionCalculator.CalculateUnfavorableScore(d))
                + analysis.NegativeAspects.Sum(a =>
                    a.AspectType == AspectType.Square ? (a.MadeThroughCompany ? -4 : -3)
                    : a.AspectType == AspectType.Opposition ? (a.MadeThroughCompany ? -5 : -4)
                    : 0);

            Assert.AreEqual(listedUnfavorable, analysis.TotalUnfavorableScore);
        }

        [TestMethod]
        public void Impedition_StaticAspectDoesNotSurfaceInUiAspectLists()
        {
            // No movement → impedition only. Houses 1 and 7 are in static opposition, but that must not
            // appear as a cast aspect row when no figure actually translated.
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Tristitia"); // avoid a second Populus elsewhere
            chart.SetHouseFigure(7, "Populus");

            var analysis = PerfectionCalculator.AnalyzePerfections(chart, 1, 7);

            Assert.AreEqual(0, analysis.PositiveAspects.Count);
            Assert.AreEqual(0, analysis.NegativeAspects.Count,
                "Static house-pair opposition must not be shown as a cast aspect when Mode is None.");
        }

        [TestMethod]
        public void CompanyAspect_PerfectionNotDuplicatedInPositiveAspectList()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Via");
            chart.SetHouseFigure(6, "Populus");

            var engineResults = PerfectionCalculator.Find(chart, 1, 6, returnAllModes: true);
            var companyAspect = engineResults.FirstOrDefault(r =>
                r.Mode == PerfectionType.Company && r.BaseMode == PerfectionType.Aspect);

            Assert.IsNotNull(companyAspect, "Expected a company-mediated aspect perfection.");
            AssertResultMatchesEngine(companyAspect);

            var analysis = PerfectionCalculator.AnalyzePerfections(chart, 1, 6);

            Assert.IsTrue(analysis.Perfections.Any(p =>
                p.Mode == PerfectionType.Company && p.BaseMode == PerfectionType.Aspect));
            Assert.AreEqual(0, analysis.PositiveAspects.Count,
                "Company-mediated aspect perfections must not duplicate into PositiveAspects.");
        }

        [TestMethod]
        public void FavorableStandaloneAspect_ListedOnceInPositiveAspectsNotPerfections()
        {
            // Q Via H1 also at H3; casts sinister sextile to X Populus H5.
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Conjunctio");
            chart.SetHouseFigure(3, "Via");
            chart.SetHouseFigure(5, "Populus");

            var analysis = PerfectionCalculator.AnalyzePerfections(chart, 1, 5);

            Assert.IsFalse(analysis.Perfections.Any(p => p.Mode == PerfectionType.Aspect),
                "Favorable Mode=Aspect must not also appear under Perfections.");
            Assert.AreEqual(1, analysis.PositiveAspects.Count);
            Assert.AreEqual(3, analysis.PositiveAspects[0].FromHouse);
            Assert.AreEqual(5, analysis.PositiveAspects[0].ToHouse);
            Assert.AreEqual(3, analysis.TotalFavorableScore,
                "Favorable aspect must be scored once (+3), not double-counted via Perfections.");
            Assert.AreEqual(analysis.TotalFavorableScore + analysis.TotalUnfavorableScore, analysis.NetScore);
        }

        [TestMethod]
        public void CompanyMaleficAspect_SurfacesInNegativeAspectsWhenOtherPerfectionsExist()
        {
            // Q Via H1 in simple company with Via H2 (square to X H5); Caput translates H12↔H6.
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Via");
            chart.SetHouseFigure(5, "Populus");
            chart.SetHouseFigure(6, "Caput Draconis");
            chart.SetHouseFigure(12, "Caput Draconis");

            var engineResults = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true);
            var companySquare = engineResults.FirstOrDefault(r =>
                r.Mode == PerfectionType.Company
                && r.AspectBetweenSignificators == AspectType.Square
                && r.AspectFromHouse == 2 && r.AspectToHouse == 5);
            var translation = engineResults.FirstOrDefault(r => r.Mode == PerfectionType.Translation);

            Assert.IsNotNull(companySquare, "Expected company-mediated square from H2 to H5.");
            Assert.IsNotNull(translation, "Expected Caput translation so other perfections exist.");

            var analysis = PerfectionCalculator.AnalyzePerfections(chart, 1, 5);

            Assert.IsTrue(analysis.Perfections.Any(p => p.Mode == PerfectionType.Translation));
            Assert.IsFalse(analysis.Perfections.Any(p =>
                p.Mode == PerfectionType.Company && p.AspectBetweenSignificators == AspectType.Square),
                "Unfavorable company aspects are not perfections.");
            Assert.IsFalse(analysis.Denials.Any(d =>
                d.Mode == PerfectionType.Company && d.AspectBetweenSignificators == AspectType.Square),
                "With other perfections present, company malefics are difficulties not Denials.");

            var neg = analysis.NegativeAspects.FirstOrDefault(a =>
                a.FromHouse == 2 && a.ToHouse == 5 && a.AspectType == AspectType.Square);
            Assert.IsNotNull(neg, "Company square must appear under NegativeAspects.");
            Assert.IsTrue(neg.MadeThroughCompany);
            Assert.AreEqual(CompanyType.Simple, neg.CompanyType,
                "Collapsed company malefic must retain CompanyType for accurate UI labeling.");
            Assert.AreEqual(-4, analysis.TotalUnfavorableScore,
                "Company square scores -4 (-3 base, -1 company) once via NegativeAspects.");
        }

        [TestMethod]
        public void StandaloneNegativeAspect_DoesNotClaimCompany()
        {
            // Translation square only (quesited Populus H7 also in H10 → square to H1).
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Conjunctio");
            chart.SetHouseFigure(7, "Populus");
            chart.SetHouseFigure(10, "Populus");

            var analysis = PerfectionCalculator.AnalyzePerfections(chart, 1, 7);
            var sq = analysis.NegativeAspects.FirstOrDefault(a => a.AspectType == AspectType.Square);

            Assert.IsNotNull(sq, "Expected a standalone translation square.");
            Assert.IsFalse(sq.MadeThroughCompany);
            Assert.AreEqual(CompanyType.None, sq.CompanyType);
            Assert.AreEqual("Sq", PerfectionDetailCopy.AspectListLabel(sq.AspectType.ToString(), sq.MadeThroughCompany, sq.CompanyType.ToString()));
        }

        [TestMethod]
        public void AnalyzePerfections_TotalsMatchListedRowScores()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Via");
            chart.SetHouseFigure(6, "Populus");
            chart.SetHouseFigure(4, "Populus");
            chart.SetHouseFigure(10, "Populus");

            var analysis = PerfectionCalculator.AnalyzePerfections(chart, 1, 6);

            int listedFavorable =
                analysis.Perfections.Sum(p => PerfectionCalculator.CalculateScore(p))
                + analysis.Denials.Sum(d => PerfectionCalculator.CalculateScore(d))
                + analysis.PositiveAspects.Sum(a =>
                    a.AspectType == AspectType.Trine || a.AspectType == AspectType.Sextile
                        ? (a.MadeThroughCompany ? 2 : 3) : 0);

            int listedUnfavorable =
                analysis.Perfections.Sum(p => PerfectionCalculator.CalculateUnfavorableScore(p))
                + analysis.Denials.Where(d => d.Mode != PerfectionType.Aspect)
                    .Sum(d => PerfectionCalculator.CalculateUnfavorableScore(d))
                + analysis.NegativeAspects.Sum(a =>
                    a.AspectType == AspectType.Square ? (a.MadeThroughCompany ? -4 : -3)
                    : a.AspectType == AspectType.Opposition ? (a.MadeThroughCompany ? -5 : -4)
                    : 0);

            Assert.AreEqual(listedFavorable, analysis.TotalFavorableScore);
            Assert.AreEqual(listedUnfavorable, analysis.TotalUnfavorableScore);
            Assert.AreEqual(analysis.TotalFavorableScore + analysis.TotalUnfavorableScore, analysis.NetScore);
        }

        [TestMethod]
        public void CompanySimple_AdjacentSignificators_DoesNotInventSelfPathConjunction()
        {
            // Screenshot bug: Q H7 Cauda in Simple company with H8 Cauda, X H6 Caput (adjacent to H7).
            // Old logic treated Cauda-in-H7 as a "company pass" → H7→H7 conjunction.
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(6, "Caput Draconis");
            chart.SetHouseFigure(7, "Cauda Draconis");
            chart.SetHouseFigure(8, "Cauda Draconis");

            var results = PerfectionCalculator.Find(chart, 7, 6, returnAllModes: true);

            Assert.IsFalse(results.Any(r =>
                    r.Mode == PerfectionType.Company
                    && r.BaseMode == PerfectionType.Conjunction
                    && r.PathFromHouse == r.PathToHouse),
                "Company conjunction must never path a house to itself.");

            Assert.IsFalse(results.Any(r =>
                    r.Mode == PerfectionType.Company
                    && r.BaseMode == PerfectionType.Conjunction
                    && r.PathFromHouse == 7 && r.PathToHouse == 7),
                "Must not invent Conjunction via the significator's own seat.");

            // Companion H8 sextiles H6 — valid company aspect instead of fake conjunction.
            var companyAspect = results.FirstOrDefault(r =>
                r.Mode == PerfectionType.Company
                && r.BaseMode == PerfectionType.Aspect
                && r.AspectFromHouse == 8
                && r.AspectToHouse == 6);
            Assert.IsNotNull(companyAspect, "Expected company-mediated aspect from companion H8 to X H6.");
            Assert.AreEqual(CompanyType.Simple, companyAspect.CompanyType);

            var explanation = PerfectionMechanismHelper.ExplainFromPerfectionResult(
                companyAspect,
                chart.GetHouseFigure(8)?.Name ?? string.Empty,
                chart.GetHouseFigure(6)?.Name ?? string.Empty);
            Assert.IsFalse(explanation.Steps.Any(s =>
                System.Text.RegularExpressions.Regex.IsMatch(s, @"House 7 .* company with House 7")),
                "Company context must not claim a house is in company with itself.");
        }

        [TestMethod]
        public void AnalyzePerfections_ImpeditionOnly_ScoresUnfavorableTotal()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Tristitia");
            chart.SetHouseFigure(7, "Populus");

            var analysis = PerfectionCalculator.AnalyzePerfections(chart, 1, 7);

            Assert.IsTrue(analysis.TotalUnfavorableScore < 0,
                "Impedition-only charts must contribute to unfavorable totals.");
            Assert.IsTrue(analysis.Denials.Any(d => d.Mode == PerfectionType.None));
        }

        [TestMethod]
        public void GeomanticAspects_AllAspects_UsesFigureNameHelperForRoots()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Fortuna Major");
            chart.SetHouseFigure(3, "Fortuna Minor"); // sinister sextile between 1 and 3

            var pairs = GeomanticAspects.AllAspects(chart, AspectType.Sextile)
                .Where(p => (p.from == 1 && p.to == 3) || (p.from == 3 && p.to == 1))
                .ToList();

            Assert.AreEqual(1, pairs.Count,
                "Fortuna Major vs Fortuna Minor should still count as different figures for aspect enumeration.");
        }
    }
}
