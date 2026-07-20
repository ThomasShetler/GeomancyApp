using System;
using System.Collections.Generic;
using System.Linq;
using GeomancyAPI.Services;
using GeomancyApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeomancyUnitTesting
{
    [TestClass]
    public class HouseDirectoryContentTests
    {
        private static readonly string[] BannedInHousePhrases =
        {
            "intimacy",
            "joint resource",
            "psychological transformation",
            "scorpio"
        };

        [TestMethod]
        public void HouseDirectoryLoader_AllTwelveHouses_HaveReadWhenAndClassicalNote()
        {
            var houses = HouseDirectoryLoader.GetHouses();

            Assert.AreEqual(12, houses.Count);
            foreach (var house in houses.OrderBy(h => h.Id))
            {
                Assert.IsTrue(house.ReadWhen?.Count > 0, $"House {house.Id} missing read_when.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(house.ClassicalNote), $"House {house.Id} missing classical_note.");
            }
        }

        [TestMethod]
        public void HouseDirectoryLoader_AllTwelveHouses_ClassicalFieldsHaveNoEmDash()
        {
            foreach (var house in HouseDirectoryLoader.GetHouses())
            {
                AssertNoEmDash(house.ClassicalNote, $"House {house.Id} classical_note");
                AssertNoEmDash(house.InterpretiveEssence, $"House {house.Id} interpretive_essence");
                foreach (var rw in house.ReadWhen ?? new List<string>())
                {
                    AssertNoEmDash(rw, $"House {house.Id} read_when");
                }
            }
        }

        [TestMethod]
        public void AllFigures_InHouses_HaveTwelveKeys()
        {
            foreach (var figure in FigureData.GetAllFigures())
            {
                Assert.IsNotNull(figure.InHouses, $"{figure.Name} missing in_houses.");
                Assert.AreEqual(12, figure.InHouses.Count, $"{figure.Name} should have 12 in_houses keys.");

                for (var house = 1; house <= 12; house++)
                {
                    Assert.IsTrue(
                        figure.InHouses.ContainsKey(house.ToString()),
                        $"{figure.Name} missing in_houses key {house}.");
                    Assert.IsFalse(
                        string.IsNullOrWhiteSpace(figure.InHouses[house.ToString()]),
                        $"{figure.Name} in_houses[{house}] is empty.");
                }
            }
        }

        [TestMethod]
        public void AllFigures_InHouses_EighthHouseAvoidsModernDrift()
        {
            foreach (var figure in FigureData.GetAllFigures())
            {
                var eighth = figure.InHouses["8"];
                foreach (var phrase in BannedInHousePhrases)
                {
                    Assert.IsTrue(
                        eighth.IndexOf(phrase, StringComparison.OrdinalIgnoreCase) < 0,
                        $"{figure.Name} in_houses[8] contains banned phrase '{phrase}': {eighth}");
                }

                AssertNoEmDash(eighth, $"{figure.Name} in_houses[8]");
            }
        }

        [TestMethod]
        public void AllFigures_InCourtRoles_HaveFourKeys()
        {
            foreach (var figure in FigureData.GetAllFigures())
            {
                Assert.IsNotNull(figure.InCourtRoles, $"{figure.Name} missing in_court_roles.");
                Assert.AreEqual(4, figure.InCourtRoles.Count, $"{figure.Name} should have 4 in_court_roles keys.");
            }
        }

        [TestMethod]
        public void GreerAlongsideText_DoesNotBreakOnHouseDirectoryStrings()
        {
            var first = HouseDirectoryLoader.GetHouse(1);
            var sample = first.ReadWhen[0];

            Assert.IsFalse(GreerAlongsideText.GreerFieldDiffers(sample, sample));
            Assert.IsTrue(GreerAlongsideText.GreerFieldDiffers(sample, first.ClassicalNote));
            Assert.IsFalse(string.IsNullOrWhiteSpace(first.ClassicalNote));
        }

        private static void AssertNoEmDash(string value, string context)
        {
            if (string.IsNullOrEmpty(value)) return;
            Assert.IsFalse(value.Contains("—"), $"{context} contains em dash.");
            Assert.IsFalse(value.Contains("–"), $"{context} contains en dash.");
        }
    }
}
