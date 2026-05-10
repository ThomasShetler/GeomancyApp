using System;
using System.Collections.Generic;
using System.Linq;
using GeomancyAPI.Models;
using GeomancyAPI.Services;
using GeomancyApp;

namespace GeomancyAPI.Handlers
{
    /// <summary>
    /// Pure static handler functions for every Geomancy API route. Bodies lifted out of
    /// the F4.8 <c>GeomancyController</c> minus the <c>Request.CreateResponse(...)</c>
    /// wrapping. Both the F4.8 self-host and the .NET 8 ASP.NET Core controller delegate
    /// here so endpoint logic has a single source of truth.
    /// </summary>
    public static class GeomancyHandlers
    {
        // -----------------------------------------------------------------------------
        // Test
        // -----------------------------------------------------------------------------

        public static object Test()
        {
            return new { message = "API is working!", timestamp = DateTime.UtcNow };
        }

        // -----------------------------------------------------------------------------
        // Figures
        // -----------------------------------------------------------------------------

        public static GenerateFigureResponse GenerateFigure(GenerateFigureRequest request)
        {
            var figure = new GeomanticFigure(request.HeadLine, request.NeckLine, request.BodyLine, request.FootLine);

            return new GenerateFigureResponse
            {
                Figure = ConvertToFigureResponse(figure),
                Success = true,
                Message = "Figure generated successfully"
            };
        }

        public static GenerateFourFiguresResponse GenerateFourFigures(GenerateFourFiguresRequest request)
        {
            var figures = new List<FigureResponse>();

            var figure1 = new GeomanticFigure(request.House1.HeadLine, request.House1.NeckLine, request.House1.BodyLine, request.House1.FootLine, 1);
            var figure2 = new GeomanticFigure(request.House2.HeadLine, request.House2.NeckLine, request.House2.BodyLine, request.House2.FootLine, 2);
            var figure3 = new GeomanticFigure(request.House3.HeadLine, request.House3.NeckLine, request.House3.BodyLine, request.House3.FootLine, 3);
            var figure4 = new GeomanticFigure(request.House4.HeadLine, request.House4.NeckLine, request.House4.BodyLine, request.House4.FootLine, 4);

            figures.Add(ConvertToFigureResponse(figure1));
            figures.Add(ConvertToFigureResponse(figure2));
            figures.Add(ConvertToFigureResponse(figure3));
            figures.Add(ConvertToFigureResponse(figure4));

            return new GenerateFourFiguresResponse
            {
                Figures = figures,
                Success = true,
                Message = "Four figures generated successfully"
            };
        }

        public static HouseChartResponse GenerateHouseChart(GenerateFourFiguresRequest request)
        {
            var houseChart = new HouseChart();

            houseChart.SetFirstFourHousesAndCalculate(
                request.House1.HeadLine, request.House1.NeckLine, request.House1.BodyLine, request.House1.FootLine,
                request.House2.HeadLine, request.House2.NeckLine, request.House2.BodyLine, request.House2.FootLine,
                request.House3.HeadLine, request.House3.NeckLine, request.House3.BodyLine, request.House3.FootLine,
                request.House4.HeadLine, request.House4.NeckLine, request.House4.BodyLine, request.House4.FootLine
            );

            return ConvertToHouseChartResponse(houseChart);
        }

        public static GenerateFigureResponse GetFigureByName(string figureName)
        {
            var figure = new GeomanticFigure(figureName);

            return new GenerateFigureResponse
            {
                Figure = ConvertToFigureResponse(figure),
                Success = true,
                Message = $"Figure '{figureName}' retrieved successfully"
            };
        }

        public static List<FigureResponse> GetAllFigures()
        {
            var allFigures = FigureData.GetAllFigures();
            return allFigures.Select(f => ConvertToFigureResponse(new GeomanticFigure(f.Name))).ToList();
        }

        public static GenerateFigureResponse GetFigureByPattern(int headLine, int neckLine, int bodyLine, int footLine)
        {
            ValidateLine(headLine, "headLine");
            ValidateLine(neckLine, "neckLine");
            ValidateLine(bodyLine, "bodyLine");
            ValidateLine(footLine, "footLine");

            var figure = new GeomanticFigure(headLine, neckLine, bodyLine, footLine);

            return new GenerateFigureResponse
            {
                Figure = ConvertToFigureResponse(figure),
                Success = true,
                Message = "Figure retrieved by pattern successfully"
            };
        }

        // -----------------------------------------------------------------------------
        // Charts (figures by name / comma-separated)
        // -----------------------------------------------------------------------------

        public static HouseChartResponse GenerateChartFromFigureNames(
            string firstMother, string secondMother, string thirdMother, string fourthMother)
        {
            ValidateMotherNames(firstMother, secondMother, thirdMother, fourthMother);

            var houseChart = BuildChartFromMotherNames(firstMother, secondMother, thirdMother, fourthMother);
            return ConvertToHouseChartResponse(houseChart);
        }

