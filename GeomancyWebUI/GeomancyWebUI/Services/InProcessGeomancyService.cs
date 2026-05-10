using GeomancyWebUI.Client.Models;
using GeomancyWebUI.Client.Services;
using ContractModels = GeomancyAPI.Models;
using Handlers = GeomancyAPI.Handlers.GeomancyHandlers;

namespace GeomancyWebUI.Services
{
    /// <summary>
    /// Server-side implementation of <see cref="IGeomancyService"/> that calls
    /// <see cref="GeomancyAPI.Handlers.GeomancyHandlers"/> directly - no HTTP loopback.
    /// This is the production default for Blazor Server interactive components.
    /// The WebAssembly client still goes through HTTP (same-origin to the embedded
    /// ASP.NET Core controller) since the WASM runtime obviously cannot run server code.
    /// </summary>
    public class InProcessGeomancyService : IGeomancyService
    {
        private List<HouseDirectoryEntry>? _housesDirectoryCache;
        private List<CourtDirectoryEntry>? _courtsDirectoryCache;
        private List<WayOfPointsElementEntry>? _wayOfPointsElementsCache;
        private List<WayOfPointsPathTypeEntry>? _wayOfPointsPathTypesCache;

        public Task<HouseChartModel> GenerateChartAsync(GenerateFourFiguresRequest request)
        {
            var contractsRequest = ToContractsFourFiguresRequest(request);
            var response = Handlers.GenerateHouseChart(contractsRequest);
            return Task.FromResult(MapToHouseChartModel(response));
        }

        public Task<FigureModel> GetFigureAsync(int headLine, int neckLine, int bodyLine, int footLine)
        {
            var contractsRequest = new ContractModels.GenerateFigureRequest
            {
                HeadLine = headLine,
                NeckLine = neckLine,
                BodyLine = bodyLine,
                FootLine = footLine,
            };
            var response = Handlers.GenerateFigure(contractsRequest);
            return Task.FromResult(MapToFigureModel(response.Figure));
        }

        public Task<FigureModel> GetFigureByNameAsync(string figureName)
        {
            var response = Handlers.GetFigureByName(figureName);
            return Task.FromResult(MapToFigureModel(response.Figure));
        }

        public Task<List<PerfectionModel>> CalculatePerfectionAsync(PerfectionRequestModel request)
        {
            var contractsRequest = ToContractsPerfectionRequest(request);
            var response = Handlers.CalculatePerfection(contractsRequest);
            var perfections = response.Perfections?.Select(MapToPerfectionModel).ToList()
                              ?? new List<PerfectionModel>();
            return Task.FromResult(perfections);
        }

        public Task<PerfectionAnalysisModel> AnalyzePerfectionsAsync(PerfectionRequestModel request)
        {
            var contractsRequest = ToContractsPerfectionRequest(request);
            var response = Handlers.AnalyzePerfections(contractsRequest);

            var model = new PerfectionAnalysisModel
            {
                Success = response.Success,
                Message = response.Message ?? string.Empty,
                Perfections = response.Perfections?.Select(MapToPerfectionModel).ToList()
                              ?? new List<PerfectionModel>(),
                Denials = response.Denials?.Select(MapToPerfectionModel).ToList()
                          ?? new List<PerfectionModel>(),
                PositiveAspects = response.PositiveAspects?.Select(MapToAspectRecordModel).ToList()
                                  ?? new List<AspectRecordModel>(),
                NegativeAspects = response.NegativeAspects?.Select(MapToAspectRecordModel).ToList()
                                  ?? new List<AspectRecordModel>(),
                TotalFavorableScore = response.TotalFavorableScore,
                TotalUnfavorableScore = response.TotalUnfavorableScore,
                NetScore = response.NetScore,
                QuerentHouse = response.QuerentHouse,
                QuesitedHouse = response.QuesitedHouse,
            };
            return Task.FromResult(model);
        }

