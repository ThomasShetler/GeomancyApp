using System;
using System.Collections.Generic;

namespace GeomancyApp
{
    internal static partial class FigureCorpus
    {
        private static TraditionalSourceEntry Ts(string author, string work, string section = null, int? year = null) =>
            new TraditionalSourceEntry { Author = author, Work = work, Section = section ?? string.Empty, Year = year };

        private static Dictionary<string, string> H(params (int n, string t)[] rows)
        {
            var d = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var (n, t) in rows) d[n.ToString()] = t;
            return d;
        }

        // Court-role companion to H(): keys are "RightWitness", "LeftWitness",
        // "Judge", "Reconciler" so the FigureDetailPanel can do a direct lookup
        // off SlotKind without massaging strings.
        private static Dictionary<string, string> C(string rightWitness, string leftWitness, string judge, string reconciler)
        {
            return new Dictionary<string, string>(StringComparer.Ordinal)
            {
                ["RightWitness"] = rightWitness,
                ["LeftWitness"] = leftWitness,
                ["Judge"] = judge,
                ["Reconciler"] = reconciler,
            };
        }

        private static FigureData Fig01Puer() => new FigureData
        {
            FigureID = "1",
            Name = "Puer",
            EnglishName = "Boy",
            OtherNames = "Beardless, yellow, warrior, man, sword",
            Quality = "Mobile",
            Keyword = "Energy",
            Imagery = "A sword; a male figure with exaggerated testicles",
            StrongHouse = "First",
            StrongHouseID = 1,
            WeakHouse = "Seventh",
            WeakHouseID = 7,
            Planet = "Mars",
            Sign = "Aries",
            InnerEl = "Air",
            OuterEl = "Fire",
            FireElement = "Active",
            AirElement = "Active",
            WaterElement = "Passive",
            EarthElement = "Active",
            Anatomy = "The head",
            BodyType = "A compact and athletic frame, with quick gestures and a complexion easily flushed by exertion or weather. The jaw and brow tend to be pronounced, the hairline somewhat uneven, and the hands often bear the small marks of tools, instruments, or contact sport.",
            CharacterType = "One who acts first and reflects afterward: competitive, blunt of speech, loyal in bursts, and restlessly curious. Such a person resists being managed and tends to find composure only after motion or contest has spent the initial charge.",
            Colors = "White, flecked with red",
            Commentary = @"Puer names the martial impulse that arrives ahead of reflection. The classical authors place the figure under Mars and Aries: outer fire, inner air, with the intellect overrunning its own consequences. Three lines stand active while water rests, so the figure carries heat and edge without the tempering grace of feeling. Its image is not the lover but the engineer of pressure—force in search of a channel. Where the medieval tradition described the young soldier or the duelist, the contemporary practitioner should attend to vectors: who initiates, who escalates, and where the first blow lands. Puer favors questions that turn on decisive intervention and disfavors those that require patience, secrecy, or careful diplomacy.",
            DivinatoryMeaning = "Swift motion, the opening of a contest, surgical intervention, mechanical repair, or any matter in which force applied early shapes the field. The figure favors situations that require crossing a threshold before doubt erodes the will, and disfavors reconciliation, long-horizon finance, or delicate questions of reputation. In the older texts it stands beside justice understood as contest rather than as calm judgment.",
            Tagline = "Heat at the threshold—courage, hurry, and a blade that wants a sheath",
            CoreMeaning = new List<string> { "initiative", "collision", "courage", "impetuosity", "contest", "breakthrough" },
            FavorableFor = new List<string> { "Decisions that cannot wait for consensus", "Physical training and honest contest", "Opening or sustaining a legal defense", "Breaking an impasse that decisive action can shift", "Technical repair under genuine deadline" },
            UnfavorableFor = new List<string> { "Repairing trust in a partnership", "Long-horizon budgeting and accumulation", "Anonymous leaks or testimony without clear attribution", "Nuanced maneuvering within institutions", "Diplomacy that requires many slow passes" },
            ElementalSynthesis = "Three active lines—fire, air, and earth—press outward while water lies passive, producing visible heat and articulation set against a dry emotional register. Inner air fans the outer fire, so that thought and speech accelerate action faster than feeling can refine it.",
            TraditionalImagery = new List<string> { "Sword or blade in classical geomantic glyphs", "Youthful masculine signifier in historical figure tables", "Mars and Aries paired in Renaissance correspondence wheels" },
            Interpretation = new List<string>
            {
                "Read Puer as kinetic honesty: the chart is naming a place where the querent will not wait for consensus. In consultation, identify the cost of an impulsive message, an abrupt resignation, or a public statement that cannot be retracted.",
                "Because water sleeps in this figure, empathy tends to arrive late. Where the matter calls for harmony, pair Puer with softer witnesses such as Albus or Populus before extending any promise of accord.",
                "In questions of health and surgery, Puer can affirm a cut or a decisive treatment. In matters of love it marks pursuit and argument more often than steady union."
            },
            InHouses = H(
                (1, "The querent is poised to move; self-presentation reads as direct, bold, or confrontational."),
                (2, "Funds and appetite move quickly—sharp spending, negotiation, or dispute over cash flow."),
                (3, "Siblings or near neighbors carry charge; correspondence sharpens and may escalate."),
                (4, "The household stirs—repairs, boundary tensions, or restless energy under one roof."),
                (5, "Risk in creativity, romance, or speculation is invited by daring rather than caution."),
                (6, "Labor intensifies; acute symptoms or friction with those who serve or report."),
                (7, "The other party meets as rival or spark; agreements need clear, firm terms."),
                (8, "Joint resources or crisis demand courage—shared stakes, procedures, or the surgeon's choice."),
                (9, "Travel or study presses forward; conversation about belief or law grows edged."),
                (10, "Public role sharpens—advancement pushed into view, or visible tension with authority."),
                (11, "Friends gather as allies; hopes are chased with little hedging."),
                (12, "What was hidden irritates or surfaces; regret follows haste, anger, or midnight choices.")
            ),
            InCourtRoles = C(
                rightWitness: "The beginning carries martial pressure already engaged—the querent entered with urgency, and urgency has shaped the question more than calm review.",
                leftWitness: "The outer climate escalates—rivals, deadlines, or systems lean the matter toward open contest.",
                judge: "The verdict comes by decisive thrust: resolution through force of act, often at cost to bystanders and goodwill.",
                reconciler: "What lingers after victory is brittle—a gain in the moment that asks later for repair, restraint, or apology."
            ),
            ModernExamples = new List<string>
            {
                "Sending a forceful wide reply before the correspondence has been read with care",
                "Committing to a tournament, trial shift, or sprint chiefly to prove capacity or pride",
                "Choosing immediate surgery when watchful waiting remains a reasonable alternative"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Cornelius Agrippa", "Three Books of Occult Philosophy", "Geomantic attributions / martial figures", 1651),
                Ts("Pseudo-Agrippa", "Of Geomancy", "Fourth Book", 1655),
                Ts("Christopher Cattan", "The Geomancie of Maister Christopher Cattan", "Figure tables", 1591)
            }
        };

