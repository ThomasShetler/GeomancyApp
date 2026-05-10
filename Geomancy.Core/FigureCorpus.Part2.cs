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
            BodyType = "Open chest breathing; warm steady hands; often one leg slightly longer or hip higher—compensated posture; smile reaches eyes when sincere.",
            CharacterType = "Generous, proud quietly, keeps promises; spends to uplift others; dislikes pettiness; carries responsibility without boasting.",
            Colors = "Green, yellow, or gold",
            Commentary = @"Fortuna Major is the sheltered valley that gathers water: Sun in Leo yet the outer fire rests while inner earth and active water/earth lines shape terrain. Classical authors contrast it with Fortuna Minor's ridge—here fortune is deep, slow, and structural. The querent's help comes from foundations, elders, savings, or an institution that has room to absorb shock. Agrippa's lineage ties the figure to inward protection; the diviner should look for durable luck rather than viral spikes.",
            DivinatoryMeaning = "Major luck through persistence, inheritance of effort, institutional backing, or inner integrity recognized late. Good for promotions earned by record, property that appreciates on fundamentals, recovery after illness through regimen. Poor for quick flips that rely on hype alone.",
            Tagline = "Deep luck, sheltered gain, earned crown",
            CoreMeaning = new List<string> { "durability", "patronage", "inheritance", "gravitas", "reserve", "sanctuary" },
            FavorableFor = new List<string> { "Mortgage on solid land", "Tenure review", "Rehab after injury", "Family trust restructuring", "Scholarship renewal" },
            UnfavorableFor = new List<string> { "Day-trading on rumor", "Ghosting accountability", "Burning bridges for clout" },
            ElementalSynthesis = "Passive fire and air with active water and earth: heat and debate cool while moisture and ground co-produce—valley agriculture logic. Inner earth anchors outer fire sign, so Leo pride expresses as stewardship.",
            TraditionalImagery = new List<string> { "Valley and river", "Greater fortune mark in old keys", "Solar dignity imagery" },
            Interpretation = new List<string>
            {
                "Read this as compounding interest on virtue: small right choices stack until the world notices.",
                "When paired with Tristitia, expect slow grind before the win; with Laetitia, joy after foundations settle.",
                "Court positions: strong Judge or Reconciler here suggests legacy impact beyond the headline question."
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
            BodyType = "Lithe waist, expressive hands that punctuate speech; sensitive digestion tied to stress; neat hair part; prefers cross-body bags—always ready to pivot.",
            CharacterType = "Connects dots fast; loves introductions; morally flexible about means if data improves; gossip risk; brilliant at logistics.",
            Colors = "Purple or pale gray, sometimes black speckled with blue",
            Commentary = @"Conjunctio is the crossroads where streams meet: Mercury in Virgo, inner air, outer earth, while air and water carry motion. Fire sleeps at the head, earth at the feet—middle realm negotiates. Medieval geomancers used it for messengers, brokers, and recovery of stolen goods because links reappear. Today think APIs, supply chains, and introductions that reroute a career. The figure is morally mixed by nature—contacts multiply outcomes, not guarantee virtue.",
            DivinatoryMeaning = "Networking, APIs, mediation, logistics hubs, reconciling ledgers, finding lost items through a chain of tips, polyamory negotiations, or merger talks. Favorable when connection itself is the missing variable; unfavorable when isolation, NDAs, or air-gap security is required.",
            Tagline = "Crossroads, brokers, APIs of fate",
            CoreMeaning = new List<string> { "linkage", "exchange", "mediation", "data", "mixing", "routing" },
            FavorableFor = new List<string> { "Recruiting", "Import/export", "Couples therapy", "Blockchain bridges", "Conference networking" },
            UnfavorableFor = new List<string> { "Air-gapped classified work", "Hermit phase", "Monopoly without partners" },
            ElementalSynthesis = "Active air and water braid through passive fire and earth: information and feeling circulate while ignition and ground wait. Inner air steers outer earth—analysis sorts the soil.",
            TraditionalImagery = new List<string> { "Crossroads", "Mercury caduceus echoes", "Virgo harvest sorting" },
            Interpretation = new List<string>
            {
                "Treat Conjunctio as graph theory: nodes matter less than edges; count who actually picks up the phone.",
                "Recovery questions shine—stolen bike, lost passport—because strangers link clues.",
                "If Judge is harsh but Conjunctio sits on house ruler, a translator fixes the case."
            },
            InHouses = H(
                (1, "Querent is hub; many introductions define them."),
                (2, "Split payments, joint accounts, gig stack income."),
                (3, "Slack threads, cousin connectors, short trips chaining."),
                (4, "Roommates, duplex, Airbnb co-host."),
                (5, "Collaborative art jam; polycule logistics."),
                (6, "Shift swap board; integrative medicine team."),
                (7, "Co-counsel, agent, or matchmaker in deal."),
                (8, "Debt restructuring; escrow handshake."),
                (9, "Study abroad buddy; publisher introduces editor."),
                (10, "Matrix org; mentor introduces board."),
                (11, "DAO vote; mutual aid spreadsheet."),
                (12, "Anonymous tip line; twelve-step sponsor chain.")
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
            BodyType = "Curved lumbar, graceful neck, voice tuned for harmony; lower back aches when harmony breaks; hands arrange flowers, decks, or UI layouts instinctively.",
            CharacterType = "Seeks balance; aesthetic judgment sharp; conflict-averse until cornered; romantic ideals high; social grace weaponized kindly.",
            Colors = "White, flecked with green",
            Commentary = @"Puella balances Puer: Venus in Libra, outer air, inner water, with fire, water, and earth active—only air rests. The mirror glyph hints at reciprocity: beauty as negotiation. Older texts stress short-lived joys unless earth's purse and water's feeling stay aligned. The figure is favorable yet fickle; it loves the dance more than the contract. Diviners should weigh whether charm is structure or veneer.",
            DivinatoryMeaning = "Courtship, décor, diplomacy, fair division, skincare regimens, UX polish, legal settlement that looks elegant. Favorable for first dates and gallery openings; weak for ironclad prenups without other stabilizers.",
            Tagline = "Charm, balance, mirrored desire",
            CoreMeaning = new List<string> { "beauty", "reciprocity", "flirtation", "design", "fairness", "seasonal joy" },
            FavorableFor = new List<string> { "Interior design pitch", "Peace treaty signing", "Album art direction", "Couples counseling intake", "Cosmetic procedure consult" },
            UnfavorableFor = new List<string> { "Solo polar expedition", "Brutalist truth serum", "Long bear market grit" },
            ElementalSynthesis = "Fire, water, earth active; air passive: heat, feeling, and matter co-produce while intellectual distance thins—Libra's scales tip by mood more than memo.",
            TraditionalImagery = new List<string> { "Mirror", "Venusian symmetry", "Breast glyph in woodcuts" },
            Interpretation = new List<string>
            {
                "Puella asks what is being polished for show versus what will hold Monday morning.",
                "Excellent when art direction or bedside manner changes outcomes; risky when ethics need blunt refusal.",
                "With Amissio, beauty costs; with Acquisitio, charm attracts backers."
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
                (11, "Galentine energy; Kickstarter pretty page."),
                (12, "Fantasy life rich; check for escapism.")
            ),
            ModernExamples = new List<string>
            {
                "Choosing the Airbnb with better photos—and better hosts",
                "Dating app match that thrives on voice notes, dies on logistics",
                "Rebrand that doubles clicks but masks tech debt"
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
            BodyType = "Flushed skin, heavy eyelids, voice drops low when aroused or angry; hips decisive; tattoos or scars tell stories; sweat copper-scented under stress.",
            CharacterType = "All-in intimacy; jealous streak; party catalyst; sober version channels investigative grit; bored version self-sabotages for drama.",
            Colors = "Red, flecked or streaked with brown",
            Commentary = @"Rubeus is the cup overturned: Mars in Scorpio, outer water, inner air—only air acts while fire rests at the head. Passion without upward outlet smears downward; medieval authors warned it is good in evil and evil in good. The diviner must not moralize lazily: sometimes the figure names necessary surgery, sexual healing, or investigative journalism that burns sources. Air inside steers obsession toward narrative; water outside keeps it tidal.",
            DivinatoryMeaning = "Sex, intoxication, investigation, purge, riot, detox, BDSM negotiation, ransomware crisis—anything where intensity strips varnish. Favorable when controlled descent into underworld is the cure; terrible for squeaky-clean optics.",
            Tagline = "Spilled cup, underground heat",
            CoreMeaning = new List<string> { "intensity", "purge", "taboo", "nightlife", "investigation", "risk" },
            FavorableFor = new List<string> { "Therapy breakthrough", "Penetration test", "Journalistic exposé", "Radical honesty in affair recovery", "Controlled burn forestry" },
            UnfavorableFor = new List<string> { "IPO roadshow polish", "Visa interview decorum", "Silent retreat vows" },
            ElementalSynthesis = "Only air active atop passive fire, water, earth: breath and story steer while fuel, feeling, and ground lie inert—pressure cooker until vented.",
            TraditionalImagery = new List<string> { "Inverted cup", "Scorpionic mars red", "Danger house joy in old texts" },
            Interpretation = new List<string>
            {
                "Never shame the querent for drawing Rubeus—ask where heat must be honored without scorching dependents.",
                "With Cauda, endings are messy; with Caput, rebirth through crisis.",
                "First house Rubeus still demands honesty about motives—without naming any modern author."
            },
            InHouses = H(
                (1, "Querent magnetizes conflict or desire; mask slips."),
                (2, "Binge spend, crypto wager, sexual expense."),
                (3, "Sexting leak risk; brother war."),
                (4, "Basement mold literal or metaphor; family secret sex."),
                (5, "Club night, affair spark, horror film joy."),
                (6, "STD screen, night shift toxicity, trainer pushing too hard."),
                (7, "Obsessive ex; merger due diligence finds fraud."),
                (8, "STD/HIV storylines; inheritance fight; kink contract."),
                (9, "Taboo research; occult initiation; deep web."),
                (10, "Scandal-boss; whistleblower sexism case."),
                (11, "Underground rave hopes; friend with benefits politics."),
                (12, "Addiction spiral; psychiatric hold; revenge porn fear.")
            ),
            ModernExamples = new List<string>
            {
                "Glassdoor post naming toxic manager—true, explosive",
                "Relapse during launch week then honest rehab intake",
                "Ethical hacker selling zero-day to highest bidder—debated"
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
