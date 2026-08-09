using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeomancyApp;

namespace GeomancyUnitTesting
{
    [TestClass]
    public class PerfectionMechanismHelperTests
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

        private static string FigureAt(HouseChart chart, int house) =>
            chart.GetHouseFigure(house)?.Name ?? string.Empty;

        [TestMethod]
        public void CompanyCompanionCast_Trine_MatchesEngineGeometry()
        {
            // H1+H2 simple company (Via); company house 2 casts sinister trine to quesited house 6.
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Via");
            chart.SetHouseFigure(6, "Populus");

            var companyAspect = PerfectionCalculator.Find(chart, 1, 6, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Company && r.BaseMode == PerfectionType.Aspect);

            Assert.IsNotNull(companyAspect);
            Assert.AreEqual(2, companyAspect.AspectFromHouse);
            Assert.AreEqual(6, companyAspect.AspectToHouse);

            var (expectedAspect, expectedDirection) =
                GeomanticAspects.GetAspectWithDirection(companyAspect.AspectFromHouse, companyAspect.AspectToHouse);

            var explanation = PerfectionMechanismHelper.ExplainFromPerfectionResult(
                companyAspect,
                FigureAt(chart, companyAspect.AspectFromHouse),
                FigureAt(chart, companyAspect.AspectToHouse));

            Assert.IsTrue(explanation.HasStructuredCast);
            Assert.AreEqual(companyAspect.AspectFromHouse, explanation.Cast.FromHouse);
            Assert.AreEqual(companyAspect.AspectToHouse, explanation.Cast.ToHouse);
            Assert.AreEqual(expectedAspect.ToString(), explanation.Cast.AspectType);
            Assert.AreEqual(expectedDirection, explanation.Cast.Direction);

            Assert.IsTrue(explanation.Steps.Any(s => s.Contains("company")),
                "Steps should explain the company pairing.");
            Assert.IsTrue(explanation.Steps.Any(s =>
                s.Contains("House 2") && s.Contains("House 6") && s.Contains("Trine")),
                "Steps should name the cast houses and aspect.");
            Assert.IsTrue(explanation.Steps.Any(s => s.Contains("Via") && s.Contains("Populus")),
                "Steps should name the casting and receiving figures.");
            Assert.IsTrue(explanation.Steps.Any(s => s.Contains("company companion of the querent")),
                "Steps should identify casting house as querent's company companion.");
        }

        [TestMethod]
        public void QuesitedSideCompanyCast_IdentifiesQuesitedCompanion()
        {
            // Quesited Caput in house 10; Cauda in pair house 9 (compound company); casts sinister trine to house 1.
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(10, "Caput Draconis");
            chart.SetHouseFigure(9, "Cauda Draconis");

            var companyAspect = PerfectionCalculator.Find(chart, 1, 10, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Company && r.BaseMode == PerfectionType.Aspect
                    && r.AspectFromHouse == 9);

            Assert.IsNotNull(companyAspect, "Expected company cast from house 9 (quesited companion).");

            var explanation = PerfectionMechanismHelper.ExplainAspectCast(
                companyAspect.AspectFromHouse,
                companyAspect.AspectToHouse,
                FigureAt(chart, companyAspect.AspectFromHouse),
                FigureAt(chart, companyAspect.AspectToHouse),
                companyAspect.AspectBetweenSignificators.ToString(),
                companyAspect.AspectDirection,
                1,
                10,
                "Via",
                "Caput Draconis",
                true,
                companyAspect.CompanyType.ToString(),
                companyAspect.CompanyTypeDescription,
                "Company",
                "Aspect");

            Assert.AreEqual(9, explanation.Cast.FromHouse);
            Assert.AreEqual(1, explanation.Cast.ToHouse);
            Assert.IsTrue(explanation.Steps.Any(s => s.Contains("company companion of the quesited")),
                "Steps should identify casting house as quesited's company companion.");
            Assert.IsTrue(explanation.Steps.Any(s => s.Contains("House 10") && s.Contains("House 9")),
                "Steps should describe quesited pairing with house 9.");
        }