        public static HouseChartResponse GenerateChartFromCommaSeparatedFigures(string mothers)
        {
            if (string.IsNullOrWhiteSpace(mothers))
            {
                throw new ArgumentException("Mothers parameter is required (comma-separated figure names)", nameof(mothers));
            }

            var figureNames = mothers.Split(',').Select(f => f.Trim()).ToArray();

            if (figureNames.Length != 4)
            {
                throw new ArgumentException("Exactly 4 figure names are required, separated by commas", nameof(mothers));
            }

            return GenerateChartFromFigureNames(figureNames[0], figureNames[1], figureNames[2], figureNames[3]);
        }

        // -----------------------------------------------------------------------------
        // Aspects
        // -----------------------------------------------------------------------------

        public static AspectsResponse GetChartAspects(GenerateFourFiguresRequest request, string minAspect)
        {
            var houseChart = new HouseChart();
            houseChart.SetFirstFourHousesAndCalculate(
                request.House1.HeadLine, request.House1.NeckLine, request.House1.BodyLine, request.House1.FootLine,
                request.House2.HeadLine, request.House2.NeckLine, request.House2.BodyLine, request.House2.FootLine,
                request.House3.HeadLine, request.House3.NeckLine, request.House3.BodyLine, request.House3.FootLine,
                request.House4.HeadLine, request.House4.NeckLine, request.House4.BodyLine, request.House4.FootLine
            );

            return BuildAspectsResponse(houseChart, minAspect);
        }

        public static AspectsResponse GetAspectsFromFigures(
            string firstMother, string secondMother, string thirdMother, string fourthMother, string minAspect)
        {
            ValidateMotherNames(firstMother, secondMother, thirdMother, fourthMother);

            var houseChart = BuildChartFromMotherNames(firstMother, secondMother, thirdMother, fourthMother);
            return BuildAspectsResponse(houseChart, minAspect);
        }

        public static AspectsResponse GetAspectsFromPattern(
            int h1Head, int h1Neck, int h1Body, int h1Foot,
            int h2Head, int h2Neck, int h2Body, int h2Foot,
            int h3Head, int h3Neck, int h3Body, int h3Foot,
            int h4Head, int h4Neck, int h4Body, int h4Foot,
            string minAspect)
        {
            ValidatePatternLines(h1Head, h1Neck, h1Body, h1Foot,
                                 h2Head, h2Neck, h2Body, h2Foot,
                                 h3Head, h3Neck, h3Body, h3Foot,
                                 h4Head, h4Neck, h4Body, h4Foot);

            var houseChart = new HouseChart();
            houseChart.SetFirstFourHousesAndCalculate(
                h1Head, h1Neck, h1Body, h1Foot,
                h2Head, h2Neck, h2Body, h2Foot,
                h3Head, h3Neck, h3Body, h3Foot,
                h4Head, h4Neck, h4Body, h4Foot);

            return BuildAspectsResponse(houseChart, minAspect);
        }

        // -----------------------------------------------------------------------------
        // Perfections
        // -----------------------------------------------------------------------------

        public static MultiplePerfectionsResponse CalculatePerfection(PerfectionRequest request)
        {
            var houseChart = new HouseChart();
            houseChart.SetFirstFourHousesAndCalculate(
                request.Mothers.House1.HeadLine, request.Mothers.House1.NeckLine, request.Mothers.House1.BodyLine, request.Mothers.House1.FootLine,
                request.Mothers.House2.HeadLine, request.Mothers.House2.NeckLine, request.Mothers.House2.BodyLine, request.Mothers.House2.FootLine,
                request.Mothers.House3.HeadLine, request.Mothers.House3.NeckLine, request.Mothers.House3.BodyLine, request.Mothers.House3.FootLine,
                request.Mothers.House4.HeadLine, request.Mothers.House4.NeckLine, request.Mothers.House4.BodyLine, request.Mothers.House4.FootLine
            );

            var allPerfections = PerfectionCalculator.Find(houseChart, request.QuerentHouse, request.QuesitedHouse, returnAllModes: true);

            var validPerfections = allPerfections.Where(p => p.Mode != PerfectionType.None).ToList();

            var perfectionResponses = new List<PerfectionResponse>();
            foreach (var perfection in validPerfections)
            {
                var perfectionResponse = ToPerfectionResponseWithoutScoring(perfection, houseChart, request.QuerentHouse, request.QuesitedHouse);
                perfectionResponses.Add(perfectionResponse);
            }

            int totalFavorableScore = 0;
            foreach (var perfection in validPerfections)
            {
                totalFavorableScore += PerfectionCalculator.CalculateScore(perfection);
            }

            int totalUnfavorableScore = PerfectionCalculator.CalculateTotalUnfavorableScore(houseChart, request.QuerentHouse, request.QuesitedHouse);
            int aggregateNetScore = totalFavorableScore + totalUnfavorableScore;

            foreach (var perfectionResponse in perfectionResponses)
            {
                perfectionResponse.FavorableScore = totalFavorableScore;
                perfectionResponse.UnfavorableScore = totalUnfavorableScore;
                perfectionResponse.NetScore = aggregateNetScore;
            }

            return new MultiplePerfectionsResponse
            {
                Success = true,
                Message = perfectionResponses.Count > 0
                    ? $"Found {perfectionResponses.Count} perfection(s)."
                    : "No perfections found.",
                Perfections = perfectionResponses,
                TotalPerfections = perfectionResponses.Count
            };
        }

