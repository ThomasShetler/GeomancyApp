using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeomancyApp
{
    public class TestFigureData
    {
        public static void TestAllFigures()
        {
            FigureData figureData = new FigureData();
            string[] figureNames = {
                "Puer", "Amissio", "Albus", "Populus", "Fortuna Major", "Conjunctio", 
                "Puella", "Rubeus", "Acquisitio", "Carcer", "Tristitia", "Laetitia", 
                "Cauda Draconis", "Caput Draconis", "Fortuna Minor", "Via"
            };

            Console.WriteLine("Testing all 16 geomancy figures:");
            Console.WriteLine("=================================");

            foreach (string name in figureNames)
            {
                FigureData fig = figureData.ReturnFigureData(name);
                Console.WriteLine($"\n{fig.Name}:");
                Console.WriteLine($"  Quality: {fig.Quality}");
                Console.WriteLine($"  Keyword: {fig.Keyword}");
                Console.WriteLine($"  Planet: {fig.Planet}");
                Console.WriteLine($"  Sign: {fig.Sign}");
                Console.WriteLine($"  Elements: Fire({fig.FireElement}) Air({fig.AirElement}) Water({fig.WaterElement}) Earth({fig.EarthElement})");
                Console.WriteLine($"  Inner/Outer: {fig.InnerEl}/{fig.OuterEl}");
            }

            Console.WriteLine("\nTest completed successfully!");
        }

        public static void TestNewFigureSystem()
        {
            Console.WriteLine("=== Testing New Figure Data System ===\n");

            // Test 1: Get figure by name
            Console.WriteLine("1. Testing GetFigureByName:");
            var puer = FigureData.GetFigureByName("Puer");
            if (puer != null)
            {
                Console.WriteLine($"Found: {puer.Name}");
                Console.WriteLine($"Elemental Pattern: {puer.FireElement}-{puer.AirElement}-{puer.WaterElement}-{puer.EarthElement}");
                Console.WriteLine($"Keyword: {puer.Keyword}");
            }
            Console.WriteLine();

            // Test 2: Get figure by elemental pattern (bool values)
            Console.WriteLine("2. Testing GetFigureByElementalPattern (bool):");
            var figureByBool = FigureData.GetFigureByElementalPattern(true, true, false, true); // Fire:Active, Air:Active, Water:Passive, Earth:Active
            if (figureByBool != null)
            {
                Console.WriteLine($"Found: {figureByBool.Name}");
                Console.WriteLine($"Pattern: Fire(Active)-Air(Active)-Water(Passive)-Earth(Active)");
            }
            Console.WriteLine();

            // Test 3: Get figure by elemental pattern (integer values)
            Console.WriteLine("3. Testing GetFigureByElementalPattern (int):");
            var figureByInt = FigureData.GetFigureByElementalPattern(1, 1, 2, 1); // 1=Active, 2=Passive
            if (figureByInt != null)
            {
                Console.WriteLine($"Found: {figureByInt.Name}");
                Console.WriteLine($"Pattern: 1-1-2-1 (Fire:Active, Air:Active, Water:Passive, Earth:Active)");
            }
            Console.WriteLine();

            // Test 4: Get all figures
            Console.WriteLine("4. Testing GetAllFigures:");
            var allFigures = FigureData.GetAllFigures();
            Console.WriteLine($"Total figures: {allFigures.Count}");
            Console.WriteLine("First 5 figures:");
            foreach (var figure in allFigures.Take(5))
            {
                Console.WriteLine($"  - {figure.Name}: {figure.Keyword}");
            }
            Console.WriteLine();

            // Test 5: Test GeomanticFigure creation
            Console.WriteLine("5. Testing GeomanticFigure creation:");
            var geomanticFigure = new GeomanticFigure(1, 1, 2, 1); // Puer pattern
            Console.WriteLine($"Created figure: {geomanticFigure.Name}");
            Console.WriteLine($"Elemental pattern: {geomanticFigure.GetElementalPattern()}");
            Console.WriteLine($"House number: {geomanticFigure.HouseNumber}");
            Console.WriteLine();

            // Test 6: Test HouseChart functionality
            Console.WriteLine("6. Testing HouseChart functionality:");
            var chart = new HouseChart();
            
            // Set houses using elemental patterns
            chart.SetHouseFigure(1, 1, 1, 2, 1); // Puer
            chart.SetHouseFigure(2, 1, 2, 1, 2); // Amissio
            chart.SetHouseFigure(3, 2, 2, 1, 2); // Albus
            chart.SetHouseFigure(4, 2, 2, 2, 2); // Populus
            
            // Calculate the chart
            chart.CalculateCompleteChart();
            
            Console.WriteLine("Chart Summary:");
            Console.WriteLine(chart.GetChartSummary());
            Console.WriteLine($"Chart complete: {chart.IsChartComplete()}");
            Console.WriteLine();

            // Test 7: Test figure creation by name
            Console.WriteLine("7. Testing figure creation by name:");
            var figureByName = new GeomanticFigure("Via", 5);
            Console.WriteLine($"Created figure: {figureByName.Name}");
            Console.WriteLine($"Elemental pattern: {figureByName.GetElementalPattern()}");
            Console.WriteLine($"House number: {figureByName.HouseNumber}");
            Console.WriteLine();

            // Test 8: Test elemental pattern conversion
            Console.WriteLine("8. Testing elemental pattern conversion:");
            var boolPattern = figureByName.GetElementalPatternAsBools();
            Console.WriteLine($"Bool pattern: Fire={boolPattern[0]}, Air={boolPattern[1]}, Water={boolPattern[2]}, Earth={boolPattern[3]}");
            Console.WriteLine();

            Console.WriteLine("=== Test Complete ===");
        }

        public static void TestSpecificPatterns()
        {
            Console.WriteLine("=== Testing Specific Elemental Patterns ===\n");

            // Test all 16 possible patterns
            var patterns = new[]
            {
                new { Name = "Puer", Pattern = new int[] { 1, 1, 2, 1 } },
                new { Name = "Amissio", Pattern = new int[] { 1, 2, 1, 2 } },
                new { Name = "Albus", Pattern = new int[] { 2, 2, 1, 2 } },
                new { Name = "Populus", Pattern = new int[] { 2, 2, 2, 2 } },
                new { Name = "Fortuna Major", Pattern = new int[] { 2, 2, 1, 1 } },
                new { Name = "Conjunctio", Pattern = new int[] { 2, 1, 1, 2 } },
                new { Name = "Puella", Pattern = new int[] { 1, 2, 1, 1 } },
                new { Name = "Rubeus", Pattern = new int[] { 2, 1, 2, 2 } },
                new { Name = "Acquisitio", Pattern = new int[] { 2, 1, 2, 1 } },
                new { Name = "Carcer", Pattern = new int[] { 1, 2, 2, 1 } },
                new { Name = "Tristitia", Pattern = new int[] { 2, 2, 2, 1 } },
                new { Name = "Laetitia", Pattern = new int[] { 1, 2, 2, 2 } },
                new { Name = "Cauda Draconis", Pattern = new int[] { 1, 1, 1, 2 } },
                new { Name = "Caput Draconis", Pattern = new int[] { 2, 1, 1, 1 } },
                new { Name = "Fortuna Minor", Pattern = new int[] { 1, 1, 2, 2 } },
                new { Name = "Via", Pattern = new int[] { 1, 1, 1, 1 } }
            };

            foreach (var pattern in patterns)
            {
                var figure = FigureData.GetFigureByElementalPattern(
                    pattern.Pattern[0], 
                    pattern.Pattern[1], 
                    pattern.Pattern[2], 
                    pattern.Pattern[3]
                );
                
                if (figure != null)
                {
                    Console.WriteLine($"{pattern.Name}: {string.Join("-", pattern.Pattern)} -> {figure.Name}");
                }
                else
                {
                    Console.WriteLine($"{pattern.Name}: {string.Join("-", pattern.Pattern)} -> NOT FOUND");
                }
            }
            
            Console.WriteLine("\n=== Pattern Test Complete ===");
        }

        public static void TestGeomancyAPI()
        {
            Console.WriteLine("=== Testing GeomancyAPI ===\n");

            // Test 1: Get figure by name using API
            Console.WriteLine("1. Testing API GetFigureByName:");
            var puerAPI = GeomancyAPI.GetFigureByName("Puer");
            if (puerAPI != null)
            {
                Console.WriteLine($"Found: {puerAPI.Name} - {puerAPI.Keyword}");
            }
            Console.WriteLine();

            // Test 2: Get figure by elemental pattern using API
            Console.WriteLine("2. Testing API GetFigureByElementalPattern:");
            var figureAPI = GeomancyAPI.GetFigureByElementalPattern(1, 1, 2, 1);
            if (figureAPI != null)
            {
                Console.WriteLine($"Found: {figureAPI.Name} - Pattern: 1-1-2-1");
            }
            Console.WriteLine();

            // Test 3: Create figure using API
            Console.WriteLine("3. Testing API CreateFigure:");
            var newFigure = GeomancyAPI.CreateFigure(1, 1, 1, 1, 1); // Via in house 1
            Console.WriteLine($"Created: {newFigure.Name} in House {newFigure.HouseNumber}");
            Console.WriteLine($"Pattern: {GeomancyAPI.GetElementalPattern(newFigure)}");
            Console.WriteLine();

            // Test 4: Create complete chart using API
            Console.WriteLine("4. Testing API CreateCompleteChart:");
            var chart = GeomancyAPI.CreateCompleteChart(
                new int[] { 1, 1, 2, 1 }, // Puer
                new int[] { 1, 2, 1, 2 }, // Amissio
                new int[] { 2, 2, 1, 2 }, // Albus
                new int[] { 2, 2, 2, 2 }  // Populus
            );
            Console.WriteLine("Chart created successfully!");
            Console.WriteLine($"Chart complete: {GeomancyAPI.IsChartComplete(chart)}");
            Console.WriteLine();

            // Test 5: Get chart summary using API
            Console.WriteLine("5. Testing API GetChartSummary:");
            string summary = GeomancyAPI.GetChartSummary(chart);
            Console.WriteLine(summary);
            Console.WriteLine();

            // Test 6: Get all figure names using API
            Console.WriteLine("6. Testing API GetAllFigureNames:");
            string[] allNames = GeomancyAPI.GetAllFigureNames();
            Console.WriteLine($"Total figures: {allNames.Length}");
            Console.WriteLine("First 5 names:");
            foreach (string name in allNames.Take(5))
            {
                Console.WriteLine($"  - {name}");
            }
            Console.WriteLine();

            // Test 7: Search figures by keyword using API
            Console.WriteLine("7. Testing API SearchFiguresByKeyword:");
            var energyFigures = GeomancyAPI.SearchFiguresByKeyword("Energy");
            Console.WriteLine($"Figures with keyword 'Energy': {energyFigures.Count}");
            foreach (var figure in energyFigures)
            {
                Console.WriteLine($"  - {figure.Name}: {figure.Keyword}");
            }
            Console.WriteLine();

            // Test 8: Get figures by quality using API
            Console.WriteLine("8. Testing API GetFiguresByQuality:");
            var mobileFigures = GeomancyAPI.GetFiguresByQuality("Mobile");
            Console.WriteLine($"Mobile figures: {mobileFigures.Count}");
            var stableFigures = GeomancyAPI.GetFiguresByQuality("Stable");
            Console.WriteLine($"Stable figures: {stableFigures.Count}");
            Console.WriteLine();

            // Test 9: Get figures by planet using API
            Console.WriteLine("9. Testing API GetFiguresByPlanet:");
            var marsFigures = GeomancyAPI.GetFiguresByPlanet("Mars");
            Console.WriteLine($"Mars figures: {marsFigures.Count}");
            foreach (var figure in marsFigures)
            {
                Console.WriteLine($"  - {figure.Name}: {figure.Planet}");
            }
            Console.WriteLine();

            // Test 10: Get elemental pattern from name using API
            Console.WriteLine("10. Testing API GetElementalPatternFromName:");
            int[] pattern = GeomancyAPI.GetElementalPatternFromName("Via");
            Console.WriteLine($"Via pattern: {string.Join("-", pattern)}");
            Console.WriteLine();

            Console.WriteLine("=== API Test Complete ===");
        }
    }
} 