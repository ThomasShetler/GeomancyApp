using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using GeomancyApp;

namespace GeomancyUnitTesting
{
    [TestClass]
    public class PolarityBalanceTests
    {
        //-----------------------------------------------------------------
        //  Helpers
        //-----------------------------------------------------------------
        private static HouseChart BuildDummyChart()
        {
            // We only need house numbers—the root figure names don't matter
            var ch = new HouseChart();
            ch.SetFirstFourHousesAndCalculate(
                new GeomanticFigure("Via", 1),
                new GeomanticFigure("Via", 2),
                new GeomanticFigure("Via", 3),
                new GeomanticFigure("Via", 4));
            return ch;
        }

        private static (int easy,int hard) RecountPolarity(AspectType min)
        {
            int easy = 0, hard = 0;

            for (int i = 1; i <= 12; i++)
            for (int j = i + 1; j <= 12; j++)
            {
                var asp = GeomanticAspects.GetAspect(i, j);
                if ((int)asp < (int)min || asp == AspectType.None) continue;

                int w = 0;
                switch (asp)
                {
                    case AspectType.Conjunction: w = 5; break;
                    case AspectType.Trine:       w = 4; break;
                    case AspectType.Sextile:     w = 3; break;
                    case AspectType.Square:      w = 2; break;
                    case AspectType.Opposition:  w = 1; break;
                    default: break;
                }

                if (asp == AspectType.Square || asp == AspectType.Opposition)
                    hard += w;
                else
                    easy += w;
            }
            return (easy, hard);
        }

        //-----------------------------------------------------------------
        //  Test 1 – Default min (Sextile)
        //-----------------------------------------------------------------
        [TestMethod]
        public void PolarityBalances_AddUpToTotal_SextileFilter()
        {
            var chart   = BuildDummyChart();
            var report  = ChartAspectAnalysis.Evaluate(chart);  // min = Sextile
            var oracle  = RecountPolarity(AspectType.Sextile);

            Assert.AreEqual(oracle.easy, report.EasyScore,
                "EasyScore mismatch");
            Assert.AreEqual(oracle.hard, report.HardScore,
                "HardScore mismatch");
            Assert.AreEqual(report.EasyScore + report.HardScore,
                            report.TotalScore,
                "Easy + Hard must equal Total");
        }

        //-----------------------------------------------------------------
        //  Test 2 – Trine-or-better filter
        //-----------------------------------------------------------------
        [TestMethod]
        public void PolarityBalances_AddUpToTotal_TrineFilter()
        {
            var chart   = BuildDummyChart();
            var report  = ChartAspectAnalysis.Evaluate(chart, AspectType.Trine);
            var oracle  = RecountPolarity(AspectType.Trine);

            Assert.AreEqual(oracle.easy, report.EasyScore);
            Assert.AreEqual(oracle.hard, report.HardScore);
            Assert.AreEqual(report.EasyScore + report.HardScore,
                            report.TotalScore);
        }
    }
} 