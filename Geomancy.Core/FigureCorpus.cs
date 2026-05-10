using System.Collections.Generic;

namespace GeomancyApp
{
    internal static partial class FigureCorpus
    {
        public static List<FigureData> BuildFigures() => new List<FigureData>
        {
            Fig01Puer(),
            Fig02Amissio(),
            Fig03Albus(),
            Fig04Populus(),
            Fig05FortunaMajor(),
            Fig06Conjunctio(),
            Fig07Puella(),
            Fig08Rubeus(),
            Fig09Acquisitio(),
            Fig10Carcer(),
            Fig11Tristitia(),
            Fig12Laetitia(),
            Fig13CaudaDraconis(),
            Fig14CaputDraconis(),
            Fig15FortunaMinor(),
            Fig16Via()
        };
    }
}
