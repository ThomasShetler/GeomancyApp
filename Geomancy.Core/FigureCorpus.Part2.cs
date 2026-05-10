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
            Tagline = "Deep fortune, sheltered gain, earned stature",
            CoreMeaning = new List<string> { "durability", "patronage", "inheritance", "gravitas", "reserve", "sanctuary" },
            FavorableFor = new List<string> { "Mortgage on well-chosen land", "Tenure or long-service review", "Structured rehabilitation after injury", "Family trust restructuring", "Renewal of merit-based aid" },
            UnfavorableFor = new List<string> { "Speculation driven chiefly by rumor", "Avoiding accountability after commitments", "Sacrificing durable ties for spectacle" },
            ElementalSynthesis = "Fire and air lie passive while water and earth move: heat and debate quiet themselves as moisture and ground cooperate, after the manner of agriculture in a sheltered valley. Inner earth anchors the outer fire of the solar sign, so that the pride of Leo expresses itself as stewardship rather than display.",
            TraditionalImagery = new List<string> { "Sheltered valley and flowing water", "Greater Fortune in classical keys", "Solar dignity in emblem tables" },
            Interpretation = new List<string>
            {
                "Read this figure as the compounding interest of virtue: small, correct choices accumulate until the world begins to notice.",
                "Paired with Tristitia, expect a long grind before the win; paired with Laetitia, the joy arrives after the foundations have settled.",
                "When the figure occupies a court position—particularly Judge or Reconciler—it suggests an effect on legacy that reaches well beyond the immediate question."
            },
            InHouses = H(
                (1, "Confidence grounded in lineage or formation; the frame restores itself when pace allows."),
                (2, "Resources deepen through real property, pension, or patient equity."),
                (3, "Circle of mentors; instruments and filings that shelter the querent."),
                (4, "Ancestral holding; a tended hearth; continuity carried through household craft."),
                (5, "Young dependents flourish under steadiness; patronage of arts or learning."),
                (6, "Long-standing ailment finds steadier footing; livelihood anchored by durable collective agreement."),
                (7, "Partnership between peers; covenants weighted toward the distant horizon."),
                (8, "Coverage fulfills its promise; joint holdings gain sober value."),
                (9, "Endowment or structured study away; pilgrimage or scholarship as blessing with form."),
                (10, "Standing earned by reliability; authority consonant with demonstrated tenure."),
                (11, "Patrons who commit substance, not only applause."),
                (12, "Withdrawal that ripens into retreat—cloister, residency, or chosen solitude.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is genuine standing—skill, reputation, or resources accumulated over time precede the question.",
                leftWitness: "External conditions favor the querent—visibility, allies, and timing tend toward openness rather than obstruction.",
                judge: "Verdict affirms clearly: the matter settles with the querent's authority intact and the outcome recognizably theirs.",
                reconciler: "Aftermath compounds wisely—success here leaves reserves that ease what follows without needless friction."
            ),
            ModernExamples = new List<string>
            {
                "Accepting a modest title at a durable institution when the wider economy contracts",
                "Investing in seismic retrofit before the shock that would have tested the structure",
                "A trust released when stated milestones, not appetite alone, have been met"
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
            Tagline = "Crossroads, mediation, converging threads",
            CoreMeaning = new List<string> { "linkage", "exchange", "mediation", "intelligence", "mixing", "routing" },
            FavorableFor = new List<string> { "Hiring or placement through trusted networks", "Import and export", "Couples or contract mediation", "Bridging separate accounts or networks", "Gatherings that form durable alliances" },
            UnfavorableFor = new List<string> { "Isolated classified systems", "Deliberate solitary retreat", "Exclusive control without partners" },
            ElementalSynthesis = "Active air and water braid through passive fire and earth: information and feeling circulate while ignition and ground wait their turn. The inner air steers the outer earth, so that analysis sorts the soil before any planting begins.",
            TraditionalImagery = new List<string> { "Crossroads", "Caduceus and messenger emblems", "Virgo's sorting of the harvest" },
            Interpretation = new List<string>
            {
                "Read Conjunctio in the manner of a graph: the nodes themselves matter less than the edges between them, and the practical question is who in the network will actually answer when called.",
                "Recovery questions—lost belongings, missing records—often resolve under this figure, because strangers and second-hand acquaintances supply the linking clues.",
                "If the Judge is harsh but Conjunctio sits on the ruler of the relevant house, a translator or intermediary may yet open the case."
            },
            InHouses = H(
                (1, "The querent sits at a hub; introductions and referrals define their path."),
                (2, "Split payments, joint holdings, income from several coordinated streams."),
                (3, "Dense messages and short hops; kin or neighbors who broker useful ties."),
                (4, "Shared dwelling or divided title; hospitality that links households."),
                (5, "Collaborative making; affection or artistry sustained through careful coordination."),
                (6, "Shift swaps and handoffs; multidisciplinary care around one regimen."),
                (7, "Co-counsel, agent, or matchmaker at the heart of the agreement."),
                (8, "Debt restructuring; funds released through escrow and timed conditions."),
                (9, "Travel companionship; teacher or publisher opens the next gate."),
                (10, "Overlapping lines of duty; mentor sponsors access to authority."),
                (11, "Collective allocation; mutual aid or governance that depends on many small links."),
                (12, "Anonymous channels; mentor chains in recovery; confidential lines that still connect.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is a recent meeting or convergence—the question took shape after a contact, dispatch, or timely overlap.",
                leftWitness: "Outside circumstance keeps joining threads—introductions, agreements, or unlikely parallels recur.",
                judge: "Verdict is union: resolution comes through alliance, contract, or merger that sets a lasting configuration.",
                reconciler: "Aftermath binds the querent into a relationship or system—the tie outlasts the first transaction."
            ),
            ModernExamples = new List<string>
            {
                "A measured introduction that secures the interview without exaggeration",
                "A parcel traced through a neighbor's witness, yielding the decisive clue",
                "A small facilitated group where the obstructing detail finally surfaces"
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
            CoreMeaning = new List<string> { "beauty", "reciprocity", "grace", "design", "fairness", "seasonal joy" },
            FavorableFor = new List<string> { "Interior design presentation", "Formal reconciliation or treaty", "Visual direction for a lasting work", "Couples counseling intake", "Consultation on beauty, balance, or presentation" },
            UnfavorableFor = new List<string> { "Solo expedition without alliance", "Coerced blunt disclosure without tact", "Long austerity demanding blunt endurance alone" },
            ElementalSynthesis = "Fire, water, and earth all act while air alone is passive: heat, feeling, and matter co-produce, while the cool intellectual distance thins. The scales of Libra here tip by mood more than by memorandum.",
            TraditionalImagery = new List<string> { "Mirror", "Venusian symmetry", "Traditional breast motif in woodcuts" },
            Interpretation = new List<string>
            {
                "Puella invites the practitioner to ask what is being polished for presentation, and what beneath the polish will hold once the audience departs.",
                "The figure is excellent where art direction or attentive bearing changes outcomes; it grows risky where ethics require a blunt refusal.",
                "Paired with Amissio, beauty exacts a cost; paired with Acquisitio, charm attracts the patron willing to underwrite it."
            },
            InHouses = H(
                (1, "The querent refines self-presentation; gracious bearing advances the request."),
                (2, "Spending shaped by taste; supplementary craft income; budget tuned to beauty."),
                (3, "Diplomatic sibling or peer; public face of one's affairs carefully composed."),
                (4, "Home staging; deliberate cultivation of domestic harmony."),
                (5, "Romantic beginnings; collaborative design; celebration arranged with care."),
                (6, "Restorative care; nursing touched by aesthetics; equilibrium in daily regimen."),
                (7, "Partnership planning or mediation; opposing counsel yields civility."),
                (8, "Graceful settlement of joint affairs; inheritance with a cosmetic dimension."),
                (9, "Romance or study abroad; fellowship in arts or letters."),
                (10, "Renewal of public name; deliberate refresh of reputation."),
                (11, "Friends drawn by shared taste; support won through coherent presentation."),
                (12, "Interior life grows vivid; distinguish genuine consolation from avoidance.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin lies in the querent's appeal—comeliness, tact, or cultivated grace has shaped reception thus far.",
                leftWitness: "The setting welcomes the querent—access eases, warmth meets them, first impressions run favorable.",
                judge: "Verdict pleases on the surface: resolution comes agreeably, while depth and endurance remain to be tested.",
                reconciler: "Aftermath stays congenial yet light—goodwill lingers without a binding pledge."
            ),
            ModernExamples = new List<string>
            {
                "Choosing lodging whose order signals the hosts' genuine care",
                "An acquaintance eloquent in conversation yet tentative when duty calls",
                "A refreshed public face that draws notice while the substance of the work stays familiar"
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
            Tagline = "Cup overturned, heat beneath the surface",
            CoreMeaning = new List<string> { "intensity", "purge", "taboo", "nocturnal strain", "investigation", "risk" },
            FavorableFor = new List<string> { "Therapeutic breakthrough after resistance", "Security audit with invasive scope", "Journalistic exposé grounded in evidence", "Frank disclosure within relationship repair", "Prescribed burn or controlled clearance" },
            UnfavorableFor = new List<string> { "Fundraising or offering before a scrutinizing public", "Formal immigration interview requiring restraint", "Structured silent retreat governed by vows" },
            ElementalSynthesis = "Only air is active above passive fire, water, and earth: breath and story do all the steering while fuel, feeling, and ground stay inert—a pressure vessel that holds until something gives it vent.",
            TraditionalImagery = new List<string> { "Inverted cup", "Scorpionic Mars and emblematic red", "Malefic joy in classical house lore" },
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
                rightWitness: "Origin carries compromise—the querent enters with mixed motives, suppressed resentment, or impairment clouding the question.",
                leftWitness: "The outer climate skews hostile or volatile—deception, aggression, or unstable structures steer events.",
                judge: "Verdict counsels restraint or withdrawal: resolution tends harsh, often through exposure or a clash better left unfought.",
                reconciler: "Aftermath costs endure—standing, trust, or vitality bear harm that outlasts the immediate quarrel."
            ),
            ModernExamples = new List<string>
            {
                "A careful public account of a troubled workplace, accurate yet far-reaching",
                "A lapse under strain answered by renewed, accountable commitment to healing",
                "A vulnerability disclosed that forces an unavoidable ethical reckoning"
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