        private static FigureData Fig02Amissio() => new FigureData
        {
            FigureID = "2",
            Name = "Amissio",
            EnglishName = "Loss",
            OtherNames = "Grasping externally, outer wealth, something escaped or lost",
            Quality = "Mobile",
            Keyword = "Loss",
            Imagery = "A bag held mouth downward, letting the contents fall out",
            StrongHouse = "Second",
            StrongHouseID = 2,
            WeakHouse = "Eighth",
            WeakHouseID = 8,
            Planet = "Venus",
            Sign = "Taurus",
            InnerEl = "Fire",
            OuterEl = "Earth",
            FireElement = "Active",
            AirElement = "Passive",
            WaterElement = "Active",
            EarthElement = "Passive",
            Anatomy = "The neck and throat",
            BodyType = "A solid neck and a jaw that is either square or softly rounded, with a voice that carries. The shoulders are broader than the hips, and the hands handle weight with practiced ease, whether at an instrument, a kitchen, or any work that requires lifting and setting down. The skin flushes readily with feeling.",
            CharacterType = "Warm, candid, and given to attachment that runs deeper than pride will easily admit. Such a person dislikes being overlooked and oscillates between open generosity and blunt requests; reconciliation is sincere when it comes, but rarely arrives by the shortest road.",
            Colors = "White, flecked with citrine (greenish yellow)",
            Commentary = @"Amissio pictures the purse inverted: a Venusian appetite meeting an earth that cannot keep every coin. Fire and water flare without a steady mediating air, and earth at the rim lies passive, so what is gained slips because the vessel itself is open. Medieval writers placed loss alongside love-offerings and the relief of burdens, and the contemporary reader does well to hold both valences at once. The figure speaks not only of poverty but of release—what leaves makes room. Practitioners formed by Agrippa and Cattan will recognize outer wealth in outflow while the inner fire still desires the object; the tension is appetite without a tight lid.",
            DivinatoryMeaning = "Outflow, ransom, donation, separation, depreciation, the shedding of debt, or the loss of track of an asset. The figure favors questions that require letting go—divestment, candid confession, the exit from a binding contract—and disfavors questions that depend on retention, credit, or exclusive possession.",
            Tagline = "What leaves makes room—open purse, shifting worth",
            CoreMeaning = new List<string> { "release", "outlay", "generosity", "dispersal", "vulnerable attachment", "liquidity" },
            FavorableFor = new List<string> { "Paying debt down with intention", "Giving, donating, or downsizing by choice", "Closing a bond that clings past its truth", "Medical withdrawal or habit change under skilled care", "Clearing stock, clutter, or backlog honestly" },
            UnfavorableFor = new List<string> { "Raising or tightly holding capital", "Keeping secrets that require a sealed vault", "Locking into a long lease without exit", "Compensation or perks that delay an honest exit" },
            ElementalSynthesis = "Fire and water are both active while air and earth lie passive: appetite and feeling move, but neither mediation nor structure follows. Value slips between the fingers because no airy contract and no earthen vault close the circuit.",
            TraditionalImagery = new List<string> { "Bag or purse inverted, mouth downward", "Coins spilling in woodcut and emblem traditions", "Venus and Taurus mapped on elemental correspondence wheels" },
            Interpretation = new List<string>
            {
                "Read Amissio as value in motion toward the exit. The first question is whether what leaves is being poured out by intention or drained by neglect.",
                "In matters of relationship the figure can bless a needed catharsis; in matters of finance it warns of thin margins unless Acquisitio answers it elsewhere in the chart.",
                "Held with care, the figure teaches that some losses purchase freedom. The practitioner should verify, however, that the querent is not mistaking waste for sacrifice."
            },
            InHouses = H(
                (1, "The sense of self loosens—rename, outward shift, or confession peels an old mask."),
                (2, "Money flows out—cut pay, levy, generous spend; subscriptions and fees deserve scrutiny."),
                (3, "Word travels wide; reputational scatter when a name or confidence escapes."),
                (4, "Domestic stake passes away—sale, heirloom leaving home, or caregiver emptied by duty."),
                (5, "Romance or recreation thins; a draft is set aside; a wager costs its stake."),
                (6, "Health or routine drains reserves; a companion animal finds a new home; hours at work shrink."),
                (7, "Partners divide shares in settlement, separate paths open, or clientele drifts to other hands."),
                (8, "Joint pools thin; insurance pays out; inheritance scatters among heirs."),
                (9, "Study or journey claims tuition or fare; a gatekeeper declines—the price of knowing."),
                (10, "Station or compensation slips; public honor traded for private conscience."),
                (11, "Collective funding falls short; friendship strains after money passed hands."),
                (12, "What was concealed surfaces in cost—care bill, leak of privacy, or quiet bleed from neglected agreements.")
            ),
            InCourtRoles = C(
                rightWitness: "The querent arrived already in release—an asset, attachment, or expectation has been leaving by degrees.",
                leftWitness: "The outer field moves outward—markets, partners, or platforms draw value away before it is fully noticed.",
                judge: "Resolution is parting: something tangible or cherished exits the querent's grasp—welcome when chosen, hard when unintended.",
                reconciler: "What remains is leaner and truer—fewer possessions or illusions, and a clearer sense of what actually sustains."
            ),
            ModernExamples = new List<string>
            {
                "Liquidating a brokerage account to fund relocation abroad",
                "Removing a dating profile after an honest conversation about limits",
                "Selling tools and inventory after a deliberate change of vocation"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Cornelius Agrippa", "Three Books of Occult Philosophy", "On fortune and Venusian figures", 1651),
                Ts("John Heydon", "Theomagia, or the Temple of Wisdome", "Vol. III geomantic chapters", 1664),
                Ts("Franz Hartmann", "The Principles of Astrological Geomancy", "Elemental loss and gain", 1889)
            }
        };

