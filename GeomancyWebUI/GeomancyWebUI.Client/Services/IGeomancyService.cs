using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Services
{
    public interface IGeomancyService
    {
        Task<HouseChartModel> GenerateChartAsync(GenerateFourFiguresRequest request);
        Task<FigureModel> GetFigureAsync(int headLine, int neckLine, int bodyLine, int footLine);
        Task<FigureModel> GetFigureByNameAsync(string figureName);
        Task<IReadOnlyList<FigureModel>> GetAllFiguresAsync();
        Task<List<PerfectionModel>> CalculatePerfectionAsync(PerfectionRequestModel request);
        Task<PerfectionAnalysisModel> AnalyzePerfectionsAsync(PerfectionRequestModel request);
        Task<AspectAnalysisModel> GetAspectAnalysisAsync(GenerateFourFiguresRequest request);
        Task<WayOfPointsAnalysisModel> CalculateWayOfPointsAsync(GenerateFourFiguresRequest request);

        // Static reference directory (databank/HouseAndCourtDirectory/*.json on the API)
        Task<List<HouseDirectoryEntry>> GetHousesDirectoryAsync();
        Task<List<CourtDirectoryEntry>> GetCourtsDirectoryAsync();

        // Static reference directory (databank/WayOfPointsDirectory/*.json on the API)
        Task<List<WayOfPointsElementEntry>> GetWayOfPointsElementsDirectoryAsync();
        Task<List<WayOfPointsPathTypeEntry>> GetWayOfPointsPathTypesDirectoryAsync();

        // Static reference directory (databank/PerfectionDirectory/*.json on the API)
        Task<CompanyTypeDirectory> GetCompanyTypesDirectoryAsync();

        Task<List<GreerFigureModel>> GetGreerFiguresDirectoryAsync();
        Task<GreerHouseDirectory> GetGreerHousesDirectoryAsync();
    }
}

