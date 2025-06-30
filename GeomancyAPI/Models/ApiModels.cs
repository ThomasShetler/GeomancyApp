using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace GeomancyAPI.Models
{
    // Request model for generating a single figure
    public class GenerateFigureRequest
    {
        [Microsoft.Build.Framework.Required]
        [Range(1, 2)]
        public int HeadLine { get; set; }

        [Microsoft.Build.Framework.Required]
        [Range(1, 2)]
        public int NeckLine { get; set; }

        [Microsoft.Build.Framework.Required]
        [Range(1, 2)]
        public int BodyLine { get; set; }

        [Microsoft.Build.Framework.Required]
        [Range(1, 2)]
        public int FootLine { get; set; }
    }

    // Request model for generating 4 figures
    public class GenerateFourFiguresRequest
    {
        [Microsoft.Build.Framework.Required]
        public GenerateFigureRequest House1 { get; set; }

        [Microsoft.Build.Framework.Required]
        public GenerateFigureRequest House2 { get; set; }

        [Microsoft.Build.Framework.Required]
        public GenerateFigureRequest House3 { get; set; }

        [Microsoft.Build.Framework.Required]
        public GenerateFigureRequest House4 { get; set; }
    }

    // Response model for a single figure
    public class FigureResponse
    {
        public string Name { get; set; }
        public string OtherNames { get; set; }
        public string Quality { get; set; }
        public string Keyword { get; set; }
        public string Imagery { get; set; }
        public string StrongHouse { get; set; }
        public string WeakHouse { get; set; }
        public string Planet { get; set; }
        public string Sign { get; set; }
        public string InnerEl { get; set; }
        public string OuterEl { get; set; }
        public string FireElement { get; set; }
        public string AirElement { get; set; }
        public string WaterElement { get; set; }
        public string EarthElement { get; set; }
        public string Anatomy { get; set; }
        public string BodyType { get; set; }
        public string CharacterType { get; set; }
        public string Colors { get; set; }
        public string Commentary { get; set; }
        public string DivinatoryMeaning { get; set; }
        public string ElementalPattern { get; set; }
        public int HeadLine { get; set; }
        public int NeckLine { get; set; }
        public int BodyLine { get; set; }
        public int FootLine { get; set; }
    }

    // Response model for a house in the chart
    public class HouseResponse
    {
        public int HouseNumber { get; set; }
        public FigureResponse Figure { get; set; }
    }

    // Response model for the complete house chart
    public class HouseChartResponse
    {
        public List<HouseResponse> Houses { get; set; }
        public FigureResponse RightWitness { get; set; }
        public FigureResponse LeftWitness { get; set; }
        public FigureResponse Judge { get; set; }
        public FigureResponse Sentence { get; set; }
        public string ChartSummary { get; set; }
        public bool IsComplete { get; set; }
        public DateTime GeneratedAt { get; set; }
    }

    // Response model for generating a single figure
    public class GenerateFigureResponse
    {
        public FigureResponse Figure { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    // Response model for generating 4 figures
    public class GenerateFourFiguresResponse
    {
        public List<FigureResponse> Figures { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    // Error response model
    public class ErrorResponse
    {
        public string Error { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class PerfectionRequest
    {
        public GenerateFourFiguresRequest Mothers { get; set; }
        public int QuerentHouse { get; set; }
        public int QuesitedHouse { get; set; }
    }

    public class PerfectionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Mode { get; set; }
        public string AspectBetweenSignificators { get; set; }
        public int TranslatorHouse { get; set; }
        public string TranslatorFigure { get; set; }
        public List<string> Notes { get; set; }
        public int QuerentHouse { get; set; }
        public int QuesitedHouse { get; set; }
        public string QuerentFigure { get; set; }
        public string QuesitedFigure { get; set; }
        public IndentScoreResponse Indentation { get; set; }
        public IndentScoreResponse TranslatorIndentation { get; set; }
    }

    public class IndentScoreResponse
    {
        public int QuerentHouse { get; set; }
        public int QuesitedHouse { get; set; }
        public int Index { get; set; }
        public int Bonus { get; set; }
    }

    public class MultiplePerfectionsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<PerfectionResponse> Perfections { get; set; }
        public int TotalPerfections { get; set; }
        public IndentScoreResponse Indentation { get; set; }
        public IndentScoreResponse TranslatorIndentation { get; set; }
    }

    public class IndentationResponse
    {
        public IndentScoreResponse Indentation { get; set; }
        public IndentScoreResponse TranslatorIndentation { get; set; }
    }

    public class AspectAnalysisResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int TotalScore { get; set; }
        public List<AspectDetail> Aspects { get; set; }
        public int TotalAspects { get; set; }
        public PolarityReport Polarity { get; set; }
        public Dictionary<string, int> AspectCounts { get; set; }
    }

    public class PolarityReport
    {
        public int Easy { get; set; }
        public int Hard { get; set; }
        public int Delta { get; set; }
        public double Percent { get; set; }
        public string Verdict { get; set; }
    }

    public class AspectDetail
    {
        public int FromHouse { get; set; }
        public int ToHouse { get; set; }
        public string AspectType { get; set; }
        public int Weight { get; set; }
        public string FromFigure { get; set; }
        public string ToFigure { get; set; }
    }

    public class AspectInfo
    {
        public int FromHouse { get; set; }
        public int ToHouse { get; set; }
        public string AspectType { get; set; }
        public string FromFigure { get; set; }
        public string ToFigure { get; set; }
    }

    public class AspectsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<AspectInfo> Aspects { get; set; } = new List<AspectInfo>();
        public int TotalAspects { get; set; }
        public string MinimumAspectType { get; set; }
    }
} 