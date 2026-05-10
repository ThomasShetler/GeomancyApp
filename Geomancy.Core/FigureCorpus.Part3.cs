using System;
using System.Collections.Generic;

namespace GeomancyApp
{
    internal static partial class FigureCorpus
    {
        private static FigureData Fig09Acquisitio() => new FigureData
        {
            FigureID = "9",
            Name = "Acquisitio",
            EnglishName = "Gain",
            OtherNames = "Grasping internally, inner wealth, something gained or picked up",
            Quality = "Stable",
            Keyword = "Gain",
            Imagery = "A bag held mouth upward, as though to take something in",
            StrongHouse = "Ninth",
            StrongHouseID = 9,
            WeakHouse = "Third",
            WeakHouseID = 3,
            Planet = "Jupiter",
            Sign = "Sagittarius",
            InnerEl = "Air",
            OuterEl = "Fire",
            FireElement = "Passive",
            AirElement = "Active",
            WaterElement = "Passive",
            EarthElement = "Active",
            Anatomy = "The hips and thighs",
            BodyType = "Broad hips, strong thighs, stride eats distance; hands pocket bonuses; laugh loud; weight carried low like a wrestler or dancer.",
            CharacterType = "Optimistic dealmaker; believes growth solves most sins; blunt honesty; teaches night classes; shares PDFs freely.",
            Colors = "Red, yellow, or green",
            Commentary = @"Acquisitio lifts the purse: Jupiter in Sagittarius, inner air, outer fire, while air and earth act to catch what falls. Medieval authors paired it with prudence because appetite must still sort real from fool's gold. Fire rests at the crown—vision without burnout if discipline holds. The diviner tracks expansion: market share, waistline, knowledge, all can widen. Contrast with Amissio: here the mouth opens to receive.",
            DivinatoryMeaning = "Raises, grants, scholarships, stock upside, pregnancy news, audience growth, harvest. Favorable when acquisition is ethical and budgeted; poor when the question needs austerity, deletion, or anonymity.",
            Tagline = "Open purse, widening horizon",
            CoreMeaning = new List<string> { "expansion", "profit", "blessing", "travel", "enrollment", "bonus" },
            FavorableFor = new List<string> { "Fundraising round", "Grad school admit", "Farm purchase", "Podcast sponsor", "Grant application" },
            UnfavorableFor = new List<string> { "Minimalist off-grid plan", "Witness protection", "Bankruptcy filing" },
            ElementalSynthesis = "Active air and earth with passive fire and water: ideas and matter cooperate while raw heat and tears stay banked—Sagittarian arrow nocked, not yet burning.",
            TraditionalImagery = new List<string> { "Upright bag", "Jupiter glyph luck", "Sagittarian arrow" },
            Interpretation = new List<string>
            {
                "Ask what will be done with surplus—Acquisitio without Tristitia can mean weight as well as wealth.",
                "Tech reading: user growth, ARR, follower count—verify churn elsewhere.",
                "With Carcer, gain locks into structure; with Via, gain while moving."
            },
            InHouses = H(
                (1, "Querent levels up—confidence from new credential."),
                (2, "Raise, dividend, crypto green day."),
                (3, "Publisher accepts pitch; sibling shares lead."),
                (4, "Bigger apartment; family inheritance rumor true."),
                (5, "BFP test; creative grant; lottery smaller win."),
                (6, "Better benefits package; gym gains."),
                (7, "Partner brings opportunity; JV equity."),
                (8, "Angel investor; insurance dividend."),
                (9, "Visa granted; book advance."),
                (10, "Promotion; award; board seat offer."),
                (11, "Crowdfund exceeds goal; mentor opens rolodex."),
                (12, "Anonymous donation; settlement windfall.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is upward momentum—the querent already has wind at their back and arrived with resources to deploy.",
                leftWitness: "External flow is bringing in—offers, payments, or opportunities are converging toward the querent.",
                judge: "Verdict is gain: the matter resolves with something added to the querent's column—money, contract, or rank.",
                reconciler: "Aftermath enlarges the situation—what is acquired now reshapes future obligations and choices."
            ),
            ModernExamples = new List<string>
            {
                "RSUs vesting the month rent is due",
                "Substack paid tier crossing sustainable line",
                "Adopting foster dog who also guards the shop"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Cornelius Agrippa", "Three Books of Occult Philosophy", "Jovial increase", 1651),
                Ts("John Heydon", "Theomagia, or the Temple of Wisdome", "Gain figures", 1664),
                Ts("Franz Hartmann", "The Principles of Astrological Geomancy", "Benefic expansion", 1889)
            }
        };

        private static FigureData Fig10Carcer() => new FigureData
        {
            FigureID = "10",
            Name = "Carcer",
            EnglishName = "Prison",
            OtherNames = "Constricted, lock",
            Quality = "Stable",
            Keyword = "Isolation",
            Imagery = "An enclosure",
            StrongHouse = "Tenth",
            StrongHouseID = 10,
            WeakHouse = "Fourth",
            WeakHouseID = 4,
            Planet = "Saturn",
            Sign = "Capricorn",
            InnerEl = "Earth",
            OuterEl = "Earth",
            FireElement = "Active",
            AirElement = "Passive",
            WaterElement = "Passive",
            EarthElement = "Active",
            Anatomy = "The knees and lower legs",
            BodyType = "Knobby knees, careful steps, carries tension in jaw; prefers boots; saves receipts in rubber bands; posture stiffens when budgets discussed.",
            CharacterType = "Frugal, stubborn, loyal to protocol; fears chaos more than boredom; slow trust; excellent executor once briefed.",
            Colors = "White, russet, or dun (pale brown)",
            Commentary = @"Carcer is the lock and the ledger: Saturn in Capricorn, doubled earth, fire and earth active while air and water sleep. Energy and matter touch without mediators—work becomes tunnel. Medieval readers assigned delay, greed, and safekeeping at once. Modern diviners should ask whether walls protect or suffocate: NDA, mortgage, monastic cell, or spreadsheet prison. The figure stabilizes what it touches—verify the querent wants freeze-frame.",
            DivinatoryMeaning = "Delay, escrow, quarantine, NDA, fence, bunker, savings account, tenure clock, legal hold. Favorable when structure saves; unfavorable when speed, publicity, or emotional flow is needed.",
            Tagline = "Walls, ledgers, slow clocks",
            CoreMeaning = new List<string> { "delay", "security", "hoarding", "focus", "isolation", "compliance" },
            FavorableFor = new List<string> { "Cold storage crypto", "Patent filing", "Monastic year", "Construction permit wait", "Pre-surgery fasting" },
            UnfavorableFor = new List<string> { "Viral launch", "Impulse travel", "Open relationship negotiation" },
            ElementalSynthesis = "Active fire and earth bracket passive air and water: heat and matter meet without breath or tear to soften—vault logic, kiln logic.",
            TraditionalImagery = new List<string> { "Enclosure square", "Saturn lock", "Capricorn mountain" },
            Interpretation = new List<string>
            {
                "Carcer plus Fortuna Major can mean delayed but sure promotion; with Via, jailbreak urge fights habit.",
                "Ask if the lock is external (policy) or internal (shame).",
                "Finance: liquidity traps—cash exists but rules block spend."
            },
            InHouses = H(
                (1, "Querent self-limits; stiff upper lip."),
                (2, "Frozen assets; CD ladder; coupon clipping."),
                (3, "NDA silence; sibling estrangement."),
                (4, "Mortgage chain; elderly parent lock-in care."),
                (5, "Creative block; chastity pledge; IVF calendar."),
                (6, "Cubicle farm; chronic pain management routine."),
                (7, "Prenup; noncompete; cold marriage duty."),
                (8, "Trust lock until 30; debt snowball."),
                (9, "Grad school cage; visa limbo."),
                (10, "Corporate ladder guardrails; security clearance."),
                (11, "Union rules; Patreon tier freeze."),
                (12, "Psych ward hold; house arrest ankle monitor.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is constraint already binding—the querent walked in already locked into a contract, role, or repeating pattern.",
                leftWitness: "External climate is restrictive—rules, gatekeepers, or material limits are tightening the trajectory.",
                judge: "Verdict is closure or hold: the matter resolves by being fixed in place—either secured for good or shut down.",
                reconciler: "Aftermath is structural—the boundary set here becomes a permanent feature the querent must learn to live inside."
            ),
            ModernExamples = new List<string>
            {
                "Two-week security review before deploy",
                "Choosing cheaper apartment to slam student loans",
                "Password manager nobody else knows—estate risk"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Pseudo-Agrippa", "Of Geomancy", "Carcer delay", 1655),
                Ts("Christopher Cattan", "The Geomancie of Maister Christopher Cattan", "Enclosure", 1591),
                Ts("Robert Fludd", "De Geomantia (Utriusque Cosmi)", "Saturnine binding", 1617)
            }
        };

        private static FigureData Fig11Tristitia() => new FigureData
        {
            FigureID = "11",
            Name = "Tristitia",
            EnglishName = "Sorrow",
            OtherNames = "Crosswise, diminished, accursed, head down, fallen tower, cross",
            Quality = "Stable",
            Keyword = "Sorrow",
            Imagery = "A pit, a stake driven downward",
            StrongHouse = "Eleventh",
            StrongHouseID = 11,
            WeakHouse = "Fifth",
            WeakHouseID = 5,
            Planet = "Saturn",
            Sign = "Aquarius",
            InnerEl = "Earth",
            OuterEl = "Air",
            FireElement = "Passive",
            AirElement = "Passive",
            WaterElement = "Passive",
            EarthElement = "Active",
            Anatomy = "The ankles",
            BodyType = "Heavy ankles, loves sturdy shoes; grounding walk; collects rocks; knuckles crack when thinking; winter skin.",
            CharacterType = "Dry humor; loyal after long test; systems thinker; depressive realism; innovates because old ways hurt.",
            Colors = "Tawny or sky blue",
            Commentary = @"Tristitia drives the stake: Saturn in Aquarius, outer air, inner earth, only earth active—three passives above weight. Medieval authors called it sorrow but also foundation for towers if patience holds. Aquarius lifts the paradox: networks that freeze progress until specs improve. Diviners should read downward motion—price drop, mood dip, shovel—but also root-setting for wind turbines.",
            DivinatoryMeaning = "Depression spell, stock slide, layoff rumor, basement dig, research rabbit hole, funeral, sobriety seriousness. Favorable for construction, mining, keeping secrets buried; poor for parties or launches needing hype.",
            Tagline = "Weight down, root deep, slow truth",
            CoreMeaning = new List<string> { "gravitas", "decline", "foundation", "grief", "engineering", "silence" },
            FavorableFor = new List<string> { "Pouring concrete", "Deleting legacy code", "Grief work", "Bunker research", "Longitudinal study" },
            UnfavorableFor = new List<string> { "Grand opening balloons", "Honeymoon", "Influencer livestream" },
            ElementalSynthesis = "Only earth acts under passive fire, air, water: everything compresses toward soil—Aquarian sky ideas wait until ground proves them.",
            TraditionalImagery = new List<string> { "Pit and stake", "Tower foundation", "Saturn in fixed air emblems" },
            Interpretation = new List<string>
            {
                "Pair with Laetitia in court to read manic-depressive cycle of a startup.",
                "Ask what must be buried to grow—seeds, cables, trauma.",
                "Justice questions: slow courts, but evidence stacks."
            },
            InHouses = H(
                (1, "Querent feels heavy; imposter syndrome."),
                (2, "Pay cut; austerity; coupon life."),
                (3, "Bad news thread; ankle sprain commute."),
                (4, "Basement flood repair; ancestor trauma."),
                (5, "Miscarriage risk; writer's block."),
                (6, "Chronic diagnosis; thankless grind."),
                (7, "Cold partner; slow legal bleed."),
                (8, "Debt collector; inheritance tax."),
                (9, "Rejected paper; visa denial."),
                (10, "Demotion rumor; glass ceiling."),
                (11, "Dream deferred; activist burnout."),
                (12, "Hospital depression; secret weight.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is grief or fatigue—the querent has been carrying weight that long predates the question.",
                leftWitness: "External climate is grinding—delays, scarcity, or institutional indifference are wearing the situation down.",
                judge: "Verdict is heavy: the matter resolves slowly and against the querent's hopes; what comes is real but costly.",
                reconciler: "Aftermath is depth, not damage—what hurts here teaches a discipline the querent will carry as quiet authority."
            ),
            ModernExamples = new List<string>
            {
                "Layoffs announced same day as product launch",
                "Digging through old logs to prove harassment",
                "Choosing cheaper therapy sliding scale—still helps"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Cornelius Agrippa", "Three Books of Occult Philosophy", "Saturnine sorrow", 1651),
                Ts("John Heydon", "Theomagia, or the Temple of Wisdome", "Tristitia", 1664),
                Ts("Franz Hartmann", "The Principles of Astrological Geomancy", "Earth-heavy figures", 1889)
            }
        };

        private static FigureData Fig12Laetitia() => new FigureData
        {
            FigureID = "12",
            Name = "Laetitia",
            EnglishName = "Joy",
            OtherNames = "Bearded, laughing, singing, high tower, head lifted, candelabrum, high mountain",
            Quality = "Mobile",
            Keyword = "Joy",
            Imagery = "A tower",
            StrongHouse = "Twelfth",
            StrongHouseID = 12,
            WeakHouse = "Sixth",
            WeakHouseID = 6,
            Planet = "Jupiter",
            Sign = "Pisces",
            InnerEl = "Fire",
            OuterEl = "Water",
            FireElement = "Active",
            AirElement = "Passive",
            WaterElement = "Passive",
            EarthElement = "Passive",
            Anatomy = "The feet",
            BodyType = "Bouncy step; big feet jokes; laughs with whole torso; hair wild; tends barefoot home; cries easy at weddings.",
            CharacterType = "Innocent enthusiasm; religious awe; gambler's optimism; forgives fast; needs grounding buddy.",
            Colors = "Glittering pale green",
            Commentary = @"Laetitia lifts the head: Jupiter in Pisces, inner fire, outer water, only fire active while siblings sleep—torch on fog. Medieval joy figures warn visibility: towers attract lightning. The diviner balances elation with OPSEC: promotion announced too early, pregnancy before trimester, song dropped before mix master. Watery outer shell means empathy broadcasts—beautiful until boundaries blur.",
            DivinatoryMeaning = "Promotion, viral hit, sobriety pink cloud, spiritual high, stock rally, pregnancy glow, festival season. Favorable for launches needing joy; poor for secrets, stealth, or holding still.",
            Tagline = "Tower lights, lifted mood, bright risk",
            CoreMeaning = new List<string> { "ascent", "visibility", "elation", "faith", "virality", "relief" },
            FavorableFor = new List<string> { "Album drop", "Proposal flash mob", "Charity gala", "Recovery milestone party", "TikTok explainer" },
            UnfavorableFor = new List<string> { "Witness protection", "Silent retreat", "Short selling quietly" },
            ElementalSynthesis = "Only fire acts within Piscean water shell: enthusiasm pierces fog while air and earth stay latent—balloon without sandbags until other figures add weight.",
            TraditionalImagery = new List<string> { "Tower height", "Candelabrum joy", "Jupiter exaltation echoes" },
            Interpretation = new List<string>
            {
                "Laetitia loves a microphone—check if the querent is ready for scrutiny that follows applause.",
                "With Tristitia, expect hangover; with Fortuna Minor, fast rise faster fall.",
                "Health: mania watch—sleep debt hides behind glow."
            },
            InHouses = H(
                (1, "Querent glows; hair color change; viral moment."),
                (2, "Bonus surprise; stock pop."),
                (3, "Tweet blows up; sibling wins award."),
                (4, "House party; zillow zest high."),
                (5, "Conception joy; rave; art show."),
                (6, "Remission news; puppy adopted."),
                (7, "Engagement photo likes explode."),
                (8, "Inheritance win; settlement celebration."),
                (9, "Dream school yes; pilgrimage booked."),
                (10, "Viral CEO moment; Emmy nom."),
                (11, "Kickstarter confetti; hope restored."),
                (12, "Spiritual retreat high; watch disclosure.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is rising mood—the querent arrived already lifted by recent good news, healing, or a creative wave.",
                leftWitness: "External flow is buoyant—celebrations, blessings, or favorable currents are coming toward the querent.",
                judge: "Verdict is joyous: the matter resolves with relief, success, or a freedom the querent has been waiting for.",
                reconciler: "Aftermath is genuine elevation—not a fluke; the new altitude becomes the querent's working baseline."
            ),
            ModernExamples = new List<string>
            {
                "ProductHunt #1 same day servers melt",
                "Coming out post heartwarming then exhausting",
                "Lottery win posted—cousins appear"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Pseudo-Agrippa", "Of Geomancy", "Laetitia joy", 1655),
                Ts("Christopher Cattan", "The Geomancie of Maister Christopher Cattan", "Tower figure", 1591),
                Ts("John Heydon", "Theomagia, or the Temple of Wisdome", "Jupiter joy", 1664)
            }
        };
    }
}
