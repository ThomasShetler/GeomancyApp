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
    }
}

