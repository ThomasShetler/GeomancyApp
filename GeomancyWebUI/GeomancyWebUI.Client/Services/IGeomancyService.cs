using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Services
{
    public interface IGeomancyService
    {
        Task<HouseChartModel> GenerateChartAsync(GenerateFourFiguresRequest request);
        Task<FigureModel> GetFigureAsync(int headLine, int neckLine, int bodyLine, int footLine);
        Task<FigureModel> GetFigureByNameAsync(string figureName);
        Task<List<PerfectionModel>> CalculatePerfectionAsync(PerfectionRequestModel request);
        Task<PerfectionAnalysisModel> AnalyzePerfectionsAsync(PerfectionRequestModel request);
        Task<AspectAnalysisModel> GetAspectAnalysisAsync(GenerateFourFiguresRequest request);
        Task<WayOfPointsAnalysisModel> CalculateWayOfPointsAsync(GenerateFourFiguresRequest request);

        // Static reference directory (HouseAndCourtDirectory/*.json on the API)
        Task<List<HouseDirectoryEntry>> GetHousesDirectoryAsync();
        Task<List<CourtDirectoryEntry>> GetCourtsDirectoryAsync();

        // Static reference directory (WayOfPointsDirectory/*.json on the API)
        Task<List<WayOfPointsElementEntry>> GetWayOfPointsElementsDirectoryAsync();
        Task<List<WayOfPointsPathTypeEntry>> GetWayOfPointsPathTypesDirectoryAsync();
    }
}

