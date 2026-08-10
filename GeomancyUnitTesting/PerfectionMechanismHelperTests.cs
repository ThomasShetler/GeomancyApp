using System;
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
            Assert.IsTrue(explanation.Steps.Any(s =>
                s.Contains("House 1") && s.Contains("querent") && s.Contains("home seat")));
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
            Assert.AreEqual("How this Trine formed", explanation.Title);
            Assert.IsTrue(explanation.Steps.Count >= 2);
            Assert.IsTrue(explanation.Steps[0].IndexOf("Trine", System.StringComparison.OrdinalIgnoreCase) >= 0,
                "Aspect cast must lead the formation steps.");
            Assert.IsTrue(explanation.Steps.Any(s => s.Contains("Compound")));
            Assert.IsTrue(explanation.Steps.Any(s =>
                s.Contains("Cauda Draconis") && s.Contains("House 2") && s.Contains("House 10")));
            Assert.IsTrue(explanation.Steps.Any(s =>
                    s.IndexOf("paired under", StringComparison.OrdinalIgnoreCase) >= 0
                    || s.IndexOf("formed as", StringComparison.OrdinalIgnoreCase) >= 0
                    || s.IndexOf("opposite figures", StringComparison.OrdinalIgnoreCase) >= 0),
                "Steps should explain how the company type is formed.");
        }

        [TestMethod]
        public void Translation_FormationFlowAndStepsMatchPath()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Puella");
            chart.SetHouseFigure(5, "Laetitia");
            chart.SetHouseFigure(2, "Caput Draconis");
            chart.SetHouseFigure(6, "Caput Draconis");

            var translation = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Translation);

            Assert.IsNotNull(translation);

            var explanation = PerfectionMechanismHelper.ExplainFromPerfectionResult(
                translation,
                FigureAt(chart, translation.PathFromHouse),
                FigureAt(chart, translation.PathToHouse));

            Assert.AreEqual("H2 → H6 · Caput Draconis", explanation.Flow);
            Assert.IsTrue(explanation.Steps.Any(s =>
                s.Contains("Caput Draconis") && s.Contains("House 2") && s.Contains("House 6")));
            Assert.IsTrue(explanation.Participants.Any(p =>
                p.Role.Contains("Translator") && p.FigureName.Contains("Caput")));
        }

        [TestMethod]
        public void Conjunction_FormationIncludesActorAndPassHouse()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Conjunctio");
            chart.SetHouseFigure(5, "Laetitia");
            chart.SetHouseFigure(12, "Laetitia");

            var conjunction = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Conjunction && r.PathActor == "Qst.");

            Assert.IsNotNull(conjunction);

            var explanation = PerfectionMechanismHelper.ExplainFromPerfectionResult(
                conjunction,
                FigureAt(chart, conjunction.PathFromHouse),
                FigureAt(chart, conjunction.PathToHouse));

            Assert.AreEqual("Qst. H5 → H12 · Laetitia", explanation.Flow);
            Assert.IsTrue(explanation.Steps.Any(s =>
                s.Contains("Laetitia") && s.Contains("House 5") && s.Contains("House 12")));
            Assert.IsTrue(explanation.Participants.Any(p =>
                p.Role.IndexOf("Quesited", StringComparison.OrdinalIgnoreCase) >= 0));
        }

        [TestMethod]
        public void CompanyOccupation_FormationIncludesCompanyPairingAndPath()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Conjunctio");
            chart.SetHouseFigure(5, "Populus");
            chart.SetHouseFigure(6, "Via");

            var companyOcc = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Company
                    && r.BaseMode == PerfectionType.Occupation);

            Assert.IsNotNull(companyOcc);

            var explanation = PerfectionMechanismHelper.ExplainFromPerfectionResult(
                companyOcc,
                FigureAt(chart, companyOcc.PathFromHouse),
                FigureAt(chart, companyOcc.PathToHouse));

            Assert.AreEqual("Qst. H5 → H6 · Via", explanation.Flow);
            Assert.IsTrue(explanation.Steps.Any(s => s.IndexOf("company", StringComparison.OrdinalIgnoreCase) >= 0));
            Assert.IsTrue(explanation.Steps.Any(s =>
                s.IndexOf("occup", StringComparison.OrdinalIgnoreCase) >= 0
                && s.Contains("Via")));
            Assert.IsTrue(explanation.Participants.Any(p =>
                p.Role.IndexOf("Company", StringComparison.OrdinalIgnoreCase) >= 0
                || p.FigureName == "Via"));
        }

        [TestMethod]
        public void Mutation_FormationIncludesBothPassFigures()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Fortuna Major");
            chart.SetHouseFigure(2, "Conjunctio");
            chart.SetHouseFigure(5, "Rubeus");
            chart.SetHouseFigure(8, "Fortuna Major");
            chart.SetHouseFigure(9, "Rubeus");

            var mutation = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Mutation);

            Assert.IsNotNull(mutation);

            var explanation = PerfectionMechanismHelper.ExplainFromPerfectionResult(
                mutation,
                FigureAt(chart, mutation.PathFromHouse),
                FigureAt(chart, mutation.PathToHouse));

            Assert.AreEqual("H8 → H9 · Fortuna Major · Rubeus", explanation.Flow);
            Assert.IsTrue(explanation.Steps.Any(s =>
                s.Contains("House 8") && s.Contains("House 9")
                && s.Contains("Fortuna Major") && s.Contains("Rubeus")));
            Assert.IsTrue(explanation.Participants.Any(p => p.FigureName == "Fortuna Major"));
            Assert.IsTrue(explanation.Participants.Any(p => p.FigureName == "Rubeus"));
        }

        [TestMethod]
        public void Aspect_FormationFlowEqualsCastHousesNotQuerentQuesited()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Conjunctio");
            chart.SetHouseFigure(3, "Via");
            chart.SetHouseFigure(5, "Populus");

            var aspect = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Aspect
                    && r.AspectFromHouse == 3 && r.AspectToHouse == 5);

            Assert.IsNotNull(aspect);

            var explanation = PerfectionMechanismHelper.ExplainFromPerfectionResult(
                aspect,
                FigureAt(chart, 3),
                FigureAt(chart, 5));

            Assert.AreEqual("H3 → H5 · Via", explanation.Flow);
            Assert.AreEqual(3, explanation.Cast.FromHouse);
            Assert.AreEqual(5, explanation.Cast.ToHouse);
            Assert.AreNotEqual(1, explanation.Cast.FromHouse);
            Assert.IsTrue(explanation.Steps.Any(s => s.Contains("Sextile") || s.Contains("House 3")));
        }
    }
}
