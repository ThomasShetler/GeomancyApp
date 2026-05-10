using System;
using System.Collections.Generic;

namespace GeomancyApp
{
    internal static partial class FigureCorpus
    {
        private static FigureData Fig05FortunaMajor() => new FigureData
        {
            FigureID = "5",
            Name = "Fortuna Major",
            EnglishName = "Greater Fortune",
            OtherNames = "Inward fortune, protection going in, greater omen, inside or hidden help",
            Quality = "Stable",
            Keyword = "Power",
            Imagery = "A valley through which a river flows",
            StrongHouse = "Fifth",
            StrongHouseID = 5,
            WeakHouse = "Eleventh",
            WeakHouseID = 11,
            Planet = "Sun",
            Sign = "Leo",
            InnerEl = "Earth",
            OuterEl = "Fire",
            FireElement = "Passive",
            AirElement = "Passive",
            WaterElement = "Active",
            EarthElement = "Active",
            Anatomy = "The heart and chest",
            BodyType = "Open, deep chest-breathing and warm, steady hands. The posture often shows a small compensation—one leg carried slightly differently from the other—and the smile, when offered in sincerity, reaches the eyes.",
            CharacterType = "Generous, quietly proud, and reliable in the keeping of promises. Such a person spends to lift others, dislikes pettiness, and bears responsibility without making a display of it.",
            Colors = "Green, yellow, or gold",
            Commentary = @"Fortuna Major is the sheltered valley that gathers water: Sun in Leo, yet the outer fire rests while inner earth and the active water and earth lines shape the terrain. The classical authors place this figure in deliberate contrast with the ridge of Fortuna Minor—here fortune is deep, slow, and structural. The help arriving for the querent comes from foundations, elders, savings, or an institution with room enough to absorb shock. Agrippa's lineage ties the figure to inward protection, and the practitioner should therefore look for durable, accumulating luck rather than for a sudden visible spike.",
            DivinatoryMeaning = "Substantial fortune obtained through persistence, the inheritance of one's own past effort, institutional backing, or an integrity recognized late. The figure favors promotions earned by record, properties that appreciate on fundamentals, and recoveries that succeed through patient regimen. It disfavors short-horizon transactions that rest on attention alone.",
            Tagline = "Deep luck, sheltered gain, earned crown",
            CoreMeaning = new List<string> { "durability", "patronage", "inheritance", "gravitas", "reserve", "sanctuary" },
            FavorableFor = new List<string> { "Mortgage on solid land", "Tenure review", "Rehab after injury", "Family trust restructuring", "Scholarship renewal" },
            UnfavorableFor = new List<string> { "Day-trading on rumor", "Ghosting accountability", "Burning bridges for clout" },
            ElementalSynthesis = "Fire and air lie passive while water and earth move: heat and debate quiet themselves as moisture and ground cooperate, after the manner of agriculture in a sheltered valley. Inner earth anchors the outer fire of the solar sign, so that the pride of Leo expresses itself as stewardship rather than display.",
            TraditionalImagery = new List<string> { "Valley and river", "Greater fortune mark in old keys", "Solar dignity imagery" },
            Interpretation = new List<string>
            {
                "Read this figure as the compounding interest of virtue: small, correct choices accumulate until the world begins to notice.",
                "Paired with Tristitia, expect a long grind before the win; paired with Laetitia, the joy arrives after the foundations have settled.",
                "When the figure occupies a court position—particularly Judge or Reconciler—it suggests an effect on legacy that reaches well beyond the immediate question."
            },
            InHouses = H(
                (1, "Core confidence rooted in lineage or training; body recovers with rest."),
                (2, "Wealth accrues through property, pension, or slow equity."),
                (3, "Mentor network; documents that protect the querent."),
                (4, "Ancestral land, well-maintained home, generational recipes."),
                (5, "Children thrive with steady support; art patronage."),
                (6, "Chronic condition stabilizes; union job security."),
                (7, "Marriage of equals; contract favors long horizon."),
                (8, "Insurance pays; shared asset appreciates."),
                (9, "University endowment; pilgrimage scholarship."),
                (10, "Reputation for reliability; corner office earned."),
                (11, "Benefactors who write checks, not just likes."),
                (12, "Isolation that becomes retreat; monastery, artist residency.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is genuine standing—skill, status, or resources the querent earned over time precede the question.",
                leftWitness: "External pull is favorable—visibility, allies, or daylight conditions are working in the querent's direction.",
                judge: "Verdict is solid yes: the matter resolves with the querent's authority intact and the outcome clearly theirs.",
                reconciler: "Aftermath compounds—success here builds capital that opens the next door without further struggle."
            ),
            ModernExamples = new List<string>
            {
                "Accepting a lower title at a stable firm during a recession",
                "Paying extra for earthquake retrofit that later saves the house",
                "Trust fund released at 30 after milestones met"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Cornelius Agrippa", "Three Books of Occult Philosophy", "Fortune figures", 1651),
                Ts("Pseudo-Agrippa", "Of Geomancy", "Fortuna Major", 1655),
                Ts("Robert Fludd", "De Geomantia (Utriusque Cosmi)", "Valley symbolism", 1617)
            }
        };

        private static FigureData Fig06Conjunctio() => new FigureData
        {
            FigureID = "6",
            Name = "Conjunctio",
            EnglishName = "Conjunction",
            OtherNames = "Association, gathering together",
            Quality = "Mobile",
            Keyword = "Interaction",
            Imagery = "A crossroads",
            StrongHouse = "Sixth",
            StrongHouseID = 6,
            WeakHouse = "Twelfth",
            WeakHouseID = 12,
            Planet = "Mercury",
            Sign = "Virgo",
            InnerEl = "Air",
            OuterEl = "Earth",
            FireElement = "Passive",
            AirElement = "Active",
            WaterElement = "Active",
            EarthElement = "Passive",
            Anatomy = "The intestines and belly",
            BodyType = "A lithe waist and expressive hands that punctuate speech, with a digestion sensitive to stress. The hair tends toward a neat part, and the carriage favors a satchel worn across the body—always ready to turn.",
            CharacterType = "One who joins points quickly, takes pleasure in introductions, and is willing to entertain flexibility of means when better information becomes available. The risk is loose talk; the gift is an unusual fluency with logistics.",
            Colors = "Purple or pale gray, sometimes black speckled with blue",
            Commentary = @"Conjunctio is the crossroads at which streams meet: Mercury in Virgo, inner air over outer earth, with air and water carrying the motion. Fire sleeps at the head and earth at the feet, leaving the middle realm to do the negotiation. The medieval geomancers turned to this figure for messengers, brokers, and the recovery of lost goods, on the principle that links reappear. The contemporary practitioner may translate that intuition into the language of supply chains, networks, and the introductions that re-route a vocation. The figure is, by nature, morally mixed: contacts multiply outcomes; they do not guarantee virtue.",
            DivinatoryMeaning = "Networks, mediation, logistics, reconciliations of accounts, the recovery of items through a chain of acquaintances, and conversations that bring previously separate parties into agreement. The figure favors questions in which connection itself is the missing variable, and disfavors questions in which isolation, confidentiality, or strict separation is required.",
            Tagline = "Crossroads, brokers, APIs of fate",
            CoreMeaning = new List<string> { "linkage", "exchange", "mediation", "data", "mixing", "routing" },
            FavorableFor = new List<string> { "Recruiting", "Import/export", "Couples therapy", "Blockchain bridges", "Conference networking" },
            UnfavorableFor = new List<string> { "Air-gapped classified work", "Hermit phase", "Monopoly without partners" },
            ElementalSynthesis = "Active air and water braid through passive fire and earth: information and feeling circulate while ignition and ground wait their turn. The inner air steers the outer earth, so that analysis sorts the soil before any planting begins.",
            TraditionalImagery = new List<string> { "Crossroads", "Mercury caduceus echoes", "Virgo harvest sorting" },
            Interpretation = new List<string>
            {
                "Read Conjunctio in the manner of a graph: the nodes themselves matter less than the edges between them, and the practical question is who in the network will actually answer when called.",
                "Recovery questions—lost belongings, missing records—often resolve under this figure, because strangers and second-hand acquaintances supply the linking clues.",
                "If the Judge is harsh but Conjunctio sits on the ruler of the relevant house, a translator or intermediary may yet open the case."
            },
            InHouses = H(
                (1, "Querent is hub; many introductions define them."),
                (2, "Split payments, joint accounts, gig stack income."),
                (3, "Slack threads, cousin connectors, short trips chaining."),
                (4, "Roommates, duplex, Airbnb co-host."),
                (5, "A collaborative creative project; the careful coordination of layered relationships."),
                (6, "Shift swap board; integrative medicine team."),
                (7, "Co-counsel, agent, or matchmaker in deal."),
                (8, "Debt restructuring; escrow handshake."),
                (9, "Study abroad buddy; publisher introduces editor."),
                (10, "Matrix org; mentor introduces board."),
                (11, "DAO vote; mutual aid spreadsheet."),
                (12, "Anonymous tip line; twelve-step sponsor chain.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is a recent meeting or convergence—the question crystallized after a contact, message, or chance overlap.",
                leftWitness: "External flow is bringing parties or threads together—introductions, deals, or coincidences keep arriving.",
                judge: "Verdict is union: the matter resolves through an alliance, contract, or merger that locks the situation into a new shape.",
                reconciler: "Aftermath ties the querent into a relationship or system—the connection persists past the original transaction."
            ),
            ModernExamples = new List<string>
            {
                "LinkedIn intro that lands the interview",
                "Package rerouted through neighbor's porch cam",
                "Zoom breakout room that solves the bug"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Pseudo-Agrippa", "Of Geomancy", "Conjunctio", 1655),
                Ts("Christopher Cattan", "The Geomancie of Maister Christopher Cattan", "Crossroads meetings", 1591),
                Ts("John Heydon", "Theomagia, or the Temple of Wisdome", "Mercury conjunctions", 1664)
            }
        };

        private static FigureData Fig07Puella() => new FigureData
        {
            FigureID = "7",
            Name = "Puella",
            EnglishName = "Girl",
            OtherNames = "Beauty, purity",
            Quality = "Stable",
            Keyword = "Harmony",
            Imagery = "A mirror; a female figure with exaggerated breasts",
            StrongHouse = "Seventh",
            StrongHouseID = 7,
            WeakHouse = "First",
            WeakHouseID = 1,
            Planet = "Venus",
            Sign = "Libra",
            InnerEl = "Water",
            OuterEl = "Air",
            FireElement = "Active",
            AirElement = "Passive",
            WaterElement = "Active",
            EarthElement = "Active",
            Anatomy = "The kidneys and lower back",
            BodyType = "A gracefully curved lumbar line, a long neck, and a voice naturally tuned to harmony. The lower back tends to register imbalance before the mind acknowledges it, and the hands instinctively arrange whatever materials the day presents—flowers, fabrics, or the surfaces of a room.",
            CharacterType = "One who seeks equilibrium and possesses a sharp aesthetic judgment. Such a person tends to avoid open conflict until cornered, holds romantic ideals at a high pitch, and uses social grace as a deliberate, kind instrument.",
            Colors = "White, flecked with green",
            Commentary = @"Puella stands in counterpoise to Puer: Venus in Libra, outer air over inner water, with fire, water, and earth active and only air at rest. The mirror in the figure's image hints at reciprocity—beauty understood as negotiation. The older authors warn that the joys conferred here can be short-lived unless the purse of earth and the feeling of water remain in alignment. The figure is favorable but inclined to fickleness; it loves the dance more readily than it loves the contract. The practitioner should therefore weigh whether the charm in question is structural or merely a finish.",
            DivinatoryMeaning = "Courtship, the care of surfaces, diplomacy, the fair division of goods, regimens of grooming, the elegance of a settlement. The figure favors first encounters and occasions that depend on grace, and is weak for instruments such as binding agreements unless other figures supply the necessary weight.",
            Tagline = "Charm, balance, mirrored desire",
            CoreMeaning = new List<string> { "beauty", "reciprocity", "flirtation", "design", "fairness", "seasonal joy" },
            FavorableFor = new List<string> { "Interior design pitch", "Peace treaty signing", "Album art direction", "Couples counseling intake", "Cosmetic procedure consult" },
            UnfavorableFor = new List<string> { "Solo polar expedition", "Brutalist truth serum", "Long bear market grit" },
            ElementalSynthesis = "Fire, water, and earth all act while air alone is passive: heat, feeling, and matter co-produce, while the cool intellectual distance thins. The scales of Libra here tip by mood more than by memorandum.",
            TraditionalImagery = new List<string> { "Mirror", "Venusian symmetry", "Breast glyph in woodcuts" },
            Interpretation = new List<string>
            {
                "Puella invites the practitioner to ask what is being polished for presentation, and what beneath the polish will hold once the audience departs.",
                "The figure is excellent where art direction or attentive bearing changes outcomes; it grows risky where ethics require a blunt refusal.",
                "Paired with Amissio, beauty exacts a cost; paired with Acquisitio, charm attracts the patron willing to underwrite it."
            },
            InHouses = H(
                (1, "Querent polishes image; charisma carries the ask."),
                (2, "Styled spending; Etsy side, floral budget."),
                (3, "Diplomatic sibling; curated Instagram grid."),
                (4, "Home staging; domestic harmony project."),
                (5, "Rom-com arc; design sprint; baby shower aesthetics."),
                (6, "Spa shift; aesthetic nursing; diet balance."),
                (7, "Wedding planner; opposing counsel respects tone."),
                (8, "Elegant divorce settlement; cosmetic inheritance."),
                (9, "Study abroad romance; museum fellowship."),
                (10, "Brand relaunch; executive headshot season."),
                (11, "A circle of friends gathers around shared aesthetics; a campaign succeeds on presentation."),
                (12, "An interior fantasy life grows rich; the practitioner should distinguish solace from escape.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is the querent's appeal—charm, beauty, or social grace shapes how the matter has been received so far.",
                leftWitness: "Environment is welcoming the querent—doors open, smiles meet them, surfaces look easy.",
                judge: "Verdict is pleasant on the surface: the matter resolves agreeably, though depth and durability are unproven.",
                reconciler: "Aftermath is congenial but light—the situation leaves goodwill, not necessarily commitment."
            ),
            ModernExamples = new List<string>
            {
                "Choosing the lodging whose careful presentation reflects the care of its hosts",
                "An acquaintance that flourishes in conversation and falters at the first practical demand",
                "A rebrand that gains attention while leaving the underlying instrument unchanged"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Cornelius Agrippa", "Three Books of Occult Philosophy", "Venus figures", 1651),
                Ts("John Heydon", "Theomagia, or the Temple of Wisdome", "Puella harmonies", 1664),
                Ts("Franz Hartmann", "The Principles of Astrological Geomancy", "Libra triads", 1889)
            }
        };

        private static FigureData Fig08Rubeus() => new FigureData
        {
            FigureID = "8",
            Name = "Rubeus",
            EnglishName = "Red",
            OtherNames = "Burning, danger",
            Quality = "Mobile",
            Keyword = "Passion",
            Imagery = "A goblet turned upside down",
            StrongHouse = "Eighth",
            StrongHouseID = 8,
            WeakHouse = "Second",
            WeakHouseID = 2,
            Planet = "Mars",
            Sign = "Scorpio",
            InnerEl = "Air",
            OuterEl = "Water",
            FireElement = "Passive",
            AirElement = "Active",
            WaterElement = "Passive",
            EarthElement = "Passive",
            Anatomy = "The genitals and reproductive system",
            BodyType = "Skin that flushes readily, eyelids that hood under strong feeling, and a voice that drops in register when emotion rises. The hips move with deliberate weight, the body often carries the marks of past intensity, and stress finds a metallic note in the perspiration.",
            CharacterType = "One who gives intimacy entirely or not at all, with a guarded streak of jealousy and an instinct to set the room in motion. Channeled with discipline, the same force becomes investigative grit; left unchanneled, it can manufacture difficulty for the sake of feeling alive.",
            Colors = "Red, flecked or streaked with brown",
            Commentary = @"Rubeus is the cup overturned: Mars in Scorpio, outer water over inner air, with only air in motion while fire rests at the head. Passion that finds no upward outlet runs downward, and the medieval authors warned that this figure is good in evil things and evil in good ones. The practitioner must not moralize too quickly: at times Rubeus names a necessary surgery, a long-postponed honesty, or the investigation that will inevitably scorch its sources. The inner air steers obsession into narrative, while the outer water keeps the whole composition tidal.",
            DivinatoryMeaning = "Intensity that strips the surface from a matter—the encounter that cannot remain decorous, the purge, the investigation, the long-postponed reckoning. The figure favors questions in which a controlled descent is itself the medicine, and disfavors questions that depend on a clean public surface.",
            Tagline = "Spilled cup, underground heat",
            CoreMeaning = new List<string> { "intensity", "purge", "taboo", "nightlife", "investigation", "risk" },
            FavorableFor = new List<string> { "Therapy breakthrough", "Penetration test", "Journalistic exposé", "Radical honesty in affair recovery", "Controlled burn forestry" },
            UnfavorableFor = new List<string> { "IPO roadshow polish", "Visa interview decorum", "Silent retreat vows" },
            ElementalSynthesis = "Only air is active above passive fire, water, and earth: breath and story do all the steering while fuel, feeling, and ground stay inert—a pressure vessel that holds until something gives it vent.",
            TraditionalImagery = new List<string> { "Inverted cup", "Scorpionic mars red", "Danger house joy in old texts" },
            Interpretation = new List<string>
            {
                "The querent who draws Rubeus should never be shamed for it. The proper question is where the heat must be honored, and how it can be honored without scorching the people who depend on the querent.",
                "Paired with Cauda Draconis, endings tend to be uncontrolled; paired with Caput Draconis, the same intensity becomes the engine of a deliberate rebirth.",
                "When Rubeus occupies the first house, the figure asks the querent for honesty about motive—truthfully, and without flinching."
            },
            InHouses = H(
                (1, "The querent draws conflict and desire to themselves; the public mask begins to slip."),
                (2, "Spending becomes impulsive—a wager, an indulgence, an expensive emotional release."),
                (3, "A private message risks exposure; a sibling quarrel turns sharp."),
                (4, "Something hidden in the home or in the family line surfaces and demands attention."),
                (5, "An evening of intensity, a romantic spark, a creative work that does not flinch."),
                (6, "A health concern needing honesty; a workplace whose conditions cannot be ignored."),
                (7, "An obsessive former tie returns; due diligence on a partnership uncovers what was hidden."),
                (8, "A contested estate, an unresolved intimacy, a binding decision made under pressure."),
                (9, "Inquiry into difficult subjects—initiation, taboo material, the harder corners of research."),
                (10, "A reputational crisis or the exposure of misconduct in a position of authority."),
                (11, "Friendships entangled with desire; alliances formed below the public surface."),
                (12, "An old habit reasserts itself; a private fear that has waited too long to be named.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is something compromised—the querent walked in with mixed motives, hidden grudge, or substances clouding the question itself.",
                leftWitness: "External climate is hostile or chaotic—deception, hostility, or unsafe systems are driving the trajectory.",
                judge: "Verdict warns off: the matter resolves badly, often through revealed corruption or a fight that should not have been picked.",
                reconciler: "Aftermath leaves wreckage—reputation, trust, or health pay a price that lasts longer than the conflict."
            ),
            ModernExamples = new List<string>
            {
                "A truthful public account of a difficult workplace, which is both accurate and consequential",
                "A relapse during a high-pressure season followed by honest commitment to recovery",
                "An exposed vulnerability whose disclosure provokes a difficult ethical debate"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Pseudo-Agrippa", "Of Geomancy", "Rubeus warnings", 1655),
                Ts("Christopher Cattan", "The Geomancie of Maister Christopher Cattan", "Malefic joy", 1591),
                Ts("Cornelius Agrippa", "Three Books of Occult Philosophy", "Mars in fixed water", 1651)
            }
        };
    }
}
