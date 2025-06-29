using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeomancyApp
{
    public class GeomanticFigure : FigureData
    {
        public int HeadLine { get; set; }
        public int NeckLine { get; set; }
        public int BodyLine { get; set; }
        public int FootLine { get; set; }
        public int HouseNumber { get; set; }
        public string FigureName { get; set; }

        // Constructor to create figure from elemental pattern
        public GeomanticFigure(int headLine, int neckLine, int bodyLine, int footLine, int houseNumber = 0)
        {
            HeadLine = headLine;
            NeckLine = neckLine;
            BodyLine = bodyLine;
            FootLine = footLine;
            HouseNumber = houseNumber;
            
            // Get the figure data based on the elemental pattern
            var figureData = FigureData.GetFigureByElementalPattern(headLine, neckLine, bodyLine, footLine);
            if (figureData != null)
            {
                // Copy all properties from the found figure
                Name = figureData.Name;
                OtherNames = figureData.OtherNames;
                Quality = figureData.Quality;
                Keyword = figureData.Keyword;
                Imagery = figureData.Imagery;
                StrongHouse = figureData.StrongHouse;
                WeakHouse = figureData.WeakHouse;
                Planet = figureData.Planet;
                Sign = figureData.Sign;
                InnerEl = figureData.InnerEl;
                OuterEl = figureData.OuterEl;
                FireElement = figureData.FireElement;
                AirElement = figureData.AirElement;
                WaterElement = figureData.WaterElement;
                EarthElement = figureData.EarthElement;
                Anatomy = figureData.Anatomy;
                BodyType = figureData.BodyType;
                CharacterType = figureData.CharacterType;
                Colors = figureData.Colors;
                Commentary = figureData.Commentary;
                DivinatoryMeaning = figureData.DivinatoryMeaning;
                FigureName = figureData.Name;
            }
        }

        // Constructor to create figure from name
        public GeomanticFigure(string figureName, int houseNumber = 0)
        {
            FigureName = figureName;
            HouseNumber = houseNumber;
            
            var figureData = FigureData.GetFigureByName(figureName);
            if (figureData != null)
            {
                // Copy all properties from the found figure
                Name = figureData.Name;
                OtherNames = figureData.OtherNames;
                Quality = figureData.Quality;
                Keyword = figureData.Keyword;
                Imagery = figureData.Imagery;
                StrongHouse = figureData.StrongHouse;
                WeakHouse = figureData.WeakHouse;
                Planet = figureData.Planet;
                Sign = figureData.Sign;
                InnerEl = figureData.InnerEl;
                OuterEl = figureData.OuterEl;
                FireElement = figureData.FireElement;
                AirElement = figureData.AirElement;
                WaterElement = figureData.WaterElement;
                EarthElement = figureData.EarthElement;
                Anatomy = figureData.Anatomy;
                BodyType = figureData.BodyType;
                CharacterType = figureData.CharacterType;
                Colors = figureData.Colors;
                Commentary = figureData.Commentary;
                DivinatoryMeaning = figureData.DivinatoryMeaning;
                
                // Set the elemental lines based on the figure's pattern
                SetElementalLinesFromFigure(figureData);
            }
        }

        private void SetElementalLinesFromFigure(FigureData figureData)
        {
            // Convert the elemental states to line values
            HeadLine = figureData.FireElement.Equals("Active", StringComparison.OrdinalIgnoreCase) ? 1 : 2;
            NeckLine = figureData.AirElement.Equals("Active", StringComparison.OrdinalIgnoreCase) ? 1 : 2;
            BodyLine = figureData.WaterElement.Equals("Active", StringComparison.OrdinalIgnoreCase) ? 1 : 2;
            FootLine = figureData.EarthElement.Equals("Active", StringComparison.OrdinalIgnoreCase) ? 1 : 2;
        }

        // Get elemental pattern as string for display
        public string GetElementalPattern()
        {
            return $"{HeadLine}-{NeckLine}-{BodyLine}-{FootLine}";
        }

        // Get elemental pattern as bool array
        public bool[] GetElementalPatternAsBools()
        {
            return new bool[] 
            {
                HeadLine == 1,    // Fire
                NeckLine == 1,    // Air
                BodyLine == 1,    // Water
                FootLine == 1     // Earth
            };
        }
    }

    public class HouseChart
    {
        public GeomanticFigure FirstHouse { get; set; }
        public GeomanticFigure SecondHouse { get; set; }
        public GeomanticFigure ThirdHouse { get; set; }
        public GeomanticFigure FourthHouse { get; set; }
        public GeomanticFigure FifthHouse { get; set; }
        public GeomanticFigure SixthHouse { get; set; }
        public GeomanticFigure SeventhHouse { get; set; }
        public GeomanticFigure EighthHouse { get; set; }
        public GeomanticFigure NinthHouse { get; set; }
        public GeomanticFigure TenthHouse { get; set; }
        public GeomanticFigure EleventhHouse { get; set; }
        public GeomanticFigure TwelfthHouse { get; set; }

        public GeomanticFigure RightWitness { get; set; }
        public GeomanticFigure LeftWitness { get; set; }
        public GeomanticFigure Judge { get; set; }
        public GeomanticFigure Sentence { get; set; }

        // Constructor
        public HouseChart()
        {
            // Initialize all houses as null
        }

        // Method to set a house figure by elemental pattern
        public void SetHouseFigure(int houseNumber, int headLine, int neckLine, int bodyLine, int footLine)
        {
            var figure = new GeomanticFigure(headLine, neckLine, bodyLine, footLine, houseNumber);
            SetHouseFigure(houseNumber, figure);
        }

        // Method to set a house figure by name
        public void SetHouseFigure(int houseNumber, string figureName)
        {
            var figure = new GeomanticFigure(figureName, houseNumber);
            SetHouseFigure(houseNumber, figure);
        }

        // Method to set a house figure by GeomanticFigure object
        public void SetHouseFigure(int houseNumber, GeomanticFigure figure)
        {
            switch (houseNumber)
            {
                case 1: FirstHouse = figure; break;
                case 2: SecondHouse = figure; break;
                case 3: ThirdHouse = figure; break;
                case 4: FourthHouse = figure; break;
                case 5: FifthHouse = figure; break;
                case 6: SixthHouse = figure; break;
                case 7: SeventhHouse = figure; break;
                case 8: EighthHouse = figure; break;
                case 9: NinthHouse = figure; break;
                case 10: TenthHouse = figure; break;
                case 11: EleventhHouse = figure; break;
                case 12: TwelfthHouse = figure; break;
            }
        }

        // Method to get a house figure
        public GeomanticFigure GetHouseFigure(int houseNumber)
        {
            switch (houseNumber)
            {
                case 1: return FirstHouse;
                case 2: return SecondHouse;
                case 3: return ThirdHouse;
                case 4: return FourthHouse;
                case 5: return FifthHouse;
                case 6: return SixthHouse;
                case 7: return SeventhHouse;
                case 8: return EighthHouse;
                case 9: return NinthHouse;
                case 10: return TenthHouse;
                case 11: return EleventhHouse;
                case 12: return TwelfthHouse;
                default: return null;
            }
        }

        // Method to set the first 4 houses and calculate the complete chart
        public void SetFirstFourHousesAndCalculate(GeomanticFigure house1, GeomanticFigure house2, GeomanticFigure house3, GeomanticFigure house4)
        {
            FirstHouse = house1;
            SecondHouse = house2;
            ThirdHouse = house3;
            FourthHouse = house4;
            
            CalculateCompleteChartFromFirstFour();
        }

        // Method to set the first 4 houses by elemental patterns and calculate the complete chart
        public void SetFirstFourHousesAndCalculate(int h1Head, int h1Neck, int h1Body, int h1Foot,
                                                  int h2Head, int h2Neck, int h2Body, int h2Foot,
                                                  int h3Head, int h3Neck, int h3Body, int h3Foot,
                                                  int h4Head, int h4Neck, int h4Body, int h4Foot)
        {
            FirstHouse = new GeomanticFigure(h1Head, h1Neck, h1Body, h1Foot, 1);
            SecondHouse = new GeomanticFigure(h2Head, h2Neck, h2Body, h2Foot, 2);
            ThirdHouse = new GeomanticFigure(h3Head, h3Neck, h3Body, h3Foot, 3);
            FourthHouse = new GeomanticFigure(h4Head, h4Neck, h4Body, h4Foot, 4);
            
            CalculateCompleteChartFromFirstFour();
        }

        // Method to calculate the complete chart from just the first 4 houses
        public void CalculateCompleteChartFromFirstFour()
        {
            if (FirstHouse != null && SecondHouse != null && ThirdHouse != null && FourthHouse != null)
            {
                CalculateHouses5To8();
                CalculateHouses9To12();
                CalculateWitnesses();
                CalculateJudge();
                CalculateSentence();
            }
        }

        // Method to calculate the complete chart
        public void CalculateCompleteChart()
        {
            CalculateHouses5To8();
            CalculateHouses9To12();
            CalculateWitnesses();
            CalculateJudge();
            CalculateSentence();
        }

        // Method to calculate Houses 5-8 from Houses 1-4
        public void CalculateHouses5To8()
        {
            if (FirstHouse != null && SecondHouse != null && ThirdHouse != null && FourthHouse != null)
            {
                // House 5: Head from House 1, Head from House 2, Head from House 3, Head from House 4
                FifthHouse = new GeomanticFigure(
                    FirstHouse.HeadLine, SecondHouse.HeadLine, ThirdHouse.HeadLine, FourthHouse.HeadLine, 5);

                // House 6: Neck from House 1, Neck from House 2, Neck from House 3, Neck from House 4
                SixthHouse = new GeomanticFigure(
                    FirstHouse.NeckLine, SecondHouse.NeckLine, ThirdHouse.NeckLine, FourthHouse.NeckLine, 6);

                // House 7: Body from House 1, Body from House 2, Body from House 3, Body from House 4
                SeventhHouse = new GeomanticFigure(
                    FirstHouse.BodyLine, SecondHouse.BodyLine, ThirdHouse.BodyLine, FourthHouse.BodyLine, 7);

                // House 8: Foot from House 1, Foot from House 2, Foot from House 3, Foot from House 4
                EighthHouse = new GeomanticFigure(
                    FirstHouse.FootLine, SecondHouse.FootLine, ThirdHouse.FootLine, FourthHouse.FootLine, 8);
            }
        }

        // Method to calculate Houses 9-12 using the "nieces" calculation
        public void CalculateHouses9To12()
        {
            if (FirstHouse != null && SecondHouse != null && ThirdHouse != null && FourthHouse != null &&
                FifthHouse != null && SixthHouse != null && SeventhHouse != null && EighthHouse != null)
            {
                // House 9: Calculated from Houses 1 and 2 using nieces calculation
                NinthHouse = new GeomanticFigure(
                    CalculateNieces(FirstHouse.HeadLine, SecondHouse.HeadLine),
                    CalculateNieces(FirstHouse.NeckLine, SecondHouse.NeckLine),
                    CalculateNieces(FirstHouse.BodyLine, SecondHouse.BodyLine),
                    CalculateNieces(FirstHouse.FootLine, SecondHouse.FootLine), 9);

                // House 10: Calculated from Houses 3 and 4 using nieces calculation
                TenthHouse = new GeomanticFigure(
                    CalculateNieces(ThirdHouse.HeadLine, FourthHouse.HeadLine),
                    CalculateNieces(ThirdHouse.NeckLine, FourthHouse.NeckLine),
                    CalculateNieces(ThirdHouse.BodyLine, FourthHouse.BodyLine),
                    CalculateNieces(ThirdHouse.FootLine, FourthHouse.FootLine), 10);

                // House 11: Calculated from Houses 5 and 6 using nieces calculation
                EleventhHouse = new GeomanticFigure(
                    CalculateNieces(FifthHouse.HeadLine, SixthHouse.HeadLine),
                    CalculateNieces(FifthHouse.NeckLine, SixthHouse.NeckLine),
                    CalculateNieces(FifthHouse.BodyLine, SixthHouse.BodyLine),
                    CalculateNieces(FifthHouse.FootLine, SixthHouse.FootLine), 11);

                // House 12: Calculated from Houses 7 and 8 using nieces calculation
                TwelfthHouse = new GeomanticFigure(
                    CalculateNieces(SeventhHouse.HeadLine, EighthHouse.HeadLine),
                    CalculateNieces(SeventhHouse.NeckLine, EighthHouse.NeckLine),
                    CalculateNieces(SeventhHouse.BodyLine, EighthHouse.BodyLine),
                    CalculateNieces(SeventhHouse.FootLine, EighthHouse.FootLine), 12);
            }
        }

        // Helper method to calculate nieces (same as Form1's CalNieces method)
        private int CalculateNieces(int h1, int h2)
        {
            return (h1 + h2) % 2 == 0 ? 2 : 1;
        }

        // Method to calculate witnesses from the houses
        public void CalculateWitnesses()
        {
            if (FifthHouse != null && SixthHouse != null && SeventhHouse != null && EighthHouse != null)
            {
                // Right Witness calculation (from Houses 5 and 7)
                int rightHead = CalculateNieces(FifthHouse.HeadLine, SeventhHouse.HeadLine);
                int rightNeck = CalculateNieces(FifthHouse.NeckLine, SeventhHouse.NeckLine);
                int rightBody = CalculateNieces(FifthHouse.BodyLine, SeventhHouse.BodyLine);
                int rightFoot = CalculateNieces(FifthHouse.FootLine, SeventhHouse.FootLine);
                RightWitness = new GeomanticFigure(rightHead, rightNeck, rightBody, rightFoot);

                // Left Witness calculation (from Houses 6 and 8)
                int leftHead = CalculateNieces(SixthHouse.HeadLine, EighthHouse.HeadLine);
                int leftNeck = CalculateNieces(SixthHouse.NeckLine, EighthHouse.NeckLine);
                int leftBody = CalculateNieces(SixthHouse.BodyLine, EighthHouse.BodyLine);
                int leftFoot = CalculateNieces(SixthHouse.FootLine, EighthHouse.FootLine);
                LeftWitness = new GeomanticFigure(leftHead, leftNeck, leftBody, leftFoot);
            }
        }

        // Method to calculate the Judge from the witnesses
        public void CalculateJudge()
        {
            if (RightWitness != null && LeftWitness != null)
            {
                int judgeHead = (RightWitness.HeadLine + LeftWitness.HeadLine) % 2 == 0 ? 2 : 1;
                int judgeNeck = (RightWitness.NeckLine + LeftWitness.NeckLine) % 2 == 0 ? 2 : 1;
                int judgeBody = (RightWitness.BodyLine + LeftWitness.BodyLine) % 2 == 0 ? 2 : 1;
                int judgeFoot = (RightWitness.FootLine + LeftWitness.FootLine) % 2 == 0 ? 2 : 1;
                Judge = new GeomanticFigure(judgeHead, judgeNeck, judgeBody, judgeFoot);
            }
        }

        // Method to calculate the Sentence (sum of Judge and First House)
        public void CalculateSentence()
        {
            if (Judge != null && FirstHouse != null)
            {
                int sentenceHead = (Judge.HeadLine + FirstHouse.HeadLine) % 2 == 0 ? 2 : 1;
                int sentenceNeck = (Judge.NeckLine + FirstHouse.NeckLine) % 2 == 0 ? 2 : 1;
                int sentenceBody = (Judge.BodyLine + FirstHouse.BodyLine) % 2 == 0 ? 2 : 1;
                int sentenceFoot = (Judge.FootLine + FirstHouse.FootLine) % 2 == 0 ? 2 : 1;
                Sentence = new GeomanticFigure(sentenceHead, sentenceNeck, sentenceBody, sentenceFoot);
            }
        }

        // Method to get chart summary
        public string GetChartSummary()
        {
            var summary = new StringBuilder();
            summary.AppendLine("=== GEOMANTIC CHART SUMMARY ===");
            
            for (int i = 1; i <= 12; i++)
            {
                var house = GetHouseFigure(i);
                if (house != null)
                {
                    summary.AppendLine($"House {i}: {house.Name} ({house.GetElementalPattern()})");
                }
            }
            
            if (RightWitness != null)
                summary.AppendLine($"Right Witness: {RightWitness.Name} ({RightWitness.GetElementalPattern()})");
            if (LeftWitness != null)
                summary.AppendLine($"Left Witness: {LeftWitness.Name} ({LeftWitness.GetElementalPattern()})");
            if (Judge != null)
                summary.AppendLine($"Judge: {Judge.Name} ({Judge.GetElementalPattern()})");
            if (Sentence != null)
                summary.AppendLine($"Sentence: {Sentence.Name} ({Sentence.GetElementalPattern()})");
            
            return summary.ToString();
        }

        // Method to validate if the chart is complete
        public bool IsChartComplete()
        {
            return FirstHouse != null && SecondHouse != null && ThirdHouse != null && FourthHouse != null &&
                   RightWitness != null && LeftWitness != null && Judge != null && Sentence != null;
        }

        // Legacy compatibility - keep the old figure class for backward compatibility
        public class figure : GeomanticFigure
        {
            public figure(int headLine, int neckLine, int bodyLine, int footLine, int houseNumber = 0) 
                : base(headLine, neckLine, bodyLine, footLine, houseNumber) { }
            
            public figure(string figureName, int houseNumber = 0) 
                : base(figureName, houseNumber) { }
        }
    }
}