        public async Task<AspectAnalysisModel> GetAspectAnalysisAsync(GenerateFourFiguresRequest request)
        {
            // Mirror the HTTP service's behaviour: derive figure names from the four
            // mothers, then call the figures-by-name aspect-analysis handler.
            var figure1 = await GetFigureAsync(request.House1.HeadLine, request.House1.NeckLine, request.House1.BodyLine, request.House1.FootLine);
            var figure2 = await GetFigureAsync(request.House2.HeadLine, request.House2.NeckLine, request.House2.BodyLine, request.House2.FootLine);
            var figure3 = await GetFigureAsync(request.House3.HeadLine, request.House3.NeckLine, request.House3.BodyLine, request.House3.FootLine);
            var figure4 = await GetFigureAsync(request.House4.HeadLine, request.House4.NeckLine, request.House4.BodyLine, request.House4.FootLine);

            var response = Handlers.GetAspectAnalysisFromFigures(figure1.Name, figure2.Name, figure3.Name, figure4.Name);
            return MapToAspectAnalysisModel(response);
        }

        public Task<WayOfPointsAnalysisModel> CalculateWayOfPointsAsync(GenerateFourFiguresRequest request)
        {
            var contractsRequest = ToContractsFourFiguresRequest(request);
            var response = Handlers.CalculateWayOfPoints(contractsRequest);

            var model = new WayOfPointsAnalysisModel
            {
                Success = response.Success,
                Message = response.Message ?? string.Empty,
                FireWay = MapToWayOfPointsResultModel(response.FireWay),
                AirWay = MapToWayOfPointsResultModel(response.AirWay),
                WaterWay = MapToWayOfPointsResultModel(response.WaterWay),
                EarthWay = MapToWayOfPointsResultModel(response.EarthWay),
            };
            return Task.FromResult(model);
        }

        public Task<List<HouseDirectoryEntry>> GetHousesDirectoryAsync()
        {
            if (_housesDirectoryCache != null) return Task.FromResult(_housesDirectoryCache);

            var entries = Handlers.GetHousesDirectory();
            _housesDirectoryCache = entries?.Select(MapToHouseDirectoryEntry).ToList()
                                    ?? new List<HouseDirectoryEntry>();
            return Task.FromResult(_housesDirectoryCache);
        }

        public Task<List<CourtDirectoryEntry>> GetCourtsDirectoryAsync()
        {
            if (_courtsDirectoryCache != null) return Task.FromResult(_courtsDirectoryCache);

            var entries = Handlers.GetCourtsDirectory();
            _courtsDirectoryCache = entries?.Select(MapToCourtDirectoryEntry).ToList()
                                    ?? new List<CourtDirectoryEntry>();
            return Task.FromResult(_courtsDirectoryCache);
        }

        public Task<List<WayOfPointsElementEntry>> GetWayOfPointsElementsDirectoryAsync()
        {
            if (_wayOfPointsElementsCache != null) return Task.FromResult(_wayOfPointsElementsCache);

            var entries = Handlers.GetWayOfPointsElementsDirectory();
            _wayOfPointsElementsCache = entries?.Select(MapToWayOfPointsElementEntry).ToList()
                                        ?? new List<WayOfPointsElementEntry>();
            return Task.FromResult(_wayOfPointsElementsCache);
        }

        public Task<List<WayOfPointsPathTypeEntry>> GetWayOfPointsPathTypesDirectoryAsync()
        {
            if (_wayOfPointsPathTypesCache != null) return Task.FromResult(_wayOfPointsPathTypesCache);

            var entries = Handlers.GetWayOfPointsPathTypesDirectory();
            _wayOfPointsPathTypesCache = entries?.Select(MapToWayOfPointsPathTypeEntry).ToList()
                                         ?? new List<WayOfPointsPathTypeEntry>();
            return Task.FromResult(_wayOfPointsPathTypesCache);
        }

        // ---------------------------------------------------------------------
        // Client request -> Contracts request mappers
        // ---------------------------------------------------------------------