        public static PerfectionResponse CalculatePerfectionFromFigures(
            string firstMother, string secondMother, string thirdMother, string fourthMother,
            int querentHouse, int quesitedHouse)
        {
            ValidateMotherNames(firstMother, secondMother, thirdMother, fourthMother);
            ValidateHouseNumber(querentHouse, "querentHouse");
            ValidateHouseNumber(quesitedHouse, "quesitedHouse");

            var houseChart = BuildChartFromMotherNames(firstMother, secondMother, thirdMother, fourthMother);
            var perfection = PerfectionCalculator.Find(houseChart, querentHouse, quesitedHouse);
            return ToPerfectionResponse(perfection, houseChart, querentHouse, quesitedHouse);
        }

        public static PerfectionResponse CalculatePerfectionFromPattern(
            int h1Head, int h1Neck, int h1Body, int h1Foot,
            int h2Head, int h2Neck, int h2Body, int h2Foot,
            int h3Head, int h3Neck, int h3Body, int h3Foot,
            int h4Head, int h4Neck, int h4Body, int h4Foot,
            int querentHouse, int quesitedHouse)
        {
            ValidatePatternLines(h1Head, h1Neck, h1Body, h1Foot,
                                 h2Head, h2Neck, h2Body, h2Foot,
                                 h3Head, h3Neck, h3Body, h3Foot,
                                 h4Head, h4Neck, h4Body, h4Foot);
            ValidateHouseNumber(querentHouse, "querentHouse");
            ValidateHouseNumber(quesitedHouse, "quesitedHouse");

            var houseChart = new HouseChart();
            houseChart.SetFirstFourHousesAndCalculate(
                h1Head, h1Neck, h1Body, h1Foot,
                h2Head, h2Neck, h2Body, h2Foot,
                h3Head, h3Neck, h3Body, h3Foot,
                h4Head, h4Neck, h4Body, h4Foot);

            var perfection = PerfectionCalculator.Find(houseChart, querentHouse, quesitedHouse);
            return ToPerfectionResponse(perfection, houseChart, querentHouse, quesitedHouse);
        }

        public static MultiplePerfectionsResponse GetAllPerfectionsFromFigures(
            string firstMother, string secondMother, string thirdMother, string fourthMother)
        {
            ValidateMotherNames(firstMother, secondMother, thirdMother, fourthMother);

            var houseChart = BuildChartFromMotherNames(firstMother, secondMother, thirdMother, fourthMother);
            var perfections = PerfectionCalculator.FindAll(houseChart).ToList();
            var perfectionList = perfections.Select(p => ToPerfectionResponse(p, houseChart, p.QuerentHouse, p.QuesitedHouse)).ToList();

            return new MultiplePerfectionsResponse
            {
                Perfections = perfectionList,
                TotalPerfections = perfectionList.Count,
                Success = true,
                Message = $"Found {perfectionList.Count} perfections in the chart"
            };
        }

        public static AspectAnalysisResponse GetAspectAnalysisFromFigures(
            string firstMother, string secondMother, string thirdMother, string fourthMother)
        {
            ValidateMotherNames(firstMother, secondMother, thirdMother, fourthMother);

            var houseChart = BuildChartFromMotherNames(firstMother, secondMother, thirdMother, fourthMother);
            var aspectReport = ChartAspectAnalysis.Evaluate(houseChart);

            var aspectList = aspectReport.Details.Select(d => new AspectDetail
            {
                FromHouse = d.From,
                ToHouse = d.To,
                AspectType = d.Aspect.ToString(),
                Weight = d.Weight,
                FromFigure = houseChart.GetHouseFigure(d.From)?.Name,
                ToFigure = houseChart.GetHouseFigure(d.To)?.Name,
                Justification = d.Justification != null ? new WeightJustificationDetail
                {
                    BaseWeight = d.Justification.BaseWeight,
                    TotalModifier = d.Justification.TotalModifier,
                    FinalWeight = d.Justification.FinalWeight,
                    Justifications = d.Justification.Justifications
                } : null
            }).ToList();

            return new AspectAnalysisResponse
            {
                TotalScore = aspectReport.TotalScore,
                Aspects = aspectList,
                TotalAspects = aspectList.Count,
                Polarity = new PolarityReport
                {
                    Easy = aspectReport.EasyScore,
                    Hard = aspectReport.HardScore,
                    Delta = aspectReport.Delta,
                    Percent = aspectReport.PolarityPercent,
                    Verdict = aspectReport.PolarityVerdict
                },
                AspectCounts = aspectReport.AspectCounts,
                Success = true,
                Message = $"Chart aspect strength: {aspectReport.TotalScore} points from {aspectList.Count} aspects. Polarity: {aspectReport.PolarityVerdict} ({aspectReport.PolarityPercent:F1}%)"
            };
        }

