using System;
using System.Collections.Generic;
using System.Linq;
using GeomancyAPI.Services;
using GeomancyApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeomancyUnitTesting
{
    /// <summary>
    /// Cross-corpus integrity checks for house, court, and figure slot reference data.
    /// </summary>
    [TestClass]
    public class ReferenceIntegrityTests
    {
        private static readonly string[] BannedEighthHousePhrases =
        {
            "intimacy",
            "joint resource",
            "psychological transformation",
            "scorpio"
        };

        [TestMethod]
        public void CourtDirectory_AllPlacements_HaveReadWhenAndEssence()
        {
            var courts = HouseDirectoryLoader.GetCourts();
            Assert.AreEqual(4, courts.Count);

            foreach (var court in courts)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(court.Essence), $"{court.Placement} missing essence.");
                Assert.IsTrue(court.ReadWhen?.Count > 0, $"{court.Placement} missing read_when.");
                Assert.IsTrue(court.Meaning?.Count > 0, $"{court.Placement} missing meaning.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(court.UtilityInReading), $"{court.Placement} missing utility.");
            }
        }

        [TestMethod]
        public void HouseDirectory_AllTwelveHouses_HaveCompleteReadingCraft()
        {
            foreach (var house in HouseDirectoryLoader.GetHouses())
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(house.InterpretiveEssence), $"House {house.Id} missing essence.");
                Assert.IsTrue(house.Governs?.Count >= 4, $"House {house.Id} governs list too short.");
                Assert.IsTrue(house.CommonMisreadings?.Count >= 2, $"House {house.Id} needs misreadings.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(house.SignificatorOfQuesitedWhen), $"House {house.Id} missing quesited note.");
            }
        }

        [TestMethod]
        public void HouseDirectory_EighthHouse_ClassicalNoteMatchesDerivedHouseLogic()
        {
            var eighth = HouseDirectoryLoader.GetHouse(8);
            var note = eighth.ClassicalNote ?? string.Empty;

            Assert.IsTrue(note.IndexOf("second from the seventh", StringComparison.OrdinalIgnoreCase) >= 0
                || note.IndexOf("2nd from the 7th", StringComparison.OrdinalIgnoreCase) >= 0
                || note.IndexOf("other party", StringComparison.OrdinalIgnoreCase) >= 0,
                "Eighth classical_note should route other's substance / derived-house logic.");
        }

        [TestMethod]
        public void AllFigures_InHouses_AndCourtRoles_NoEmDash()
        {
            foreach (var figure in FigureData.GetAllFigures())
            {
                foreach (var kv in figure.InHouses ?? new Dictionary<string, string>())
                {
                    AssertNoDash(kv.Value, $"{figure.Name} in_houses[{kv.Key}]");
                }

                foreach (var kv in figure.InCourtRoles ?? new Dictionary<string, string>())
                {
                    AssertNoDash(kv.Value, $"{figure.Name} in_court_roles[{kv.Key}]");
                }
            }
        }

        [TestMethod]
        public void AllFigures_InHouses_EighthHouse_AvoidsModernDrift()
        {
            foreach (var figure in FigureData.GetAllFigures())
            {
                var eighth = figure.InHouses["8"];
                foreach (var phrase in BannedEighthHousePhrases)
                {
                    Assert.IsTrue(
                        eighth.IndexOf(phrase, StringComparison.OrdinalIgnoreCase) < 0,
                        $"{figure.Name} in_houses[8] contains banned phrase '{phrase}': {eighth}");
                }
            }
        }

        [TestMethod]
        public void FigureCorpus_AllSixteenFigures_LoadWithSlotText()
        {
            var figures = FigureData.GetAllFigures().ToList();
            Assert.AreEqual(16, figures.Count);

            foreach (var figure in figures)
            {
                Assert.AreEqual(12, figure.InHouses?.Count ?? 0, $"{figure.Name} in_houses count.");
                Assert.AreEqual(4, figure.InCourtRoles?.Count ?? 0, $"{figure.Name} in_court_roles count.");
            }
        }

        private static void AssertNoDash(string value, string context)
        {
            if (string.IsNullOrEmpty(value)) return;
            Assert.IsFalse(value.Contains("—"), $"{context} contains em dash.");
            Assert.IsFalse(value.Contains("–"), $"{context} contains en dash.");
        }
    }
}
