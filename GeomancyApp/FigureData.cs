using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GeomancyApp
{
    public class FigureData
    {
        public string Name { get; set; }
        public string OtherNames { get; set; }
        public string Quality { get; set; }
        public string Keyword { get; set; }
        public string Imagery { get; set; }
        public string StrongHouse { get; set; }
        public string WeakHouse { get; set; }
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

        // Static collection of all figures
        private static List<FigureData> _allFigures = null;

        // Initialize the collection of all figures
        private static void InitializeFigures()
        {
            if (_allFigures != null) return;

            _allFigures = new List<FigureData>
            {
                new FigureData
                {
                    Name = "Puer (Boy)",
                    OtherNames = "Beardless, yellow, warrior, man, sword",
                    Quality = "Mobile",
                    Keyword = "Energy",
                    Imagery = "A sword; a male figure with exaggerated testicles",
                    StrongHouse = "First",
                    WeakHouse = "Seventh",
                    Planet = "Mars",
                    Sign = "Aries",
                    InnerEl = "Air",
                    OuterEl = "Fire",
                    FireElement = "Active",
                    AirElement = "Active",
                    WaterElement = "Passive",
                    EarthElement = "Active",
                    Anatomy = "The head",
                    BodyType = "Short, solidly built, and muscular, with a red face and small eyes. Skin color more brown or reddish than the person's ethnic background would suggest. Uneven teeth and, in men, a sparse beard.",
                    CharacterType = "Rash, enthusiastic, and thoughtless, with plenty of energy and very little tact or self-control. The basic adolescent male personality, with self-assertion covering a great deal of insecurity and inexperience.",
                    Colors = "White, flecked with red",
                    Commentary = "Puer is a figure of male sexual energy, balancing the feminine figure Puella. Unstable and forceful, it represents conflict, sudden change, and transformation, with all the constructive and destructive aspects these imply.",
                    DivinatoryMeaning = "Passionate energy, force, seeking, and sudden change. Favorable when energy, enthusiasm, courage, and change are desirable. Unfavorable in matters where stability, prudence, and maturity are advantages."
                },

                new FigureData
                {
                    Name = "Amissio (Loss)",
                    OtherNames = "Grasping externally, outer wealth, something escaped or lost",
                    Quality = "Mobile",
                    Keyword = "Loss",
                    Imagery = "A bag held mouth downward, letting the contents fall out",
                    StrongHouse = "Second",
                    WeakHouse = "Eighth",
                    Planet = "Venus",
                    Sign = "Taurus",
                    InnerEl = "Fire",
                    OuterEl = "Earth",
                    FireElement = "Active",
                    AirElement = "Passive",
                    WaterElement = "Active",
                    EarthElement = "Passive",
                    Anatomy = "The neck and throat",
                    BodyType = "Middle height and large-boned body structure, with a long neck, a large head, big shoulders and feet, and a round face. The mouth is small and the eyes are attractive.",
                    CharacterType = "Straightforward and sometimes tactless, quick to respond emotionally to any situation, deeply concerned with personal honor but not always honest.",
                    Colors = "White, flecked with citrine (greenish yellow)",
                    Commentary = "Amissio is a figure of transience and loss, balancing Acquisitio's imagery of gain. Central to its meaning is a recognition of the hard truth that all things pass away.",
                    DivinatoryMeaning = "Loss in every sense, positive or negative, from losing your heart to losing your money. Favorable in any matter where loss is what you desire. Unfavorable whenever you hope to gain something."
                },

                new FigureData
                {
                    Name = "Albus (White)",
                    OtherNames = "None",
                    Quality = "Stable",
                    Keyword = "Peace",
                    Imagery = "A goblet set upright",
                    StrongHouse = "Third",
                    WeakHouse = "Ninth",
                    Planet = "Mercury",
                    Sign = "Gemini",
                    InnerEl = "Water",
                    OuterEl = "Air",
                    FireElement = "Passive",
                    AirElement = "Passive",
                    WaterElement = "Active",
                    EarthElement = "Passive",
                    Anatomy = "The shoulders and lungs",
                    BodyType = "Apple-shaped—that is, larger above the waist than below it—and medium height. The head is large, the face round, the eyes relatively small. Men of this figure often have a thick beard.",
                    CharacterType = "Peaceful, kind, and loving, with a shy streak. People of this figure tend to make friends easily but keep few of them, and often spend money more freely than they can afford.",
                    Colors = "Brilliant white, flecked with red",
                    Commentary = "Albus is a figure of peace and detachment, balancing the passionate figure Rubeus. It represents awareness caught up wholly in its own inner life and turned away from the action in the outer world of experience.",
                    DivinatoryMeaning = "Peace, wisdom, and purity; generally favorable but often weak. Favors quiet progress and the use of intelligence. Favorable in most business and financial questions. Unfavorable in matters requiring courage and energy."
                },

                new FigureData
                {
                    Name = "Populus (People)",
                    OtherNames = "Congregation, multitude, double path",
                    Quality = "Stable",
                    Keyword = "Stability",
                    Imagery = "A crowd",
                    StrongHouse = "Fourth",
                    WeakHouse = "Tenth",
                    Planet = "Moon",
                    Sign = "Cancer",
                    InnerEl = "Water",
                    OuterEl = "Water",
                    FireElement = "Passive",
                    AirElement = "Passive",
                    WaterElement = "Passive",
                    EarthElement = "Passive",
                    Anatomy = "The breasts and midriff",
                    BodyType = "Tall and relatively thin, with narrow hips and a long face, large teeth, and skin darker than the person's ethnic background would normally suggest. People of this figure often have a mark or blemish near one eye.",
                    CharacterType = "Passive and receptive, taking on the characteristics of those around them, and are happiest when in the company of friends. Their emotions are stronger than their intellect.",
                    Colors = "Sea green or dark russet brown",
                    Commentary = "Populus is a figure of dispersal and multiplicity, balancing the focused movement of Via. Like a crowd, Populus has no particular motion or direction until it receives the energy of another figure.",
                    DivinatoryMeaning = "A gathering or assembly of people, or a large amount of anything else. A passive figure that takes on the qualities of the figures that interact with it. Usually favorable in matters that benefit from quiet reflection."
                },

                new FigureData
                {
                    Name = "Fortuna Major (Greater Fortune)",
                    OtherNames = "Inward fortune, protection going in, greater omen, inside or hidden help",
                    Quality = "Stable",
                    Keyword = "Power",
                    Imagery = "A valley through which a river flows",
                    StrongHouse = "Fifth",
                    WeakHouse = "Eleventh",
                    Planet = "Sun",
                    Sign = "Leo",
                    InnerEl = "Earth",
                    OuterEl = "Fire",
                    FireElement = "Passive",
                    AirElement = "Passive",
                    WaterElement = "Active",
                    EarthElement = "Active",
                    Anatomy = "The heart and chest",
                    BodyType = "Medium height and slender build; the legs are often asymmetrical, with one longer or thicker than the other. Skin color tends to be more yellow than the person's ethnic background would normally suggest.",
                    CharacterType = "Generous, honest, trustworthy, and fair, with an ambitious streak but a modest demeanor and an easy manner. Fond of spending money.",
                    Colors = "Green, yellow, or gold",
                    Commentary = "Fortuna Major is a figure of inner strength and resulting success, balancing the outer strength of Fortuna Minor. Like the valley that is its image, Fortuna Major represents a natural shape of events that brings success without apparent effort.",
                    DivinatoryMeaning = "Great good fortune, although the way there may be difficult at times. A figure of power and success that unfolds naturally rather than having to be forced. Favorable for nearly all questions except those involving escaping from a difficult situation."
                },

                new FigureData
                {
                    Name = "Conjunctio (Conjunction)",
                    OtherNames = "Association, gathering together",
                    Quality = "Mobile",
                    Keyword = "Interaction",
                    Imagery = "A crossroads",
                    StrongHouse = "Sixth",
                    WeakHouse = "Twelfth",
                    Planet = "Mercury",
                    Sign = "Virgo",
                    InnerEl = "Air",
                    OuterEl = "Earth",
                    FireElement = "Passive",
                    AirElement = "Active",
                    WaterElement = "Active",
                    EarthElement = "Passive",
                    Anatomy = "The intestines and belly",
                    BodyType = "Very attractive, with a slender, delicate build and medium height. The face is long and beautiful, with a slender nose and attractive eyes. The thighs are thin.",
                    CharacterType = "Intelligent and talkative, fond of luxuries, and unconcerned with issues of honesty and legality. People of this figure have many friends and tend to spend more money than they earn.",
                    Colors = "Purple or pale gray, sometimes black speckled with blue",
                    Commentary = "Conjunctio is a figure of contact and union, balancing the isolated and limited figure Carcer. It represents the union of opposites on all levels and the resulting potentials for change.",
                    DivinatoryMeaning = "The presence of a combination of forces. Tends to be favorable or unfavorable depending on other figures and circumstances, but is reliably favorable in any question about recovering things lost or stolen."
                },

                new FigureData
                {
                    Name = "Puella (Girl)",
                    OtherNames = "Beauty, purity",
                    Quality = "Stable",
                    Keyword = "Harmony",
                    Imagery = "A mirror; a female figure with exaggerated breasts",
                    StrongHouse = "Seventh",
                    WeakHouse = "First",
                    Planet = "Venus",
                    Sign = "Libra",
                    InnerEl = "Water",
                    OuterEl = "Air",
                    FireElement = "Active",
                    AirElement = "Passive",
                    WaterElement = "Active",
                    EarthElement = "Active",
                    Anatomy = "The kidneys and lower back",
                    BodyType = "Attractive, with medium height and a soft, somewhat plump body. The neck is long, the face round, the mouth small, the eyebrows and eyes attractive, and the shoulders large.",
                    CharacterType = "Passionate and highly emotional, with a quick temper. People of this figure are intensely conscious of their appearance, and fall in and out of love easily.",
                    Colors = "White, flecked with green",
                    Commentary = "Puella is a figure of female sexual energy, balancing the masculine figure Puer. Puella is balanced and harmonious, but ambivalent. It seeks to unite with others, where its opposite Puer seeks only to be received.",
                    DivinatoryMeaning = "Harmony and happiness that may not last indefinitely; favorable in most questions, but fickle. Especially favorable for questions involving love and friendship. Unfavorable in any situation where permanence is wanted."
                },

                new FigureData
                {
                    Name = "Rubeus (Red)",
                    OtherNames = "Burning, danger",
                    Quality = "Mobile",
                    Keyword = "Passion",
                    Imagery = "A goblet turned upside down",
                    StrongHouse = "Eighth",
                    WeakHouse = "Second",
                    Planet = "Mars",
                    Sign = "Scorpio",
                    InnerEl = "Air",
                    OuterEl = "Water",
                    FireElement = "Passive",
                    AirElement = "Active",
                    WaterElement = "Passive",
                    EarthElement = "Passive",
                    Anatomy = "The genitals and reproductive system",
                    BodyType = "Strong and muscular, with skin more reddish or brown than the person's ethnic background would normally suggest. The face is rugged and often has red spots or boils.",
                    CharacterType = "Hot-tempered, passionate, and troublesome, fond of partying, fighting, and lovemaking. Addictions to alcohol or drugs are common in people of this figure.",
                    Colors = "Red, flecked or streaked with brown",
                    Commentary = "Rubeus is a figure of passion and involvement in life, balancing the abstract detachment of Albus. The lesson here is that passionate involvement in the world comes from focusing on how we relate to others and to the world itself.",
                    DivinatoryMeaning = "A challenging figure that stands for passion, pleasure, fierceness, and violence. Old books on geomancy describe it as 'good in all that is evil and evil in all that is good.' Unfavorable in most situations, but favorable in questions involving sexuality, intoxicants, and violence."
                },

                new FigureData
                {
                    Name = "Acquisitio (Gain)",
                    OtherNames = "Grasping internally, inner wealth, something gained or picked up",
                    Quality = "Stable",
                    Keyword = "Gain",
                    Imagery = "A bag held mouth upward, as though to take something in",
                    StrongHouse = "Ninth",
                    WeakHouse = "Third",
                    Planet = "Jupiter",
                    Sign = "Sagittarius",
                    InnerEl = "Air",
                    OuterEl = "Fire",
                    FireElement = "Passive",
                    AirElement = "Active",
                    WaterElement = "Passive",
                    EarthElement = "Active",
                    Anatomy = "The hips and thighs",
                    BodyType = "Short but strongly built, with a full chest and skin color darker than the person's ethnic background would normally suggest. Arms and legs are short, and the neck is short and thick.",
                    CharacterType = "Fierce and passionate, with an insatiable gusto for life but an equally intense sense of fairness and justice.",
                    Colors = "Red, yellow, or green",
                    Commentary = "Acquisitio is a figure of gain and success, balancing the imagery of loss in Amissio. Its inner element and elemental structure stress that real gain of any kind exists only in a web of interaction, and seeks productive manifestation.",
                    DivinatoryMeaning = "Success, profit, and gain, and often means that something desired is within your grasp. Represents gain in every sense, and favors any situation where gaining something is desired. Unfavorable whenever loss is desired."
                },

                new FigureData
                {
                    Name = "Carcer (Prison)",
                    OtherNames = "Constricted, lock",
                    Quality = "Stable",
                    Keyword = "Isolation",
                    Imagery = "An enclosure",
                    StrongHouse = "Tenth",
                    WeakHouse = "Fourth",
                    Planet = "Saturn",
                    Sign = "Capricorn",
                    InnerEl = "Earth",
                    OuterEl = "Earth",
                    FireElement = "Active",
                    AirElement = "Passive",
                    WaterElement = "Passive",
                    EarthElement = "Active",
                    Anatomy = "The knees and lower legs",
                    BodyType = "Medium height and a wiry, sinewy build, with a long neck, small ears and mouth, and eyes that tend to look downward all the time. People of this figure often have strong muscular tensions.",
                    CharacterType = "Timid, especially about money and property, but stubborn. People of this figure tend to save more than they spend, and are fussy about their appearance.",
                    Colors = "White, russet, or dun (pale brown)",
                    Commentary = "Carcer is a figure of restriction and isolation, balancing the open and interactive nature of Conjunctio. This pattern of meanings has two sides, for restriction can be a source of strength and focus as well as a limitation.",
                    DivinatoryMeaning = "Solidity, restriction, binding, or even imprisonment. It always portends delay. In financial questions, it can stand for greed. Unfavorable in most questions, but favors anything involving stability, security, and isolation."
                },

                new FigureData
                {
                    Name = "Tristitia (Sorrow)",
                    OtherNames = "Crosswise, diminished, accursed, head down, fallen tower, cross",
                    Quality = "Stable",
                    Keyword = "Sorrow",
                    Imagery = "A pit, a stake driven downward",
                    StrongHouse = "Eleventh",
                    WeakHouse = "Fifth",
                    Planet = "Saturn",
                    Sign = "Aquarius",
                    InnerEl = "Earth",
                    OuterEl = "Air",
                    FireElement = "Passive",
                    AirElement = "Passive",
                    WaterElement = "Passive",
                    EarthElement = "Active",
                    Anatomy = "The ankles",
                    BodyType = "Tall and thin, with knobby joints, large feet, and darker skin than the person's ethnic background would normally suggest. The face is long, the teeth large, and the hair rough and unkempt.",
                    CharacterType = "Unconventional and idealistic, unconcerned with social expectations, and prone to dishonesty. People of this figure have quick tempers and carry grudges for a long time.",
                    Colors = "Tawny or sky blue",
                    Commentary = "Tristitia is a figure of sorrow and difficulty, balancing the joyous symbolism of Laetitia. The deeper level of meaning is simply that suffering is the one sure source of wisdom.",
                    DivinatoryMeaning = "Any downward movement. Lowered spirits (sorrow), lowered vitality (illness), and lowered expectations (failure). Unfavorable in most matters, but favorable for questions involving stability and patience. Very favorable in all questions dealing with building and the Earth."
                },

                new FigureData
                {
                    Name = "Laetitia (Joy)",
                    OtherNames = "Bearded, laughing, singing, high tower, head lifted, candelabrum, high mountain",
                    Quality = "Mobile",
                    Keyword = "Joy",
                    Imagery = "A tower",
                    StrongHouse = "Twelfth",
                    WeakHouse = "Sixth",
                    Planet = "Jupiter",
                    Sign = "Pisces",
                    InnerEl = "Fire",
                    OuterEl = "Water",
                    FireElement = "Active",
                    AirElement = "Passive",
                    WaterElement = "Passive",
                    EarthElement = "Passive",
                    Anatomy = "The feet",
                    BodyType = "Tall and muscular, with large hands, feet, facial features, and forehead. People of this figure usually have rough and unruly hair, and a puppyish, ungainly air.",
                    CharacterType = "Honest and good-natured, with a strong quality of innocence that can sometimes express itself as clueless folly. People of this figure often have strong and deeply held religious beliefs.",
                    Colors = "Glittering pale green",
                    Commentary = "Laetitia is a figure of joy, balancing the harsh symbolism of Tristitia. It represents happiness of every kind and level, from the most momentary of passing pleasures to the highest reaches of human experience.",
                    DivinatoryMeaning = "A very positive figure representing any form of ascent. It means upward movement, which is favorable in a querent's career (success), emotional state (happiness), or vitality (health). Favorable in most questions, but unfavorable when a secret needs to be kept."
                },

                new FigureData
                {
                    Name = "Cauda Draconis (Tail of the Dragon)",
                    OtherNames = "Outer threshold, threshold going out, lower boundary, stepping outside",
                    Quality = "Mobile",
                    Keyword = "Ending",
                    Imagery = "A doorway with footprints leading away from it",
                    StrongHouse = "Ninth",
                    WeakHouse = "Third",
                    Planet = "South node of the Moon",
                    Sign = "Sagittarius",
                    InnerEl = "Fire",
                    OuterEl = "Fire",
                    FireElement = "Active",
                    AirElement = "Active",
                    WaterElement = "Active",
                    EarthElement = "Passive",
                    Anatomy = "The left arm",
                    BodyType = "A long, thin body more attractive from behind than in front, with long-fingered hands. The face is long and lean, with a strong face and a large nose.",
                    CharacterType = "Corrupt, dangerous, destructive, and self-destructive, without scruples or conscience. People of this figure are obsessed with their own perceived needs and wants, and pursue them without regard for anyone or anything else.",
                    Colors = "Green, white, dark crimson, or pale tawny brown",
                    Commentary = "Cauda Draconis is a symbol of endings and completions, balancing the symbolism of beginnings in Caput Draconis. This point has some of the same symbolism as Mars and Saturn, the two malefics or negative forces in traditional astrology.",
                    DivinatoryMeaning = "Ending, completion, and letting go of the past. A figure with a complex message, it brings good with evil and evil with good. Favorable in any situation where something is coming to an end, but unfavorable for most other questions."
                },

                new FigureData
                {
                    Name = "Caput Draconis (Head of the Dragon)",
                    OtherNames = "Inner threshold, threshold coming in, upper boundary, high tree, upright staff, stepping inside",
                    Quality = "Stable",
                    Keyword = "Beginning",
                    Imagery = "A doorway with footprints leading toward it",
                    StrongHouse = "Sixth",
                    WeakHouse = "Twelfth",
                    Planet = "North node of the Moon",
                    Sign = "Virgo",
                    InnerEl = "Earth",
                    OuterEl = "Earth",
                    FireElement = "Passive",
                    AirElement = "Active",
                    WaterElement = "Active",
                    EarthElement = "Active",
                    Anatomy = "The right arm",
                    BodyType = "Medium height and slender build, with an attractive and expressive face. People indicated by this figure have abundant hair and prominent facial features.",
                    CharacterType = "Calm, trustworthy, and good-natured. An instinctive kindness is one of the most striking features of people of this figure.",
                    Colors = "White, flecked with citrine",
                    Commentary = "Caput Draconis is a figure of opportunities and beginnings, balancing Cauda Draconis's symbolism of endings. As the geomantic equivalent of the north or ascending node of the Moon, it represents change for the better.",
                    DivinatoryMeaning = "Beginnings and new possibilities. Tends to vary in meaning with its company, becoming more favorable with favorable figures and more unfavorable with unfavorable ones. Favorable in all matters having to do with beginnings."
                },

                new FigureData
                {
                    Name = "Fortuna Minor (Lesser Fortune)",
                    OtherNames = "Outward fortune, protection going out, lesser omen, outside or apparent help",
                    Quality = "Mobile",
                    Keyword = "Swiftness",
                    Imagery = "A mountain with a staff atop it",
                    StrongHouse = "Fifth",
                    WeakHouse = "Eleventh",
                    Planet = "Sun",
                    Sign = "Leo",
                    InnerEl = "Fire",
                    OuterEl = "Air",
                    FireElement = "Active",
                    AirElement = "Active",
                    WaterElement = "Passive",
                    EarthElement = "Passive",
                    Anatomy = "The spine",
                    BodyType = "Medium height and heavy build, with strong bones. The face is round and pale, the nose and forehead large, and the eyes dark. People of this figure have thick, rough, unkempt hair.",
                    CharacterType = "Bold, proud, and presumptuous, with a streak of insecurity and humility underneath the bravado, and a great deal of generosity and a strong sense of honor.",
                    Colors = "Gold or yellow",
                    Commentary = "Fortuna Minor is a figure of outer strength and success, balancing the inner strength of Fortuna Major. It represents success that is brought about by outside help or circumstances, rather than by the innate strength symbolized by Fortuna Major.",
                    DivinatoryMeaning = "Unstable success. Warns that what you gain may not stay with you for long. Very favorable whenever you want to proceed quickly, and unfavorable for matters in which fickleness or instability is a problem."
                },

                new FigureData
                {
                    Name = "Via (Way)",
                    OtherNames = "Wayfarer, candle, journey",
                    Quality = "Mobile",
                    Keyword = "Change",
                    Imagery = "A road",
                    StrongHouse = "Fourth",
                    WeakHouse = "Tenth",
                    Planet = "Moon",
                    Sign = "Cancer",
                    InnerEl = "Water",
                    OuterEl = "Water",
                    FireElement = "Active",
                    AirElement = "Active",
                    WaterElement = "Active",
                    EarthElement = "Active",
                    Anatomy = "The stomach",
                    BodyType = "Pear shaped (that is, larger below the waist than above it) and medium height, with a tendency to put on weight and sweat easily. People of this figure tend toward pale skin, round faces, and small teeth.",
                    CharacterType = "Patient and silent, with a deep inner life that rarely shows on the surface. Slow to anger, but once angered, equally slow to forgive. Fond of travel and moving from place to place.",
                    Colors = "White, flecked with blue",
                    Commentary = "Via is a figure of directed movement and change, balancing the diffuse and formless stability of Populus. With all four elements active, Via represents the elements in a constant state of change, each giving way to the next in an endless cycle.",
                    DivinatoryMeaning = "Change in all its forms, and favors all questions in which change is an advantage. Favorable for journeys of all kinds, real and metaphorical, but unfavorable whenever leaving things unchanged is desirable."
                }
            };
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