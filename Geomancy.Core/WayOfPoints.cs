using System;
using System.Collections.Generic;
using System.Linq;

namespace GeomancyApp
{
    /// <summary>
    /// Enum for line types used in way of points calculations
    /// </summary>
    public enum LineType
    {
        HeadLine,    // Fire - Via Puncti Ignis
        NeckLine,   // Air - Via Puncti Aeris
        BodyLine,    // Water - Via Puncti Aquae
        FootLine     // Earth - Via Puncti Terrae
    }

    /// <summary>
    /// Main class for calculating the way of points through the geomantic chart
    /// </summary>
    public static class WayOfPoints
    {
        // Tree structure mapping:
        // Row 4 (bottom): Judge (index 1)
        // Row 3: Right Witness (index 2), Left Witness (index 3)
        // Row 2: Houses 9-12 (Nieces) - indices 4, 5, 6, 7
        // Row 1 (top): Houses 1-8 (Mothers + Daughters) - indices 8-15

        // Branch relationships:
        // Index 1 → branches to indices 2, 3
        // Index 2 → branches to indices 4, 5
        // Index 3 → branches to indices 6, 7
        // Index 4 → branches to indices 8, 9
        // Index 5 → branches to indices 10, 11
        // Index 6 → branches to indices 12, 13
        // Index 7 → branches to indices 14, 15

        // House to Index mapping:
        // Houses 1-4 (Mothers): indices 8, 9, 10, 11
        // Houses 5-8 (Daughters): indices 12, 13, 14, 15
        // Houses 9-12 (Nieces): indices 4, 5, 6, 7
        // Right Witness: index 2
        // Left Witness: index 3
        // Judge: index 1

        /// <summary>
        /// Maps house number to tree index
        /// </summary>
        private static int GetFigureIndex(int houseNumber)
        {
            switch (houseNumber)
            {
                case 0: return 1;  // Judge
                case 13: return 2; // Right Witness
                case 14: return 3; // Left Witness
                case 9: return 4;
                case 10: return 5;
                case 11: return 6;
                case 12: return 7;
                case 1: return 8;
                case 2: return 9;
                case 3: return 10;
                case 4: return 11;
                case 5: return 12;
                case 6: return 13;
                case 7: return 14;
                case 8: return 15;
                default: return 0;
            }
        }

        /// <summary>
        /// Maps tree index back to house number
        /// </summary>
        private static int GetHouseFromIndex(int index)
        {
            switch (index)
            {
                case 1: return 0;  // Judge
                case 2: return 13; // Right Witness
                case 3: return 14; // Left Witness
                case 4: return 9;
                case 5: return 10;
                case 6: return 11;
                case 7: return 12;
                case 8: return 1;
                case 9: return 2;
                case 10: return 3;
                case 11: return 4;
                case 12: return 5;
                case 13: return 6;
                case 14: return 7;
                case 15: return 8;
                default: return 0;
            }
        }

        /// <summary>
        /// Gets the branches from a given index
        /// </summary>
        private static List<int> GetBranches(int index)
        {
            switch (index)
            {
                case 1: return new List<int> { 2, 3 };      // Judge → Witnesses
                case 2: return new List<int> { 4, 5 };      // Right Witness → Houses 9, 10
                case 3: return new List<int> { 6, 7 };      // Left Witness → Houses 11, 12
                case 4: return new List<int> { 8, 9 };      // House 9 → Houses 1, 2
                case 5: return new List<int> { 10, 11 };     // House 10 → Houses 3, 4
                case 6: return new List<int> { 12, 13 };     // House 11 → Houses 5, 6
                case 7: return new List<int> { 14, 15 };     // House 12 → Houses 7, 8
                default: return new List<int>();
            }
        }

