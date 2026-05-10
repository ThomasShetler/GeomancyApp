using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GeomancyAPI.Models
{
    public enum FigureInHouseStrength
    {
        StrongInHouse = 1,
        WeakInHouse = 2,
        Neutral = 3
    }
    // Request model for generating a single figure
    public class GenerateFigureRequest
    {
        [Required]
        [Range(1, 2)]
        public int HeadLine { get; set; }

        [Required]
        [Range(1, 2)]
        public int NeckLine { get; set; }

        [Required]
        [Range(1, 2)]
        public int BodyLine { get; set; }

        [Required]
        [Range(1, 2)]
        public int FootLine { get; set; }
    }

    // Request model for generating 4 figures
    public class GenerateFourFiguresRequest
    {
        [Required]
        public GenerateFigureRequest House1 { get; set; }

        [Required]
        public GenerateFigureRequest House2 { get; set; }

        [Required]
        public GenerateFigureRequest House3 { get; set; }

        [Required]
        public GenerateFigureRequest House4 { get; set; }
    }

    public class TraditionalSourceResponse
    {
        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("work")]
        public string Work { get; set; }

        [JsonProperty("section")]
        public string Section { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }
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
        public FigureInHouseStrength HouseStrength { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("core_meaning")]
        public List<string> CoreMeaning { get; set; } = new List<string>();

        [JsonProperty("favorable_for")]
        public List<string> FavorableFor { get; set; } = new List<string>();

        [JsonProperty("unfavorable_for")]
        public List<string> UnfavorableFor { get; set; } = new List<string>();

        [JsonProperty("elemental_synthesis")]
        public string ElementalSynthesis { get; set; }

        [JsonProperty("traditional_imagery")]
        public List<string> TraditionalImagery { get; set; } = new List<string>();

        [JsonProperty("interpretation")]
        public List<string> Interpretation { get; set; } = new List<string>();

        [JsonProperty("in_houses")]
        public Dictionary<string, string> InHouses { get; set; } = new Dictionary<string, string>();

        [JsonProperty("in_court_roles")]
        public Dictionary<string, string> InCourtRoles { get; set; } = new Dictionary<string, string>();

        [JsonProperty("modern_examples")]
        public List<string> ModernExamples { get; set; } = new List<string>();

        [JsonProperty("traditional_sources")]
        public List<TraditionalSourceResponse> TraditionalSources { get; set; } = new List<TraditionalSourceResponse>();
    }

    // Response model for a house in the chart
    public class HouseResponse
    {
        public int HouseNumber { get; set; }
        public FigureResponse Figure { get; set; }
    }

    // Response model for a triplicity
    public class TriplicityResponse
    {
        public int Number { get; set; }
        public FigureResponse FirstFigure { get; set; }
        public FigureResponse SecondFigure { get; set; }
        public FigureResponse ThirdFigure { get; set; }
        public string Description { get; set; }
    }

    // Response model for the complete house chart
    public class HouseChartResponse
    {
        public List<HouseResponse> Houses { get; set; }
        public FigureResponse RightWitness { get; set; }
        public FigureResponse LeftWitness { get; set; }
        public FigureResponse Judge { get; set; }
        public FigureResponse Sentence { get; set; }
        public List<TriplicityResponse> Triplicities { get; set; }
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
        public string AspectDirection { get; set; }
        public int TranslatorHouse { get; set; }
        public string TranslatorFigure { get; set; }
        public List<string> Notes { get; set; }
        public int QuerentHouse { get; set; }
        public int QuesitedHouse { get; set; }
        public string QuerentFigure { get; set; }
        public string QuesitedFigure { get; set; }
        public IndentScoreResponse Indentation { get; set; }
        public IndentScoreResponse TranslatorIndentation { get; set; }
        public bool MadeThroughCompany { get; set; }
        public string BaseMode { get; set; }
        public string CompanyType { get; set; } // Enum as string: None, Simple, DemiSimple, Compound, Capitular
        public string CompanyTypeDescription { get; set; } // Full description like "Company Compound (opposite figures)"
        public int FavorableScore { get; set; }
        public int UnfavorableScore { get; set; }
        public int NetScore { get; set; }
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
        public WeightJustificationDetail Justification { get; set; }
    }
    
    public class WeightJustificationDetail
    {
        public int BaseWeight { get; set; }
        public int TotalModifier { get; set; }
        public int FinalWeight { get; set; }
        public List<string> Justifications { get; set; } = new List<string>();
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

    public class AspectRecordResponse
    {
        public string AspectType { get; set; }
        public string Direction { get; set; }
        public int FromHouse { get; set; }
        public int ToHouse { get; set; }
        public string Description { get; set; }
        public bool MadeThroughCompany { get; set; }
        public bool IsMajorAspect { get; set; }
        public int FavorableScore { get; set; }
        public int UnfavorableScore { get; set; }
    }

    public class PerfectionAnalysisResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<PerfectionResponse> Perfections { get; set; } = new List<PerfectionResponse>();
        public List<PerfectionResponse> Denials { get; set; } = new List<PerfectionResponse>();
        public List<AspectRecordResponse> PositiveAspects { get; set; } = new List<AspectRecordResponse>();
        public List<AspectRecordResponse> NegativeAspects { get; set; } = new List<AspectRecordResponse>();
        public int TotalFavorableScore { get; set; }
        public int TotalUnfavorableScore { get; set; }
        public int NetScore { get; set; }
        public int QuerentHouse { get; set; }
        public int QuesitedHouse { get; set; }
    }

    public class WayOfPointsPathResponse
    {
        public List<int> Houses { get; set; } = new List<int>();
        public int RowReached { get; set; }
        public string PathType { get; set; }
        public int EndpointHouse { get; set; }
        public string Description { get; set; }
    }

    public class WayOfPointsResultResponse
    {
        public string WayName { get; set; }
        public string LineType { get; set; }
        public bool CanBeEstablished { get; set; }
        public List<WayOfPointsPathResponse> AllPaths { get; set; } = new List<WayOfPointsPathResponse>();
        public List<WayOfPointsPathResponse> StrongPaths { get; set; } = new List<WayOfPointsPathResponse>();
        public List<WayOfPointsPathResponse> PassivePaths { get; set; } = new List<WayOfPointsPathResponse>();
        public List<WayOfPointsPathResponse> StrongPassivePaths { get; set; } = new List<WayOfPointsPathResponse>();
        public List<WayOfPointsPathResponse> WeakerPassivePaths { get; set; } = new List<WayOfPointsPathResponse>();
        public List<string> Notes { get; set; } = new List<string>();
    }

    public class WayOfPointsAnalysisResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public WayOfPointsResultResponse FireWay { get; set; } = new WayOfPointsResultResponse();
        public WayOfPointsResultResponse AirWay { get; set; } = new WayOfPointsResultResponse();
        public WayOfPointsResultResponse WaterWay { get; set; } = new WayOfPointsResultResponse();
        public WayOfPointsResultResponse EarthWay { get; set; } = new WayOfPointsResultResponse();
    }

    // Reference directory entry for a single house (1-12), sourced from HouseAndCourtDirectory/HouseData.json
    public class HouseDirectoryEntryResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("house")]
        public string House { get; set; }

        [JsonProperty("traditional_name")]
        public string TraditionalName { get; set; }

        [JsonProperty("astrological_correspondence")]
        public string AstrologicalCorrespondence { get; set; }

        [JsonProperty("governs")]
        public List<string> Governs { get; set; } = new List<string>();

        [JsonProperty("significator_of_quesited_when")]
        public string SignificatorOfQuesitedWhen { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("interpretive_essence")]
        public string InterpretiveEssence { get; set; }

        [JsonProperty("key_significators")]
        public List<string> KeySignificators { get; set; } = new List<string>();

        [JsonProperty("common_misreadings")]
        public List<string> CommonMisreadings { get; set; } = new List<string>();

        [JsonProperty("figure_combinations_to_watch")]
        public string FigureCombinationsToWatch { get; set; }

        [JsonProperty("example_questions")]
        public List<string> ExampleQuestions { get; set; } = new List<string>();
    }

    // Reference directory entry for a single court placement (Right/Left Witness, Judge, Reconciler)
    public class CourtDirectoryEntryResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("placement")]
        public string Placement { get; set; }

        [JsonProperty("traditional_name")]
        public string TraditionalName { get; set; }

        [JsonProperty("generated_by")]
        public string GeneratedBy { get; set; }

        [JsonProperty("meaning")]
        public List<string> Meaning { get; set; } = new List<string>();

        [JsonProperty("utility_in_reading")]
        public string UtilityInReading { get; set; }

        [JsonProperty("essence")]
        public string Essence { get; set; }

        [JsonProperty("read_when")]
        public List<string> ReadWhen { get; set; } = new List<string>();

        [JsonProperty("pitfalls")]
        public List<string> Pitfalls { get; set; } = new List<string>();

        [JsonProperty("examples")]
        public List<string> Examples { get; set; } = new List<string>();
    }

    // Wrappers matching the JSON file shape on disk
    public class HouseDirectoryFile
    {
        [JsonProperty("HouseData")]
        public HouseDirectoryFilePayload HouseData { get; set; }
    }

    public class HouseDirectoryFilePayload
    {
        [JsonProperty("Houses")]
        public List<HouseDirectoryEntryResponse> Houses { get; set; } = new List<HouseDirectoryEntryResponse>();
    }

    public class CourtDirectoryFile
    {
        [JsonProperty("CourtData")]
        public CourtDirectoryFilePayload CourtData { get; set; }
    }

    public class CourtDirectoryFilePayload
    {
        [JsonProperty("Placements")]
        public List<CourtDirectoryEntryResponse> Placements { get; set; } = new List<CourtDirectoryEntryResponse>();
    }

    // Reference directory entry for a single Way Of Points element (Fire/Air/Water/Earth),
    // sourced from WayOfPointsDirectory/ElementData.json
    public class WayOfPointsElementEntryResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("element")]
        public string Element { get; set; }

        [JsonProperty("latin_name")]
        public string LatinName { get; set; }

        [JsonProperty("line_name")]
        public string LineName { get; set; }

        [JsonProperty("line_index")]
        public int LineIndex { get; set; }

        [JsonProperty("glyph")]
        public string Glyph { get; set; }

        [JsonProperty("color_hint")]
        public string ColorHint { get; set; }

        [JsonProperty("quality")]
        public string Quality { get; set; }

        [JsonProperty("polarity")]
        public string Polarity { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("when_established")]
        public string WhenEstablished { get; set; }

        [JsonProperty("when_not_established")]
        public string WhenNotEstablished { get; set; }

        [JsonProperty("endpoint_house_emphasis")]
        public string EndpointHouseEmphasis { get; set; }

        [JsonProperty("interpretation_paragraphs")]
        public List<string> InterpretationParagraphs { get; set; } = new List<string>();
    }

    // Reference directory entry for a single Way Of Points path type (Strong / StrongPassive
    // / WeakerPassive / Passive), sourced from WayOfPointsDirectory/PathTypeData.json
    public class WayOfPointsPathTypeEntryResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("glyph")]
        public string Glyph { get; set; }

        [JsonProperty("color_hint")]
        public string ColorHint { get; set; }

        [JsonProperty("badge_class")]
        public string BadgeClass { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("mechanism_summary")]
        public string MechanismSummary { get; set; }

        [JsonProperty("co_reads")]
        public string CoReads { get; set; }

        [JsonProperty("interpretation_paragraphs")]
        public List<string> InterpretationParagraphs { get; set; } = new List<string>();
    }

    // Wrappers matching the on-disk JSON shape for the WayOfPoints directory files
    public class WayOfPointsElementFile
    {
        [JsonProperty("ElementData")]
        public WayOfPointsElementFilePayload ElementData { get; set; }
    }

    public class WayOfPointsElementFilePayload
    {
        [JsonProperty("Elements")]
        public List<WayOfPointsElementEntryResponse> Elements { get; set; } = new List<WayOfPointsElementEntryResponse>();
    }

    public class WayOfPointsPathTypeFile
    {
        [JsonProperty("PathTypeData")]
        public WayOfPointsPathTypeFilePayload PathTypeData { get; set; }
    }

    public class WayOfPointsPathTypeFilePayload
    {
        [JsonProperty("PathTypes")]
        public List<WayOfPointsPathTypeEntryResponse> PathTypes { get; set; } = new List<WayOfPointsPathTypeEntryResponse>();
    }
}
