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
            Assert.AreEqual(PerfectionType.Aspect, result.Mode);
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
