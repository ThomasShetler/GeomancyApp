using System.Collections.Generic;

namespace GeomancyApp
{
    /// <summary>
    /// Represents the type of a way of points path
    /// </summary>
    public enum WayOfPointsPathType
    {
        Strong,
        Passive,
        StrongPassive,
        WeakerPassive
    }

    /// <summary>
    /// Represents a single valid path in the way of points calculation
    /// </summary>
    public class WayOfPointsPath
    {
        /// <summary>
        /// List of house numbers in the path (from Judge down to the endpoint)
        /// </summary>
        public List<int> Houses { get; set; } = new List<int>();

        /// <summary>
        /// Which row the path reaches (1 = houses 1-8, 2 = houses 9-12)
        /// </summary>
        public int RowReached { get; set; }

        /// <summary>
        /// The type of this path (Strong, Passive, Strong Passive, Weaker Passive)
        /// </summary>
        public WayOfPointsPathType PathType { get; set; }

        /// <summary>
        /// The endpoint house number (the last house in the path)
        /// </summary>
        public int EndpointHouse { get; set; }

        /// <summary>
        /// Description of the path for notes
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// True when this path is the classical Via Ignis "Way of the Light":
        /// a single strong fire path with an active (single-dot) fire line at every step.
        /// </summary>
        public bool IsClassicalWayOfLight { get; set; }
    }

    /// <summary>
    /// Result for one way of points (Fire, Air, Water, or Earth)
    /// </summary>
    public class WayOfPointsResult
    {
        /// <summary>
        /// Name of the way (Via Puncti Ignis, Via Puncti Aeris, etc.)
        /// </summary>
        public string WayName { get; set; } = string.Empty;

        /// <summary>
        /// Line type used (HeadLine, NeckLine, BodyLine, FootLine)
        /// </summary>
        public string LineType { get; set; } = string.Empty;

        /// <summary>
        /// Whether this way can be established (Judge matches both Witnesses)
        /// </summary>
        public bool CanBeEstablished { get; set; }

        /// <summary>
        /// List of all valid paths found
        /// </summary>
        public List<WayOfPointsPath> AllPaths { get; set; } = new List<WayOfPointsPath>();

        /// <summary>
        /// List of strong paths (1-2 total paths)
        /// </summary>
        public List<WayOfPointsPath> StrongPaths { get; set; } = new List<WayOfPointsPath>();

        /// <summary>
        /// List of passive paths (when all paths are the same type)
        /// </summary>
        public List<WayOfPointsPath> PassivePaths { get; set; } = new List<WayOfPointsPath>();

        /// <summary>
        /// List of strong passive paths (reach row 1 when there are 3+ paths and mixed rows)
        /// </summary>
        public List<WayOfPointsPath> StrongPassivePaths { get; set; } = new List<WayOfPointsPath>();

        /// <summary>
        /// List of weaker passive paths (only reach row 2 when there are 3+ paths and mixed rows)
        /// </summary>
        public List<WayOfPointsPath> WeakerPassivePaths { get; set; } = new List<WayOfPointsPath>();

        /// <summary>
        /// True when Via Puncti Ignis presents exactly one strong path entirely on active (single-dot) fire.
        /// Traditionally read as the "Way of the Light" / heart of the matter on the fire line.
        /// </summary>
        public bool HasClassicalWayOfLight { get; set; }

        /// <summary>
        /// Notes and descriptions about the calculation
        /// </summary>
        public List<string> Notes { get; set; } = new List<string>();

        /// <summary>
        /// Gets the endpoint houses for strong paths
        /// </summary>
        public List<int> GetStrongEndpointHouses()
        {
            var houses = new List<int>();
            foreach (var path in StrongPaths)
            {
                if (path.EndpointHouse > 0 && !houses.Contains(path.EndpointHouse))
                    houses.Add(path.EndpointHouse);
            }
            return houses;
        }

        /// <summary>
        /// Gets the endpoint houses for strong passive paths
        /// </summary>
        public List<int> GetStrongPassiveEndpointHouses()
        {
            var houses = new List<int>();
            foreach (var path in StrongPassivePaths)
            {
                if (path.EndpointHouse > 0 && !houses.Contains(path.EndpointHouse))
                    houses.Add(path.EndpointHouse);
            }
            return houses;
        }

        /// <summary>
        /// Gets the endpoint houses for weaker passive paths
        /// </summary>
        public List<int> GetWeakerPassiveEndpointHouses()
        {
            var houses = new List<int>();
            foreach (var path in WeakerPassivePaths)
            {
                if (path.EndpointHouse > 0 && !houses.Contains(path.EndpointHouse))
                    houses.Add(path.EndpointHouse);
            }
            return houses;
        }
    }

    /// <summary>
    /// Complete analysis for all 4 ways of points
    /// </summary>
    public class WayOfPointsAnalysis
    {
        /// <summary>
        /// Result for Via Puncti Ignis (Way of Fire - HeadLine)
        /// </summary>
        public WayOfPointsResult FireWay { get; set; } = new WayOfPointsResult();

        /// <summary>
        /// Result for Via Puncti Aeris (Way of Air - NeckLine)
        /// </summary>
        public WayOfPointsResult AirWay { get; set; } = new WayOfPointsResult();

        /// <summary>
        /// Result for Via Puncti Aquae (Way of Water - BodyLine)
        /// </summary>
        public WayOfPointsResult WaterWay { get; set; } = new WayOfPointsResult();

        /// <summary>
        /// Result for Via Puncti Terrae (Way of Earth - FootLine)
        /// </summary>
        public WayOfPointsResult EarthWay { get; set; } = new WayOfPointsResult();

        /// <summary>
        /// List of all way results for easy iteration
        /// </summary>
        public List<WayOfPointsResult> AllWays
        {
            get
            {
                return new List<WayOfPointsResult> { FireWay, AirWay, WaterWay, EarthWay };
            }
        }
    }
}

