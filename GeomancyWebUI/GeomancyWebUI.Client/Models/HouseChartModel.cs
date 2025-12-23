namespace GeomancyWebUI.Client.Models
{
    public class HouseChartModel
    {
        public List<HouseModel> Houses { get; set; } = new List<HouseModel>();
        public FigureModel? RightWitness { get; set; }
        public FigureModel? LeftWitness { get; set; }
        public FigureModel? Judge { get; set; }
        public FigureModel? Sentence { get; set; }
        public string ChartSummary { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}

