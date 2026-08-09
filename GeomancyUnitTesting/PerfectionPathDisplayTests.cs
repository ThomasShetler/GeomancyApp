using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeomancyApp;

namespace GeomancyUnitTesting
{
    [TestClass]
    public class PerfectionPathDisplayTests
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

        [TestMethod]
        public void FormatFlow_SingleFigureWithActor()
        {
            var path = new PerfectionPathDisplay.ListRowPath
            {
                FromHouse = 5,
                ToHouse = 12,
                FromFigure = "Laetitia",
                ActorPrefix = "Qst."
            };
            Assert.AreEqual("Qst. H5 → H12 · Laetitia", PerfectionPathDisplay.FormatFlow(path));
        }

        [TestMethod]
        public void FormatFlow_MutationTwoFigures()
        {
            var path = new PerfectionPathDisplay.ListRowPath
            {
                FromHouse = 11,
                ToHouse = 12,
                FromFigure = "Fortuna Major",
                ToFigure = "Rubeus"
            };
            Assert.AreEqual("H11 → H12 · Fortuna Major · Rubeus", PerfectionPathDisplay.FormatFlow(path));
        }

        [TestMethod]
        public void Translation_CaputAdjacentHouses_PathAndFormat()
        {
            // Q H1 Puella, X H5 Laetitia; Caput translates via H2 (adj Q) and H6 (adj X).
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Puella");
            chart.SetHouseFigure(5, "Laetitia");
            chart.SetHouseFigure(2, "Caput Draconis");
            chart.SetHouseFigure(6, "Caput Draconis");

            var translation = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Translation);

            Assert.IsNotNull(translation, "Expected translation of Caput Draconis.");
            Assert.AreEqual(2, translation.PathFromHouse);
            Assert.AreEqual(6, translation.PathToHouse);
            Assert.AreEqual("Caput Draconis", translation.PathFigure);
            Assert.AreEqual(string.Empty, translation.PathActor);
            Assert.AreEqual(2, translation.TranslatorHouse);
            Assert.AreEqual(6, translation.TranslatorHouseSecondary);
            Assert.AreEqual("H2 → H6 · Caput Draconis", PerfectionPathDisplay.FormatFlow(translation));
        }

        [TestMethod]
        public void Conjunction_QuesitedPass_PathUsesActorAndPassHouse()
        {
            // X Laetitia H5 also in H12, adjacent to Q H1.
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(5, "Laetitia");
            chart.SetHouseFigure(12, "Laetitia");
            // Break default H2 company that could confuse single-Find ordering.
            chart.SetHouseFigure(2, "Conjunctio");

            var conjunction = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Conjunction
                    && r.PathActor == "Qst.");

            Assert.IsNotNull(conjunction, "Expected quesited-pass conjunction.");
            Assert.AreEqual(5, conjunction.PathFromHouse);
            Assert.AreEqual(12, conjunction.PathToHouse);
            Assert.AreEqual("Laetitia", conjunction.PathFigure);
            Assert.AreEqual("Qst.", conjunction.PathActor);
            Assert.AreEqual("Qst. H5 → H12 · Laetitia", PerfectionPathDisplay.FormatFlow(conjunction));
        }

        [TestMethod]
        public void CompanyOccupation_QuesitedCompanion_PathMatchesMechanism()
        {
            // X Populus H5 in compound company with Via H6; Via also occupies Q H1.
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Conjunctio"); // avoid Q-side company
            chart.SetHouseFigure(5, "Populus");
            chart.SetHouseFigure(6, "Via");

            var companyOcc = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Company
                    && r.BaseMode == PerfectionType.Occupation);

            Assert.IsNotNull(companyOcc, "Expected company-mediated occupation.");
            Assert.AreEqual("Qst.", companyOcc.PathActor);
            Assert.AreEqual(5, companyOcc.PathFromHouse);
            Assert.AreEqual(6, companyOcc.PathToHouse);
            Assert.AreEqual("Via", companyOcc.PathFigure);
            Assert.AreEqual("Qst. H5 → H6 · Via", PerfectionPathDisplay.FormatFlow(companyOcc));
        }

        [TestMethod]
        public void Mutation_NeighboringPasses_BothFiguresOnPath()
        {
            // Q Fortuna Major H1 also at H8; X Rubeus H5 also at H9 (adjacent passes).
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Fortuna Major");
            chart.SetHouseFigure(2, "Conjunctio");
            chart.SetHouseFigure(5, "Rubeus");
            chart.SetHouseFigure(8, "Fortuna Major");
            chart.SetHouseFigure(9, "Rubeus");

            var mutation = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Mutation);

            Assert.IsNotNull(mutation, "Expected mutation between neighboring pass houses.");
            Assert.AreEqual(8, mutation.PathFromHouse);
            Assert.AreEqual(9, mutation.PathToHouse);
            Assert.AreEqual("Fortuna Major", mutation.PathFigure);
            Assert.AreEqual("Rubeus", mutation.PathSecondaryFigure);
            Assert.AreEqual(string.Empty, mutation.PathActor);
            Assert.AreEqual("H8 → H9 · Fortuna Major · Rubeus", PerfectionPathDisplay.FormatFlow(mutation));
        }

        [TestMethod]
        public void Aspect_CastPath_NotQuerentQuesitedHomes()
        {
            // Q Via H1 also at H3; casts sinister sextile to X Populus H5.
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Conjunctio");
            chart.SetHouseFigure(3, "Via");
            chart.SetHouseFigure(5, "Populus");

            var aspect = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Aspect
                    && r.AspectFromHouse == 3 && r.AspectToHouse == 5);

            Assert.IsNotNull(aspect, "Expected translation aspect from H3 to H5.");
            Assert.AreEqual(3, aspect.PathFromHouse);
            Assert.AreEqual(5, aspect.PathToHouse);
            Assert.AreNotEqual(1, aspect.PathFromHouse);
            Assert.AreEqual(aspect.AspectFromHouse, aspect.PathFromHouse);
            Assert.AreEqual(aspect.AspectToHouse, aspect.PathToHouse);
            Assert.AreEqual("Via", aspect.PathFigure);
            Assert.AreEqual(string.Empty, aspect.PathActor);
            Assert.AreEqual("H3 → H5 · Via", PerfectionPathDisplay.FormatFlow(aspect));
        }

        [TestMethod]
        public void ForListRow_FallsBackToAspectHousesWhenPathUnset()
        {
            var path = PerfectionPathDisplay.ForListRow(
                pathFromHouse: 0,
                pathToHouse: 0,
                pathFigure: "Via",
                pathSecondaryFigure: null,
                pathActor: null,
                aspectFromHouse: 3,
                aspectToHouse: 5);

            Assert.AreEqual(3, path.FromHouse);
            Assert.AreEqual(5, path.ToHouse);
            Assert.AreEqual("Via", path.FromFigure);
            Assert.AreEqual(string.Empty, path.ToFigure);
            Assert.AreEqual("H3 → H5 · Via", PerfectionPathDisplay.FormatFlow(path));
        }
    }
}