        private static FigureData Fig03Albus() => new FigureData
        {
            FigureID = "3",
            Name = "Albus",
            EnglishName = "White",
            OtherNames = "The Sage, The Hermit",
            Quality = "Stable",
            Keyword = "Peace, Solitude, Wisdom",
            Imagery = "A goblet set upright",
            StrongHouse = "Third",
            StrongHouseID = 3,
            WeakHouse = "Ninth",
            WeakHouseID = 9,
            Planet = "Mercury",
            Sign = "Gemini",
            InnerEl = "Water",
            OuterEl = "Air",
            FireElement = "Passive",
            AirElement = "Passive",
            WaterElement = "Active",
            EarthElement = "Passive",
            Anatomy = "The shoulders and lungs",
            BodyType = "An upper torso held with quiet lift, breathing that remains steady under stress, and a preference for layered clothing. The fingers rest lightly at the keys or stain with ink, and the eyes visibly soften when the person is listening.",
            CharacterType = "One who speaks little but precisely, keeps notebooks, and avoids crowds while building a small network with deliberation. Such a person calms a room by lowering the volume rather than raising it, and may withdraw into study for days at a time.",
            Colors = "Brilliant white, flecked with red",
            Commentary = @"Albus is the bright cup raised without spill: Mercury in Gemini, outer air over inner water, an intellect that mirrors a quiet depth. Only water acts here while fire and earth lie at rest. The pattern is one of reflection preceding motion—a scholar's lamp rather than a forge. Renaissance texts link the figure to peace treaties and to snowfields that quiet the advance of armies. In practice Albus marks clarity won by stepping sideways from the fight: where Rubeus pours passion out, Albus collects dew, and insight condenses as the heat drops.",
            DivinatoryMeaning = "Peaceful progress, study, paperwork that clears, the truce, the diagnosis that finally clarifies, and the gentle rain that follows drought. The figure disfavors brawls and any matter that requires scandal to gather attention; it favors examinations, audits in which truth is the asset, and negotiations conducted in good faith.",
            Tagline = "Still water, clear paper—quiet strength",
            CoreMeaning = new List<string> { "clarity", "study", "treaty", "restraint", "patience", "careful record" },
            FavorableFor = new List<string> { "Drafting briefs, policies, or testimony with care", "Sustained contemplation or structured retreat", "Seeking a measured second clinical opinion", "Careful review of records or policy with integrity as the aim", "Letters of apology or clarification that hold proportion" },
            UnfavorableFor = new List<string> { "Attention engineered through spectacle", "Hostile acquisition or seizure by pressure", "Public brawl or street confrontation", "High-stakes deception or bluff as strategy" },
            ElementalSynthesis = "A single active water line within airy outer and inner shells: feeling and intuition move while fire and earth keep watch. Thought circulates after the manner of Mercury and Gemini, but embodiment and aggression remain temperate.",
            TraditionalImagery = new List<string> { "White cup standing upright without spill", "Mercury's caduceus echoed in later emblem decks", "Embassy-of-peace motifs in Cattan-era figure charts" },
            Interpretation = new List<string>
            {
                "Albus rewards the querent who will document, breathe, and verify. The figure is a vote for procedure over charisma.",
                "Because only water is active, momentum is subtle: progress here shows itself as errors caught early rather than as visible fireworks.",
                "Where the chart contains adverse martial figures, Albus suggests cooling protocols—formal mediation, deliberate pause, or the careful edit of a trusted mentor."
            },
            InHouses = H(
                (1, "The querent favors restraint; bearing reads as thoughtful rather than theatrical."),
                (2, "Accounts yield steadiness; modest surplus from disciplined cuts and honest reckoning."),
                (3, "Near relations cool into truce; siblings verify facts before feud."),
                (4, "Dwelling becomes sanctuary—measured repair or careful planning for elders."),
                (5, "Romance or art proceeds with modesty; craft deepens in a quiet residency."),
                (6, "Healing follows regimen; a calm companion animal; workplace policy applied with humanity."),
                (7, "A mediator enters the partnership fray; agreements read line by line."),
                (8, "Legacy affairs settle without theater; therapeutic work loosens shame."),
                (9, "Foreign-study permits or travel papers advance; academic outline wins measured approval."),
                (10, "Leadership keeps its temper; a mentor shelters a junior from rumor."),
                (11, "Trusted circle—study group, steward, elder ally—offers sound counsel; hopes stay modest and workable."),
                (12, "Dreams yield usable insight; sleep improves when stimulation leaves the bedside.")
            ),
            InCourtRoles = C(
                rightWitness: "The origin is reflective—the querent has studied, journaled, or prepared in quiet rather than striking outward.",
                leftWitness: "The milieu bends cooperative—mediators, documents, or institutional goodwill incline toward the querent.",
                judge: "Judgment rests on clarity—a document, a ruling, or an honest conversation closes the matter without theater.",
                reconciler: "What follows is written down and holds—the settlement endures because it was reasoned, not improvised."
            ),
            ModernExamples = new List<string>
            {
                "Assembling clear records before a regulatory or compliance interview",
                "Deferring public reply until counsel or close friends have steadied the pulse",
                "Resuming a household budget after honest avoidance has run its course"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Pseudo-Agrippa", "Of Geomancy", "Figure Albus / white", 1655),
                Ts("Christopher Cattan", "The Geomancie of Maister Christopher Cattan", "Air and Mercury triplicities", 1591),
                Ts("Robert Fludd", "De Geomantia (Utriusque Cosmi)", "Latin commentary on passive triads", 1617)
            }
        };