        /// <summary>
        /// Gets the row number for a given index (1 = houses 1-8, 2 = houses 9-12, 3 = witnesses, 4 = judge)
        /// </summary>
        private static int GetRowForIndex(int index)
        {
            if (index == 1) return 4;  // Judge
            if (index >= 2 && index <= 3) return 3;  // Witnesses
            if (index >= 4 && index <= 7) return 2;  // Nieces (Houses 9-12)
            if (index >= 8 && index <= 15) return 1; // Mothers/Daughters (Houses 1-8)
            return 0;
        }

        /// <summary>
        /// Gets the appropriate line value from a figure based on line type
        /// </summary>
        private static int GetLineValue(GeomanticFigure figure, LineType lineType)
        {
            if (figure == null) return 0;

            switch (lineType)
            {
                case LineType.HeadLine:
                    return figure.HeadLine;
                case LineType.NeckLine:
                    return figure.NeckLine;
                case LineType.BodyLine:
                    return figure.BodyLine;
                case LineType.FootLine:
                    return figure.FootLine;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gets a figure from the chart by index
        /// </summary>
        private static GeomanticFigure GetFigureByIndex(HouseChart chart, int index)
        {
            int houseNumber = GetHouseFromIndex(index);
            
            if (houseNumber == 0) return chart.Judge;
            if (houseNumber == 13) return chart.RightWitness;
            if (houseNumber == 14) return chart.LeftWitness;
            if (houseNumber >= 1 && houseNumber <= 12)
                return chart.GetHouseFigure(houseNumber);
            
            return null;
        }

        /// <summary>
        /// Gets the way name for a line type
        /// </summary>
        private static string GetWayName(LineType lineType)
        {
            switch (lineType)
            {
                case LineType.HeadLine:
                    return "Via Puncti Ignis";
                case LineType.NeckLine:
                    return "Via Puncti Aeris";
                case LineType.BodyLine:
                    return "Via Puncti Aquae";
                case LineType.FootLine:
                    return "Via Puncti Terrae";
                default:
                    return "Unknown";
            }
        }

        /// <summary>
        /// Gets the line type name
        /// </summary>
        private static string GetLineTypeName(LineType lineType)
        {
            switch (lineType)
            {
                case LineType.HeadLine:
                    return "HeadLine (Fire)";
                case LineType.NeckLine:
                    return "NeckLine (Air)";
                case LineType.BodyLine:
                    return "BodyLine (Water)";
                case LineType.FootLine:
                    return "FootLine (Earth)";
                default:
                    return "Unknown";
            }
        }

        /// <summary>
        /// Traces all valid paths through the tree for a given line type
        /// </summary>
        private static List<WayOfPointsPath> TracePaths(HouseChart chart, LineType lineType)
        {
            var paths = new List<WayOfPointsPath>();

            // Check if chart is complete
            if (chart.Judge == null || chart.RightWitness == null || chart.LeftWitness == null)
            {
                return paths;
            }

            // Get Judge's line value
            int judgeValue = GetLineValue(chart.Judge, lineType);
            if (judgeValue == 0) return paths;

            // Check if Judge matches either Right Witness or Left Witness (or both)
            int rightWitnessValue = GetLineValue(chart.RightWitness, lineType);
            int leftWitnessValue = GetLineValue(chart.LeftWitness, lineType);

            bool rightMatches = judgeValue == rightWitnessValue;
            bool leftMatches = judgeValue == leftWitnessValue;

            // If neither witness matches, way cannot be established
            if (!rightMatches && !leftMatches)
            {
                return paths;
            }

            // Start tracing from Judge (index 1)
            // The recursive function will automatically trace through whichever witness branch(es) match
            var currentPath = new List<int> { 1 }; // Start with Judge (index 1)
            TracePathsRecursive(chart, lineType, judgeValue, 1, currentPath, paths);

            return paths;
        }

        /// <summary>
        /// Recursively traces paths through the tree
        /// </summary>
        private static void TracePathsRecursive(HouseChart chart, LineType lineType, int targetValue, 
            int currentIndex, List<int> currentPath, List<WayOfPointsPath> allPaths)
        {
            // Get branches from current index
            var branches = GetBranches(currentIndex);
            
            if (branches.Count == 0)
            {
                // We've reached a leaf node - this is a complete path
                // Only add if we reached row 1 (houses 1-8) or row 2 (houses 9-12)
                int row = GetRowForIndex(currentIndex);
                if (row == 1 || row == 2)
                {
                    var path = new WayOfPointsPath();
                    // Convert indices to house numbers for the path
                    // The path includes: Judge → Witness → Niece → House
                    // Note: currentPath already includes currentIndex, so we process all indices
                    foreach (var idx in currentPath)
                    {
                        int houseNum = GetHouseFromIndex(idx);
                        // Judge is index 1, represented as house 0, we'll skip it
                        // and just track the chain: Witness → Niece → Endpoint
                        if (idx == 1) continue; // Skip Judge in the path list
                        if (houseNum > 0)
                            path.Houses.Add(houseNum);
                    }
                    // The endpoint is already in currentPath, so we just need to set it
                    int endpointHouse = GetHouseFromIndex(currentIndex);
                    if (endpointHouse > 0)
                    {
                        // Make sure endpoint is in the list (it should be from currentPath)
                        if (!path.Houses.Contains(endpointHouse))
                            path.Houses.Add(endpointHouse);
                        path.EndpointHouse = endpointHouse;
                        path.RowReached = row;
                        allPaths.Add(path);
                    }
                }
                return;
            }

            // Check each branch
            foreach (var branchIndex in branches)
            {
                var figure = GetFigureByIndex(chart, branchIndex);
                if (figure == null) continue;

                int branchValue = GetLineValue(figure, lineType);
                
                // If the branch matches the target value, continue tracing
                if (branchValue == targetValue)
                {
                    var newPath = new List<int>(currentPath) { branchIndex };
                    TracePathsRecursive(chart, lineType, targetValue, branchIndex, newPath, allPaths);
                }
            }
        }

        /// <summary>
        /// Classifies paths as strong, passive, strong passive, or weaker passive
        /// </summary>
        private static void ClassifyPaths(WayOfPointsResult result, List<WayOfPointsPath> paths)
        {
            if (paths.Count == 0)
            {
                result.CanBeEstablished = false;
                result.Notes.Add("Way cannot be established - no valid paths found.");
                return;
            }

            result.CanBeEstablished = true;

            // If 1-2 paths, all are strong
            if (paths.Count <= 2)
            {
                result.StrongPaths = paths.ToList();
                result.Notes.Add($"Found {paths.Count} valid path(s) - all are strong.");
                foreach (var path in paths)
                {
                    path.PathType = WayOfPointsPathType.Strong;
                    path.Description = $"Strong path to House {path.EndpointHouse}";
                    result.Notes.Add($"  - Strong path to House {path.EndpointHouse} (Row {path.RowReached})");
                }
                return;
            }

            // 3+ paths - need to classify as passive
            var row1Paths = paths.Where(p => p.RowReached == 1).ToList();
            var row2Paths = paths.Where(p => p.RowReached == 2).ToList();

            // If all paths reach the same row, they're all just "passive"
            if (row1Paths.Count == 0 || row2Paths.Count == 0)
            {
                result.PassivePaths = paths.ToList();
                result.Notes.Add($"Found {paths.Count} valid paths - all reach Row {(row1Paths.Count > 0 ? 1 : 2)}, all are passive.");
                foreach (var path in paths)
                {
                    path.PathType = WayOfPointsPathType.Passive;
                    path.Description = $"Passive path to House {path.EndpointHouse}";
                    result.Notes.Add($"  - Passive path to House {path.EndpointHouse}");
                }
                return;
            }

            // Mixed rows - classify as strong passive and weaker passive
            // Strong passives: paths that reach row 1 when there are 3+ paths and mixed rows
            // Weaker passives: paths that only reach row 2 when there are 3+ paths and mixed rows
            // But only if 2 or fewer extend to row 1
            if (row1Paths.Count <= 2)
            {
                result.StrongPassivePaths = row1Paths.ToList();
                result.WeakerPassivePaths = row2Paths.ToList();
                
                result.Notes.Add($"Found {paths.Count} valid paths with mixed rows:");
                result.Notes.Add($"  - {row1Paths.Count} path(s) reach Row 1 (strong passives)");
                result.Notes.Add($"  - {row2Paths.Count} path(s) reach Row 2 (weaker passives)");
                
                foreach (var path in row1Paths)
                {
                    path.PathType = WayOfPointsPathType.StrongPassive;
                    path.Description = $"Strong passive path to House {path.EndpointHouse}";
                    result.Notes.Add($"    - Strong passive: House {path.EndpointHouse}");
                }
                
                foreach (var path in row2Paths)
                {
                    path.PathType = WayOfPointsPathType.WeakerPassive;
                    path.Description = $"Weaker passive path to House {path.EndpointHouse}";
                    result.Notes.Add($"    - Weaker passive: House {path.EndpointHouse}");
                }
            }
            else
            {
                // If more than 2 paths reach row 1, they're all just passive
                result.PassivePaths = paths.ToList();
                result.Notes.Add($"Found {paths.Count} valid paths - {row1Paths.Count} reach Row 1, {row2Paths.Count} reach Row 2, all are passive.");
                foreach (var path in paths)
                {
                    path.PathType = WayOfPointsPathType.Passive;
                    path.Description = $"Passive path to House {path.EndpointHouse}";
                    result.Notes.Add($"  - Passive path to House {path.EndpointHouse} (Row {path.RowReached})");
                }
            }
        }

        /// <summary>
        /// Calculates one way of points for a given line type
        /// </summary>
        public static WayOfPointsResult CalculateWay(HouseChart chart, LineType lineType)
        {
            var result = new WayOfPointsResult
            {
                WayName = GetWayName(lineType),
                LineType = GetLineTypeName(lineType)
            };

            // Check if chart is complete
            if (chart.Judge == null || chart.RightWitness == null || chart.LeftWitness == null)
            {
                result.CanBeEstablished = false;
                result.Notes.Add("Chart is not complete - missing Judge or Witnesses.");
                return result;
            }

            // Check if at least one witness matches Judge (way can be established if either matches)
            int judgeValue = GetLineValue(chart.Judge, lineType);
            if (judgeValue == 0)
            {
                result.CanBeEstablished = false;
                result.Notes.Add("Judge's line value is invalid.");
                return result;
            }

            int rightWitnessValue = GetLineValue(chart.RightWitness, lineType);
            int leftWitnessValue = GetLineValue(chart.LeftWitness, lineType);
            bool rightMatches = judgeValue == rightWitnessValue;
            bool leftMatches = judgeValue == leftWitnessValue;

            // Way can be established if at least one witness matches
            result.CanBeEstablished = rightMatches || leftMatches;

            if (!result.CanBeEstablished)
            {
                result.Notes.Add("Way cannot be established - Judge's line value does not match either Witness.");
                return result;
            }

            // Trace all valid paths
            var paths = TracePaths(chart, lineType);
            result.AllPaths = paths;

            // Classify the paths
            ClassifyPaths(result, paths);

            return result;
        }

        /// <summary>
        /// Calculates all four ways of points for a chart
        /// </summary>
        public static WayOfPointsAnalysis CalculateAllWays(HouseChart chart)
        {
            var analysis = new WayOfPointsAnalysis
            {
                FireWay = CalculateWay(chart, LineType.HeadLine),
                AirWay = CalculateWay(chart, LineType.NeckLine),
                WaterWay = CalculateWay(chart, LineType.BodyLine),
                EarthWay = CalculateWay(chart, LineType.FootLine)
            };

            return analysis;
        }
    }
}

