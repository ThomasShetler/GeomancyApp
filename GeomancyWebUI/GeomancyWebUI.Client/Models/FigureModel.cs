namespace GeomancyWebUI.Client.Models
{
    public enum FigureInHouseStrength
    {
        StrongInHouse = 1,
        WeakInHouse = 2,
        Neutral = 3
    }

    public class FigureModel
    {
        public string Name { get; set; } = string.Empty;
        public string OtherNames { get; set; } = string.Empty;
        public string Quality { get; set; } = string.Empty;
        public string Keyword { get; set; } = string.Empty;
        public string Imagery { get; set; } = string.Empty;
        public string StrongHouse { get; set; } = string.Empty;
        public string WeakHouse { get; set; } = string.Empty;
        public string Planet { get; set; } = string.Empty;
        public string Sign { get; set; } = string.Empty;
        public string InnerEl { get; set; } = string.Empty;
        public string OuterEl { get; set; } = string.Empty;
        public string FireElement { get; set; } = string.Empty;
        public string AirElement { get; set; } = string.Empty;
        public string WaterElement { get; set; } = string.Empty;
        public string EarthElement { get; set; } = string.Empty;
        public string Anatomy { get; set; } = string.Empty;
        public string BodyType { get; set; } = string.Empty;
        public string CharacterType { get; set; } = string.Empty;
        public string Colors { get; set; } = string.Empty;
        public string Commentary { get; set; } = string.Empty;
        public string DivinatoryMeaning { get; set; } = string.Empty;
        public string ElementalPattern { get; set; } = string.Empty;
        public int HeadLine { get; set; }
        public int NeckLine { get; set; }
        public int BodyLine { get; set; }
        public int FootLine { get; set; }
        public FigureInHouseStrength HouseStrength { get; set; }
    }
}

