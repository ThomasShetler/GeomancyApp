using System;
using System.Collections.Generic;

namespace GeomancyApp
{
    public static class DignityTables
    {
        // Scores based on Traditional Astrology & Geomancy:
        // Rulership (Domicile): +5
        // Exaltation: +4
        // House Joy: +4
        // Triplicity/Friendly: +2
        // Neutral: 0
        // Detriment: -5
        // Fall: -4
        // Peregrine/Unfriendly: -2

        private static readonly Dictionary<string, Dictionary<int, int>> PlanetHouseWeights = new Dictionary<string, Dictionary<int, int>>(StringComparer.OrdinalIgnoreCase)
        {
            // SATURN: Rules Capricorn (10) & Aquarius (11). Exalted Libra (7). Joys in 12.
            { "Saturn", new Dictionary<int, int> {
                { 1, -4 },  // Fall (Aries)
                { 2, 0 },
                { 3, 0 },
                { 4, -5 },  // Detriment (Cancer - Opposition to Cap)
                { 5, -5 },  // Detriment (Leo - Opposition to Aq)
                { 6, 0 },
                { 7, 4 },   // Exaltation (Libra)
                { 8, 0 },
                { 9, 0 },
                { 10, 5 },  // Rulership (Capricorn)
                { 11, 5 },  // Rulership (Aquarius)
                { 12, 4 }   // House Joy
            }},

            // JUPITER: Rules Sagittarius (9) & Pisces (12). Exalted Cancer (4). Joys in 11.
            { "Jupiter", new Dictionary<int, int> {
                { 1, 0 },
                { 2, 0 },
                { 3, -5 },  // Detriment (Gemini)
                { 4, 4 },   // Exaltation (Cancer)
                { 5, 0 },
                { 6, -5 },  // Detriment (Virgo)
                { 7, 0 },
                { 8, 0 },
                { 9, 5 },   // Rulership (Sagittarius)
                { 10, -4 }, // Fall (Capricorn)
                { 11, 4 },  // House Joy
                { 12, 5 }   // Rulership (Pisces)
            }},

            // MARS: Rules Aries (1) & Scorpio (8). Exalted Capricorn (10). Joys in 6.
            { "Mars", new Dictionary<int, int> {
                { 1, 5 },   // Rulership (Aries)
                { 2, -5 },  // Detriment (Taurus)
                { 3, 0 },
                { 4, -4 },  // Fall (Cancer)
                { 5, 0 },
                { 6, 4 },   // House Joy
                { 7, -5 },  // Detriment (Libra)
                { 8, 5 },   // Rulership (Scorpio)
                { 9, 0 },
                { 10, 4 },  // Exaltation (Capricorn)
                { 11, 0 },
                { 12, 0 }
            }},

            // SUN: Rules Leo (5). Exalted Aries (1). Joys in 9.
            { "Sun", new Dictionary<int, int> {
                { 1, 4 },   // Exaltation (Aries)
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 5 },   // Rulership (Leo)
                { 6, 0 },
                { 7, -4 },  // Fall (Libra)
                { 8, 0 },
                { 9, 4 },   // House Joy
                { 10, 0 },
                { 11, -5 }, // Detriment (Aquarius)
                { 12, 0 }
            }},

            // VENUS: Rules Taurus (2) & Libra (7). Exalted Pisces (12). Joys in 5.
            { "Venus", new Dictionary<int, int> {
                { 1, -5 },  // Detriment (Aries)
                { 2, 5 },   // Rulership (Taurus)
                { 3, 0 },
                { 4, 0 },
                { 5, 4 },   // House Joy
                { 6, -4 },  // Fall (Virgo)
                { 7, 5 },   // Rulership (Libra)
                { 8, -5 },  // Detriment (Scorpio)
                { 9, 0 },
                { 10, 0 },
                { 11, 0 },
                { 12, 4 }   // Exaltation (Pisces)
            }},

            // MERCURY: Rules Gemini (3) & Virgo (6). Exalted Virgo (6). Joys in 1.
            { "Mercury", new Dictionary<int, int> {
                { 1, 4 },   // House Joy
                { 2, 0 },
                { 3, 5 },   // Rulership (Gemini)
                { 4, 0 },
                { 5, 0 },
                { 6, 9 },   // Rulership + Exaltation (Virgo is unique)
                { 7, 0 },
                { 8, 0 },
                { 9, -5 },  // Detriment (Sagittarius)
                { 10, 0 },
                { 11, 0 },
                { 12, -4 }  // Fall & Detriment (Pisces)
            }},

            // MOON: Rules Cancer (4). Exalted Taurus (2). Joys in 3.
            { "Moon", new Dictionary<int, int> {
                { 1, 0 },
                { 2, 4 },   // Exaltation (Taurus)
                { 3, 4 },   // House Joy
                { 4, 5 },   // Rulership (Cancer)
                { 5, 0 },
                { 6, 0 },
                { 7, 0 },
                { 8, -4 },  // Fall (Scorpio)
                { 9, 0 },
                { 10, -5 }, // Detriment (Capricorn)
                { 11, 0 },
                { 12, 0 }
            }},

