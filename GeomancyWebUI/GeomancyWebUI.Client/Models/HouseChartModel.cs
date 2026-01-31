namespace GeomancyWebUI.Client.Models
{
    public class TriplicityModel
    {
        public int Number { get; set; }
        public FigureModel? FirstFigure { get; set; }
        public FigureModel? SecondFigure { get; set; }
        public FigureModel? ThirdFigure { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class HouseChartModel
    {
        public List<HouseModel> Houses { get; set; } = new List<HouseModel>();
        public FigureModel? RightWitness { get; set; }
        public FigureModel? LeftWitness { get; set; }
        public FigureModel? Judge { get; set; }
        public FigureModel? Sentence { get; set; }
        public List<TriplicityModel> Triplicities { get; set; } = new List<TriplicityModel>();
        public string ChartSummary { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}

