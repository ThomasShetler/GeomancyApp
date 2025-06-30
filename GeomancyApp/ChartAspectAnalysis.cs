using System.Collections.Generic;
using System.Linq;

namespace GeomancyApp
{
    public class AspectLine
    {
        public int From { get; }
        public int To { get; }
        public AspectType Aspect { get; }
        public int Weight { get; }
        public AspectLine(int from, int to, AspectType aspect, int weight)
        {
            From = from;
            To = to;
            Aspect = aspect;
            Weight = weight;
        }
    }

    public class IndentScore
    {
        public int QuerentHouse { get; }
        public int QuesitedHouse { get; }
        public int Index { get; }
        public int Bonus { get; }
        public IndentScore(int querentHouse, int quesitedHouse, int index, int bonus)
        {
            QuerentHouse = querentHouse;
            QuesitedHouse = quesitedHouse;
            Index = index;
            Bonus = bonus;
        }
    }

    public class AspectReport
    {
        public int TotalScore { get; set; }
        public int EasyScore { get; set; }
        public int HardScore { get; set; }
        public int Delta { get; set; } // Easy - Hard
        public double PolarityPercent { get; set; } // Normalized
        public string PolarityVerdict { get; set; }
        public List<AspectLine> Details { get; } = new List<AspectLine>();
        public Dictionary<string, int> AspectCounts { get; set; } = new Dictionary<string, int>();
    }

    public static class ChartAspectAnalysis
    {
        private static readonly Dictionary<AspectType, int> Weights = new Dictionary<AspectType, int>
        {
            {AspectType.Conjunction, 5},
            {AspectType.Trine,       4},
            {AspectType.Sextile,     3},
            {AspectType.Square,      2},
            {AspectType.Opposition,  1}
        };

        private static int GetWeight(AspectType a) { int w; return Weights.TryGetValue(a, out w) ? w : 0; }

        public static int IndentationIndex(int from, int to)
        {
            int d = (to - from + 12) % 12;
            return d <= 6 ? d : -1;
        }

        public static IndentScore ComputeIndent(HouseChart ch, int Qhouse, int Xhouse)
        {
            int idx = IndentationIndex(Qhouse, Xhouse);
            if (idx == -1)
                return new IndentScore(Qhouse, Xhouse, -1, 0);
            int bonus = 6 - idx;
            return new IndentScore(Qhouse, Xhouse, idx, bonus);
        }

        public static AspectReport Evaluate(HouseChart c, AspectType min = AspectType.Sextile)
        {
            if (c == null || !c.IsChartComplete())
                throw new System.InvalidOperationException("Chart not complete.");

            var rpt = new AspectReport();
            var aspectCounts = new Dictionary<string, int>();

            foreach (var aspectTuple in GeomanticAspects.AllAspects(c, min))
            {
                int w = GetWeight(aspectTuple.aspect);
                var line = new AspectLine(aspectTuple.from, aspectTuple.to, aspectTuple.aspect, w);
                rpt.Details.Add(line);

                // Count aspect types
                string aspectName = aspectTuple.aspect.ToString();
                if (!aspectCounts.ContainsKey(aspectName))
                    aspectCounts[aspectName] = 0;
                aspectCounts[aspectName]++;
            }

            // Now tally scores from Details to ensure consistency
            rpt.TotalScore = 0;
            rpt.EasyScore = 0;
            rpt.HardScore = 0;
            foreach (var d in rpt.Details)
            {
                rpt.TotalScore += d.Weight;
                if (d.Aspect == AspectType.Square || d.Aspect == AspectType.Opposition)
                    rpt.HardScore += d.Weight;
                else
                    rpt.EasyScore += d.Weight;
            }

            rpt.Delta = rpt.EasyScore - rpt.HardScore;
            int total = rpt.EasyScore + rpt.HardScore;
            rpt.PolarityPercent = (total == 0) ? 0 : 100.0 * rpt.Delta / total;
            rpt.PolarityVerdict = GetPolarityVerdict(rpt.Delta, rpt.PolarityPercent);
            rpt.AspectCounts = aspectCounts;
            return rpt;
        }

        public static string GetPolarityVerdict(int delta, double pct)
        {
            if (pct >= 60) return "strongly benefic";
            if (pct >= 30) return "mildly benefic";
            if (pct >= 10) return "slightly benefic";
            if (pct > -10) return "balanced / neutral";
            if (pct > -30) return "slightly malefic";
            if (pct > -60) return "mildly malefic";
            return "strongly malefic";
        }
    }
} 