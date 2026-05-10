using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeomancyApp
{
    public class FigureData
    {
        public string FigureID {get; set;}
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string OtherNames { get; set; }
        public string Quality { get; set; }
        public string Keyword { get; set; }
        public string Imagery { get; set; }
        public string StrongHouse { get; set; }
        public int    StrongHouseID {get; set;}
        public string WeakHouse { get; set; }
        public int    WeakHouseID { get; set; }
        public string Planet { get; set; }
        public string Sign { get; set; }
        public string InnerEl { get; set; }
        public string OuterEl { get; set; }
        public string FireElement { get; set; }
        public string AirElement { get; set; }
        public string WaterElement { get; set; }
        public string EarthElement { get; set; }
        public string Anatomy { get; set; }
        public string BodyType { get; set; }
        public string CharacterType { get; set; }
        public string Colors { get; set; }
        public string Commentary { get; set; }
        public string DivinatoryMeaning { get; set; }

        public string Tagline { get; set; }
        public List<string> CoreMeaning { get; set; } = new List<string>();
        public List<string> FavorableFor { get; set; } = new List<string>();
        public List<string> UnfavorableFor { get; set; } = new List<string>();
        public string ElementalSynthesis { get; set; }
        public List<string> TraditionalImagery { get; set; } = new List<string>();
        public List<string> Interpretation { get; set; } = new List<string>();
        public Dictionary<string, string> InHouses { get; set; } = new Dictionary<string, string>(StringComparer.Ordinal);
        public List<string> ModernExamples { get; set; } = new List<string>();
        public List<TraditionalSourceEntry> TraditionalSources { get; set; } = new List<TraditionalSourceEntry>();

        /// <summary>Copies catalog fields from <paramref name="source"/> onto <paramref name="target"/> (geomantic instances).</summary>
        public static void CopyFigureMetadata(FigureData target, FigureData source)
        {
            if (target == null || source == null) return;
            target.FigureID = source.FigureID;
            target.Name = source.Name;
            target.EnglishName = source.EnglishName;
            target.OtherNames = source.OtherNames;
            target.Quality = source.Quality;
            target.Keyword = source.Keyword;
            target.Imagery = source.Imagery;
            target.StrongHouse = source.StrongHouse;
            target.StrongHouseID = source.StrongHouseID;
            target.WeakHouse = source.WeakHouse;
            target.WeakHouseID = source.WeakHouseID;
            target.Planet = source.Planet;
            target.Sign = source.Sign;
            target.InnerEl = source.InnerEl;
            target.OuterEl = source.OuterEl;
            target.FireElement = source.FireElement;
            target.AirElement = source.AirElement;
            target.WaterElement = source.WaterElement;
            target.EarthElement = source.EarthElement;
            target.Anatomy = source.Anatomy;
            target.BodyType = source.BodyType;
            target.CharacterType = source.CharacterType;
            target.Colors = source.Colors;
            target.Commentary = source.Commentary;
            target.DivinatoryMeaning = source.DivinatoryMeaning;
            target.Tagline = source.Tagline;
            target.CoreMeaning = source.CoreMeaning != null ? new List<string>(source.CoreMeaning) : new List<string>();
            target.FavorableFor = source.FavorableFor != null ? new List<string>(source.FavorableFor) : new List<string>();
            target.UnfavorableFor = source.UnfavorableFor != null ? new List<string>(source.UnfavorableFor) : new List<string>();
            target.ElementalSynthesis = source.ElementalSynthesis;
            target.TraditionalImagery = source.TraditionalImagery != null ? new List<string>(source.TraditionalImagery) : new List<string>();
            target.Interpretation = source.Interpretation != null ? new List<string>(source.Interpretation) : new List<string>();
            target.InHouses = source.InHouses != null
                ? new Dictionary<string, string>(source.InHouses, StringComparer.Ordinal)
                : new Dictionary<string, string>(StringComparer.Ordinal);
            target.ModernExamples = source.ModernExamples != null ? new List<string>(source.ModernExamples) : new List<string>();
            target.TraditionalSources = source.TraditionalSources != null
                ? source.TraditionalSources.ConvertAll(s => new TraditionalSourceEntry
                {
                    Author = s.Author,
                    Work = s.Work,
                    Section = s.Section,
                    Year = s.Year
                })
                : new List<TraditionalSourceEntry>();
        }

        // Static collection of all figures
        private static List<FigureData> _allFigures = null;

        // Initialize the collection of all figures
        private static void InitializeFigures()
        {
            if (_allFigures != null) return;

            _allFigures = FigureCorpus.BuildFigures();
        }


        // Get figure by name (case-insensitive)
        public static FigureData GetFigureByName(string name)
        {
            InitializeFigures();
            return _allFigures.FirstOrDefault(f => 
                f.Name.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                f.Name.Split(' ')[0].Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Get figure by elemental pattern (4 bools: Fire, Air, Water, Earth)
        public static FigureData GetFigureByElementalPattern(bool fireActive, bool airActive, bool waterActive, bool earthActive)
        {
            InitializeFigures();
            return _allFigures.FirstOrDefault(f => 
                f.FireElement.Equals(fireActive ? "Active" : "Passive", StringComparison.OrdinalIgnoreCase) &&
                f.AirElement.Equals(airActive ? "Active" : "Passive", StringComparison.OrdinalIgnoreCase) &&
                f.WaterElement.Equals(waterActive ? "Active" : "Passive", StringComparison.OrdinalIgnoreCase) &&
                f.EarthElement.Equals(earthActive ? "Active" : "Passive", StringComparison.OrdinalIgnoreCase));
        }

        // Get figure by elemental pattern using integers (1 = active, 0 or 2 = passive)
        public static FigureData GetFigureByElementalPattern(int fireValue, int airValue, int waterValue, int earthValue)
        {
            bool fireActive = fireValue == 1;
            bool airActive = airValue == 1;
            bool waterActive = waterValue == 1;
            bool earthActive = earthValue == 1;
            
            return GetFigureByElementalPattern(fireActive, airActive, waterActive, earthActive);
        }

        // Get all figures
        public static List<FigureData> GetAllFigures()
        {
            InitializeFigures();
            return _allFigures.ToList();
        }

        // Legacy method for backward compatibility
        public FigureData ReturnFigureData(string name)
        {
            return GetFigureByName(name) ?? new FigureData
            {
                Name = "NA",
                EnglishName = "NA",
                OtherNames = "NA",
                Quality = "NA",
                Keyword = "NA",
                Imagery = "NA",
                StrongHouse = "NA",
                WeakHouse = "NA",
                Planet = "NA",
                Sign = "NA",
                InnerEl = "NA",
                OuterEl = "NA",
                FireElement = "NA",
                AirElement = "NA",
                WaterElement = "NA",
                EarthElement = "NA",
                Anatomy = "NA",
                BodyType = "NA",
                CharacterType = "NA",
                Colors = "NA",
                Commentary = "NA",
                DivinatoryMeaning = "NA"
            };
        }
    }
}