        private static FigureData Fig04Populus() => new FigureData
        {
            FigureID = "4",
            Name = "Populus",
            EnglishName = "People",
            OtherNames = "Congregation, multitude, double path",
            Quality = "Stable",
            Keyword = "Stability",
            Imagery = "A crowd",
            StrongHouse = "Fourth",
            StrongHouseID = 4,
            WeakHouse = "Tenth",
            WeakHouseID = 10,
            Planet = "Moon",
            Sign = "Cancer",
            InnerEl = "Water",
            OuterEl = "Water",
            FireElement = "Passive",
            AirElement = "Passive",
            WaterElement = "Passive",
            EarthElement = "Passive",
            Anatomy = "The breasts and midriff",
            BodyType = "A torso that holds tension at the diaphragm and a gait that subtly synchronizes with whoever walks alongside. The complexion shifts with sleep, season, and inner tide, and the classical sketches emphasize a soft, receptive midsection.",
            CharacterType = "One who mirrors the company present and whose moods are easily caught by others. Loyal to the people they call their own, yet indecisive when alone, such a person collects stories and remembers, with surprising clarity, who sat where on the day that mattered.",
            Colors = "Sea green or dark russet brown",
            Commentary = @"Populus is the throng: every line passive, water doubled within and without. The Moon's sign of Cancer underscores collective memory, tide, and the kitchen in which everyone speaks at once. Agrippa's lineage names the figure a mirror for whatever approaches—neither good nor ill until colored by its neighbors in the chart. Elementally, nothing initiates; inertia itself becomes stability. The practitioner should therefore map the network around the question: who is actually in the room, which rumor or public channel shapes the opinion, and where the quorum, not the individual, rules the outcome.",
            DivinatoryMeaning = "The assembly, the audience, the poll, the household vote, the public mood, or any outcome decided by collective sentiment rather than by a single hand. The figure favors questions that require allies, sampling, or democratic cover, and disfavors questions that depend on a solitary decision or on stealth.",
            Tagline = "Many voices, one tide—motion borrowed from the whole",
            CoreMeaning = new List<string> { "multitude", "mirror", "inertia", "public square", "forum", "circulating story" },
            FavorableFor = new List<string> { "Community-backed funding or patronage", "Grassroots organizing and coalition work", "Measured comparison of options across a sample", "Peer support structured by mutual aid", "Negotiation carried by union or collective voice" },
            UnfavorableFor = new List<string> { "Solo venture without ally or crew", "Union undertaken in strict concealment", "Disclosure without corroborating witness or record", "Long concealment of invention or strategy alone" },
            ElementalSynthesis = "All four lines lie passive, so no element leads. The doubling of water at inner and outer registers suggests that emotional and somatic fields hold the field while fire, air, and earth remain latent. Change enters only when another figure energizes the row.",
            TraditionalImagery = new List<string> { "Crowd or multitude in classical glyphs", "Double path in Renaissance geomantic keys", "Moon and Cancer paired on elemental wheels" },
            Interpretation = new List<string>
            {
                "Populus invites the question of whose tide the querent is swimming in. In the absence of a strong Judge elsewhere in the chart, drift should be expected.",
                "In contemporary readings the figure points to the distributed actor: the circulating story, the household or team's ongoing exchange, the collective response. Agency is dispersed, and a single villain is rarely the answer.",
                "When paired with Rubeus or Puer the crowd grows volatile; when paired with Albus or Fortuna Major it tends toward the benevolent."
            },
            InHouses = H(
                (1, "Identity follows roles—parent, partner, title—more than private whim alone."),
                (2, "Earnings ride platforms, gratuities, or pooled household resources."),
                (3, "Neighborhood rumor sets what counts as fact; local consensus steers the tale."),
                (4, "Kinship table, tenants under one roof, or shared dwelling decides by vote."),
                (5, "Audience of enthusiasts, company of players, or crowd at play shapes pleasure."),
                (6, "Shift work, clinic queue, or open office—many bodies, one rhythm."),
                (7, "Marriage or firm partnership lived before witnesses; jury or peer panel weighs the bond."),
                (8, "Shared obligation—association levy, reserve fund, mutual insurance exposure."),
                (9, "Cohort of scholars, pilgrims traveling together, or long-distance study circle."),
                (10, "The institution's middle ranks convene; advancement follows the collective nod."),
                (11, "Patrons, correspondents, or pledge rolls carry hope through fellowship."),
                (12, "Ward of care, silent watchers online, or diffuse blame—pressure without a single face.")
            ),
            InCourtRoles = C(
                rightWitness: "The beginning is the room itself—family, crew, audience—the querent did not carry this question in solitude.",
                leftWitness: "Outer motion is collective: crowd, feed, or quorum bends the path more than any lone actor.",
                judge: "The verdict belongs to the assembly—what the wider body settles stands; solo leverage stays narrow.",
                reconciler: "Afterward the ripples run through the network—reputation, collective memory, and standing roles outlive the moment."
            ),
            ModernExamples = new List<string>
            {
                "A community-funded campaign exceeding its goal after trusted advocates widen the circle",
                "Neighbors weighing whether a new shop may open—sentiment, not one officer, decides tone",
                "A distributed team settling policy by asynchronous ballot across time zones"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Cornelius Agrippa", "Three Books of Occult Philosophy", "Lunar multitudes", 1651),
                Ts("John Heydon", "Theomagia, or the Temple of Wisdome", "Populus and assemblies", 1664),
                Ts("Franz Hartmann", "The Principles of Astrological Geomancy", "Passive totality", 1889)
            }
        };
    }
}
