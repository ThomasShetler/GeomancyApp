using System;
using System.Collections.Generic;
using System.Linq;

namespace GeomancyApp
{
    public class WeightJustification
    {
        public int BaseWeight { get; set; }
        public int TotalModifier { get; set; }
        public int FinalWeight { get; set; }
        public List<string> Justifications { get; set; } = new List<string>();
    }

    public class AspectLine
    {
        public int From { get; }
        public int To { get; }
        public AspectType Aspect { get; }
        public int Weight { get; }
        public WeightJustification Justification { get; }
        public AspectLine(int from, int to, AspectType aspect, int weight, WeightJustification justification = null)
        {
            From = from;
            To = to;
            Aspect = aspect;
            Weight = weight;
            Justification = justification;
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
        // Balanced weights for fair polarity calculation
        // Base weights are equalized to prevent inherent bias toward malefic readings
        // Modifiers (house strength, planet nature) will create the variation
        // Note: Table 6-3 values are used as reference but balanced for fairness
        private static readonly Dictionary<AspectType, int> Weights = new Dictionary<AspectType, int>
        {
            {AspectType.Conjunction, 4},  // Balanced with Trine
            {AspectType.Trine,       3},  // +3 per trine (Table 6-3)
            {AspectType.Sextile,     3},  // +3 per sextile (Table 6-3)
            {AspectType.Square,      3},  // Balanced with Trine/Sextile (was -3, now 3 for symmetry)
            {AspectType.Opposition,  3}   // Balanced with other aspects (was -4, now 3 for symmetry)
        };

        private static int GetWeight(AspectType a) { int w; return Weights.TryGetValue(a, out w) ? w : 0; }

        /// <summary>
        /// Gets the elemental nature of a zodiac sign.
        /// Fire: Aries, Leo, Sagittarius
        /// Earth: Taurus, Virgo, Capricorn
        /// Air: Gemini, Libra, Aquarius
        /// Water: Cancer, Scorpio, Pisces
        /// </summary>
        private static string GetSignElement(string sign)
        {
            if (string.IsNullOrWhiteSpace(sign)) return null;

            string signLower = sign.ToLowerInvariant();
            
            // Fire signs
            if (signLower.Contains("aries") || signLower.Contains("leo") || signLower.Contains("sagittarius"))
                return "Fire";
            
            // Earth signs
            if (signLower.Contains("taurus") || signLower.Contains("virgo") || signLower.Contains("capricorn"))
                return "Earth";
            
            // Air signs
            if (signLower.Contains("gemini") || signLower.Contains("libra") || signLower.Contains("aquarius"))
                return "Air";
            
            // Water signs
            if (signLower.Contains("cancer") || signLower.Contains("scorpio") || signLower.Contains("pisces"))
                return "Water";
            
            return null;
        }

        /// <summary>
        /// Determines planet interaction between two figures.
        /// Benefic + Benefic = amplified benefic (+1)
        /// Malefic + Malefic = amplified malefic (+1)
        /// Benefic + Malefic = tension/conflict (-1 for favorable, +1 for unfavorable)
        /// </summary>
        private static int GetPlanetInteractionModifier(bool? planet1Benefic, bool? planet2Benefic, bool isFavorable, bool isUnfavorable)
        {
            if (!planet1Benefic.HasValue || !planet2Benefic.HasValue) return 0;
            
            // Both benefic: amplify benefic nature
            if (planet1Benefic.Value && planet2Benefic.Value)
            {
                if (isFavorable) return 1;  // Two benefics enhance favorable aspects
                if (isUnfavorable) return -1;  // Two benefics reduce unfavorable aspects
            }
            
            // Both malefic: amplify malefic nature
            if (!planet1Benefic.Value && !planet2Benefic.Value)
            {
                if (isFavorable) return -1;  // Two malefics reduce favorable aspects
                if (isUnfavorable) return 1;  // Two malefics enhance unfavorable aspects
            }
            
            // Mixed (benefic + malefic): creates tension
            // This tension affects aspects differently
            if (isFavorable) return -1;  // Tension reduces favorable aspects
            if (isUnfavorable) return 1;  // Tension enhances unfavorable aspects
            
            return 0;
        }

        /// <summary>
        /// Determines sign compatibility between two figures.
        /// Same element = harmonious (+1 bonus)
        /// Compatible elements (Fire+Air, Earth+Water) = neutral (0)
        /// Incompatible elements (Fire+Water, Earth+Air) = challenging (-1 penalty)
        /// </summary>
        private static int GetSignCompatibilityModifier(string sign1, string sign2)
        {
            var element1 = GetSignElement(sign1);
            var element2 = GetSignElement(sign2);
            
            if (element1 == null || element2 == null) return 0;
            
            // Same element = harmonious
            if (element1 == element2) return 1;
            
            // Compatible pairs: Fire+Air (both active), Earth+Water (both receptive)
            if ((element1 == "Fire" && element2 == "Air") || (element1 == "Air" && element2 == "Fire"))
                return 0;  // Neutral compatibility
            
            if ((element1 == "Earth" && element2 == "Water") || (element1 == "Water" && element2 == "Earth"))
                return 0;  // Neutral compatibility
            
            // Incompatible pairs: Fire+Water (opposites), Earth+Air (opposites)
            if ((element1 == "Fire" && element2 == "Water") || (element1 == "Water" && element2 == "Fire"))
                return -1;  // Challenging
            
            if ((element1 == "Earth" && element2 == "Air") || (element1 == "Air" && element2 == "Earth"))
                return -1;  // Challenging
            
            return 0;
        }

        /// <summary>
        /// Determines if a planet is benefic, malefic, or neutral based on traditional astrology.
        /// According to astrological tradition: Venus and Jupiter are benefic; Mars and Saturn are malefic;
        /// Moon and Sun are sometimes considered benefic; Mercury is neutral.
        /// </summary>
        private static bool? IsPlanetBenefic(string planet)
        {
            if (string.IsNullOrWhiteSpace(planet)) return null;

            string planetLower = planet.ToLowerInvariant();
            
            // Benefic planets
            if (planetLower.Contains("venus") || planetLower.Contains("jupiter") || 
                planetLower.Contains("moon") || planetLower.Contains("sun"))
            {
                return true;
            }
            
            // Malefic planets
            if (planetLower.Contains("mars") || planetLower.Contains("saturn"))
            {
                return false;
            }
            
            // Dragon nodes: North node (Caput) is benefic-like, South node (Cauda) is malefic-like
            if (planetLower.Contains("north node") || planetLower.Contains("caput"))
            {
                return true;  // Benefic-like
            }
            if (planetLower.Contains("south node") || planetLower.Contains("cauda"))
            {
                return false;  // Malefic-like
            }
            
            // Mercury and others are neutral
            return null;
        }

        /// <summary>
        /// Calculates the adjusted weight for an aspect based on:
        /// 1. WEIGHTED DIGNITY TABLES: Essential Dignity (Rulership/Exaltation) + Accidental Dignity (House Joys)
        ///    + Sign Natural Rulership + Figure-specific strong/weak house overrides
        /// 2. Individual planet nature (benefic/malefic for each figure)
        /// 3. Planet interaction (how the two planets interact with each other)
        /// 4. Sign compatibility (elemental compatibility between the two figures' signs)
        /// 5. Synergy bonuses (when planet nature aligns with dignity scores)
        /// 
        /// WEIGHTED DIGNITY SYSTEM:
        /// - Uses traditional astrological dignity tables (Rulership +5, Exaltation +4, House Joy +4, etc.)
        /// - Combines planet dignity, sign natural rulership, and figure-specific overrides
        /// - High dignity (>3): +3 for favorable, -2 for unfavorable (greatly enhances/reduces)
        /// - Moderate dignity (0-3): +1 for favorable, -1 for unfavorable (moderately enhances/reduces)
        /// - Low dignity (<0): -1 for favorable, +1 for unfavorable (weakens/strengthens)
        /// - Very low dignity (<-3): -3 for favorable, +2 for unfavorable (greatly weakens/strengthens)
        /// 
        /// PLANET INTERACTIONS (comparing the two figures' planets):
        /// - Benefic + Benefic: +1 (amplified benefic)
        /// - Malefic + Malefic: +1 (amplified malefic)
        /// - Benefic + Malefic: -1/+1 (tension/conflict)
        /// 
        /// SIGN COMPATIBILITY (comparing the two figures' signs):
        /// - Same element (e.g., Aries+Leo): +1 (harmonious)
        /// - Compatible elements (Fire+Air, Earth+Water): 0 (neutral)
        /// - Incompatible elements (Fire+Water, Earth+Air): -1 (challenging)
        /// 
        /// SYNERGY BONUSES (amplified effects when planet nature aligns with dignity):
        /// - Benefic planet with high dignity (>3): +2 synergy bonus (amplified benefic)
        /// - Malefic planet with low dignity (<-3): +2 synergy bonus (amplified malefic)
        /// - Benefic planet with low dignity (<0): -1 anti-synergy penalty (diminished benefic)
        /// - Malefic planet with high dignity (>0): -1 anti-synergy bonus (dignity mitigates malefic)
        /// 
        /// This creates a comprehensive system that uses traditional astrological dignity calculations
        /// combined with geomantic figure properties and cross-figure comparisons.
        /// </summary>
        private static (int weight, WeightJustification justification) GetAdjustedWeightWithJustification(AspectType aspect, GeomanticFigure figure1, GeomanticFigure figure2, int house1, int house2)
        {
            var justification = new WeightJustification();
            int baseWeight = GetWeight(aspect);
            justification.BaseWeight = baseWeight;
            
            if (baseWeight == 0)
            {
                justification.FinalWeight = 0;
                justification.Justifications.Add("Base weight is 0 (aspect type not recognized)");
                return (0, justification);
            }
            
            justification.Justifications.Add($"Base weight: {baseWeight} (from {aspect} aspect)");

            // Determine if this is a favorable or unfavorable aspect
            bool isFavorable = (aspect == AspectType.Trine || aspect == AspectType.Sextile || aspect == AspectType.Conjunction);
            bool isUnfavorable = (aspect == AspectType.Square || aspect == AspectType.Opposition);

            // Calculate granular Dignity Scores using the Weighted Dignity Tables
            // This combines Essential Dignity (Rulership/Exaltation) with Accidental Dignity (House Joys)
            // and Sign Natural Rulership, plus figure-specific strong/weak house overrides
            int dignity1 = DignityTables.GetTotalDignityScore(figure1, house1);
            int dignity2 = DignityTables.GetTotalDignityScore(figure2, house2);

            // Get planet benefic/malefic nature
            var planet1Benefic = IsPlanetBenefic(figure1?.Planet);
            var planet2Benefic = IsPlanetBenefic(figure2?.Planet);

            // Get sign compatibility between the two figures
            var signCompatibility = GetSignCompatibilityModifier(figure1?.Sign, figure2?.Sign);
            
            // Get planet interaction between the two figures
            var planetInteraction = GetPlanetInteractionModifier(planet1Benefic, planet2Benefic, isFavorable, isUnfavorable);

            int modifier = 0;

            // Apply modifiers based on DIGNITY SCORES (replaces simple strong/weak house check)
            // High positive dignity = planet is acting "Honorably" and "Strongly"
            // Low negative dignity = planet is acting "Weakly" or "Debilitated"
            // Convert dignity scores into modifiers based on aspect type
            
            // Figure 1 Dignity Modifier (SYMMETRIC for balance)
            string figure1Name = figure1?.Name ?? "Unknown";
            string figure1Planet = figure1?.Planet ?? "Unknown";
            string figure1Sign = figure1?.Sign ?? "Unknown";
            
            if (dignity1 > 3)  // Very Dignified (Rulership + Exaltation, etc.)
            {
                if (isFavorable)
                {
                    modifier += 3;
                    justification.Justifications.Add($"Figure 1 ({figure1Name}) in House {house1}: Very high dignity ({dignity1}) - {figure1Planet} in {figure1Sign} - greatly enhances favorable aspect (+3)");
                }
                else if (isUnfavorable)
                {
                    modifier -= 3;
                    justification.Justifications.Add($"Figure 1 ({figure1Name}) in House {house1}: Very high dignity ({dignity1}) - {figure1Planet} in {figure1Sign} - significantly reduces unfavorable aspect (-3)");
                }
            }
            else if (dignity1 > 0)  // Slightly Dignified
            {
                if (isFavorable)
                {
                    modifier += 1;
                    justification.Justifications.Add($"Figure 1 ({figure1Name}) in House {house1}: Moderate dignity ({dignity1}) - {figure1Planet} in {figure1Sign} - enhances favorable aspect (+1)");
                }
                else if (isUnfavorable)
                {
                    modifier -= 1;
                    justification.Justifications.Add($"Figure 1 ({figure1Name}) in House {house1}: Moderate dignity ({dignity1}) - {figure1Planet} in {figure1Sign} - reduces unfavorable aspect (-1)");
                }
            }
            else if (dignity1 == 0)  // Neutral Dignity
            {
                if (isFavorable)
                {
                    modifier -= 1;
                    justification.Justifications.Add($"Figure 1 ({figure1Name}) in House {house1}: Neutral dignity (0) - {figure1Planet} in {figure1Sign} - slight reduction to prevent benefic bias (-1)");
                }
                else if (isUnfavorable)
                {
                    modifier += 1;
                    justification.Justifications.Add($"Figure 1 ({figure1Name}) in House {house1}: Neutral dignity (0) - {figure1Planet} in {figure1Sign} - slight increase to prevent benefic bias (+1)");
                }
            }
            else if (dignity1 < -3)  // Very Debilitated (Detriment + Fall, etc.)
            {
                if (isFavorable)
                {
                    modifier -= 3;
                    justification.Justifications.Add($"Figure 1 ({figure1Name}) in House {house1}: Very low dignity ({dignity1}) - {figure1Planet} in {figure1Sign} - greatly reduces favorable aspect (-3)");
                }
                else if (isUnfavorable)
                {
                    modifier += 3;
                    justification.Justifications.Add($"Figure 1 ({figure1Name}) in House {house1}: Very low dignity ({dignity1}) - {figure1Planet} in {figure1Sign} - significantly increases unfavorable aspect (+3)");
                }
            }
            else if (dignity1 < 0)  // Weak/Debilitated
            {
                if (isFavorable)
                {
                    modifier -= 1;
                    justification.Justifications.Add($"Figure 1 ({figure1Name}) in House {house1}: Low dignity ({dignity1}) - {figure1Planet} in {figure1Sign} - reduces favorable aspect (-1)");
                }
                else if (isUnfavorable)
                {
                    modifier += 1;
                    justification.Justifications.Add($"Figure 1 ({figure1Name}) in House {house1}: Low dignity ({dignity1}) - {figure1Planet} in {figure1Sign} - increases unfavorable aspect (+1)");
                }
            }

            // Figure 2 Dignity Modifier (SYMMETRIC for balance)
            string figure2Name = figure2?.Name ?? "Unknown";
            string figure2Planet = figure2?.Planet ?? "Unknown";
            string figure2Sign = figure2?.Sign ?? "Unknown";
            
            if (dignity2 > 3)  // Very Dignified
            {
                if (isFavorable)
                {
                    modifier += 3;
                    justification.Justifications.Add($"Figure 2 ({figure2Name}) in House {house2}: Very high dignity ({dignity2}) - {figure2Planet} in {figure2Sign} - greatly enhances favorable aspect (+3)");
                }
                else if (isUnfavorable)
                {
                    modifier -= 3;
                    justification.Justifications.Add($"Figure 2 ({figure2Name}) in House {house2}: Very high dignity ({dignity2}) - {figure2Planet} in {figure2Sign} - significantly reduces unfavorable aspect (-3)");
                }
            }
            else if (dignity2 > 0)  // Slightly Dignified
            {
                if (isFavorable)
                {
                    modifier += 1;
                    justification.Justifications.Add($"Figure 2 ({figure2Name}) in House {house2}: Moderate dignity ({dignity2}) - {figure2Planet} in {figure2Sign} - enhances favorable aspect (+1)");
                }
                else if (isUnfavorable)
                {
                    modifier -= 1;
                    justification.Justifications.Add($"Figure 2 ({figure2Name}) in House {house2}: Moderate dignity ({dignity2}) - {figure2Planet} in {figure2Sign} - reduces unfavorable aspect (-1)");
                }
            }
            else if (dignity2 == 0)  // Neutral Dignity
            {
                if (isFavorable)
                {
                    modifier -= 1;
                    justification.Justifications.Add($"Figure 2 ({figure2Name}) in House {house2}: Neutral dignity (0) - {figure2Planet} in {figure2Sign} - slight reduction to prevent benefic bias (-1)");
                }
                else if (isUnfavorable)
                {
                    modifier += 1;
                    justification.Justifications.Add($"Figure 2 ({figure2Name}) in House {house2}: Neutral dignity (0) - {figure2Planet} in {figure2Sign} - slight increase to prevent benefic bias (+1)");
                }
            }
            else if (dignity2 < -3)  // Very Debilitated
            {
                if (isFavorable)
                {
                    modifier -= 3;
                    justification.Justifications.Add($"Figure 2 ({figure2Name}) in House {house2}: Very low dignity ({dignity2}) - {figure2Planet} in {figure2Sign} - greatly reduces favorable aspect (-3)");
                }
                else if (isUnfavorable)
                {
                    modifier += 3;
                    justification.Justifications.Add($"Figure 2 ({figure2Name}) in House {house2}: Very low dignity ({dignity2}) - {figure2Planet} in {figure2Sign} - significantly increases unfavorable aspect (+3)");
                }
            }
            else if (dignity2 < 0)  // Weak/Debilitated
            {
                if (isFavorable)
                {
                    modifier -= 1;
                    justification.Justifications.Add($"Figure 2 ({figure2Name}) in House {house2}: Low dignity ({dignity2}) - {figure2Planet} in {figure2Sign} - reduces favorable aspect (-1)");
                }
                else if (isUnfavorable)
                {
                    modifier += 1;
                    justification.Justifications.Add($"Figure 2 ({figure2Name}) in House {house2}: Low dignity ({dignity2}) - {figure2Planet} in {figure2Sign} - increases unfavorable aspect (+1)");
                }
            }

            // Apply modifiers based on planet benefic/malefic nature
            if (planet1Benefic.HasValue)
            {
                if (planet1Benefic.Value)  // Benefic planet
                {
                    if (isFavorable)
                    {
                        modifier += 1;
                        justification.Justifications.Add($"Figure 1 planet ({figure1Planet}) is benefic - enhances favorable aspect (+1)");
                    }
                    else if (isUnfavorable)
                    {
                        modifier -= 1;
                        justification.Justifications.Add($"Figure 1 planet ({figure1Planet}) is benefic - reduces unfavorable aspect (-1)");
                    }
                }
                else  // Malefic planet
                {
                    if (isFavorable)
                    {
                        modifier -= 1;
                        justification.Justifications.Add($"Figure 1 planet ({figure1Planet}) is malefic - reduces favorable aspect (-1)");
                    }
                    else if (isUnfavorable)
                    {
                        modifier += 1;
                        justification.Justifications.Add($"Figure 1 planet ({figure1Planet}) is malefic - enhances unfavorable aspect (+1)");
                    }
                }
            }

            if (planet2Benefic.HasValue)
            {
                if (planet2Benefic.Value)  // Benefic planet
                {
                    if (isFavorable)
                    {
                        modifier += 1;
                        justification.Justifications.Add($"Figure 2 planet ({figure2Planet}) is benefic - enhances favorable aspect (+1)");
                    }
                    else if (isUnfavorable)
                    {
                        modifier -= 1;
                        justification.Justifications.Add($"Figure 2 planet ({figure2Planet}) is benefic - reduces unfavorable aspect (-1)");
                    }
                }
                else  // Malefic planet
                {
                    if (isFavorable)
                    {
                        modifier -= 1;
                        justification.Justifications.Add($"Figure 2 planet ({figure2Planet}) is malefic - reduces favorable aspect (-1)");
                    }
                    else if (isUnfavorable)
                    {
                        modifier += 1;
                        justification.Justifications.Add($"Figure 2 planet ({figure2Planet}) is malefic - enhances unfavorable aspect (+1)");
                    }
                }
            }

            // SYNERGY BONUS: Amplify effects when planet nature aligns with dignity
            // This represents the figure being at full power when benefic planet has high dignity
            // or malefic planet has low dignity (debilitated)
            // Benefic planet with high dignity = amplified benefic (synergy bonus)
            // Malefic planet with low dignity = amplified malefic (synergy bonus)
            // Benefic planet with low dignity = diminished benefic (anti-synergy penalty)
            // Malefic planet with high dignity = diminished malefic (anti-synergy bonus)
            
            // Figure 1 synergy (using dignity scores instead of simple strong/weak)
            if (planet1Benefic.HasValue)
            {
                if (dignity1 > 3 && planet1Benefic.Value)
                {
                    if (isFavorable)
                    {
                        modifier += 1;
                        justification.Justifications.Add($"Synergy: Benefic planet ({figure1Planet}) with very high dignity ({dignity1}) - amplified benefic effect (+1)");
                    }
                    else if (isUnfavorable)
                    {
                        modifier -= 1;
                        justification.Justifications.Add($"Synergy: Benefic planet ({figure1Planet}) with very high dignity ({dignity1}) - reduces malefic even more (-1)");
                    }
                }
                else if (dignity1 < -3 && !planet1Benefic.Value)
                {
                    if (isFavorable)
                    {
                        modifier -= 1;
                        justification.Justifications.Add($"Synergy: Malefic planet ({figure1Planet}) with very low dignity ({dignity1}) - reduces benefic even more (-1)");
                    }
                    else if (isUnfavorable)
                    {
                        modifier += 1;
                        justification.Justifications.Add($"Synergy: Malefic planet ({figure1Planet}) with very low dignity ({dignity1}) - amplified malefic effect (+1)");
                    }
                }
                else if (dignity1 < 0 && planet1Benefic.Value)
                {
                    if (isFavorable)
                    {
                        modifier -= 1;
                        justification.Justifications.Add($"Anti-synergy: Benefic planet ({figure1Planet}) with low dignity ({dignity1}) - diminished benefic (-1)");
                    }
                }
                else if (dignity1 > 0 && !planet1Benefic.Value)
                {
                    if (isUnfavorable)
                    {
                        modifier -= 1;
                        justification.Justifications.Add($"Anti-synergy: Malefic planet ({figure1Planet}) with high dignity ({dignity1}) - dignity helps mitigate malefic (-1)");
                    }
                }
            }

            // Figure 2 synergy (using dignity scores instead of simple strong/weak)
            if (planet2Benefic.HasValue)
            {
                if (dignity2 > 3 && planet2Benefic.Value)
                {
                    if (isFavorable)
                    {
                        modifier += 1;
                        justification.Justifications.Add($"Synergy: Benefic planet ({figure2Planet}) with very high dignity ({dignity2}) - amplified benefic effect (+1)");
                    }
                    else if (isUnfavorable)
                    {
                        modifier -= 1;
                        justification.Justifications.Add($"Synergy: Benefic planet ({figure2Planet}) with very high dignity ({dignity2}) - reduces malefic even more (-1)");
                    }
                }
                else if (dignity2 < -3 && !planet2Benefic.Value)
                {
                    if (isFavorable)
                    {
                        modifier -= 1;
                        justification.Justifications.Add($"Synergy: Malefic planet ({figure2Planet}) with very low dignity ({dignity2}) - reduces benefic even more (-1)");
                    }
                    else if (isUnfavorable)
                    {
                        modifier += 1;
                        justification.Justifications.Add($"Synergy: Malefic planet ({figure2Planet}) with very low dignity ({dignity2}) - amplified malefic effect (+1)");
                    }
                }
                else if (dignity2 < 0 && planet2Benefic.Value)
                {
                    if (isFavorable)
                    {
                        modifier -= 1;
                        justification.Justifications.Add($"Anti-synergy: Benefic planet ({figure2Planet}) with low dignity ({dignity2}) - diminished benefic (-1)");
                    }
                }
                else if (dignity2 > 0 && !planet2Benefic.Value)
                {
                    if (isUnfavorable)
                    {
                        modifier -= 1;
                        justification.Justifications.Add($"Anti-synergy: Malefic planet ({figure2Planet}) with high dignity ({dignity2}) - dignity helps mitigate malefic (-1)");
                    }
                }
            }

            // Apply planet interaction modifier (how the two planets interact with each other)
            if (planetInteraction != 0)
            {
                modifier += planetInteraction;
                string interactionDesc = "";
                if (planet1Benefic.HasValue && planet2Benefic.HasValue)
                {
                    if (planet1Benefic.Value && planet2Benefic.Value)
                        interactionDesc = "Both planets are benefic - amplified benefic";
                    else if (!planet1Benefic.Value && !planet2Benefic.Value)
                        interactionDesc = "Both planets are malefic - amplified malefic";
                    else
                        interactionDesc = "Mixed benefic/malefic planets - creates tension";
                }
                string modifierStr = planetInteraction > 0 ? $"+{planetInteraction}" : planetInteraction.ToString();
                justification.Justifications.Add($"Planet interaction: {interactionDesc} ({modifierStr})");
            }

            // Apply sign compatibility modifier
            if (signCompatibility > 0)  // Same element (harmonious)
            {
                if (isFavorable)
                {
                    modifier += 1;
                    justification.Justifications.Add($"Sign compatibility: Same element ({figure1Sign} + {figure2Sign}) - harmonious, enhances favorable aspect (+1)");
                }
                else if (isUnfavorable)
                {
                    modifier -= 1;
                    justification.Justifications.Add($"Sign compatibility: Same element ({figure1Sign} + {figure2Sign}) - harmonious, reduces unfavorable aspect (-1)");
                }
            }
            else if (signCompatibility < 0)  // Incompatible elements (challenging)
            {
                if (isFavorable)
                {
                    modifier -= 1;
                    justification.Justifications.Add($"Sign compatibility: Incompatible elements ({figure1Sign} + {figure2Sign}) - challenging, reduces favorable aspect (-1)");
                }
                else if (isUnfavorable)
                {
                    modifier += 1;
                    justification.Justifications.Add($"Sign compatibility: Incompatible elements ({figure1Sign} + {figure2Sign}) - challenging, enhances unfavorable aspect (+1)");
                }
            }

            // Apply modifier to base weight
            int adjustedWeight = baseWeight + modifier;
            justification.TotalModifier = modifier;
            
            // Ensure unfavorable aspects have positive weight (they're counted as malefic in HardScore)
            // Higher weight = more malefic impact
            if (isUnfavorable && adjustedWeight < 1)
            {
                adjustedWeight = 1;  // Minimum weight for unfavorable aspects
                justification.Justifications.Add("Minimum weight applied: unfavorable aspect clamped to 1");
            }

            // Ensure favorable aspects don't go negative or zero
            if (isFavorable && adjustedWeight < 1)
            {
                adjustedWeight = 1;  // Minimum positive weight for favorable aspects
                justification.Justifications.Add("Minimum weight applied: favorable aspect clamped to 1");
            }
            
            justification.FinalWeight = adjustedWeight;
            justification.Justifications.Add($"Final calculation: {baseWeight} (base) + {modifier} (modifier) = {adjustedWeight} (final weight)");
            
            return (adjustedWeight, justification);
        }
        
        // Backward compatibility method
        private static int GetAdjustedWeight(AspectType aspect, GeomanticFigure figure1, GeomanticFigure figure2, int house1, int house2)
        {
            var (weight, _) = GetAdjustedWeightWithJustification(aspect, figure1, figure2, house1, house2);
            return weight;
        }

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
                // Get the figures in both houses to check house strength
                var figure1 = c.GetHouseFigure(aspectTuple.from);
                var figure2 = c.GetHouseFigure(aspectTuple.to);

                // Calculate adjusted weight based on dignity tables and comparisons with justifications
                var (w, justification) = GetAdjustedWeightWithJustification(aspectTuple.aspect, figure1, figure2, aspectTuple.from, aspectTuple.to);
                var line = new AspectLine(aspectTuple.from, aspectTuple.to, aspectTuple.aspect, w, justification);
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
            // Calculate polarity percentage: (Easy - Hard) / (Easy + Hard) * 100
            // Use explicit double casting to ensure precision
            rpt.PolarityPercent = (total == 0) ? 0.0 : (100.0 * (double)rpt.Delta / (double)total);
            rpt.PolarityVerdict = GetPolarityVerdict(rpt.Delta, rpt.PolarityPercent);
            rpt.AspectCounts = aspectCounts;
            return rpt;
        }

        public static string GetPolarityVerdict(int delta, double pct)
        {
            // Less sensitive thresholds to require stronger signals for polarity classification
            // This prevents "strongly benefic" from appearing too frequently
            if (pct >= 60) return "strongly benefic";  // Increased from 50% to require stronger signal
            if (pct >= 35) return "mildly benefic";   // Increased from 25% to require stronger signal
            if (pct >= 10) return "slightly benefic";  // Increased from 5% to require stronger signal
            if (pct > -10) return "balanced / neutral"; // Increased from -5% to require stronger signal
            if (pct > -35) return "slightly malefic"; // Increased from -25% to require stronger signal
            if (pct > -60) return "mildly malefic";   // Increased from -50% to require stronger signal
            return "strongly malefic";
        }
    }
} 