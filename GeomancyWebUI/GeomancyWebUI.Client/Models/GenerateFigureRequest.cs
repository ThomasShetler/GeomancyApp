namespace GeomancyWebUI.Client.Models
{
    public class GenerateFigureRequest
    {
        public int HeadLine { get; set; }
        public int NeckLine { get; set; }
        public int BodyLine { get; set; }
        public int FootLine { get; set; }
    }

    public class GenerateFourFiguresRequest
    {
        public GenerateFigureRequest House1 { get; set; } = new();
        public GenerateFigureRequest House2 { get; set; } = new();
        public GenerateFigureRequest House3 { get; set; } = new();
        public GenerateFigureRequest House4 { get; set; } = new();
    }
}

