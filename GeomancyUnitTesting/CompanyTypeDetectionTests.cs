using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeomancyApp;

namespace GeomancyUnitTesting
{
    [TestClass]
    public class CompanyTypeDetectionTests
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
        public void DemiSimple_AlbusWithConjunctio_SameMercury()
        {
            // Greer Table 6-2: Albus & Conjunctio (Mercury)
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Albus");
            chart.SetHouseFigure(2, "Conjunctio");
            chart.SetHouseFigure(6, "Populus"); // H2 sinister trine to H6

            var company = PerfectionCalculator.Find(chart, 1, 6, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Company);

            Assert.IsNotNull(company, "Expected company-mediated perfection via H2.");
            Assert.AreEqual(CompanyType.DemiSimple, company.CompanyType);
        }

        [TestMethod]
        public void Compound_PuerWithPuella_OppositePair()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(5, "Puer");
            chart.SetHouseFigure(6, "Puella");
            // Companion H6 casts to querent H1: opposition
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Carcer"); // avoid Q company

            var company = PerfectionCalculator.Find(chart, 1, 5, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Company
                    && r.CompanyType == CompanyType.Compound);

            Assert.IsNotNull(company, "Expected Compound company (Puer/Puella) on quesited side.");
        }

        [TestMethod]
        public void DemiSimple_CaputWithAcquisitio_NodeJupiterGroup()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Caput Draconis");
            chart.SetHouseFigure(2, "Acquisitio");
            chart.SetHouseFigure(6, "Populus");

            var company = PerfectionCalculator.Find(chart, 1, 6, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Company);

            Assert.IsNotNull(company);
            Assert.AreEqual(CompanyType.DemiSimple, company.CompanyType);
        }

        [TestMethod]
        public void Capitular_SameFireLine_NotStrongerTypes()
        {
            // Fortuna Major (Sun, Passive Fire) + Albus (Mercury, Passive Fire):
            // same HeadLine, different planets, not a Table 6-2 opposite pair.
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Fortuna Major");
            chart.SetHouseFigure(2, "Albus");
            var f1 = chart.GetHouseFigure(1);
            var f2 = chart.GetHouseFigure(2);
            Assert.IsNotNull(f1);
            Assert.IsNotNull(f2);
            Assert.AreEqual(f1.HeadLine, f2.HeadLine, "Test setup requires matching Fire lines.");
            Assert.IsFalse(string.Equals(f1.Planet, f2.Planet, StringComparison.OrdinalIgnoreCase),
                "Test setup requires different planets so Demi-Simple does not apply.");

            chart.SetHouseFigure(6, "Populus");

            var company = PerfectionCalculator.Find(chart, 1, 6, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Company);

            Assert.IsNotNull(company);
            Assert.AreEqual(CompanyType.Capitular, company.CompanyType,
                "When only Fire lines match, company must be Capitular.");
        }

        [TestMethod]
        public void Simple_IdenticalFigures()
        {
            var chart = ChartWithUniqueFigures();
            chart.SetHouseFigure(1, "Via");
            chart.SetHouseFigure(2, "Via");
            chart.SetHouseFigure(6, "Populus");

            var company = PerfectionCalculator.Find(chart, 1, 6, returnAllModes: true)
                .FirstOrDefault(r => r.Mode == PerfectionType.Company);

            Assert.IsNotNull(company);
            Assert.AreEqual(CompanyType.Simple, company.CompanyType);
        }

        [TestMethod]
        public void Compound_AllNonSharedPlanetOppositePairs_DetectAsCompound()
        {
            // Opposite pairs that do NOT share a planet (otherwise Demi-Simple wins by priority).
            var pairs = new[]
            {
                ("Puer", "Puella"),
                ("Amissio", "Acquisitio"),
                ("Albus", "Rubeus"),
                ("Conjunctio", "Carcer"),
                ("Tristitia", "Laetitia"),
                ("Cauda Draconis", "Caput Draconis"),
            };

            foreach (var (left, right) in pairs)
            {
                var chart = ChartWithUniqueFigures();
                chart.SetHouseFigure(1, left);
                chart.SetHouseFigure(2, right);
                chart.SetHouseFigure(6, "Populus");
                // Keep quesited pair from also forming company that could confuse the assert.
                chart.SetHouseFigure(5, "Fortuna Major");

                var company = PerfectionCalculator.Find(chart, 1, 6, returnAllModes: true)
                    .FirstOrDefault(r => r.Mode == PerfectionType.Company
                        && r.CompanyType == CompanyType.Compound);

                Assert.IsNotNull(company, $"Expected Compound for {left}/{right} on querent pair H1–H2.");
            }
        }

        [TestMethod]
        public void OppositeSamePlanetPairs_PreferDemiSimpleOverCompound()
        {
            // Greer lists these as opposites AND same-planet; engine priority is Demi first.
            foreach (var (left, right) in new[] { ("Populus", "Via"), ("Fortuna Major", "Fortuna Minor") })
            {
                var chart = ChartWithUniqueFigures();
                chart.SetHouseFigure(1, left);
                chart.SetHouseFigure(2, right);
                chart.SetHouseFigure(6, "Puer");
                chart.SetHouseFigure(5, "Albus");

                var company = PerfectionCalculator.Find(chart, 1, 6, returnAllModes: true)
                    .FirstOrDefault(r => r.Mode == PerfectionType.Company
                        && (r.PathFromHouse == 2 || r.AspectFromHouse == 2
                            || (r.Notes != null && r.Notes.Exists(n => n.Contains("house 2")))));

                Assert.IsNotNull(company, $"Expected company for {left}/{right}.");
                Assert.AreEqual(CompanyType.DemiSimple, company.CompanyType,
                    $"{left}/{right} share a planet, so Demi-Simple should win over Compound.");
            }
        }
    }
}