        public static MultiplePerfectionsResponse GetAllPerfectionsForPair(
            string firstMother, string secondMother, string thirdMother, string fourthMother,
            int querentHouse, int quesitedHouse)
        {
            ValidateMotherNames(firstMother, secondMother, thirdMother, fourthMother);
            ValidateHouseNumber(querentHouse, "querentHouse");
            ValidateHouseNumber(quesitedHouse, "quesitedHouse");

            var houseChart = BuildChartFromMotherNames(firstMother, secondMother, thirdMother, fourthMother);
            var perfections = PerfectionCalculator.FindAll(houseChart, querentHouse, quesitedHouse).ToList();

            var perfectionList = perfections.Select(p =>
            {
                var perfectionResponse = ToPerfectionResponse(p, houseChart, querentHouse, quesitedHouse);
                perfectionResponse.Indentation = ToIndentScoreResponse(ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, quesitedHouse));
                perfectionResponse.TranslatorIndentation = (p.TranslatorHouse > 0)
                    ? ToIndentScoreResponse(ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, p.TranslatorHouse))
                    : null;
                return perfectionResponse;
            }).ToList();

            var mainIndent = ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, quesitedHouse);
            IndentScoreResponse mainIndentResp = ToIndentScoreResponse(mainIndent);
            IndentScoreResponse translatorIndentResp = null;
            if (perfections.Any() && perfections[0].TranslatorHouse > 0)
            {
                translatorIndentResp = ToIndentScoreResponse(ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, perfections[0].TranslatorHouse));
            }

            return new MultiplePerfectionsResponse
            {
                Perfections = perfectionList,
                TotalPerfections = perfectionList.Count,
                Indentation = mainIndentResp,
                TranslatorIndentation = translatorIndentResp,
                Success = true,
                Message = $"Found {perfectionList.Count} perfections for the specified pair"
            };
        }

        public static IndentationResponse GetIndentation(
            string firstMother, string secondMother, string thirdMother, string fourthMother,
            int querentHouse, int quesitedHouse, int? translatorHouse)
        {
            ValidateMotherNames(firstMother, secondMother, thirdMother, fourthMother);
            ValidateHouseNumber(querentHouse, "querentHouse");
            ValidateHouseNumber(quesitedHouse, "quesitedHouse");

            var houseChart = BuildChartFromMotherNames(firstMother, secondMother, thirdMother, fourthMother);

            var mainIndent = ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, quesitedHouse);
            IndentScoreResponse mainIndentResp = ToIndentScoreResponse(mainIndent);
            IndentScoreResponse translatorIndentResp = null;
            if (translatorHouse.HasValue && translatorHouse.Value > 0)
            {
                translatorIndentResp = ToIndentScoreResponse(ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, translatorHouse.Value));
            }
            return new IndentationResponse
            {
                Indentation = mainIndentResp,
                TranslatorIndentation = translatorIndentResp
            };
        }

        public static PerfectionAnalysisResponse AnalyzePerfections(PerfectionRequest request)
        {
            var houseChart = new HouseChart();
            houseChart.SetFirstFourHousesAndCalculate(
                request.Mothers.House1.HeadLine, request.Mothers.House1.NeckLine, request.Mothers.House1.BodyLine, request.Mothers.House1.FootLine,
                request.Mothers.House2.HeadLine, request.Mothers.House2.NeckLine, request.Mothers.House2.BodyLine, request.Mothers.House2.FootLine,
                request.Mothers.House3.HeadLine, request.Mothers.House3.NeckLine, request.Mothers.House3.BodyLine, request.Mothers.House3.FootLine,
                request.Mothers.House4.HeadLine, request.Mothers.House4.NeckLine, request.Mothers.House4.BodyLine, request.Mothers.House4.FootLine
            );

            var analysis = PerfectionCalculator.AnalyzePerfections(houseChart, request.QuerentHouse, request.QuesitedHouse);

            var perfectionResponses = new List<PerfectionResponse>();
            foreach (var perfection in analysis.Perfections)
            {
                var perfectionResponse = ToPerfectionResponseWithoutScoring(perfection, houseChart, request.QuerentHouse, request.QuesitedHouse);
                perfectionResponse.FavorableScore = PerfectionCalculator.CalculateScore(perfection);
                perfectionResponse.UnfavorableScore = PerfectionCalculator.CalculateUnfavorableScore(perfection);
                perfectionResponse.NetScore = perfectionResponse.FavorableScore + perfectionResponse.UnfavorableScore;
                perfectionResponses.Add(perfectionResponse);
            }

            var denialResponses = new List<PerfectionResponse>();
            foreach (var denial in analysis.Denials)
            {
                var denialResponse = ToPerfectionResponseWithoutScoring(denial, houseChart, request.QuerentHouse, request.QuesitedHouse);
                denialResponse.FavorableScore = PerfectionCalculator.CalculateScore(denial);
                denialResponse.UnfavorableScore = PerfectionCalculator.CalculateUnfavorableScore(denial);
                if (denial.Mode == PerfectionType.None)
                {
                    denialResponse.UnfavorableScore = -5;
                    denialResponse.Success = true;
                }
                denialResponse.NetScore = denialResponse.FavorableScore + denialResponse.UnfavorableScore;
                denialResponses.Add(denialResponse);
            }

            var positiveAspects = analysis.PositiveAspects.Select(a =>
            {
                var aspectType = a.AspectType;
                return new AspectRecordResponse
                {
                    AspectType = aspectType.ToString(),
                    Direction = a.Direction,
                    FromHouse = a.FromHouse,
                    ToHouse = a.ToHouse,
                    Description = a.Description,
                    MadeThroughCompany = a.MadeThroughCompany,
                    IsMajorAspect = a.IsMajorAspect,
                    FavorableScore = CalculateAspectScore(aspectType, a.MadeThroughCompany),
                    UnfavorableScore = 0
                };
            }).ToList();

            var negativeAspects = analysis.NegativeAspects.Select(a =>
            {
                var aspectType = a.AspectType;
                return new AspectRecordResponse
                {
                    AspectType = aspectType.ToString(),
                    Direction = a.Direction,
                    FromHouse = a.FromHouse,
                    ToHouse = a.ToHouse,
                    Description = a.Description,
                    MadeThroughCompany = a.MadeThroughCompany,
                    IsMajorAspect = a.IsMajorAspect,
                    FavorableScore = 0,
                    UnfavorableScore = CalculateAspectUnfavorableScore(aspectType, a.MadeThroughCompany)
                };
            }).ToList();

            return new PerfectionAnalysisResponse
            {
                Success = true,
                Message = $"Found {analysis.Perfections.Count} perfection(s), {analysis.Denials.Count} denial(s), {analysis.PositiveAspects.Count} positive aspect(s), and {analysis.NegativeAspects.Count} negative aspect(s).",
                Perfections = perfectionResponses,
                Denials = denialResponses,
                PositiveAspects = positiveAspects,
                NegativeAspects = negativeAspects,
                TotalFavorableScore = analysis.TotalFavorableScore,
                TotalUnfavorableScore = analysis.TotalUnfavorableScore,
                NetScore = analysis.NetScore,
                QuerentHouse = analysis.QuerentHouse,
                QuesitedHouse = analysis.QuesitedHouse
            };
        }

        // -----------------------------------------------------------------------------
        // Way Of Points
        // -----------------------------------------------------------------------------

        public static WayOfPointsAnalysisResponse CalculateWayOfPoints(GenerateFourFiguresRequest request)
        {
            var houseChart = new HouseChart();
            houseChart.SetFirstFourHousesAndCalculate(
                request.House1.HeadLine, request.House1.NeckLine, request.House1.BodyLine, request.House1.FootLine,
                request.House2.HeadLine, request.House2.NeckLine, request.House2.BodyLine, request.House2.FootLine,
                request.House3.HeadLine, request.House3.NeckLine, request.House3.BodyLine, request.House3.FootLine,
                request.House4.HeadLine, request.House4.NeckLine, request.House4.BodyLine, request.House4.FootLine
            );

            var analysis = WayOfPoints.CalculateAllWays(houseChart);

            return new WayOfPointsAnalysisResponse
            {
                Success = true,
                Message = "Way of points calculation completed successfully",
                FireWay = ToWayOfPointsResultResponse(analysis.FireWay),
                AirWay = ToWayOfPointsResultResponse(analysis.AirWay),
                WaterWay = ToWayOfPointsResultResponse(analysis.WaterWay),
                EarthWay = ToWayOfPointsResultResponse(analysis.EarthWay)
            };
        }

        // -----------------------------------------------------------------------------
        // Reference Directories (House / Court / Way Of Points)
        // -----------------------------------------------------------------------------

        public static IReadOnlyList<HouseDirectoryEntryResponse> GetHousesDirectory()
            => HouseDirectoryLoader.GetHouses();

        public static HouseDirectoryEntryResponse GetHouseDirectoryEntry(int id)
            => HouseDirectoryLoader.GetHouse(id);

        public static IReadOnlyList<CourtDirectoryEntryResponse> GetCourtsDirectory()
            => HouseDirectoryLoader.GetCourts();

        public static CourtDirectoryEntryResponse GetCourtDirectoryEntry(string placement)
            => HouseDirectoryLoader.GetCourt(placement);

        public static IReadOnlyList<WayOfPointsElementEntryResponse> GetWayOfPointsElementsDirectory()
            => WayOfPointsDirectoryLoader.GetElements();

        public static WayOfPointsElementEntryResponse GetWayOfPointsElementEntry(string key)
            => WayOfPointsDirectoryLoader.GetElement(key);

        public static IReadOnlyList<WayOfPointsPathTypeEntryResponse> GetWayOfPointsPathTypesDirectory()
            => WayOfPointsDirectoryLoader.GetPathTypes();

        public static WayOfPointsPathTypeEntryResponse GetWayOfPointsPathTypeEntry(string id)
            => WayOfPointsDirectoryLoader.GetPathType(id);

        // -----------------------------------------------------------------------------
        // Internal helpers (validation / conversion / scoring)
        // -----------------------------------------------------------------------------

        private static void ValidateLine(int value, string name)
        {
            if (value < 1 || value > 2)
            {
                throw new ArgumentException("All line values must be 1 or 2", name);
            }
        }

        private static void ValidatePatternLines(
            int h1Head, int h1Neck, int h1Body, int h1Foot,
            int h2Head, int h2Neck, int h2Body, int h2Foot,
            int h3Head, int h3Neck, int h3Body, int h3Foot,
            int h4Head, int h4Neck, int h4Body, int h4Foot)
        {
            if (h1Head < 1 || h1Head > 2 || h1Neck < 1 || h1Neck > 2 || h1Body < 1 || h1Body > 2 || h1Foot < 1 || h1Foot > 2 ||
                h2Head < 1 || h2Head > 2 || h2Neck < 1 || h2Neck > 2 || h2Body < 1 || h2Body > 2 || h2Foot < 1 || h2Foot > 2 ||
                h3Head < 1 || h3Head > 2 || h3Neck < 1 || h3Neck > 2 || h3Body < 1 || h3Body > 2 || h3Foot < 1 || h3Foot > 2 ||
                h4Head < 1 || h4Head > 2 || h4Neck < 1 || h4Neck > 2 || h4Body < 1 || h4Body > 2 || h4Foot < 1 || h4Foot > 2)
            {
                throw new ArgumentException("All elemental line values must be 1 or 2");
            }
        }

        private static void ValidateMotherNames(string m1, string m2, string m3, string m4)
        {
            if (string.IsNullOrWhiteSpace(m1) || string.IsNullOrWhiteSpace(m2) ||
                string.IsNullOrWhiteSpace(m3) || string.IsNullOrWhiteSpace(m4))
            {
                throw new ArgumentException("All four mother figure names are required");
            }
        }

        private static void ValidateHouseNumber(int house, string name)
        {
            if (house < 1 || house > 12)
            {
                throw new ArgumentException("House numbers must be between 1 and 12", name);
            }
        }

        private static HouseChart BuildChartFromMotherNames(string m1, string m2, string m3, string m4)
        {
            var figure1 = new GeomanticFigure(m1);
            var figure2 = new GeomanticFigure(m2);
            var figure3 = new GeomanticFigure(m3);
            var figure4 = new GeomanticFigure(m4);

            var houseChart = new HouseChart();
            houseChart.SetFirstFourHousesAndCalculate(
                figure1.HeadLine, figure1.NeckLine, figure1.BodyLine, figure1.FootLine,
                figure2.HeadLine, figure2.NeckLine, figure2.BodyLine, figure2.FootLine,
                figure3.HeadLine, figure3.NeckLine, figure3.BodyLine, figure3.FootLine,
                figure4.HeadLine, figure4.NeckLine, figure4.BodyLine, figure4.FootLine
            );

            return houseChart;
        }

        private static AspectsResponse BuildAspectsResponse(HouseChart houseChart, string minAspect)
        {
            AspectType minAspectType = AspectType.Sextile;
            if (Enum.TryParse<AspectType>(minAspect, true, out var parsed))
            {
                minAspectType = parsed;
            }

            var aspects = GeomanticAspects.AllAspects(houseChart, minAspectType).ToList();

            var aspectList = aspects.Select(a => new AspectInfo
            {
                FromHouse = a.from,
                ToHouse = a.to,
                AspectType = a.aspect.ToString(),
                FromFigure = houseChart.GetHouseFigure(a.from)?.Name,
                ToFigure = houseChart.GetHouseFigure(a.to)?.Name
            }).ToList();

            return new AspectsResponse
            {
                Aspects = aspectList,
                TotalAspects = aspectList.Count,
                MinimumAspectType = minAspectType.ToString(),
                Success = true,
                Message = $"Found {aspectList.Count} aspects of type {minAspectType} or higher"
            };
        }

        public static FigureResponse ConvertToFigureResponse(GeomanticFigure figure)
        {
            return new FigureResponse
            {
                Name = figure.Name,
                HouseStrength = (Models.FigureInHouseStrength)(int)figure.HouseStrength,
                OtherNames = figure.OtherNames,
                Quality = figure.Quality,
                Keyword = figure.Keyword,
                Imagery = figure.Imagery,
                StrongHouse = figure.StrongHouse,
                WeakHouse = figure.WeakHouse,
                Planet = figure.Planet,
                Sign = figure.Sign,
                InnerEl = figure.InnerEl,
                OuterEl = figure.OuterEl,
                FireElement = figure.FireElement,
                AirElement = figure.AirElement,
                WaterElement = figure.WaterElement,
                EarthElement = figure.EarthElement,
                Anatomy = figure.Anatomy,
                BodyType = figure.BodyType,
                CharacterType = figure.CharacterType,
                Colors = figure.Colors,
                Commentary = figure.Commentary,
                DivinatoryMeaning = figure.DivinatoryMeaning,
                ElementalPattern = figure.GetElementalPattern(),
                HeadLine = figure.HeadLine,
                NeckLine = figure.NeckLine,
                BodyLine = figure.BodyLine,
                FootLine = figure.FootLine,
                Tagline = figure.Tagline ?? string.Empty,
                CoreMeaning = figure.CoreMeaning ?? new List<string>(),
                FavorableFor = figure.FavorableFor ?? new List<string>(),
                UnfavorableFor = figure.UnfavorableFor ?? new List<string>(),
                ElementalSynthesis = figure.ElementalSynthesis ?? string.Empty,
                TraditionalImagery = figure.TraditionalImagery ?? new List<string>(),
                Interpretation = figure.Interpretation ?? new List<string>(),
                InHouses = figure.InHouses ?? new Dictionary<string, string>(),
                ModernExamples = figure.ModernExamples ?? new List<string>(),
                TraditionalSources = figure.TraditionalSources == null
                    ? new List<TraditionalSourceResponse>()
                    : figure.TraditionalSources.Select(s => new TraditionalSourceResponse
                    {
                        Author = s.Author,
                        Work = s.Work,
                        Section = s.Section,
                        Year = s.Year
                    }).ToList()
            };
        }

        public static HouseChartResponse ConvertToHouseChartResponse(HouseChart houseChart)
        {
            var houses = new List<HouseResponse>();

            for (int i = 1; i <= 12; i++)
            {
                var house = houseChart.GetHouseFigure(i);
                if (house != null)
                {
                    houses.Add(new HouseResponse
                    {
                        HouseNumber = i,
                        Figure = ConvertToFigureResponse(house)
                    });
                }
            }

            var triplicities = houseChart.GetTriplicities().Select(t => new TriplicityResponse
            {
                Number = t.Number,
                FirstFigure = ConvertToFigureResponse(t.FirstFigure),
                SecondFigure = ConvertToFigureResponse(t.SecondFigure),
                ThirdFigure = ConvertToFigureResponse(t.ThirdFigure),
                Description = t.Description
            }).ToList();

            return new HouseChartResponse
            {
                Houses = houses,
                RightWitness = houseChart.RightWitness != null ? ConvertToFigureResponse(houseChart.RightWitness) : null,
                LeftWitness = houseChart.LeftWitness != null ? ConvertToFigureResponse(houseChart.LeftWitness) : null,
                Judge = houseChart.Judge != null ? ConvertToFigureResponse(houseChart.Judge) : null,
                Sentence = houseChart.Sentence != null ? ConvertToFigureResponse(houseChart.Sentence) : null,
                Triplicities = triplicities,
                ChartSummary = houseChart.GetChartSummary(),
                IsComplete = houseChart.IsChartComplete(),
                GeneratedAt = DateTime.UtcNow
            };
        }

        private static IndentScoreResponse ToIndentScoreResponse(IndentScore s)
        {
            return new IndentScoreResponse
            {
                QuerentHouse = s.QuerentHouse,
                QuesitedHouse = s.QuesitedHouse,
                Index = s.Index,
                Bonus = s.Bonus
            };
        }

        private static PerfectionResponse ToPerfectionResponseWithoutScoring(PerfectionResult perfection, HouseChart houseChart, int querentHouse, int quesitedHouse)
        {
            string modeString = perfection.Mode.ToString();
            if (perfection.Mode == PerfectionType.None)
            {
                modeString = "Impedition";
            }
            else if (perfection.Mode == PerfectionType.Aspect && !string.IsNullOrEmpty(perfection.AspectDirection))
            {
                modeString = "Aspect";
            }

            return new PerfectionResponse
            {
                Success = true,
                Message = perfection.Mode == PerfectionType.None ? "No perfection found." : "Perfection found.",
                Mode = modeString,
                AspectBetweenSignificators = perfection.AspectBetweenSignificators.ToString(),
                AspectDirection = perfection.AspectDirection,
                TranslatorHouse = perfection.TranslatorHouse,
                TranslatorFigure = perfection.TranslatorHouse > 0 ? houseChart.GetHouseFigure(perfection.TranslatorHouse)?.Name : string.Empty,
                Notes = perfection.Notes.ToList(),
                QuerentHouse = perfection.QuerentHouse > 0 ? perfection.QuerentHouse : querentHouse,
                QuesitedHouse = perfection.QuesitedHouse > 0 ? perfection.QuesitedHouse : quesitedHouse,
                QuerentFigure = houseChart.GetHouseFigure(perfection.QuerentHouse > 0 ? perfection.QuerentHouse : querentHouse)?.Name ?? string.Empty,
                QuesitedFigure = houseChart.GetHouseFigure(perfection.QuesitedHouse > 0 ? perfection.QuesitedHouse : quesitedHouse)?.Name ?? string.Empty,
                MadeThroughCompany = perfection.MadeThroughCompany,
                BaseMode = perfection.BaseMode != PerfectionType.None ? perfection.BaseMode.ToString() : string.Empty,
                CompanyType = perfection.CompanyType.ToString(),
                CompanyTypeDescription = perfection.CompanyTypeDescription ?? string.Empty,
                FavorableScore = 0,
                UnfavorableScore = 0,
                NetScore = 0
            };
        }

        private static PerfectionResponse ToPerfectionResponse(PerfectionResult perfection, HouseChart houseChart, int querentHouse, int quesitedHouse)
        {
            int favorableScore = PerfectionCalculator.CalculateScore(perfection);
            int unfavorableScore = PerfectionCalculator.CalculateTotalUnfavorableScore(houseChart, querentHouse, quesitedHouse);
            int netScore = favorableScore + unfavorableScore;

            string modeString = perfection.Mode.ToString();
            if (perfection.Mode == PerfectionType.None)
            {
                modeString = "Impedition";
            }
            else if (perfection.Mode == PerfectionType.Aspect && !string.IsNullOrEmpty(perfection.AspectDirection))
            {
                modeString = $"Aspect ({perfection.AspectDirection})";
            }

            return new PerfectionResponse
            {
                Mode = modeString,
                AspectBetweenSignificators = perfection.AspectBetweenSignificators.ToString(),
                AspectDirection = perfection.AspectDirection ?? string.Empty,
                TranslatorHouse = perfection.TranslatorHouse,
                TranslatorFigure = perfection.TranslatorHouse > 0 ? houseChart.GetHouseFigure(perfection.TranslatorHouse)?.Name : null,
                Notes = perfection.Notes,
                QuerentHouse = querentHouse,
                QuesitedHouse = quesitedHouse,
                QuerentFigure = houseChart.GetHouseFigure(querentHouse)?.Name,
                QuesitedFigure = houseChart.GetHouseFigure(quesitedHouse)?.Name,
                MadeThroughCompany = perfection.MadeThroughCompany,
                BaseMode = perfection.BaseMode != PerfectionType.None ? perfection.BaseMode.ToString() : null,
                CompanyType = perfection.CompanyType.ToString(),
                CompanyTypeDescription = perfection.CompanyTypeDescription ?? string.Empty,
                FavorableScore = favorableScore,
                UnfavorableScore = unfavorableScore,
                NetScore = netScore,
                Success = true,
                Message = "Perfection calculation completed successfully"
            };
        }

        private static int CalculateAspectScore(AspectType aspectType, bool madeThroughCompany)
        {
            int baseScore;

            if (aspectType == AspectType.Trine)
                baseScore = 3;
            else if (aspectType == AspectType.Sextile)
                baseScore = 3;
            else
                return 0;

            if (madeThroughCompany)
            {
                baseScore = Math.Max(0, baseScore - 1);
            }

            return baseScore;
        }

        private static int CalculateAspectUnfavorableScore(AspectType aspectType, bool madeThroughCompany)
        {
            int baseScore;

            if (aspectType == AspectType.Opposition)
                baseScore = -4;
            else if (aspectType == AspectType.Square)
                baseScore = -3;
            else
                return 0;

            if (madeThroughCompany)
            {
                baseScore -= 1;
            }

            return baseScore;
        }

        private static WayOfPointsResultResponse ToWayOfPointsResultResponse(WayOfPointsResult result)
        {
            return new WayOfPointsResultResponse
            {
                WayName = result.WayName ?? string.Empty,
                LineType = result.LineType ?? string.Empty,
                CanBeEstablished = result.CanBeEstablished,
                AllPaths = result.AllPaths.Select(p => new WayOfPointsPathResponse
                {
                    Houses = p.Houses ?? new List<int>(),
                    RowReached = p.RowReached,
                    PathType = p.PathType.ToString(),
                    EndpointHouse = p.EndpointHouse,
                    Description = p.Description ?? string.Empty
                }).ToList(),
                StrongPaths = result.StrongPaths.Select(p => new WayOfPointsPathResponse
                {
                    Houses = p.Houses ?? new List<int>(),
                    RowReached = p.RowReached,
                    PathType = p.PathType.ToString(),
                    EndpointHouse = p.EndpointHouse,
                    Description = p.Description ?? string.Empty
                }).ToList(),
                PassivePaths = result.PassivePaths.Select(p => new WayOfPointsPathResponse
                {
                    Houses = p.Houses ?? new List<int>(),
                    RowReached = p.RowReached,
                    PathType = p.PathType.ToString(),
                    EndpointHouse = p.EndpointHouse,
                    Description = p.Description ?? string.Empty
                }).ToList(),
                StrongPassivePaths = result.StrongPassivePaths.Select(p => new WayOfPointsPathResponse
                {
                    Houses = p.Houses ?? new List<int>(),
                    RowReached = p.RowReached,
                    PathType = p.PathType.ToString(),
                    EndpointHouse = p.EndpointHouse,
                    Description = p.Description ?? string.Empty
                }).ToList(),
                WeakerPassivePaths = result.WeakerPassivePaths.Select(p => new WayOfPointsPathResponse
                {
                    Houses = p.Houses ?? new List<int>(),
                    RowReached = p.RowReached,
                    PathType = p.PathType.ToString(),
                    EndpointHouse = p.EndpointHouse,
                    Description = p.Description ?? string.Empty
                }).ToList(),
                Notes = result.Notes ?? new List<string>()
            };
        }
    }
}
