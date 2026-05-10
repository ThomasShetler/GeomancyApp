using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Services
{
    public class GeomancyApiService : IGeomancyService
    {
        private readonly HttpClient _httpClient;

        // Cached reference directories - immutable lookup data, fetched once per session.
        private List<HouseDirectoryEntry>? _housesDirectoryCache;
        private List<CourtDirectoryEntry>? _courtsDirectoryCache;
        private List<WayOfPointsElementEntry>? _wayOfPointsElementsCache;
        private List<WayOfPointsPathTypeEntry>? _wayOfPointsPathTypesCache;

        public GeomancyApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
            // Ensure BaseAddress is set correctly
            // The HttpClient BaseAddress should be set during registration
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("http://localhost:5000/api/geomancy");
            }
            else
            {
                // If BaseAddress is set but doesn't end with /api/geomancy, we need to fix it
                var baseAddress = _httpClient.BaseAddress.ToString();
                if (!baseAddress.EndsWith("/api/geomancy", StringComparison.OrdinalIgnoreCase) && 
                    !baseAddress.EndsWith("/api/geomancy/", StringComparison.OrdinalIgnoreCase))
                {
                    // If it's just the base URL, append /api/geomancy
                    if (baseAddress.EndsWith("/"))
                    {
                        _httpClient.BaseAddress = new Uri(baseAddress + "api/geomancy");
                    }
                    else
                    {
                        _httpClient.BaseAddress = new Uri(baseAddress + "/api/geomancy");
                    }
                }
            }
        }

        public async Task<HouseChartModel> GenerateChartAsync(GenerateFourFiguresRequest request)
        {
            // Construct the full URL to ensure it's correct
            var baseAddress = _httpClient.BaseAddress?.ToString() ?? "http://localhost:5000/api/geomancy";
            var endpoint = baseAddress.TrimEnd('/') + "/chart";
            
            // Use absolute URI to bypass service discovery issues
            var response = await _httpClient.PostAsJsonAsync(new Uri(endpoint), request);
            response.EnsureSuccessStatusCode();
            
            var apiResponse = await response.Content.ReadFromJsonAsync<HouseChartResponse>();
            return MapToHouseChartModel(apiResponse);
        }

        public async Task<FigureModel> GetFigureAsync(int headLine, int neckLine, int bodyLine, int footLine)
        {
            var request = new GenerateFigureRequest
            {
                HeadLine = headLine,
                NeckLine = neckLine,
                BodyLine = bodyLine,
                FootLine = footLine
            };
            
            // Use absolute URI to bypass service discovery issues
            var baseAddress = _httpClient.BaseAddress?.ToString() ?? "http://localhost:5000/api/geomancy";
            var endpoint = baseAddress.TrimEnd('/') + "/figure";
            var response = await _httpClient.PostAsJsonAsync(new Uri(endpoint), request);
            response.EnsureSuccessStatusCode();
            
            var apiResponse = await response.Content.ReadFromJsonAsync<GenerateFigureResponse>();
            return MapToFigureModel(apiResponse?.Figure);
        }

        public async Task<FigureModel> GetFigureByNameAsync(string figureName)
        {
            // Use absolute URI to bypass service discovery issues
            var baseAddress = _httpClient.BaseAddress?.ToString() ?? "http://localhost:5000/api/geomancy";
            var endpoint = baseAddress.TrimEnd('/') + $"/figure/{Uri.EscapeDataString(figureName)}";
            var response = await _httpClient.GetAsync(new Uri(endpoint));
            response.EnsureSuccessStatusCode();
            
            var apiResponse = await response.Content.ReadFromJsonAsync<GenerateFigureResponse>();
            return MapToFigureModel(apiResponse?.Figure);
        }

        public async Task<List<PerfectionModel>> CalculatePerfectionAsync(PerfectionRequestModel request)
        {
            // Use absolute URI to bypass service discovery issues
            var baseAddress = _httpClient.BaseAddress?.ToString() ?? "http://localhost:5000/api/geomancy";
            var endpoint = baseAddress.TrimEnd('/') + "/perfection";
            
            var apiRequest = new
            {
                Mothers = new
                {
                    House1 = new { request.Mothers.House1.HeadLine, request.Mothers.House1.NeckLine, request.Mothers.House1.BodyLine, request.Mothers.House1.FootLine },
                    House2 = new { request.Mothers.House2.HeadLine, request.Mothers.House2.NeckLine, request.Mothers.House2.BodyLine, request.Mothers.House2.FootLine },
                    House3 = new { request.Mothers.House3.HeadLine, request.Mothers.House3.NeckLine, request.Mothers.House3.BodyLine, request.Mothers.House3.FootLine },
                    House4 = new { request.Mothers.House4.HeadLine, request.Mothers.House4.NeckLine, request.Mothers.House4.BodyLine, request.Mothers.House4.FootLine }
                },
                request.QuerentHouse,
                request.QuesitedHouse
            };
            
            var response = await _httpClient.PostAsJsonAsync(new Uri(endpoint), apiRequest);
            response.EnsureSuccessStatusCode();
            
            var apiResponse = await response.Content.ReadFromJsonAsync<MultiplePerfectionsResponse>();
            if (apiResponse?.Perfections == null)
                return new List<PerfectionModel>();
            
            return apiResponse.Perfections.Select(MapToPerfectionModel).ToList();
        }

        public async Task<PerfectionAnalysisModel> AnalyzePerfectionsAsync(PerfectionRequestModel request)
        {
            // Use absolute URI to bypass service discovery issues
            var baseAddress = _httpClient.BaseAddress?.ToString() ?? "http://localhost:5000/api/geomancy";
            var endpoint = baseAddress.TrimEnd('/') + "/perfection/analyze";
            
            var apiRequest = new
            {
                Mothers = new
                {
                    House1 = new { request.Mothers.House1.HeadLine, request.Mothers.House1.NeckLine, request.Mothers.House1.BodyLine, request.Mothers.House1.FootLine },
                    House2 = new { request.Mothers.House2.HeadLine, request.Mothers.House2.NeckLine, request.Mothers.House2.BodyLine, request.Mothers.House2.FootLine },
                    House3 = new { request.Mothers.House3.HeadLine, request.Mothers.House3.NeckLine, request.Mothers.House3.BodyLine, request.Mothers.House3.FootLine },
                    House4 = new { request.Mothers.House4.HeadLine, request.Mothers.House4.NeckLine, request.Mothers.House4.BodyLine, request.Mothers.House4.FootLine }
                },
                request.QuerentHouse,
                request.QuesitedHouse
            };
            
            var response = await _httpClient.PostAsJsonAsync(new Uri(endpoint), apiRequest);
            response.EnsureSuccessStatusCode();
            
            var apiResponse = await response.Content.ReadFromJsonAsync<PerfectionAnalysisResponse>();
            if (apiResponse == null)
                return new PerfectionAnalysisModel();
            
            return new PerfectionAnalysisModel
            {
                Success = apiResponse.Success,
                Message = apiResponse.Message ?? string.Empty,
                Perfections = apiResponse.Perfections?.Select(MapToPerfectionModel).ToList() ?? new List<PerfectionModel>(),
                Denials = apiResponse.Denials?.Select(MapToPerfectionModel).ToList() ?? new List<PerfectionModel>(),
                PositiveAspects = apiResponse.PositiveAspects?.Select(a => new AspectRecordModel
                {
                    AspectType = a.AspectType ?? string.Empty,
                    Direction = a.Direction ?? string.Empty,
                    FromHouse = a.FromHouse,
                    ToHouse = a.ToHouse,
                    Description = a.Description ?? string.Empty,
                    MadeThroughCompany = a.MadeThroughCompany,
                    IsMajorAspect = a.IsMajorAspect,
                    FavorableScore = a.FavorableScore,
                    UnfavorableScore = a.UnfavorableScore
                }).ToList() ?? new List<AspectRecordModel>(),
                NegativeAspects = apiResponse.NegativeAspects?.Select(a => new AspectRecordModel
                {
                    AspectType = a.AspectType ?? string.Empty,
                    Direction = a.Direction ?? string.Empty,
                    FromHouse = a.FromHouse,
                    ToHouse = a.ToHouse,
                      Description = a.Description ?? string.Empty,
                      MadeThroughCompany = a.MadeThroughCompany,
                      IsMajorAspect = a.IsMajorAspect,
                      FavorableScore = a.FavorableScore,
                      UnfavorableScore = a.UnfavorableScore
                  }).ToList() ?? new List<AspectRecordModel>(),
                  TotalFavorableScore = apiResponse.TotalFavorableScore,
                TotalUnfavorableScore = apiResponse.TotalUnfavorableScore,
                NetScore = apiResponse.NetScore,
                QuerentHouse = apiResponse.QuerentHouse,
                QuesitedHouse = apiResponse.QuesitedHouse
            };
        }

        public async Task<AspectAnalysisModel> GetAspectAnalysisAsync(GenerateFourFiguresRequest request)
        {
            // First generate the figures to get their names
            var figure1 = await GetFigureAsync(request.House1.HeadLine, request.House1.NeckLine, request.House1.BodyLine, request.House1.FootLine);
            var figure2 = await GetFigureAsync(request.House2.HeadLine, request.House2.NeckLine, request.House2.BodyLine, request.House2.FootLine);
            var figure3 = await GetFigureAsync(request.House3.HeadLine, request.House3.NeckLine, request.House3.BodyLine, request.House3.FootLine);
            var figure4 = await GetFigureAsync(request.House4.HeadLine, request.House4.NeckLine, request.House4.BodyLine, request.House4.FootLine);
            
            // Use figure names for the endpoint
            var baseAddress = _httpClient.BaseAddress?.ToString() ?? "http://localhost:5000/api/geomancy";
            var endpoint = $"{baseAddress.TrimEnd('/')}/aspect-analysis/figures" +
                $"?firstMother={Uri.EscapeDataString(figure1.Name)}" +
                $"&secondMother={Uri.EscapeDataString(figure2.Name)}" +
                $"&thirdMother={Uri.EscapeDataString(figure3.Name)}" +
                $"&fourthMother={Uri.EscapeDataString(figure4.Name)}";
            
            var response = await _httpClient.GetAsync(new Uri(endpoint));
            response.EnsureSuccessStatusCode();
            
            var apiResponse = await response.Content.ReadFromJsonAsync<AspectAnalysisResponse>();
            return MapToAspectAnalysisModel(apiResponse);
        }

        public async Task<WayOfPointsAnalysisModel> CalculateWayOfPointsAsync(GenerateFourFiguresRequest request)
        {
            // Use absolute URI to bypass service discovery issues
            var baseAddress = _httpClient.BaseAddress?.ToString() ?? "http://localhost:5000/api/geomancy";
            var endpoint = baseAddress.TrimEnd('/') + "/way-of-points";
            
            var apiRequest = new
            {
                House1 = new { request.House1.HeadLine, request.House1.NeckLine, request.House1.BodyLine, request.House1.FootLine },
                House2 = new { request.House2.HeadLine, request.House2.NeckLine, request.House2.BodyLine, request.House2.FootLine },
                House3 = new { request.House3.HeadLine, request.House3.NeckLine, request.House3.BodyLine, request.House3.FootLine },
                House4 = new { request.House4.HeadLine, request.House4.NeckLine, request.House4.BodyLine, request.House4.FootLine }
            };
            
            var response = await _httpClient.PostAsJsonAsync(new Uri(endpoint), apiRequest);
            response.EnsureSuccessStatusCode();
            
            var apiResponse = await response.Content.ReadFromJsonAsync<WayOfPointsAnalysisResponse>();
            if (apiResponse == null)
                return new WayOfPointsAnalysisModel();
            
            return new WayOfPointsAnalysisModel
            {
                Success = apiResponse.Success,
                Message = apiResponse.Message ?? string.Empty,
                FireWay = MapToWayOfPointsResultModel(apiResponse.FireWay),
                AirWay = MapToWayOfPointsResultModel(apiResponse.AirWay),
                WaterWay = MapToWayOfPointsResultModel(apiResponse.WaterWay),
                EarthWay = MapToWayOfPointsResultModel(apiResponse.EarthWay)
            };
        }

        public async Task<List<HouseDirectoryEntry>> GetHousesDirectoryAsync()
        {
            if (_housesDirectoryCache != null) return _housesDirectoryCache;

            var baseAddress = _httpClient.BaseAddress?.ToString() ?? "http://localhost:5000/api/geomancy";
            var endpoint = baseAddress.TrimEnd('/') + "/directory/houses";

            var response = await _httpClient.GetAsync(new Uri(endpoint));
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<List<HouseDirectoryEntryResponse>>();
            _housesDirectoryCache = apiResponse?.Select(MapToHouseDirectoryEntry).ToList() ?? new List<HouseDirectoryEntry>();
            return _housesDirectoryCache;
        }

        public async Task<List<CourtDirectoryEntry>> GetCourtsDirectoryAsync()
        {
            if (_courtsDirectoryCache != null) return _courtsDirectoryCache;

            var baseAddress = _httpClient.BaseAddress?.ToString() ?? "http://localhost:5000/api/geomancy";
            var endpoint = baseAddress.TrimEnd('/') + "/directory/courts";

            var response = await _httpClient.GetAsync(new Uri(endpoint));
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<List<CourtDirectoryEntryResponse>>();
            _courtsDirectoryCache = apiResponse?.Select(MapToCourtDirectoryEntry).ToList() ?? new List<CourtDirectoryEntry>();
            return _courtsDirectoryCache;
        }

        public async Task<List<WayOfPointsElementEntry>> GetWayOfPointsElementsDirectoryAsync()
        {
            if (_wayOfPointsElementsCache != null) return _wayOfPointsElementsCache;

            var baseAddress = _httpClient.BaseAddress?.ToString() ?? "http://localhost:5000/api/geomancy";
            var endpoint = baseAddress.TrimEnd('/') + "/way-of-points/elements";

            var response = await _httpClient.GetAsync(new Uri(endpoint));
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<List<WayOfPointsElementEntryResponse>>();
            _wayOfPointsElementsCache = apiResponse?.Select(MapToWayOfPointsElementEntry).ToList() ?? new List<WayOfPointsElementEntry>();
            return _wayOfPointsElementsCache;
        }

        public async Task<List<WayOfPointsPathTypeEntry>> GetWayOfPointsPathTypesDirectoryAsync()
        {
            if (_wayOfPointsPathTypesCache != null) return _wayOfPointsPathTypesCache;

            var baseAddress = _httpClient.BaseAddress?.ToString() ?? "http://localhost:5000/api/geomancy";
            var endpoint = baseAddress.TrimEnd('/') + "/way-of-points/path-types";

            var response = await _httpClient.GetAsync(new Uri(endpoint));
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<List<WayOfPointsPathTypeEntryResponse>>();
            _wayOfPointsPathTypesCache = apiResponse?.Select(MapToWayOfPointsPathTypeEntry).ToList() ?? new List<WayOfPointsPathTypeEntry>();
            return _wayOfPointsPathTypesCache;
        }

        private static HouseDirectoryEntry MapToHouseDirectoryEntry(HouseDirectoryEntryResponse src)
        {
            return new HouseDirectoryEntry
            {
                Id = src.Id,
                House = src.House ?? string.Empty,
                TraditionalName = src.TraditionalName ?? string.Empty,
                AstrologicalCorrespondence = src.AstrologicalCorrespondence ?? string.Empty,
                Governs = src.Governs ?? new List<string>(),
                SignificatorOfQuesitedWhen = src.SignificatorOfQuesitedWhen ?? string.Empty,
                Notes = src.Notes ?? string.Empty,
                InterpretiveEssence = src.InterpretiveEssence ?? string.Empty,
                KeySignificators = src.KeySignificators ?? new List<string>(),
                CommonMisreadings = src.CommonMisreadings ?? new List<string>(),
                FigureCombinationsToWatch = src.FigureCombinationsToWatch ?? string.Empty,
                ExampleQuestions = src.ExampleQuestions ?? new List<string>()
            };
        }

        private static CourtDirectoryEntry MapToCourtDirectoryEntry(CourtDirectoryEntryResponse src)
        {
            return new CourtDirectoryEntry
            {
                Id = src.Id,
                Placement = src.Placement ?? string.Empty,
                TraditionalName = src.TraditionalName ?? string.Empty,
                GeneratedBy = src.GeneratedBy ?? string.Empty,
                Meaning = src.Meaning ?? new List<string>(),
                UtilityInReading = src.UtilityInReading ?? string.Empty,
                Essence = src.Essence ?? string.Empty,
                ReadWhen = src.ReadWhen ?? new List<string>(),
                Pitfalls = src.Pitfalls ?? new List<string>(),
                Examples = src.Examples ?? new List<string>()
            };
        }

        private static WayOfPointsElementEntry MapToWayOfPointsElementEntry(WayOfPointsElementEntryResponse src)
        {
            return new WayOfPointsElementEntry
            {
                Id = src.Id,
                Element = src.Element ?? string.Empty,
                LatinName = src.LatinName ?? string.Empty,
                LineName = src.LineName ?? string.Empty,
                LineIndex = src.LineIndex,
                Glyph = src.Glyph ?? string.Empty,
                ColorHint = src.ColorHint ?? string.Empty,
                Quality = src.Quality ?? string.Empty,
                Polarity = src.Polarity ?? string.Empty,
                Tagline = src.Tagline ?? string.Empty,
                Domain = src.Domain ?? string.Empty,
                WhenEstablished = src.WhenEstablished ?? string.Empty,
                WhenNotEstablished = src.WhenNotEstablished ?? string.Empty,
                EndpointHouseEmphasis = src.EndpointHouseEmphasis ?? string.Empty,
                InterpretationParagraphs = src.InterpretationParagraphs ?? new List<string>()
            };
        }

        private static WayOfPointsPathTypeEntry MapToWayOfPointsPathTypeEntry(WayOfPointsPathTypeEntryResponse src)
        {
            return new WayOfPointsPathTypeEntry
            {
                Id = src.Id ?? string.Empty,
                Name = src.Name ?? string.Empty,
                Glyph = src.Glyph ?? string.Empty,
                ColorHint = src.ColorHint ?? string.Empty,
                BadgeClass = src.BadgeClass ?? string.Empty,
                Tagline = src.Tagline ?? string.Empty,
                MechanismSummary = src.MechanismSummary ?? string.Empty,
                CoReads = src.CoReads ?? string.Empty,
                InterpretationParagraphs = src.InterpretationParagraphs ?? new List<string>()
            };
        }

        private AspectAnalysisModel MapToAspectAnalysisModel(AspectAnalysisResponse? apiResponse)
        {
            if (apiResponse == null)
                return new AspectAnalysisModel();

            return new AspectAnalysisModel
            {
                Success = apiResponse.Success,
                Message = apiResponse.Message ?? string.Empty,
                TotalScore = apiResponse.TotalScore,
                TotalAspects = apiResponse.TotalAspects,
                Aspects = apiResponse.Aspects?.Select(a => new AspectDetailModel
                {
                    FromHouse = a.FromHouse,
                    ToHouse = a.ToHouse,
                    AspectType = a.AspectType ?? string.Empty,
                    Weight = a.Weight,
                    FromFigure = a.FromFigure ?? string.Empty,
                    ToFigure = a.ToFigure ?? string.Empty
                }).ToList() ?? new List<AspectDetailModel>(),
                Polarity = apiResponse.Polarity != null ? new PolarityReportModel
                {
                    Easy = apiResponse.Polarity.Easy,
                    Hard = apiResponse.Polarity.Hard,
                    Delta = apiResponse.Polarity.Delta,
                    Percent = apiResponse.Polarity.Percent,
                    Verdict = apiResponse.Polarity.Verdict ?? string.Empty
                } : null,
                AspectCounts = apiResponse.AspectCounts
            };
        }

        private FigureModel MapToFigureModel(FigureResponse? apiFigure)
        {
            if (apiFigure == null)
                return new FigureModel();

            return new FigureModel
            {
                Name = apiFigure.Name ?? string.Empty,
                OtherNames = apiFigure.OtherNames ?? string.Empty,
                Quality = apiFigure.Quality ?? string.Empty,
                Keyword = apiFigure.Keyword ?? string.Empty,
                Imagery = apiFigure.Imagery ?? string.Empty,
                StrongHouse = apiFigure.StrongHouse ?? string.Empty,
                WeakHouse = apiFigure.WeakHouse ?? string.Empty,
                Planet = apiFigure.Planet ?? string.Empty,
                Sign = apiFigure.Sign ?? string.Empty,
                InnerEl = apiFigure.InnerEl ?? string.Empty,
                OuterEl = apiFigure.OuterEl ?? string.Empty,
                FireElement = apiFigure.FireElement ?? string.Empty,
                AirElement = apiFigure.AirElement ?? string.Empty,
                WaterElement = apiFigure.WaterElement ?? string.Empty,
                EarthElement = apiFigure.EarthElement ?? string.Empty,
                Anatomy = apiFigure.Anatomy ?? string.Empty,
                BodyType = apiFigure.BodyType ?? string.Empty,
                CharacterType = apiFigure.CharacterType ?? string.Empty,
                Colors = apiFigure.Colors ?? string.Empty,
                Commentary = apiFigure.Commentary ?? string.Empty,
                DivinatoryMeaning = apiFigure.DivinatoryMeaning ?? string.Empty,
                ElementalPattern = apiFigure.ElementalPattern ?? string.Empty,
                Tagline = apiFigure.Tagline ?? string.Empty,
                CoreMeaning = apiFigure.CoreMeaning ?? new List<string>(),
                FavorableFor = apiFigure.FavorableFor ?? new List<string>(),
                UnfavorableFor = apiFigure.UnfavorableFor ?? new List<string>(),
                ElementalSynthesis = apiFigure.ElementalSynthesis ?? string.Empty,
                TraditionalImagery = apiFigure.TraditionalImagery ?? new List<string>(),
                Interpretation = apiFigure.Interpretation ?? new List<string>(),
                InHouses = apiFigure.InHouses ?? new Dictionary<string, string>(),
                InCourtRoles = apiFigure.InCourtRoles ?? new Dictionary<string, string>(),
                ModernExamples = apiFigure.ModernExamples ?? new List<string>(),
                TraditionalSources = apiFigure.TraditionalSources?.Select(s => new TraditionalSourceModel
                {
                    Author = s.Author ?? string.Empty,
                    Work = s.Work ?? string.Empty,
                    Section = s.Section ?? string.Empty,
                    Year = s.Year
                }).ToList() ?? new List<TraditionalSourceModel>(),
                HeadLine = apiFigure.HeadLine,
                NeckLine = apiFigure.NeckLine,
                BodyLine = apiFigure.BodyLine,
                FootLine = apiFigure.FootLine,
                HouseStrength = (FigureInHouseStrength)(int)apiFigure.HouseStrength
            };
        }

        private HouseChartModel MapToHouseChartModel(HouseChartResponse? apiChart)
        {
            if (apiChart == null)
                return new HouseChartModel();

            return new HouseChartModel
            {
                Houses = apiChart.Houses?.Select(h => new HouseModel
                {
                    HouseNumber = h.HouseNumber,
                    Figure = MapToFigureModel(h.Figure)
                }).ToList() ?? new List<HouseModel>(),
                RightWitness = MapToFigureModel(apiChart.RightWitness),
                LeftWitness = MapToFigureModel(apiChart.LeftWitness),
                Judge = MapToFigureModel(apiChart.Judge),
                Sentence = MapToFigureModel(apiChart.Sentence),
                Triplicities = apiChart.Triplicities?.Select(t => new TriplicityModel
                {
                    Number = t.Number,
                    FirstFigure = MapToFigureModel(t.FirstFigure),
                    SecondFigure = MapToFigureModel(t.SecondFigure),
                    ThirdFigure = MapToFigureModel(t.ThirdFigure),
                    Description = t.Description ?? string.Empty
                }).ToList() ?? new List<TriplicityModel>(),
                ChartSummary = apiChart.ChartSummary ?? string.Empty,
                IsComplete = apiChart.IsComplete,
                GeneratedAt = apiChart.GeneratedAt
            };
        }

        // API Response models (mirroring the API structure)
        private class FigureResponse
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
            public int HouseStrength { get; set; }

            [JsonPropertyName("tagline")]
            public string? Tagline { get; set; }

            [JsonPropertyName("core_meaning")]
            public List<string>? CoreMeaning { get; set; }

            [JsonPropertyName("favorable_for")]
            public List<string>? FavorableFor { get; set; }

            [JsonPropertyName("unfavorable_for")]
            public List<string>? UnfavorableFor { get; set; }

            [JsonPropertyName("elemental_synthesis")]
            public string? ElementalSynthesis { get; set; }

            [JsonPropertyName("traditional_imagery")]
            public List<string>? TraditionalImagery { get; set; }

            [JsonPropertyName("interpretation")]
            public List<string>? Interpretation { get; set; }

            [JsonPropertyName("in_houses")]
            public Dictionary<string, string>? InHouses { get; set; }

            [JsonPropertyName("in_court_roles")]
            public Dictionary<string, string>? InCourtRoles { get; set; }

            [JsonPropertyName("modern_examples")]
            public List<string>? ModernExamples { get; set; }

            [JsonPropertyName("traditional_sources")]
            public List<TraditionalSourceWire>? TraditionalSources { get; set; }
        }

        private class TraditionalSourceWire
        {
            [JsonPropertyName("author")]
            public string? Author { get; set; }

            [JsonPropertyName("work")]
            public string? Work { get; set; }

            [JsonPropertyName("section")]
            public string? Section { get; set; }

            [JsonPropertyName("year")]
            public int? Year { get; set; }
        }

        private class HouseResponse
        {
            public int HouseNumber { get; set; }
            public FigureResponse? Figure { get; set; }
        }

        private class TriplicityResponse
        {
            public int Number { get; set; }
            public FigureResponse? FirstFigure { get; set; }
            public FigureResponse? SecondFigure { get; set; }
            public FigureResponse? ThirdFigure { get; set; }
            public string? Description { get; set; }
        }

        private class HouseChartResponse
        {
            public List<HouseResponse>? Houses { get; set; }
            public FigureResponse? RightWitness { get; set; }
            public FigureResponse? LeftWitness { get; set; }
            public FigureResponse? Judge { get; set; }
            public FigureResponse? Sentence { get; set; }
            public List<TriplicityResponse>? Triplicities { get; set; }
            public string ChartSummary { get; set; } = string.Empty;
            public bool IsComplete { get; set; }
            public DateTime GeneratedAt { get; set; }
        }

        private class GenerateFigureResponse
        {
            public FigureResponse? Figure { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
        }

        private class PerfectionResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public string Mode { get; set; } = string.Empty;
            public string AspectBetweenSignificators { get; set; } = string.Empty;
            public string AspectDirection { get; set; } = string.Empty;
            public int TranslatorHouse { get; set; }
            public string TranslatorFigure { get; set; } = string.Empty;
            public List<string>? Notes { get; set; }
            public int QuerentHouse { get; set; }
            public int QuesitedHouse { get; set; }
            public string QuerentFigure { get; set; } = string.Empty;
            public string QuesitedFigure { get; set; } = string.Empty;
            public IndentScoreResponse? Indentation { get; set; }
            public IndentScoreResponse? TranslatorIndentation { get; set; }
            public bool MadeThroughCompany { get; set; }
            public string BaseMode { get; set; } = string.Empty;
            public string CompanyType { get; set; } = string.Empty;
            public string CompanyTypeDescription { get; set; } = string.Empty;
            public int FavorableScore { get; set; }
            public int UnfavorableScore { get; set; }
            public int NetScore { get; set; }
        }

        private class IndentScoreResponse
        {
            public int QuerentHouse { get; set; }
            public int QuesitedHouse { get; set; }
            public int Index { get; set; }
            public int Bonus { get; set; }
            public int Total { get; set; }
        }

        private class AspectAnalysisResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public int TotalScore { get; set; }
            public List<AspectDetailResponse>? Aspects { get; set; }
            public int TotalAspects { get; set; }
            public PolarityReportResponse? Polarity { get; set; }
            public Dictionary<string, int>? AspectCounts { get; set; }
        }

        private class PolarityReportResponse
        {
            public int Easy { get; set; }
            public int Hard { get; set; }
            public int Delta { get; set; }
            public double Percent { get; set; }
            public string Verdict { get; set; } = string.Empty;
        }

        private class AspectDetailResponse
        {
            public int FromHouse { get; set; }
            public int ToHouse { get; set; }
            public string AspectType { get; set; } = string.Empty;
            public int Weight { get; set; }
            public string FromFigure { get; set; } = string.Empty;
            public string ToFigure { get; set; } = string.Empty;
        }

        private class AspectRecordResponse
        {
            public string? AspectType { get; set; }
            public string? Direction { get; set; }
            public int FromHouse { get; set; }
            public int ToHouse { get; set; }
            public string? Description { get; set; }
            public bool MadeThroughCompany { get; set; }
            public bool IsMajorAspect { get; set; }
            public int FavorableScore { get; set; }
            public int UnfavorableScore { get; set; }
        }

        private class PerfectionAnalysisResponse
        {
            public bool Success { get; set; }
            public string? Message { get; set; }
            public List<PerfectionResponse>? Perfections { get; set; }
            public List<PerfectionResponse>? Denials { get; set; }
            public List<AspectRecordResponse>? PositiveAspects { get; set; }
            public List<AspectRecordResponse>? NegativeAspects { get; set; }
            public int TotalFavorableScore { get; set; }
            public int TotalUnfavorableScore { get; set; }
            public int NetScore { get; set; }
            public int QuerentHouse { get; set; }
            public int QuesitedHouse { get; set; }
        }

        private class MultiplePerfectionsResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public List<PerfectionResponse>? Perfections { get; set; }
            public int TotalPerfections { get; set; }
        }

        private class WayOfPointsPathResponse
        {
            public List<int>? Houses { get; set; }
            public int RowReached { get; set; }
            public string? PathType { get; set; }
            public int EndpointHouse { get; set; }
            public string? Description { get; set; }
        }

        private class WayOfPointsResultResponse
        {
            public string? WayName { get; set; }
            public string? LineType { get; set; }
            public bool CanBeEstablished { get; set; }
            public List<WayOfPointsPathResponse>? AllPaths { get; set; }
            public List<WayOfPointsPathResponse>? StrongPaths { get; set; }
            public List<WayOfPointsPathResponse>? PassivePaths { get; set; }
            public List<WayOfPointsPathResponse>? StrongPassivePaths { get; set; }
            public List<WayOfPointsPathResponse>? WeakerPassivePaths { get; set; }
            public List<string>? Notes { get; set; }
        }

        private class WayOfPointsAnalysisResponse
        {
            public bool Success { get; set; }
            public string? Message { get; set; }
            public WayOfPointsResultResponse? FireWay { get; set; }
            public WayOfPointsResultResponse? AirWay { get; set; }
            public WayOfPointsResultResponse? WaterWay { get; set; }
            public WayOfPointsResultResponse? EarthWay { get; set; }
        }

        // The API serializes these via Newtonsoft.Json with [JsonProperty("snake_case")]
        // attributes, so the wire format uses snake_case keys. System.Text.Json (used by
        // ReadFromJsonAsync) doesn't honor Newtonsoft attributes, so we declare the
        // matching keys explicitly with [JsonPropertyName] here.
        private class HouseDirectoryEntryResponse
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("house")]
            public string? House { get; set; }

            [JsonPropertyName("traditional_name")]
            public string? TraditionalName { get; set; }

            [JsonPropertyName("astrological_correspondence")]
            public string? AstrologicalCorrespondence { get; set; }

            [JsonPropertyName("governs")]
            public List<string>? Governs { get; set; }

            [JsonPropertyName("significator_of_quesited_when")]
            public string? SignificatorOfQuesitedWhen { get; set; }

            [JsonPropertyName("notes")]
            public string? Notes { get; set; }

            [JsonPropertyName("interpretive_essence")]
            public string? InterpretiveEssence { get; set; }

            [JsonPropertyName("key_significators")]
            public List<string>? KeySignificators { get; set; }

            [JsonPropertyName("common_misreadings")]
            public List<string>? CommonMisreadings { get; set; }

            [JsonPropertyName("figure_combinations_to_watch")]
            public string? FigureCombinationsToWatch { get; set; }

            [JsonPropertyName("example_questions")]
            public List<string>? ExampleQuestions { get; set; }
        }

        private class CourtDirectoryEntryResponse
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("placement")]
            public string? Placement { get; set; }

            [JsonPropertyName("traditional_name")]
            public string? TraditionalName { get; set; }

            [JsonPropertyName("generated_by")]
            public string? GeneratedBy { get; set; }

            [JsonPropertyName("meaning")]
            public List<string>? Meaning { get; set; }

            [JsonPropertyName("utility_in_reading")]
            public string? UtilityInReading { get; set; }

            [JsonPropertyName("essence")]
            public string? Essence { get; set; }

            [JsonPropertyName("read_when")]
            public List<string>? ReadWhen { get; set; }

            [JsonPropertyName("pitfalls")]
            public List<string>? Pitfalls { get; set; }

            [JsonPropertyName("examples")]
            public List<string>? Examples { get; set; }
        }

        private class WayOfPointsElementEntryResponse
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("element")]
            public string? Element { get; set; }

            [JsonPropertyName("latin_name")]
            public string? LatinName { get; set; }

            [JsonPropertyName("line_name")]
            public string? LineName { get; set; }

            [JsonPropertyName("line_index")]
            public int LineIndex { get; set; }

            [JsonPropertyName("glyph")]
            public string? Glyph { get; set; }

            [JsonPropertyName("color_hint")]
            public string? ColorHint { get; set; }

            [JsonPropertyName("quality")]
            public string? Quality { get; set; }

            [JsonPropertyName("polarity")]
            public string? Polarity { get; set; }

            [JsonPropertyName("tagline")]
            public string? Tagline { get; set; }

            [JsonPropertyName("domain")]
            public string? Domain { get; set; }

            [JsonPropertyName("when_established")]
            public string? WhenEstablished { get; set; }

            [JsonPropertyName("when_not_established")]
            public string? WhenNotEstablished { get; set; }

            [JsonPropertyName("endpoint_house_emphasis")]
            public string? EndpointHouseEmphasis { get; set; }

            [JsonPropertyName("interpretation_paragraphs")]
            public List<string>? InterpretationParagraphs { get; set; }
        }

        private class WayOfPointsPathTypeEntryResponse
        {
            [JsonPropertyName("id")]
            public string? Id { get; set; }

            [JsonPropertyName("name")]
            public string? Name { get; set; }

            [JsonPropertyName("glyph")]
            public string? Glyph { get; set; }

            [JsonPropertyName("color_hint")]
            public string? ColorHint { get; set; }

            [JsonPropertyName("badge_class")]
            public string? BadgeClass { get; set; }

            [JsonPropertyName("tagline")]
            public string? Tagline { get; set; }

            [JsonPropertyName("mechanism_summary")]
            public string? MechanismSummary { get; set; }

            [JsonPropertyName("co_reads")]
            public string? CoReads { get; set; }

            [JsonPropertyName("interpretation_paragraphs")]
            public List<string>? InterpretationParagraphs { get; set; }
        }

        private PerfectionModel MapToPerfectionModel(PerfectionResponse? apiResponse)
        {
            if (apiResponse == null)
                return new PerfectionModel();

            return new PerfectionModel
            {
                Success = apiResponse.Success,
                Message = apiResponse.Message ?? string.Empty,
                Mode = apiResponse.Mode ?? string.Empty,
                AspectBetweenSignificators = apiResponse.AspectBetweenSignificators ?? string.Empty,
                TranslatorHouse = apiResponse.TranslatorHouse,
                TranslatorFigure = apiResponse.TranslatorFigure ?? string.Empty,
                Notes = apiResponse.Notes ?? new List<string>(),
                QuerentHouse = apiResponse.QuerentHouse,
                QuesitedHouse = apiResponse.QuesitedHouse,
                QuerentFigure = apiResponse.QuerentFigure ?? string.Empty,
                QuesitedFigure = apiResponse.QuesitedFigure ?? string.Empty,
                Indentation = apiResponse.Indentation != null ? new IndentScoreModel
                {
                    QuerentHouse = apiResponse.Indentation.QuerentHouse,
                    QuesitedHouse = apiResponse.Indentation.QuesitedHouse,
                    Index = apiResponse.Indentation.Index,
                    Bonus = apiResponse.Indentation.Bonus,
                    Total = apiResponse.Indentation.Total
                } : null,
                TranslatorIndentation = apiResponse.TranslatorIndentation != null ? new IndentScoreModel
                {
                    QuerentHouse = apiResponse.TranslatorIndentation.QuerentHouse,
                    QuesitedHouse = apiResponse.TranslatorIndentation.QuesitedHouse,
                    Index = apiResponse.TranslatorIndentation.Index,
                    Bonus = apiResponse.TranslatorIndentation.Bonus,
                    Total = apiResponse.TranslatorIndentation.Total
                } : null,
                MadeThroughCompany = apiResponse.MadeThroughCompany,
                BaseMode = apiResponse.BaseMode ?? string.Empty,
                CompanyType = apiResponse.CompanyType ?? string.Empty,
                CompanyTypeDescription = apiResponse.CompanyTypeDescription ?? string.Empty,
                FavorableScore = apiResponse.FavorableScore,
                UnfavorableScore = apiResponse.UnfavorableScore,
                NetScore = apiResponse.NetScore
            };
        }

        private WayOfPointsPathModel MapToWayOfPointsPathModel(WayOfPointsPathResponse? apiPath)
        {
            if (apiPath == null)
                return new WayOfPointsPathModel();

            return new WayOfPointsPathModel
            {
                Houses = apiPath.Houses ?? new List<int>(),
                RowReached = apiPath.RowReached,
                PathType = apiPath.PathType ?? string.Empty,
                EndpointHouse = apiPath.EndpointHouse,
                Description = apiPath.Description ?? string.Empty
            };
        }

        private WayOfPointsResultModel MapToWayOfPointsResultModel(WayOfPointsResultResponse? apiResult)
        {
            if (apiResult == null)
                return new WayOfPointsResultModel();

            return new WayOfPointsResultModel
            {
                WayName = apiResult.WayName ?? string.Empty,
                LineType = apiResult.LineType ?? string.Empty,
                CanBeEstablished = apiResult.CanBeEstablished,
                AllPaths = apiResult.AllPaths?.Select(MapToWayOfPointsPathModel).ToList() ?? new List<WayOfPointsPathModel>(),
                StrongPaths = apiResult.StrongPaths?.Select(MapToWayOfPointsPathModel).ToList() ?? new List<WayOfPointsPathModel>(),
                PassivePaths = apiResult.PassivePaths?.Select(MapToWayOfPointsPathModel).ToList() ?? new List<WayOfPointsPathModel>(),
                StrongPassivePaths = apiResult.StrongPassivePaths?.Select(MapToWayOfPointsPathModel).ToList() ?? new List<WayOfPointsPathModel>(),
                WeakerPassivePaths = apiResult.WeakerPassivePaths?.Select(MapToWayOfPointsPathModel).ToList() ?? new List<WayOfPointsPathModel>(),
                Notes = apiResult.Notes ?? new List<string>()
            };
        }
    }
}

