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
        public void GreerHouseDirectoryLoader_ReturnsTwelveHousesWithCautions()
        {
            var directory = GreerHouseDirectoryLoader.GetDirectory();

            Assert.AreEqual(12, directory.Houses.Count);
            Assert.IsFalse(string.IsNullOrWhiteSpace(directory.ChartCautions));
            StringAssert.Contains(directory.ChartCautions, "Rubeus");
            Assert.AreEqual(ExpectedAttribution, directory.License.Attribution);
        }

        [TestMethod]
        public void GreerHouseDirectoryLoader_SecondHouse_HasExampleQuestions()
        {
            var house = GreerHouseDirectoryLoader.GetHouse(2);

            Assert.IsNotNull(house);
            Assert.IsTrue(house.ExampleQuestions.Count >= 5);
            StringAssert.Contains(house.Description, "Second house");
        }
    }
}
