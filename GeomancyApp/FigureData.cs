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
                    CharacterType = "Rash, enthusiastic, and thoughtless, with plenty of energy and very little tact or self-control. The basic adolescent male personality, with self-assertion covering a great deal of insecurity and inexperience. People of this figure are prone to get into trouble by not thinking before they act.",
                    Colors = "White, flecked with red",
                    Commentary = "Puer is a figure of male sexual energy, balancing the feminine figure Puella. Unstable and forceful, it represents conflict, sudden change, and transformation, with all the constructive and destructive aspects these imply. Its astrological symbolism, Mars, Aries, and Fire, carries forward this pattern of meaning. Its elemental lines and inner element, by contrast, point to a deeper level of interpretation: Puer possesses energy and purpose, interactions with others, and a material expression, but no receptive inner life. All the aspects of its nature are projected outward into the world of experience, as an act of creation or a source of delusion. Like a young warrior riding forth on a quest, it carries the spear of Fire, the sword of Air, and the disk or shield of Earth, but must seek the cup of Water elsewhere—an image that has more than a little to do with the inner meaning of the legendary quest for the Holy Grail.",
                    DivinatoryMeaning = "Puer in a geomantic reading stands for passionate energy, force, seeking, and sudden change. In geomantic tradition, this figure is also associated with justice. Whenever energy, enthusiasm, courage, and change are desirable, Puer can be favorable, though even there it can be problematic due to its unthinking nature. It is always unfavorable in matters where stability, prudence, and maturity are advantages."
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
                    BodyType = "Middle height and large-boned body structure, with a long neck, a large head, big shoulders and feet, and a round face. The mouth is small and the eyes are attractive. Men of this figure tend to have facial hair, women have long and thick hair; in either case there may be a visible scar.",
                    CharacterType = "Straightforward and sometimes tactless, quick to respond emotionally to any situation, deeply concerned with personal honor but not always honest.",
                    Colors = "White, flecked with citrine (greenish yellow)",
                    Commentary = "Amissio is a figure of transience and loss, balancing Acquisitio's imagery of gain. Central to its meaning is a recognition of the hard truth that all things pass away. Its astrological symbols, Venus, Taurus, and Earth, point toward desire for material things as one of the classic examples of impermanence in human life; both the desire and the thing desired are certain to pass away in time, and so the experience of desire brings with it the certainty that the experience of loss will follow. Similarly, the elemental structure of the figure has Fire and Water alone; without Air to join them together or Earth to bring them into manifestation, these two opposed elements fly apart, and any contact between them is impermanent and without result.",
                    DivinatoryMeaning = "In a geomantic reading Amissio stands for loss in every sense, positive or negative, from losing your heart to losing your money. It often represents something outside one's grasp. Traditionally this is also a figure of generosity. It is favorable in any matter where loss is what you desire; this includes such things as love and sexuality (losing your heart), recovery from sickness (losing your illness), facing things you fear (losing your fear), and getting out of difficult situations (losing your problems). It is unfavorable whenever you hope to gain something from the situation."
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
                    Commentary = "Albus is a figure of peace and detachment, balancing the passionate figure Rubeus. Its astrological symbols, Mercury, Gemini, and Air, point to the way in which detachment has most often expressed itself in the Western world—the way of the intellect, which moves away from direct experience into abstract ideas. More deeply, though, Albus is a figure of Water, which is its only active line and also its inner element; it represents awareness caught up wholly in its own inner life and turned away from the action in the outer world of experience. In its highest form, this inward focus can lead the attentive mind to transcendence by the ways of mysticism, but it can also become a retreat from life that ends in sterility, isolation, and madness.",
                    DivinatoryMeaning = "Albus in a geomantic chart stands for peace, wisdom, and purity; it is generally a favorable figure, but often weak, and where it occurs, you may need to seek help from other people. It is traditionally a figure of chastity. Albus favors quiet progress and the use of intelligence, and is favorable in most business and financial questions; it is also favorable for beginnings. It is unfavorable in matters requiring courage and energy, or in any situation involving conflict or disruptive change."
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
                    BodyType = "Tall and relatively thin, with narrow hips and a long face, large teeth, and skin darker than the person's ethnic background would normally suggest. People of this figure often have a mark or blemish near one eye, or one eye of a different size or color than another. The face is often more attractive than the body. Men of this figure commonly have a thick beard.",
                    CharacterType = "People of this figure are passive and receptive, taking on the characteristics of those around them, and are happiest when in the company of friends. Their emotions are stronger than their intellect. An unexpected wandering streak makes them fond of travel and discontented when they have to remain in one place for too long.",
                    Colors = "Sea green or dark russet brown",
                    Commentary = "Populus is a figure of dispersal and multiplicity, balancing the focused movement of Via. Its astrological symbols, the Moon, Cancer, and Water, are all images of passivity, reflection, and indirect action; they represent patterns of experience that have no direction or focus of their own, but simply respond to energies coming from outside. The elemental structure of the figure shows all four elements as passive and latent, but the receptive nature of Water comes closest to expressing the figure's nature in elemental terms. Like a crowd, Populus has no particular motion or direction until it receives the energy of another figure. Its stability is a function of sheer inertia, rather than of any special strength of its own.",
                    DivinatoryMeaning = "Populus in a geomantic chart represents a gathering or assembly of people, or a large amount of anything else. A passive figure that takes on the qualities of the figures that interact with it, Populus tends to be fortunate with fortunate figures and unfortunate with unfortunate ones. Still, it is usually favorable in matters that benefit from quiet reflection, and unfavorable in matters demanding focused action."
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
                    BodyType = "Medium height and slender build; the legs are often asymmetrical, with one longer or thicker than the other. Skin color tends to be more yellow than the person's ethnic background would normally suggest. A round face, attractive teeth, and large luminous eyes.",
                    CharacterType = "Generous, honest, trustworthy, and fair, with an ambitious streak but a modest demeanor and an easy manner. Fond of spending money.",
                    Colors = "Green, yellow, or gold",
                    Commentary = "Fortuna Major is a figure of inner strength and resulting success, balancing the outer strength of Fortuna Minor. Its astrological symbols, the Sun, Leo, and Fire, are standard metaphorical images for strength and victory, but its elemental structure leads in some unexpected directions. Fire and Air are passive in this figure, with Water and Earth active, and the inner element is Earth; like the valley that is its image, Fortuna Major represents a natural shape of events that brings success without apparent effort. Though we too often tend to think of success as a matter of vigorous action and struggle, real success comes about because our inner life is reflected in our outer circumstances (as it always is, for good or ill) without any conscious effort at all. This is one of the central secrets of magic.",
                    DivinatoryMeaning = "Fortuna Major in a geomantic chart foretells great good fortune, although the way there may be difficult at times. It is a figure of power and success that unfolds naturally rather than having to be forced, and is especially favorable whenever the querent desires to win something. It often predicts a difficult beginning leading to a very good result. Traditionally it is a figure of nobility. Fortuna Major is favorable for nearly all questions except those involving escaping from a difficult situation; the Fortuna Major response to difficulty is to press ahead, and make lemonade out of life's lemons."
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
                    BodyType = "Very attractive, with a slender, delicate build and medium height. The face is long and beautiful, with a slender nose and attractive eyes. The thighs are thin. Men of this figure who have beards keep them short and neatly trimmed.",
                    CharacterType = "Intelligent and talkative, fond of luxuries, and unconcerned with issues of honesty and legality. People of this figure have many friends and tend to spend more money than they earn.",
                    Colors = "Purple or pale gray, sometimes black speckled with blue",
                    Commentary = "Conjunctio is a figure of contact and union, balancing the isolated and limited figure Carcer. It represents the union of opposites on all levels and the resulting potentials for change. Here the astrological symbolism of Mercury, Virgo, and Earth ties into ancient magical images of fertility, and the elemental structure is open to energy in the Fire line and to manifestation in the line of Earth. Air and Water, the active elements in this figure, are thought of in magical philosophy as middle realms uniting the two ends of the elemental spectrum; Air, the inner element of this figure, also has a role here as a symbol of interaction. Like a crossroads, Conjunctio forms a context in which movement can lead in unexpected directions, and bring energies and people on highly different trajectories into interaction.",
                    DivinatoryMeaning = "Conjunctio in a geomantic chart shows the presence of a combination of forces. It tends to be favorable or unfavorable depending on other figures and circumstances, but is reliably favorable in any question about recovering things lost or stolen. It is traditionally a figure of temperance. It is favorable for matters in which things need to come into contact, and unfavorable for any situation that calls for solitude, separation, and clarification."
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
                    BodyType = "Attractive, with medium height and a soft, somewhat plump body. The neck is long, the face round, the mouth small, the eyebrows and eyes attractive, and the shoulders large. People of this figure sing well and speak in an attractive voice.",
                    CharacterType = "Passionate and highly emotional, with a quick temper. People of this figure are intensely conscious of their appearance, and fall in and out of love easily.",
                    Colors = "White, flecked with green",
                    Commentary = "Puella is a figure of female sexual energy, balancing the masculine figure Puer. Puella is balanced and harmonious, but ambivalent. Its astrological symbols, Venus, Libra, and Air, suggest polar opposites held together in harmony and interaction by way of love, while its ruling inner element Water suggests that its energies are turned within, into a reflective inner life. The elemental structure is the key to this figure; with purpose and energy, inner receptivity, and the stability of a material basis, Puella lacks only relationship and interaction to be complete. It seeks to unite with others, where its opposite Puer seeks only to be received—a distinction that has more than a little to say about the complexity of relationships between the sexes.",
                    DivinatoryMeaning = "Puella in a geomantic reading stands for harmony and happiness that may not last indefinitely; it is a favorable figure in most questions, but fickle. It is especially favorable for questions involving love and friendship, and brings a positive answer to any question involving short-term happiness. Because of its fickleness, however, it is unfavorable in any situation where permanence is wanted."
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
                    BodyType = "Strong and muscular, with skin more reddish or brown than the person's ethnic background would normally suggest. The face is rugged and often has red spots or boils. People of this figure have deep voices and hair that refuses to stay neat. Men of this figure have sparse beards.",
                    CharacterType = "Hot-tempered, passionate, and troublesome, fond of partying, fighting, and lovemaking. Addictions to alcohol or drugs are common in people of this figure. Rubeus definitely means a walk on the wild side!",
                    Colors = "Red, flecked or streaked with brown",
                    Commentary = "Rubeus is a figure of passion and involvement in life, balancing the abstract detachment of Albus. Its astrological symbols, Mars, Scorpio, and Water, are the standard images of passionate energy in the symbolic language of the heavens. Its inner element and the one active part of its elemental structure, however, are both Air. The lesson here is that passionate involvement in the world comes from focusing on how we relate to others and to the world itself. At its worst, this too easily becomes a blind intoxication with appearances, but it also has the potential to open the way to a joyous participation in the experience of life.",
                    DivinatoryMeaning = "Rubeus in a geomantic chart is a challenging figure that stands for passion, pleasure, fierceness, and violence. Old books on geomancy describe it as 'good in all that is evil and evil in all that is good.' It is unfavorable in most situations, but favorable in questions involving sexuality, intoxicants, and violence."
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
                    BodyType = "Short but strongly built, with a full chest and skin color darker than the person's ethnic background would normally suggest. Arms and legs are short, and the neck is short and thick. The face is round, head large, mouth and eyes small.",
                    CharacterType = "Fierce and passionate, with an insatiable gusto for life but an equally intense sense of fairness and justice.",
                    Colors = "Red, yellow, or green",
                    Commentary = "Acquisitio is a figure of gain and success, balancing the imagery of loss in Amissio. In its astrological symbolism, Jupiter is the traditional planet of good fortune, while Sagittarius and Fire represent energy directed toward goals. Its inner element and elemental structure stress that real gain of any kind exists only in a web of interaction, and seeks productive manifestation; all the money in the world is useless if no one will accept it in exchange, or if it simply piles up untouched. The elemental structure also suggests the far from minor point that material gain, despite all its potential for interaction and material wealth, does not necessarily add up to the fulfillment of one's desires or the deepening of one's inner life.",
                    DivinatoryMeaning = "Acquisitio in a geomantic reading foretells success, profit, and gain, and often means that something desired is within your grasp. Traditionally, Acquisitio is a figure of prudence. It represents gain in every sense, and favors any situation where gaining something is desired. It is unfavorable whenever loss is desired, such as illness, facing things you fear, and getting out of difficult situations."
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
                    BodyType = "Medium height and a wiry, sinewy build, with a long neck, small ears and mouth, and eyes that tend to look downward all the time. People of this figure often have strong muscular tensions, especially in the shoulders and around the mouth.",
                    CharacterType = "Timid, especially about money and property, but stubborn. People of this figure tend to save more than they spend, and are fussy about their appearance.",
                    Colors = "White, russet, or dun (pale brown)",
                    Commentary = "Carcer is a figure of restriction and isolation, balancing the open and interactive nature of Conjunctio. This pattern of meanings has two sides, for restriction can be a source of strength and focus as well as a limitation. This is shown in its astrological symbolism, for Saturn, Capricorn, and Earth establish an imagery of rigidity and fixation, but also one of energy expended in productive work. The elemental structure develops the same theme; Fire and Earth represent energy and material expression, but they also remain at the two ends of the elemental spectrum, unable to come into contact with each other because neither of the middle elements are there to bridge the gap.",
                    DivinatoryMeaning = "Carcer in a geomantic chart represents solidity, restriction, binding, or even imprisonment. It always portends delay. In financial questions, it can stand for greed. It is unfavorable in most questions, but favors anything involving stability, security, and isolation."
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
                    Commentary = "Tristitia is a figure of sorrow and difficulty, balancing the joyous symbolism of Laetitia. Its astrological symbolism is complex; Saturn has traditional links to ideas of pain and trouble, and these are reinforced by turbulent Air, but Aquarius carries meanings of creativity and benevolence that may seem hard to relate to the symbolism of Saturn. The elemental structure and inner element stress Earth to the exclusion of all else, and this may also seem hard to connect to the other astrological symbols. The deeper level of meaning where these paradoxes resolve is simply that suffering is the one sure source of wisdom; too often, it's only when we are 'stuck,' caught up in a painful situation we do not know how to resolve, that we learn to face the world in more creative ways.",
                    DivinatoryMeaning = "In a geomantic reading, Tristitia stands for any downward movement. Lowered spirits (sorrow), lowered vitality (illness), and lowered expectations (failure) all fall within its ambit, though it can also mean stability and solidity, the sinking of roots deep into the ground. Unfavorable in most matters, it is favorable for questions involving stability and patience. It is very favorable in all questions dealing with building and the Earth, where its quality of 'stuckness' or permanence is wanted, and for any situation in which a secret needs to be kept."
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
                    Commentary = "Laetitia is a figure of joy, balancing the harsh symbolism of Tristitia. It represents happiness of every kind and level, from the most momentary of passing pleasures to the highest reaches of human experience. In its astrological symbolism, Jupiter has its usual role as the planet of good fortune, and watery Pisces and the outer element, Water, represent the emotional life, the context in which joy is usually experienced. The inner element and the elemental structure generally, however, stress the role of energy in the attainment of happiness; it is by way of the free flow of creative force, in ourselves as in the universe, that joy comes into being.",
                    DivinatoryMeaning = "Laetitia in a geomantic reading is a very positive figure representing any form of ascent. It means upward movement, which is favorable in a querent's career (success), emotional state (happiness), or vitality (health). It is traditionally a figure of fortitude, and is thus favorable in most questions, but is unfavorable for any question in which stability and deep roots are needed, and very unfavorable when a secret needs to be kept."
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
                    Commentary = "Cauda Draconis is a symbol of endings and completions, balancing the symbolism of beginnings in Caput Draconis. Its astrological symbolism is that of the south or descending node of the Moon—the point at which the Moon crosses the Sun's path to go into the southern heavens. This point has some of the same symbolism as Mars and Saturn, the two malefics or negative forces in traditional astrology, and so Cauda Draconis symbolizes disruptions, losses, and endings. Its inner and outer elements are both Fire; the elemental structure, which lacks only Earth, suggests a situation nearing completion and thus ripe for radical change.",
                    DivinatoryMeaning = "In a geomantic reading, Cauda Draconis represents ending, completion, and letting go of the past. A figure with a complex message, it brings good with evil and evil with good, but is traditionally a figure of generosity. It is favorable in any situation where something is coming to an end, but unfavorable for most other questions."
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
                    Commentary = "Caput Draconis is a figure of opportunities and beginnings, balancing Cauda Draconis's symbolism of endings. As the geomantic equivalent of the north or ascending node of the Moon, Caput Draconis shares much of the same focus on drastic change as Cauda, but the north node—the point at which the Moon crosses the Sun's path into the northern heavens—shares some of the symbolism of Venus and Jupiter, the two benefics or positive forces in traditional astrology. This figure thus represents change for the better, and is a particularly positive sign for beginnings. Earth, which is both its inner and outer element, and its elemental structure generally suggest fertile ground, needing only the energy of seed and sunlight; still, much depends on the seed that is planted there.",
                    DivinatoryMeaning = "In a geomantic reading, Caput Draconis represents beginnings and new possibilities. It tends to vary in meaning with its company, becoming more favorable with favorable figures and more unfavorable with unfavorable ones, but it often foretells a good result with some difficulty at the beginning. Caput Draconis is favorable in all matters having to do with beginnings, and unfavorable in questions where ending something is desirable."
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
                    BodyType = "Medium height and heavy build, with strong bones. The face is round and pale, the nose and forehead large, and the eyes dark. People of this figure have thick, rough, unkempt hair, and men tend to have beards of moderate length.",
                    CharacterType = "Bold, proud, and presumptuous, with a streak of insecurity and humility underneath the bravado, and a great deal of generosity and a strong sense of honor.",
                    Colors = "Gold or yellow",
                    Commentary = "Fortuna Minor is a figure of outer strength and success, balancing the inner strength of Fortuna Major. These two figures have the same astrological symbolism but the opposite elemental structure; they represent the same kind of experience—success—but have sharply different sources. Fortuna Minor represents success that is brought about by outside help or circumstances, rather than by the innate strength symbolized by Fortuna Major. Easily gained, the success of Fortuna Minor is just as easily lost, and it produces the best results in situations where rapid change is expected or desired. These factors are echoed in the symbolism by an unexpected shift in the outer element; Leo is a fiery sign, but Fortuna Minor is usually given the outer element of Air, representing the role of outside help in this figure as well as the instability of the results.",
                    DivinatoryMeaning = "Fortuna Minor in a geomantic reading represents unstable success. It is traditionally a figure of inconstancy, and also of generosity, and warns that what you gain may not stay with you for long. It is very favorable whenever you want to proceed quickly, and unfavorable for matters in which fickleness or instability is a problem."
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
                    BodyType = "Pear shaped (that is, larger below the waist than above it) and medium height, with a tendency to put on weight and sweat easily. People of this figure tend toward pale skin, round faces, and small teeth. One eye may be a different size or color than the other.",
                    CharacterType = "Patient and silent, with a deep inner life that rarely shows on the surface. Slow to anger, but once angered, equally slow to forgive. Fond of travel and moving from place to place.",
                    Colors = "White, flecked with blue",
                    Commentary = "Via is a figure of directed movement and change, balancing the diffuse and formless stability of Populus. With all four elements active, Via represents the elements in a constant state of change, each giving way to the next in an endless cycle. This elemental structure suggests that Via represents the most complete and balanced form of change, in which all aspects of experience are in motion and transformation. The astrological symbolism of the Moon and Cancer reinforces this image of constant change and movement, while the inner and outer elements of Water suggest that this change flows through the emotional and intuitive realms of experience.",
                    DivinatoryMeaning = "Via in a geomantic reading represents change in all its forms, and favors all questions in which change is an advantage. It is favorable for journeys of all kinds, real and metaphorical, but unfavorable whenever leaving things unchanged is desirable. Via suggests that the querent is in a period of transition and movement, and that the best course of action is to embrace change rather than resist it."
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