        private static ContractModels.GenerateFigureRequest ToContractsFigureRequest(GenerateFigureRequest src)
            => new ContractModels.GenerateFigureRequest
            {
                HeadLine = src.HeadLine,
                NeckLine = src.NeckLine,
                BodyLine = src.BodyLine,
                FootLine = src.FootLine,
            };

        private static ContractModels.GenerateFourFiguresRequest ToContractsFourFiguresRequest(GenerateFourFiguresRequest src)
            => new ContractModels.GenerateFourFiguresRequest
            {
                House1 = ToContractsFigureRequest(src.House1),
                House2 = ToContractsFigureRequest(src.House2),
                House3 = ToContractsFigureRequest(src.House3),
                House4 = ToContractsFigureRequest(src.House4),
            };

        private static ContractModels.PerfectionRequest ToContractsPerfectionRequest(PerfectionRequestModel src)
            => new ContractModels.PerfectionRequest
            {
                Mothers = ToContractsFourFiguresRequest(src.Mothers),
                QuerentHouse = src.QuerentHouse,
                QuesitedHouse = src.QuesitedHouse,
            };

        // ---------------------------------------------------------------------
        // Contracts response -> Client model mappers
        // ---------------------------------------------------------------------

        private static FigureModel MapToFigureModel(ContractModels.FigureResponse? src)
        {
            if (src == null) return new FigureModel();

            return new FigureModel
            {
                Name = src.Name ?? string.Empty,
                OtherNames = src.OtherNames ?? string.Empty,
                Quality = src.Quality ?? string.Empty,
                Keyword = src.Keyword ?? string.Empty,
                Imagery = src.Imagery ?? string.Empty,
                StrongHouse = src.StrongHouse ?? string.Empty,
                WeakHouse = src.WeakHouse ?? string.Empty,
                Planet = src.Planet ?? string.Empty,
                Sign = src.Sign ?? string.Empty,
                InnerEl = src.InnerEl ?? string.Empty,
                OuterEl = src.OuterEl ?? string.Empty,
                FireElement = src.FireElement ?? string.Empty,
                AirElement = src.AirElement ?? string.Empty,
                WaterElement = src.WaterElement ?? string.Empty,
                EarthElement = src.EarthElement ?? string.Empty,
                Anatomy = src.Anatomy ?? string.Empty,
                BodyType = src.BodyType ?? string.Empty,
                CharacterType = src.CharacterType ?? string.Empty,
                Colors = src.Colors ?? string.Empty,
                Commentary = src.Commentary ?? string.Empty,
                DivinatoryMeaning = src.DivinatoryMeaning ?? string.Empty,
                ElementalPattern = src.ElementalPattern ?? string.Empty,
                HeadLine = src.HeadLine,
                NeckLine = src.NeckLine,
                BodyLine = src.BodyLine,
                FootLine = src.FootLine,
                HouseStrength = (FigureInHouseStrength)(int)src.HouseStrength,
            };
        }

        private static HouseChartModel MapToHouseChartModel(ContractModels.HouseChartResponse? src)
        {
            if (src == null) return new HouseChartModel();

            return new HouseChartModel
            {
                Houses = src.Houses?.Select(h => new HouseModel
                {
                    HouseNumber = h.HouseNumber,
                    Figure = MapToFigureModel(h.Figure),
                }).ToList() ?? new List<HouseModel>(),
                RightWitness = MapToFigureModel(src.RightWitness),
                LeftWitness = MapToFigureModel(src.LeftWitness),
                Judge = MapToFigureModel(src.Judge),
                Sentence = MapToFigureModel(src.Sentence),
                Triplicities = src.Triplicities?.Select(t => new TriplicityModel
                {
                    Number = t.Number,
                    FirstFigure = MapToFigureModel(t.FirstFigure),
                    SecondFigure = MapToFigureModel(t.SecondFigure),
                    ThirdFigure = MapToFigureModel(t.ThirdFigure),
                    Description = t.Description ?? string.Empty,
                }).ToList() ?? new List<TriplicityModel>(),
                ChartSummary = src.ChartSummary ?? string.Empty,
                IsComplete = src.IsComplete,
                GeneratedAt = src.GeneratedAt,
            };
        }

