using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GeomancyApp;
using System.Collections.Generic;
using System.Linq;

namespace GeomancyUnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void DebugChartTest()
        {
            // Create a simple chart to debug
            var chart = new HouseChart();
            chart.SetFirstFourHousesAndCalculate(
                new GeomanticFigure("Via", 1),
                new GeomanticFigure("Populus", 2),
                new GeomanticFigure("Conjunctio", 3),
                new GeomanticFigure("Carcer", 4)
            );
            
            // Print the chart summary to see what figures are where
            var summary = chart.GetChartSummary();
            System.Diagnostics.Debug.WriteLine(summary);
            
            // Print all house figures for manual verification
            foreach (var h in Enumerable.Range(1, 12))
                System.Diagnostics.Debug.WriteLine($"House {h}: {chart.GetHouseFigure(h).Name}");
            
            // Check what's in houses 1 and 3
            var house1 = chart.GetHouseFigure(1);
            var house3 = chart.GetHouseFigure(3);
            
            System.Diagnostics.Debug.WriteLine($"House 1: {house1?.Name}");
            System.Diagnostics.Debug.WriteLine($"House 3: {house3?.Name}");
            
            // Test the perfection
            var result = PerfectionCalculator.Find(chart, 1, 3);
            System.Diagnostics.Debug.WriteLine($"Perfection result: {result.Mode}");
            System.Diagnostics.Debug.WriteLine($"Notes: {string.Join(", ", result.Notes)}");
            
            // Check if houses 1 and 3 have the same figure (they shouldn't)
            Assert.IsFalse(house1?.Name == house3?.Name, "Houses 1 and 3 should not have the same figure");
        }

        [TestMethod]
        public void TestConjunctionPerfection()
        {
            // Test Conjunction Perfection - manually set same figure in two houses
            var chart = new HouseChart();
            chart.SetFirstFourHousesAndCalculate(
                new GeomanticFigure("Via", 1),
                new GeomanticFigure("Populus", 2),
                new GeomanticFigure("Conjunctio", 3),
                new GeomanticFigure("Carcer", 4)
            );
            
            // Manually set house 5 to have the same figure as house 1 for conjunction test
            chart.SetHouseFigure(5, "Via");
            
            var result = PerfectionCalculator.Find(chart, 1, 5);
            Assert.AreEqual(PerfectionType.Conjunction, result.Mode);
        }

        [TestMethod]
        public void TestAspectPerfection()
        {
            // Aspect perfection – pick a true sextile:
            var chart = new HouseChart();
            chart.SetFirstFourHousesAndCalculate(
                new GeomanticFigure("Via", 1),
                new GeomanticFigure("Populus", 2),
                new GeomanticFigure("Conjunctio", 3),
                new GeomanticFigure("Carcer", 4)
            );
            var result = PerfectionCalculator.Find(chart, 1, 3); // Via vs Conjunctio
            Assert.AreEqual(PerfectionType.Aspect, result.Mode);
        }

        [TestMethod]
        public void TestMutationPerfection()
        {
            // Mutation – pick two *different* mobile figures in square:
            var chart = new HouseChart();
            chart.SetFirstFourHousesAndCalculate(
                new GeomanticFigure("Via", 1),
                new GeomanticFigure("Populus", 2),
                new GeomanticFigure("Conjunctio", 3),
                new GeomanticFigure("Carcer", 4)
            );
            var result = PerfectionCalculator.Find(chart, 3, 6); // Conjunctio vs Amissio
            Assert.AreEqual(PerfectionType.Mutation, result.Mode);
        }

        [TestMethod]
        public void TestTranslationPerfection()
        {
            // Translation – expect None for 1 ↔ 12 in this spread:
            var chart = new HouseChart();
            chart.SetFirstFourHousesAndCalculate(
                new GeomanticFigure("Via", 1),
                new GeomanticFigure("Populus", 2),
                new GeomanticFigure("Conjunctio", 3),
                new GeomanticFigure("Carcer", 4)
            );
            var result = PerfectionCalculator.Find(chart, 1, 12);
            Assert.AreEqual(PerfectionType.None, result.Mode);
        }

        [TestMethod]
        public void TestNoPerfection()
        {
            // Test No Perfection
            var chart = new HouseChart();
            chart.SetFirstFourHousesAndCalculate(
                new GeomanticFigure("Via", 1),
                new GeomanticFigure("Populus", 2),
                new GeomanticFigure("Conjunctio", 3),
                new GeomanticFigure("Carcer", 4)
            );
            var result = PerfectionCalculator.Find(chart, 1, 6);
            Assert.AreEqual(PerfectionType.None, result.Mode);
        }
    }
}
