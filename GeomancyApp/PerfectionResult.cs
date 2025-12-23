using System.Collections.Generic;

namespace GeomancyApp
{
    public enum PerfectionType
    {
        None,
        Conjunction,
        Occupation,
        Aspect,
        Translation,
        Mutation,
        Company
    }

    public class PerfectionResult
    {
        public PerfectionType Mode { get; set; } = PerfectionType.None;
        public AspectType AspectBetweenSignificators { get; set; } = AspectType.None;
        public string AspectDirection { get; set; } = string.Empty; // Dexter, Sinister, or Opposition for aspect mode
        public int TranslatorHouse { get; set; } = 0;   // 0 = n/a
        public List<string> Notes { get; } = new List<string>();
        public int QuerentHouse { get; set; }   // which house supplied Q
        public int QuesitedHouse { get; set; }   // which house supplied X
        public bool MadeThroughCompany { get; set; } = false;  // True if perfection is made through company of houses
        public PerfectionType BaseMode { get; set; } = PerfectionType.None;  // The underlying mode when made through company
    }

    /// <summary>
    /// Represents an aspect found during perfection calculation (for paper trail)
    /// </summary>
    public class AspectRecord
    {
        public AspectType AspectType { get; set; }
        public string Direction { get; set; } // Dexter/Sinister description
        public int FromHouse { get; set; }
        public int ToHouse { get; set; }
        public string Description { get; set; } // Full description from notes
        public bool MadeThroughCompany { get; set; }
        public bool IsMajorAspect { get; set; } // True for Dexter aspects and Opposition (high energy/dominant)
    }

    /// <summary>
    /// Comprehensive result containing all perfections with calculated favorable and unfavorable scores
    /// </summary>
    public class PerfectionAnalysis
    {
        public List<PerfectionResult> Perfections { get; set; } = new List<PerfectionResult>();
        public List<PerfectionResult> Denials { get; set; } = new List<PerfectionResult>(); // Unfavorable aspects that deny perfection (only connection)
        public List<AspectRecord> PositiveAspects { get; set; } = new List<AspectRecord>(); // Sextile, Trine
        public List<AspectRecord> NegativeAspects { get; set; } = new List<AspectRecord>(); // Square, Opposition
        public int TotalFavorableScore { get; set; }
        public int TotalUnfavorableScore { get; set; }
        public int NetScore { get; set; }
        public int QuerentHouse { get; set; }
        public int QuesitedHouse { get; set; }
    }
}
