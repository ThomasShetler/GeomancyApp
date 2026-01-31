namespace GeomancyWebUI.Client.Models
{
    public class PerfectionRequestModel
    {
        public GenerateFourFiguresRequest Mothers { get; set; } = new();
        public int QuerentHouse { get; set; }
        public int QuesitedHouse { get; set; }
    }

    public class IndentScoreModel
    {
        public int QuerentHouse { get; set; }
        public int QuesitedHouse { get; set; }
        public int Index { get; set; }
        public int Bonus { get; set; }
        public int Total { get; set; }
    }

    public class PerfectionModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty;
        public string AspectBetweenSignificators { get; set; } = string.Empty;
        public string AspectDirection { get; set; } = string.Empty;
        public int TranslatorHouse { get; set; }
        public string TranslatorFigure { get; set; } = string.Empty;
        public List<string> Notes { get; set; } = new();
        public int QuerentHouse { get; set; }
        public int QuesitedHouse { get; set; }
        public string QuerentFigure { get; set; } = string.Empty;
        public string QuesitedFigure { get; set; } = string.Empty;
        public IndentScoreModel? Indentation { get; set; }
        public IndentScoreModel? TranslatorIndentation { get; set; }
        public bool MadeThroughCompany { get; set; }
        public string BaseMode { get; set; } = string.Empty;
        public string CompanyType { get; set; } = string.Empty; // None, Simple, DemiSimple, Compound, Capitular
        public string CompanyTypeDescription { get; set; } = string.Empty; // Full description like "Company Compound (opposite figures)"
        public int FavorableScore { get; set; }
        public int UnfavorableScore { get; set; }
        public int NetScore { get; set; }
    }

    public class AspectRecordModel
    {
        public string AspectType { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
        public int FromHouse { get; set; }
        public int ToHouse { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool MadeThroughCompany { get; set; }
        public bool IsMajorAspect { get; set; }
        public int FavorableScore { get; set; }
        public int UnfavorableScore { get; set; }
    }

    public class PerfectionAnalysisModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<PerfectionModel> Perfections { get; set; } = new();
        public List<PerfectionModel> Denials { get; set; } = new();
        public List<AspectRecordModel> PositiveAspects { get; set; } = new();
        public List<AspectRecordModel> NegativeAspects { get; set; } = new();
        public int TotalFavorableScore { get; set; }
        public int TotalUnfavorableScore { get; set; }
        public int NetScore { get; set; }
        public int QuerentHouse { get; set; }
        public int QuesitedHouse { get; set; }
    }
}

