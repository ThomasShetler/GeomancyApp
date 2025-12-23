namespace GeomancyWebUI.Client.Models
{
    public class AspectAnalysisModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int TotalScore { get; set; }
        public List<AspectDetailModel> Aspects { get; set; } = new();
        public int TotalAspects { get; set; }
        public PolarityReportModel? Polarity { get; set; }
        public Dictionary<string, int>? AspectCounts { get; set; }
    }

    public class PolarityReportModel
    {
        public int Easy { get; set; }
        public int Hard { get; set; }
        public int Delta { get; set; }
        public double Percent { get; set; }
        public string Verdict { get; set; } = string.Empty;
    }

    public class AspectDetailModel
    {
        public int FromHouse { get; set; }
        public int ToHouse { get; set; }
        public string AspectType { get; set; } = string.Empty;
        public int Weight { get; set; }
        public string FromFigure { get; set; } = string.Empty;
        public string ToFigure { get; set; } = string.Empty;
    }
}

