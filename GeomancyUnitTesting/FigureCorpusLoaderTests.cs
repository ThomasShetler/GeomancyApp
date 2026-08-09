using System.Linq;
using GeomancyApp;
using GeomancyAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeomancyUnitTesting
{
    [TestClass]
    public class FigureCorpusLoaderTests
    {
        private static readonly string[] ExpectedFigureNames =
        {
            "Puer", "Amissio", "Albus", "Populus", "Fortuna Major", "Conjunctio",
            "Puella", "Rubeus", "Acquisitio", "Carcer", "Tristitia", "Laetitia",
            "Cauda Draconis", "Caput Draconis", "Fortuna Minor", "Via"
        };

        [TestMethod]
        public void LoadFigures_ReturnsSixteenWithExpectedNames()
        {
            var figures = FigureData.GetAllFigures();

            Assert.AreEqual(16, figures.Count);
            CollectionAssert.AreEqual(ExpectedFigureNames, figures.Select(f => f.Name).ToArray());
        }

        [TestMethod]
        public void GetFigureByName_Puer_HasCorrectElementPattern()
        {
            var puer = FigureData.GetFigureByName("Puer");

            Assert.IsNotNull(puer);
            Assert.AreEqual("Active", puer.FireElement);
            Assert.AreEqual("Active", puer.AirElement);
            Assert.AreEqual("Passive", puer.WaterElement);
            Assert.AreEqual("Active", puer.EarthElement);
            Assert.AreEqual("Heat at the threshold—courage, hurry, and a blade that wants a sheath", puer.Tagline);
        }

        [TestMethod]
        public void GetFigureByElementalPattern_MatchesNameLookup()
        {
            var byPattern = FigureData.GetFigureByElementalPattern(true, true, false, true);
            var byName = FigureData.GetFigureByName("Puer");

            Assert.IsNotNull(byPattern);
            Assert.IsNotNull(byName);
            Assert.AreEqual(byName.Name, byPattern.Name);
        }

        [TestMethod]
        public void Puer_InHouses_HasTwelveKeys()
        {
            var puer = FigureData.GetFigureByName("Puer");

            Assert.IsNotNull(puer);
            Assert.IsNotNull(puer.InHouses);
            Assert.AreEqual(12, puer.InHouses.Count);

            for (var house = 1; house <= 12; house++)
            {
                Assert.IsTrue(puer.InHouses.ContainsKey(house.ToString()), $"Missing house key {house}");
            }
        }

        [TestMethod]
        public void Puer_InCourtRoles_HasFourKeys()
        {
            var puer = FigureData.GetFigureByName("Puer");

            Assert.IsNotNull(puer);
            Assert.IsNotNull(puer.InCourtRoles);
            Assert.IsTrue(puer.InCourtRoles.ContainsKey("RightWitness"));
            Assert.IsTrue(puer.InCourtRoles.ContainsKey("LeftWitness"));
            Assert.IsTrue(puer.InCourtRoles.ContainsKey("Judge"));
            Assert.IsTrue(puer.InCourtRoles.ContainsKey("Reconciler"));
        }

        [TestMethod]
        public void HouseDirectoryLoader_StillLoadsTwelveHouses()
        {
            var houses = HouseDirectoryLoader.GetHouses();

            Assert.AreEqual(12, houses.Count);
            Assert.IsNotNull(houses.FirstOrDefault(h => h.Id == 1));
        }

        [TestMethod]
        public void WayOfPointsDirectoryLoader_StillLoadsElements()
        {
            var elements = WayOfPointsDirectoryLoader.GetElements();

            Assert.AreEqual(4, elements.Count);
            Assert.IsNotNull(elements.FirstOrDefault(e => e.Id == 1));
        }

        [TestMethod]
        public void CompanyTypeDirectoryLoader_LoadsReaderCentricTypes()
        {
            var directory = CompanyTypeDirectoryLoader.GetDirectory();

            Assert.IsNotNull(directory.Overview);
            Assert.AreEqual("CompanyOfHouses", directory.Overview.Id);
            Assert.IsFalse(string.IsNullOrWhiteSpace(directory.Overview.Tagline));

            Assert.AreEqual(4, directory.CompanyTypes.Count);
            Assert.IsNotNull(CompanyTypeDirectoryLoader.GetCompanyType("Simple"));
            Assert.IsNotNull(CompanyTypeDirectoryLoader.GetCompanyType("Demi-Simple"));
            Assert.IsNotNull(CompanyTypeDirectoryLoader.GetCompanyType("DemiSimple"));
            Assert.IsNotNull(CompanyTypeDirectoryLoader.GetCompanyType("Compound"));
            Assert.IsNotNull(CompanyTypeDirectoryLoader.GetCompanyType("Capitular"));

            var demi = CompanyTypeDirectoryLoader.GetCompanyType("DemiSimple");
            Assert.IsFalse(string.IsNullOrWhiteSpace(demi.MechanismSummary));
            Assert.IsTrue(demi.InterpretationParagraphs.Count > 0);
            Assert.IsTrue(demi.Variants.Count >= 3);
        }
    }
}