            // CAPUT DRACONIS (North Node): Benefic. Exalted Gemini (3).
            { "North Node", new Dictionary<int, int> { 
                { 1, 0 }, { 2, 0 }, { 3, 4 }, { 4, 0 }, { 5, 0 }, 
                { 6, 2 }, { 7, 0 }, { 8, 0 }, { 9, -4 }, { 10, 0 }, 
                { 11, 2 }, { 12, -4 } 
            }},
            { "Caput Draconis", new Dictionary<int, int> { 
                { 1, 0 }, { 2, 0 }, { 3, 4 }, { 4, 0 }, { 5, 0 }, 
                { 6, 2 }, { 7, 0 }, { 8, 0 }, { 9, -4 }, { 10, 0 }, 
                { 11, 2 }, { 12, -4 } 
            }}, // Alias
            { "North node of the Moon", new Dictionary<int, int> { 
                { 1, 0 }, { 2, 0 }, { 3, 4 }, { 4, 0 }, { 5, 0 }, 
                { 6, 2 }, { 7, 0 }, { 8, 0 }, { 9, -4 }, { 10, 0 }, 
                { 11, 2 }, { 12, -4 } 
            }}, // Alias

            // CAUDA DRACONIS (South Node): Malefic. Exalted Sagittarius (9).
            { "South Node", new Dictionary<int, int> { 
                { 1, 0 }, { 2, 0 }, { 3, -4 }, { 4, 0 }, { 5, 0 }, 
                { 6, -2 }, { 7, 0 }, { 8, 0 }, { 9, 4 }, { 10, 0 }, 
                { 11, 0 }, { 12, 2 } 
            }},
            { "Cauda Draconis", new Dictionary<int, int> { 
                { 1, 0 }, { 2, 0 }, { 3, -4 }, { 4, 0 }, { 5, 0 }, 
                { 6, -2 }, { 7, 0 }, { 8, 0 }, { 9, 4 }, { 10, 0 }, 
                { 11, 0 }, { 12, 2 } 
            }}, // Alias
            { "South node of the Moon", new Dictionary<int, int> { 
                { 1, 0 }, { 2, 0 }, { 3, -4 }, { 4, 0 }, { 5, 0 }, 
                { 6, -2 }, { 7, 0 }, { 8, 0 }, { 9, 4 }, { 10, 0 }, 
                { 11, 0 }, { 12, 2 } 
            }} // Alias
        };

        // Natural Sign Rulership Weights (Sign in House)
        // Aries = 1, Taurus = 2, etc.
        private static readonly Dictionary<string, int> SignToNaturalHouse = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "Aries", 1 }, { "Taurus", 2 }, { "Gemini", 3 }, { "Cancer", 4 },
            { "Leo", 5 }, { "Virgo", 6 }, { "Libra", 7 }, { "Scorpio", 8 },
            { "Sagittarius", 9 }, { "Capricorn", 10 }, { "Aquarius", 11 }, { "Pisces", 12 }
        };

        /// <summary>
        /// Gets the weight of a planet in a specific house.
        /// </summary>
        public static int GetPlanetWeight(string planetName, int houseNumber)
        {
            if (string.IsNullOrEmpty(planetName) || houseNumber < 1 || houseNumber > 12) return 0;

            // Normalize planet name for lookup
            if (PlanetHouseWeights.ContainsKey(planetName))
            {
                var houseDict = PlanetHouseWeights[planetName];
                if (houseDict.ContainsKey(houseNumber))
                {
                    return houseDict[houseNumber];
                }
            }
            return 0; // Neutral if defined in table but house is neutral
        }

        /// <summary>
        /// Gets the weight of a sign in a specific house based on Natural Rulership (Aries=1).
        /// Returns +5 for natural house, -5 for opposite, +3 for trine relation.
        /// </summary>
        public static int GetSignWeight(string signName, int currentHouse)
        {
            if (string.IsNullOrEmpty(signName) || !SignToNaturalHouse.ContainsKey(signName)) return 0;

            int naturalHouse = SignToNaturalHouse[signName];
            
            if (currentHouse == naturalHouse) return 5; // Domicile
            
            // Calculate opposition (distance of 6)
            int dist = Math.Abs(currentHouse - naturalHouse);
            if (dist == 6) return -5; // Opposition/Detriment

            // Calculate Square (distance of 3 or 9) - Tension
            if (dist == 3 || dist == 9) return -2;

            // Calculate Trine (distance of 4 or 8) - Harmony
            if (dist == 4 || dist == 8) return 2;

            return 0;
        }

        /// <summary>
        /// Gets the combined Dignity Score for a specific figure in a specific house.
        /// Combines planet dignity, sign dignity, and figure-specific strong/weak house overrides.
        /// </summary>
        public static int GetTotalDignityScore(FigureData figure, int house)
        {
            if (figure == null) return 0;

            int score = 0;

            // 1. Planet in House (Essential Dignity: Rulership, Exaltation, Detriment, Fall, House Joys)
            score += GetPlanetWeight(figure.Planet, house);

            // 2. Sign in House (Natural Rulership: Aries=1, Taurus=2, etc.)
            score += GetSignWeight(figure.Sign, house);

            // 3. Figure Specific Strong/Weak House Override (From your original FigureData)
            // This adds an additional layer based on geomantic tradition
            if (figure.StrongHouseID == house) score += 3; 
            if (figure.WeakHouseID == house) score -= 3;

            return score;
        }
    }
}

