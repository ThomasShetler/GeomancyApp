using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using GeomancyApp;

namespace GeomancyUnitTesting
{
    [TestClass]
    public class AspectEngineTests
    {
        // ------------------------------------------------------------
        //  Helpers
        // ------------------------------------------------------------
        private static HouseChart MakeDummyChart()
        {
            /*  A minimal chart:  we only care about house *numbers*
                because ChartAspectAnalysis never looks at the figure
                roots.  So all 12 houses can carry the same placeholder. */
            var chart = new HouseChart();
            chart.SetFirstFourHousesAndCalculate(
                new GeomanticFigure("Via", 1),
                new GeomanticFigure("Via", 2),
                new GeomanticFigure("Via", 3),
                new GeomanticFigure("Via", 4));
            // (The call above fills all 12 houses automatically.)
            return chart;
        }

        /// re-computes weights by calling GetAspect directly so we have
        /// a rock-solid oracle that is *independent* of Evaluate().
        private static int ExpectedTotal(AspectType min = AspectType.Sextile)
        {
            int total = 0;
            for (int i = 1; i <= 12; i++)
            for (int j = i + 1; j <= 12; j++)
            {
                var a = GeomanticAspects.GetAspect(i, j);
                if ((int)a >= (int)min)
                {
                    switch (a)
                    {
                        case AspectType.Conjunction: total += 5; break;
                        case AspectType.Trine:       total += 4; break;
                        case AspectType.Sextile:     total += 3; break;
                        case AspectType.Square:      total += 2; break;
                        case AspectType.Opposition:  total += 1; break;
                        default: break;
                    }
                }
            }
            return total;
        }

        // ------------------------------------------------------------
        //  Tests
        // ------------------------------------------------------------

        [TestMethod]
        public void AllPairsEnumerated_DefaultMin()
        {
            var chart  = MakeDummyChart();
            var report = ChartAspectAnalysis.Evaluate(chart);   // min = Sextile

            // 1) detail count must be 42 (12×7 / 2, Greer Table 6-1)
            Assert.AreEqual(42, report.Details.Count,
                "Should list 42 dexter aspects for 12 houses");

            // 2) total score must match independent oracle
            int oracle = ExpectedTotal();
            Assert.AreEqual(oracle, report.TotalScore,
                "Total weight mismatch versus manual sum");
        }

        [TestMethod]
        public void MinFilter_TrineOrBetter()
        {
            var chart  = MakeDummyChart();
            var report = ChartAspectAnalysis.Evaluate(chart, AspectType.Trine);

            // only Trine, Opposition, Conjunction should appear
            Assert.IsFalse(report.Details.Any(d => d.Aspect == AspectType.Sextile
                                                || d.Aspect == AspectType.Square),
                "Filter should exclude Sextile & Square");

            int oracle = ExpectedTotal(AspectType.Trine);
            Assert.AreEqual(oracle, report.TotalScore,
                "Total weight with Trine-filter mismatch");
        }

        [TestMethod]
        public void OppositionWeightIsOne()
        {
            var chart = MakeDummyChart();
            var rpt   = ChartAspectAnalysis.Evaluate(chart);

            var opp = rpt.Details.First(l => l.Aspect == AspectType.Opposition);
            Assert.AreEqual(1, opp.Weight, "Opposition should score 1");
        }
    }
} 