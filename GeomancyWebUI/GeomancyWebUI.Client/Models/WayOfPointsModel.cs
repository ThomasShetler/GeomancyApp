using System.Collections.Generic;

namespace GeomancyWebUI.Client.Models
{
    public class WayOfPointsPathModel
    {
        public List<int> Houses { get; set; } = new List<int>();
        public int RowReached { get; set; }
        public string PathType { get; set; } = string.Empty;
        public int EndpointHouse { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class WayOfPointsResultModel
    {
        public string WayName { get; set; } = string.Empty;
        public string LineType { get; set; } = string.Empty;
        public bool CanBeEstablished { get; set; }
        public List<WayOfPointsPathModel> AllPaths { get; set; } = new List<WayOfPointsPathModel>();
        public List<WayOfPointsPathModel> StrongPaths { get; set; } = new List<WayOfPointsPathModel>();
        public List<WayOfPointsPathModel> PassivePaths { get; set; } = new List<WayOfPointsPathModel>();
        public List<WayOfPointsPathModel> StrongPassivePaths { get; set; } = new List<WayOfPointsPathModel>();
        public List<WayOfPointsPathModel> WeakerPassivePaths { get; set; } = new List<WayOfPointsPathModel>();
        public List<string> Notes { get; set; } = new List<string>();
    }

    public class WayOfPointsAnalysisModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public WayOfPointsResultModel FireWay { get; set; } = new WayOfPointsResultModel();
        public WayOfPointsResultModel AirWay { get; set; } = new WayOfPointsResultModel();
        public WayOfPointsResultModel WaterWay { get; set; } = new WayOfPointsResultModel();
        public WayOfPointsResultModel EarthWay { get; set; } = new WayOfPointsResultModel();
    }
}