        private static IndentScoreModel? MapToIndentScoreModel(ContractModels.IndentScoreResponse? src)
        {
            if (src == null) return null;
            return new IndentScoreModel
            {
                QuerentHouse = src.QuerentHouse,
                QuesitedHouse = src.QuesitedHouse,
                Index = src.Index,
                Bonus = src.Bonus,
                // The contracts DTO doesn't expose Total separately; HTTP path leaves it 0.
                Total = 0,
            };
        }

        private static PerfectionModel MapToPerfectionModel(ContractModels.PerfectionResponse? src)
        {
            if (src == null) return new PerfectionModel();

            return new PerfectionModel
            {
                Success = src.Success,
                Message = src.Message ?? string.Empty,
                Mode = src.Mode ?? string.Empty,
                AspectBetweenSignificators = src.AspectBetweenSignificators ?? string.Empty,
                TranslatorHouse = src.TranslatorHouse,
                TranslatorFigure = src.TranslatorFigure ?? string.Empty,
                Notes = src.Notes ?? new List<string>(),
                QuerentHouse = src.QuerentHouse,
                QuesitedHouse = src.QuesitedHouse,
                QuerentFigure = src.QuerentFigure ?? string.Empty,
                QuesitedFigure = src.QuesitedFigure ?? string.Empty,
                Indentation = MapToIndentScoreModel(src.Indentation),
                TranslatorIndentation = MapToIndentScoreModel(src.TranslatorIndentation),
                MadeThroughCompany = src.MadeThroughCompany,
                BaseMode = src.BaseMode ?? string.Empty,
                CompanyType = src.CompanyType ?? string.Empty,
                CompanyTypeDescription = src.CompanyTypeDescription ?? string.Empty,
                FavorableScore = src.FavorableScore,
                UnfavorableScore = src.UnfavorableScore,
                NetScore = src.NetScore,
            };
        }

        private static AspectRecordModel MapToAspectRecordModel(ContractModels.AspectRecordResponse src)
            => new AspectRecordModel
            {
                AspectType = src.AspectType ?? string.Empty,
                Direction = src.Direction ?? string.Empty,
                FromHouse = src.FromHouse,
                ToHouse = src.ToHouse,
                Description = src.Description ?? string.Empty,
                MadeThroughCompany = src.MadeThroughCompany,
                IsMajorAspect = src.IsMajorAspect,
                FavorableScore = src.FavorableScore,
                UnfavorableScore = src.UnfavorableScore,
            };

        private static AspectAnalysisModel MapToAspectAnalysisModel(ContractModels.AspectAnalysisResponse? src)
        {
            if (src == null) return new AspectAnalysisModel();

            return new AspectAnalysisModel
            {
                Success = src.Success,
                Message = src.Message ?? string.Empty,
                TotalScore = src.TotalScore,
                TotalAspects = src.TotalAspects,
                Aspects = src.Aspects?.Select(a => new AspectDetailModel
                {
                    FromHouse = a.FromHouse,
                    ToHouse = a.ToHouse,
                    AspectType = a.AspectType ?? string.Empty,
                    Weight = a.Weight,
                    FromFigure = a.FromFigure ?? string.Empty,
                    ToFigure = a.ToFigure ?? string.Empty,
                }).ToList() ?? new List<AspectDetailModel>(),
                Polarity = src.Polarity != null ? new PolarityReportModel
                {
                    Easy = src.Polarity.Easy,
                    Hard = src.Polarity.Hard,
                    Delta = src.Polarity.Delta,
                    Percent = src.Polarity.Percent,
                    Verdict = src.Polarity.Verdict ?? string.Empty,
                } : null,
                AspectCounts = src.AspectCounts,
            };
        }

