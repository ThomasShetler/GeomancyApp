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
        Mutation
    }

    public class PerfectionResult
    {
        public PerfectionType Mode { get; set; } = PerfectionType.None;
        public AspectType AspectBetweenSignificators { get; set; } = AspectType.None;
        public int TranslatorHouse { get; set; } = 0;   // 0 = n/a
        public List<string> Notes { get; } = new List<string>();
        public int QuerentHouse { get; set; }   // which house supplied Q
        public int QuesitedHouse { get; set; }   // which house supplied X
    }
}
