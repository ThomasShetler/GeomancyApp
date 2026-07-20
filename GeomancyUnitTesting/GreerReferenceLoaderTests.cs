using System.Linq;
using GeomancyAPI.Handlers;
using GeomancyAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeomancyUnitTesting
{
    [TestClass]
    public class GreerReferenceLoaderTests
    {
        private const string ExpectedAttribution =
            "Material excerpted from The Art and Practice of Geomancy © 2009, John Michael Greer with permission from Red Wheel/Weiser LLC. Newburyport, MA www.redwheelweiser.com";

        [TestMethod]
        public void GreerFigureCorpusLoader_ReturnsSixteenFigures()
        {
            var figures = GeomancyHandlers.GetGreerFiguresDirectory();

            Assert.AreEqual(16, figures.Count);
            Assert.IsNotNull(figures.FirstOrDefault(f => f.Name == "Puer"));
            Assert.AreEqual(ExpectedAttribution, figures[0].Source.Attribution);
        }

        [TestMethod]
        public void GreerFigureCorpusLoader_Puer_HasGreerCommentary()
        {
            var puer = GeomancyHandlers.GetGreerFigureDirectoryEntry("Puer");

            Assert.IsNotNull(puer);
            StringAssert.Contains(puer.Commentary, "male sexual energy");
            Assert.AreEqual("Boy", puer.EnglishName);
        }

        [TestMethod]
        public void GreerHouseDirectoryLoader_ReturnsTwelveStructuredHouses()
        {
            var directory = GreerHouseDirectoryLoader.GetDirectory();

            Assert.AreEqual(12, directory.Houses.Count);
            Assert.AreEqual(ExpectedAttribution, directory.License.Attribution);
            Assert.IsTrue(directory.Houses.All(h => h.Governs != null && h.Governs.Count > 0));
        }

        [TestMethod]
        public void GreerHouseDirectoryLoader_SecondHouse_HasStructuredFields()
        {
            var house = GreerHouseDirectoryLoader.GetHouse(2);

            Assert.IsNotNull(house);
            Assert.IsTrue(house.ExampleQuestions.Count >= 5);
            Assert.IsTrue(house.Governs.Count >= 5);
            Assert.IsTrue(house.QuestionInvolves.Any(q => q.IndexOf("financ", System.StringComparison.OrdinalIgnoreCase) >= 0));
            Assert.IsTrue(house.AdditionalDetails.Count >= 1);
        }

        [TestMethod]
        public void GreerHouseDirectoryLoader_FirstAndEleventh_HavePlanetaryRejoicing()
        {
            var first = GreerHouseDirectoryLoader.GetHouse(1);
            var eleventh = GreerHouseDirectoryLoader.GetHouse(11);

            Assert.IsNotNull(first);
            Assert.IsNotNull(eleventh);
            Assert.IsTrue(first.AdditionalDetails.Any(d => d.IndexOf("Mercury", System.StringComparison.OrdinalIgnoreCase) >= 0));
            Assert.IsTrue(eleventh.AdditionalDetails.Any(d => d.IndexOf("Jupiter", System.StringComparison.OrdinalIgnoreCase) >= 0));
            Assert.IsFalse(first.AdditionalDetails.Any(d => d.IndexOf("Rubeus", System.StringComparison.OrdinalIgnoreCase) >= 0));
            Assert.IsFalse(eleventh.AdditionalDetails.Any(d => d.IndexOf("Populus", System.StringComparison.OrdinalIgnoreCase) >= 0));
        }

        [TestMethod]
        public void GreerCriticalContextRules_RubeusInFirst_IncludesDestroyNote()
        {
            var flags = GreerCriticalContextRules.Evaluate(true, 1, "Rubeus", "Via");

            Assert.AreEqual(1, flags.Count);
            Assert.AreEqual("rubeus-first", flags[0].Id);
            Assert.IsTrue(flags[0].IncludeDestroyChartNote);
        }

        [TestMethod]
        public void GreerCriticalContextRules_CaudaInFirst_IncludesDestroyNote()
        {
            var flags = GreerCriticalContextRules.Evaluate(true, 1, "Cauda Draconis", "Puer");

            Assert.AreEqual(1, flags.Count);
            Assert.AreEqual("cauda-first", flags[0].Id);
            Assert.IsTrue(flags[0].IncludeDestroyChartNote);
        }

        [TestMethod]
        public void GreerCriticalContextRules_PopulusAndRubeus_NoDestroyNote()
        {
            var onFirst = GreerCriticalContextRules.Evaluate(true, 1, "Populus", "Rubeus");
            var onEleventh = GreerCriticalContextRules.Evaluate(true, 11, "Populus", "Rubeus");

            Assert.AreEqual(1, onFirst.Count);
            Assert.AreEqual("populus-first-rubeus-eleventh", onFirst[0].Id);
            Assert.IsFalse(onFirst[0].IncludeDestroyChartNote);
            Assert.AreEqual(1, onEleventh.Count);
            Assert.IsFalse(onEleventh[0].IncludeDestroyChartNote);
        }

        [TestMethod]
        public void GreerCriticalContextRules_InactiveOrWrongHouse_ReturnsEmpty()
        {
            Assert.AreEqual(0, GreerCriticalContextRules.Evaluate(false, 1, "Rubeus", "Via").Count);
            Assert.AreEqual(0, GreerCriticalContextRules.Evaluate(true, 2, "Rubeus", "Via").Count);
            Assert.AreEqual(0, GreerCriticalContextRules.Evaluate(true, 1, "Puer", "Via").Count);
        }

        [TestMethod]
        public void GreerAlongsideText_TextMatches_CollapsesWhitespaceAndIgnoresCase()
        {
            Assert.IsTrue(GreerAlongsideText.TextMatches("Mars", "mars"));
            Assert.IsTrue(GreerAlongsideText.TextMatches("  Fire   Air  ", "Fire Air"));
            Assert.IsFalse(GreerAlongsideText.TextMatches("Mars", "Venus"));
            Assert.IsFalse(GreerAlongsideText.TextMatches("Mars", null));
            Assert.IsFalse(GreerAlongsideText.TextMatches(null, "Mars"));
            Assert.IsFalse(GreerAlongsideText.TextMatches("", "   "));
        }

        [TestMethod]
        public void GreerAlongsideText_GreerFieldDiffers_MatchVsDiffer()
        {
            Assert.IsFalse(GreerAlongsideText.GreerFieldDiffers("Boy", "boy"));
            Assert.IsTrue(GreerAlongsideText.GreerFieldDiffers("Boy", "Girl"));
            Assert.IsTrue(GreerAlongsideText.GreerFieldDiffers("", "Boy"));
            Assert.IsFalse(GreerAlongsideText.GreerFieldDiffers("Boy", ""));
        }
    }
}
