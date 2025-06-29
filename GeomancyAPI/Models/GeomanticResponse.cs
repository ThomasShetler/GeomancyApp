namespace GeomancyAPI.Models
{
    /// <summary>
    /// Response model for a single geomantic figure
    /// </summary>
    public class FigureResponse
    {
        /// <summary>
        /// The name of the geomantic figure
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Alternative names for the figure
        /// </summary>
        public string OtherNames { get; set; } = string.Empty;

        /// <summary>
        /// The quality of the figure (Good, Evil, etc.)
        /// </summary>
        public string Quality { get; set; } = string.Empty;

        /// <summary>
        /// The keyword associated with the figure
        /// </summary>
        public string Keyword { get; set; } = string.Empty;

        /// <summary>
        /// The imagery description
        /// </summary>
        public string Imagery { get; set; } = string.Empty;

        /// <summary>
        /// The strong house for this figure
        /// </summary>
        public string StrongHouse { get; set; } = string.Empty;

        /// <summary>
        /// The weak house for this figure
        /// </summary>
        public string WeakHouse { get; set; } = string.Empty;

        /// <summary>
        /// The ruling planet
        /// </summary>
        public string Planet { get; set; } = string.Empty;

        /// <summary>
        /// The zodiac sign
        /// </summary>
        public string Sign { get; set; } = string.Empty;

        /// <summary>
        /// The inner element
        /// </summary>
        public string InnerElement { get; set; } = string.Empty;

        /// <summary>
        /// The outer element
        /// </summary>
        public string OuterElement { get; set; } = string.Empty;

        /// <summary>
        /// Fire element state
        /// </summary>
        public string FireElement { get; set; } = string.Empty;

        /// <summary>
        /// Air element state
        /// </summary>
        public string AirElement { get; set; } = string.Empty;

        /// <summary>
        /// Water element state
        /// </summary>
        public string WaterElement { get; set; } = string.Empty;

        /// <summary>
        /// Earth element state
        /// </summary>
        public string EarthElement { get; set; } = string.Empty;

        /// <summary>
        /// The anatomical associations
        /// </summary>
        public string Anatomy { get; set; } = string.Empty;

        /// <summary>
        /// The body type
        /// </summary>
        public string BodyType { get; set; } = string.Empty;

        /// <summary>
        /// The character type
        /// </summary>
        public string CharacterType { get; set; } = string.Empty;

        /// <summary>
        /// Associated colors
        /// </summary>
        public string Colors { get; set; } = string.Empty;

        /// <summary>
        /// Commentary on the figure
        /// </summary>
        public string Commentary { get; set; } = string.Empty;

        /// <summary>
        /// Divinatory meaning
        /// </summary>
        public string DivinatoryMeaning { get; set; } = string.Empty;

        /// <summary>
        /// Elemental pattern as string (e.g., "1-1-2-2")
        /// </summary>
        public string ElementalPattern { get; set; } = string.Empty;

        /// <summary>
        /// House number if part of a chart
        /// </summary>
        public int? HouseNumber { get; set; }
    }

    /// <summary>
    /// Response model for four geomantic figures
    /// </summary>
    public class FourFiguresResponse
    {
        /// <summary>
        /// First figure
        /// </summary>
        public FigureResponse Figure1 { get; set; } = new();

        /// <summary>
        /// Second figure
        /// </summary>
        public FigureResponse Figure2 { get; set; } = new();

        /// <summary>
        /// Third figure
        /// </summary>
        public FigureResponse Figure3 { get; set; } = new();

        /// <summary>
        /// Fourth figure
        /// </summary>
        public FigureResponse Figure4 { get; set; } = new();
    }

    /// <summary>
    /// Response model for a complete house chart
    /// </summary>
    public class HouseChartResponse
    {
        /// <summary>
        /// All 12 houses in the chart
        /// </summary>
        public Dictionary<string, FigureResponse> Houses { get; set; } = new();

        /// <summary>
        /// The right witness
        /// </summary>
        public FigureResponse RightWitness { get; set; } = new();

        /// <summary>
        /// The left witness
        /// </summary>
        public FigureResponse LeftWitness { get; set; } = new();

        /// <summary>
        /// The judge
        /// </summary>
        public FigureResponse Judge { get; set; } = new();

        /// <summary>
        /// The sentence/fallout
        /// </summary>
        public FigureResponse Sentence { get; set; } = new();

        /// <summary>
        /// Chart summary
        /// </summary>
        public string ChartSummary { get; set; } = string.Empty;

        /// <summary>
        /// Whether the chart is complete
        /// </summary>
        public bool IsComplete { get; set; }
    }

    /// <summary>
    /// Generic API response wrapper
    /// </summary>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Whether the request was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The response data
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Error message if any
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// Timestamp of the response
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
} 