        private static WayOfPointsPathModel MapToWayOfPointsPathModel(ContractModels.WayOfPointsPathResponse? src)
        {
            if (src == null) return new WayOfPointsPathModel();

            return new WayOfPointsPathModel
            {
                Houses = src.Houses ?? new List<int>(),
                RowReached = src.RowReached,
                PathType = src.PathType ?? string.Empty,
                EndpointHouse = src.EndpointHouse,
                Description = src.Description ?? string.Empty,
            };
        }

        private static WayOfPointsResultModel MapToWayOfPointsResultModel(ContractModels.WayOfPointsResultResponse? src)
        {
            if (src == null) return new WayOfPointsResultModel();

            return new WayOfPointsResultModel
            {
                WayName = src.WayName ?? string.Empty,
                LineType = src.LineType ?? string.Empty,
                CanBeEstablished = src.CanBeEstablished,
                AllPaths = src.AllPaths?.Select(MapToWayOfPointsPathModel).ToList() ?? new List<WayOfPointsPathModel>(),
                StrongPaths = src.StrongPaths?.Select(MapToWayOfPointsPathModel).ToList() ?? new List<WayOfPointsPathModel>(),
                PassivePaths = src.PassivePaths?.Select(MapToWayOfPointsPathModel).ToList() ?? new List<WayOfPointsPathModel>(),
                StrongPassivePaths = src.StrongPassivePaths?.Select(MapToWayOfPointsPathModel).ToList() ?? new List<WayOfPointsPathModel>(),
                WeakerPassivePaths = src.WeakerPassivePaths?.Select(MapToWayOfPointsPathModel).ToList() ?? new List<WayOfPointsPathModel>(),
                Notes = src.Notes ?? new List<string>(),
            };
        }

        private static HouseDirectoryEntry MapToHouseDirectoryEntry(ContractModels.HouseDirectoryEntryResponse src)
            => new HouseDirectoryEntry
            {
                Id = src.Id,
                House = src.House ?? string.Empty,
                TraditionalName = src.TraditionalName ?? string.Empty,
                AstrologicalCorrespondence = src.AstrologicalCorrespondence ?? string.Empty,
                Governs = src.Governs ?? new List<string>(),
                SignificatorOfQuesitedWhen = src.SignificatorOfQuesitedWhen ?? string.Empty,
                Notes = src.Notes ?? string.Empty,
                ExampleQuestions = src.ExampleQuestions ?? new List<string>(),
            };

        private static CourtDirectoryEntry MapToCourtDirectoryEntry(ContractModels.CourtDirectoryEntryResponse src)
            => new CourtDirectoryEntry
            {
                Id = src.Id,
                Placement = src.Placement ?? string.Empty,
                TraditionalName = src.TraditionalName ?? string.Empty,
                GeneratedBy = src.GeneratedBy ?? string.Empty,
                Meaning = src.Meaning ?? new List<string>(),
                UtilityInReading = src.UtilityInReading ?? string.Empty,
            };

        private static WayOfPointsElementEntry MapToWayOfPointsElementEntry(ContractModels.WayOfPointsElementEntryResponse src)
            => new WayOfPointsElementEntry
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
                InterpretationParagraphs = src.InterpretationParagraphs ?? new List<string>(),
            };

        private static WayOfPointsPathTypeEntry MapToWayOfPointsPathTypeEntry(ContractModels.WayOfPointsPathTypeEntryResponse src)
            => new WayOfPointsPathTypeEntry
            {
                Id = src.Id ?? string.Empty,
                Name = src.Name ?? string.Empty,
                Glyph = src.Glyph ?? string.Empty,
                ColorHint = src.ColorHint ?? string.Empty,
                BadgeClass = src.BadgeClass ?? string.Empty,
                Tagline = src.Tagline ?? string.Empty,
                MechanismSummary = src.MechanismSummary ?? string.Empty,
                CoReads = src.CoReads ?? string.Empty,
                InterpretationParagraphs = src.InterpretationParagraphs ?? new List<string>(),
            };
    }
}