        [TestMethod]
        public void StandaloneTranslationAspect_CastFromTranslatorNotQuerent()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(3, "Via");
            chart.SetHouseFigure(5, "Populus");

            var aspect = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Aspect
                    && r.AspectFromHouse == 3 && r.AspectToHouse == 5);

            Assert.IsNotNull(aspect);

            var (expectedAspect, expectedDirection) =
                GeomanticAspects.GetAspectWithDirection(3, 5);

            var explanation = PerfectionMechanismHelper.ExplainFromPerfectionResult(
                aspect,
                FigureAt(chart, 3),
                FigureAt(chart, 5));

            Assert.AreEqual(3, explanation.Cast.FromHouse);
            Assert.AreEqual(5, explanation.Cast.ToHouse);
            Assert.AreNotEqual(1, explanation.Cast.FromHouse);
            Assert.AreEqual(expectedAspect.ToString(), explanation.Cast.AspectType);
            Assert.AreEqual(expectedDirection, explanation.Cast.Direction);
            Assert.IsTrue(explanation.Steps.Any(s =>
                s.Contains("translation of the significator") || s.Contains("House 3")));
            Assert.IsTrue(explanation.Steps.Any(s => s.Contains("Sextile")));
        }

        [TestMethod]
        public void StandaloneSquare_ReportsUnfavorableCastGeometry()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(7, "Populus");
            chart.SetHouseFigure(10, "Populus");

            var square = PerfectionCalculator.Find(chart, 1, 7, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Aspect
                    && r.AspectBetweenSignificators == AspectType.Square);

            Assert.IsNotNull(square);

            var (expectedAspect, expectedDirection) =
                GeomanticAspects.GetAspectWithDirection(square.AspectFromHouse, square.AspectToHouse);

            var explanation = PerfectionMechanismHelper.ExplainFromPerfectionResult(
                square,
                FigureAt(chart, square.AspectFromHouse),
                FigureAt(chart, square.AspectToHouse));

            Assert.AreEqual(AspectType.Square, expectedAspect);
            Assert.AreEqual("Square", explanation.Cast.AspectType);
            Assert.AreEqual(expectedDirection, explanation.Cast.Direction);
            Assert.AreEqual(square.AspectFromHouse, explanation.Cast.FromHouse);
            Assert.AreEqual(square.AspectToHouse, explanation.Cast.ToHouse);
            Assert.IsTrue(explanation.Steps.Any(s => s.Contains("Square")));
        }

        [TestMethod]
        public void Explanation_DoesNotUseQuerentQuesitedAsDefaultCastHouses()
        {
            var explanation = PerfectionMechanismHelper.ExplainAspectCast(
                aspectFromHouse: 2,
                aspectToHouse: 10,
                fromFigure: "Cauda Draconis",
                toFigure: "Conjunctio",
                aspectType: "Trine",
                direction: "Sinister",
                querentHouse: 1,
                quesitedHouse: 10,
                querentFigure: "Caput Draconis",
                quesitedFigure: "Conjunctio",
                madeThroughCompany: true,
                companyType: "Compound",
                companyTypeDescription: "Company Compound (opposite figures)",
                mode: "Company",
                baseMode: "Aspect");

            Assert.AreEqual(2, explanation.Cast.FromHouse);
            Assert.AreEqual(10, explanation.Cast.ToHouse);
            Assert.AreNotEqual(1, explanation.Cast.FromHouse);
            Assert.IsTrue(explanation.Steps.Any(s => s.Contains("Compound")));
            Assert.IsTrue(explanation.Steps.Any(s =>
                s.Contains("Cauda Draconis") && s.Contains("House 2") && s.Contains("House 10")));
        }
    }
}
