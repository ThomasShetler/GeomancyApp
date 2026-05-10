using System;
using System.Collections.Generic;

namespace GeomancyApp
{
    internal static partial class FigureCorpus
    {
        private static FigureData Fig13CaudaDraconis() => new FigureData
        {
            FigureID = "13",
            Name = "Cauda Draconis",
            EnglishName = "Tail of the Dragon",
            OtherNames = "Outer threshold, threshold going out, lower boundary, stepping outside",
            Quality = "Mobile",
            Keyword = "Ending",
            Imagery = "A doorway with footprints leading away from it",
            StrongHouse = "Ninth",
            StrongHouseID = 9,
            WeakHouse = "Third",
            WeakHouseID = 3,
            Planet = "South node of the Moon",
            Sign = "Sagittarius",
            InnerEl = "Fire",
            OuterEl = "Fire",
            FireElement = "Active",
            AirElement = "Active",
            WaterElement = "Active",
            EarthElement = "Passive",
            Anatomy = "The left arm",
            BodyType = "Left arm stories—tattoos, vaccine scars, watch tan; gait turns away from rooms; shoulders uneven from carrying exit bag.",
            CharacterType = "Burnout generosity; gives last dollar then ghosts; skeptical of returns; learns through loss; comic timing dark.",
            Colors = "Green, white, dark crimson, or pale tawny brown",
            Commentary = @"Cauda Draconis marks the south node threshold: three active lines, earth passive—fire, air, water rush out while ground withholds receipt. Medieval authors mixed good and evil in one breath because endings fertilize fields. Sagittarius adds travel and doctrine: leaving a church, a country, a paradigm. The diviner should read egress, tail risk, final invoice, or the last episode of a podcast season. Not cruelty—completion with teeth.",
            DivinatoryMeaning = "Closure, tail risk, burnout severance, graduation walk, divorce signed, server sunset, toxic job quit. Favorable when you need out, detox, or final check; unfavorable when you need retention, renewal, or quiet hold.",
            Tagline = "Exit stamp, tail risk, burnt bridge",
            CoreMeaning = new List<string> { "closure", "exhaust", "migration", "karma", "write-off", "last mile" },
            FavorableFor = new List<string> { "Deleting account", "Ending lease", "Hospice acceptance", "Sunsetting app", "Leaving cult" },
            UnfavorableFor = new List<string> { "Renewing vows", "IPO lockup extension", "Tenure clock freeze" },
            ElementalSynthesis = "Triple active above passive earth: heat, idea, and tear move outward without soil to catch them—rocket stage drop.",
            TraditionalImagery = new List<string> { "Footprints away", "Dragon tail south node", "Sagittarian departure" },
            Interpretation = new List<string>
            {
                "Pair with Caput to read entire lifecycle question—head proposes, tail disposes.",
                "Finance: write-offs, chargebacks, crypto rug aftermath.",
                "Spirit: the initiation that requires leaving the old name at the door."
            },
            InHouses = H(
                (1, "Identity shed; deadname retired."),
                (2, "Bankruptcy discharge; crypto loss realized."),
                (3, "Unsubscribe tour; sibling cutoff."),
                (4, "Move-out sale; parents downsizing."),
                (5, "Series finale; miscarriage acknowledged."),
                (6, "Resignation letter; treatment ends."),
                (7, "Divorce decree; client churn."),
                (8, "Will executed; debt forgiven."),
                (9, "Graduation walk; visa expiry."),
                (10, "CEO steps down; reputation pivot."),
                (11, "Dream abandoned consciously; network prune."),
                (12, "Hospital discharge; secret exposed then closed.")
            ),
            ModernExamples = new List<string>
            {
                "Deleting Twitter after acquisition meltdown",
                "Last day at warehouse before automation",
                "Final therapy session that actually stuck"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Cornelius Agrippa", "Three Books of Occult Philosophy", "Lunar nodes", 1651),
                Ts("Pseudo-Agrippa", "Of Geomancy", "Cauda Draconis", 1655),
                Ts("Franz Hartmann", "The Principles of Astrological Geomancy", "South node", 1889)
            }
        };

        private static FigureData Fig14CaputDraconis() => new FigureData
        {
            FigureID = "14",
            Name = "Caput Draconis",
            EnglishName = "Head of the Dragon",
            OtherNames = "Inner threshold, threshold coming in, upper boundary, high tree, upright staff, stepping inside",
            Quality = "Stable",
            Keyword = "Beginning",
            Imagery = "A doorway with footprints leading toward it",
            StrongHouse = "Sixth",
            StrongHouseID = 6,
            WeakHouse = "Twelfth",
            WeakHouseID = 12,
            Planet = "North node of the Moon",
            Sign = "Virgo",
            InnerEl = "Earth",
            OuterEl = "Earth",
            FireElement = "Passive",
            AirElement = "Active",
            WaterElement = "Active",
            EarthElement = "Active",
            Anatomy = "The right arm",
            BodyType = "Right arm carries new bags—gym gains, baby seat, work laptop; nails clean; posture improves when opportunity knocks.",
            CharacterType = "Earnest improver; spreadsheet soul; kindness with checklists; anxiety before big starts; learns by serving.",
            Colors = "White, flecked with citrine",
            Commentary = @"Caput Draconis greets the north node: earth doubled inside and out, while air and water move and fire waits—fertile furrow. Medieval texts promised new doors with rough thresholds: scholarship, baby, visa, repo fork. Virgo refines appetite into craft. Diviners should ask what seed is actually planted—earth rich but sun still passive until labor chooses heat.",
            DivinatoryMeaning = "New job offer, BFP, green card interview, repo first commit, initiation, arranged mentorship. Favorable for starts, upgrades, onboarding; unfavorable when the chart needs endings, silence, or debt zeroing.",
            Tagline = "Threshold in, fertile rough start",
            CoreMeaning = new List<string> { "ingress", "upgrade", "apprenticeship", "visa", "seed", "mentor" },
            FavorableFor = new List<string> { "First day oncall", "Adoption placement call", "Scholarship start", "Green card biometrics", "Sabbatical plan" },
            UnfavorableFor = new List<string> { "Final paycheck", "Deleting data", "Closing franchise" },
            ElementalSynthesis = "Active air, water, earth with passive fire: craft, care, and soil cooperate while raw ignition waits—greenhouse seedling.",
            TraditionalImagery = new List<string> { "Footprints inward", "North node head", "Virgo harvest start" },
            Interpretation = new List<string>
            {
                "Caput loves paperwork that unlocks—verify deadlines so the door does not slam.",
                "With Cauda in other houses, story is breathe-in breathe-out migration.",
                "Wealth: grant letter arrives with compliance strings."
            },
            InHouses = H(
                (1, "New wardrobe era; therapy intake."),
                (2, "First paycheck new job; grant deposit."),
                (3, "College acceptance; cousin introduces job."),
                (4, "House keys; nursery paint."),
                (5, "Positive test; art residency admit."),
                (6, "New hire training; wellness coach."),
                (7, "First date that matters; co-founder agreement."),
                (8, "Investor SAFE signed; surgery scheduled to fix."),
                (9, "Visa stamp; syllabus week one."),
                (10, "Title change; press release draft."),
                (11, "Discord mod promotion; hope project greenlit."),
                (12, "Dream vision leads to rehab admit—start.")
            ),
            ModernExamples = new List<string>
            {
                "GitHub org invite day one at new startup",
                "H1B lottery selected—lawyer queue begins",
                "Foster placement call—car seat installed at midnight"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Pseudo-Agrippa", "Of Geomancy", "Caput Draconis", 1655),
                Ts("Cornelius Agrippa", "Three Books of Occult Philosophy", "North node", 1651),
                Ts("Christopher Cattan", "The Geomancie of Maister Christopher Cattan", "Threshold figures", 1591)
            }
        };

        private static FigureData Fig15FortunaMinor() => new FigureData
        {
            FigureID = "15",
            Name = "Fortuna Minor",
            EnglishName = "Lesser Fortune",
            OtherNames = "Outward fortune, protection going out, lesser omen, outside or apparent help",
            Quality = "Mobile",
            Keyword = "Swiftness",
            Imagery = "A mountain with a staff atop it",
            StrongHouse = "Fifth",
            StrongHouseID = 5,
            WeakHouse = "Eleventh",
            WeakHouseID = 11,
            Planet = "Sun",
            Sign = "Leo",
            InnerEl = "Fire",
            OuterEl = "Air",
            FireElement = "Active",
            AirElement = "Active",
            WaterElement = "Passive",
            EarthElement = "Passive",
            Anatomy = "The spine",
            BodyType = "Straight spine, quick pivot; windbreaker always ready; eyes scan horizon; handshake firm but clock-watching.",
            CharacterType = "Proud helper; rescuer complex; burns bright on praise; humbles after stumble; gifts tips generously then vanishes.",
            Colors = "Gold or yellow",
            Commentary = @"Fortuna Minor crowns the ridge: Sun in Leo yet outer air, inner fire—fire and air act while water and earth sleep. Classical contrast with Fortuna Major's valley: luck here rides gusts—spotlight, referral, viral boost, outside patron. Easily won, easily spent. Diviners should track sponsorship, borrowed clout, or GPU credits from a friend. Leo dignity without inner earth means less ballast.",
            DivinatoryMeaning = "Flash sale win, hype week, borrowed ladder, gig bonus, spotlight hire, influencer bump. Favorable when speed beats depth; unfavorable for pensions, multi-decade marriage, or quiet research patents.",
            Tagline = "Flash win, borrowed spotlight",
            CoreMeaning = new List<string> { "velocity", "patron", "hype", "surface", "gift", "volatility" },
            FavorableFor = new List<string> { "Launch week spike", "Rescue helicopter", "VIP line skip", "24h hackathon prize" },
            UnfavorableFor = new List<string> { "Century bond", "Monastic stability", "Undercover years" },
            ElementalSynthesis = "Active fire and air with passive water and earth: heat and wind cooperate while moisture and soil wait—banner on battlements, not roots.",
            TraditionalImagery = new List<string> { "Mountain staff", "Lesser fortune", "Leo air outer emblems" },
            Interpretation = new List<string>
            {
                "Ask who owns the platform beneath the viral moment.",
                "With Reconciler heavy, the promotion feels hollow—check six months out.",
                "Contrast Fortuna Major: same Sun sign, different soil depth."
            },
            InHouses = H(
                (1, "15 minutes fame; personal brand stunt."),
                (2, "Side hustle surge; tips night."),
                (3, "Retweet storm; rideshare streak earnings."),
                (4, "Open house bidding war one weekend."),
                (5, "One-hit wonder chart; festival slot."),
                (6, "Overtime surge pay; ER fast lane luck."),
                (7, "Wedding sponsored; influencer collab."),
                (8, "Angel swoops; bridge loan."),
                (9, "Conference keynote invite sudden."),
                (10, "Viral LinkedIn CEO thread."),
                (11, "Kickstarter lightning round; Patreon spike."),
                (12, "Anonymous benefactor wire—verify.")
            ),
            ModernExamples = new List<string>
            {
                "GPU cloud credits from mentor for AI demo",
                "Bar cover band booked because TikTok clip",
                "Flash grant 48h application window won"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Pseudo-Agrippa", "Of Geomancy", "Fortuna Minor", 1655),
                Ts("Cornelius Agrippa", "Three Books of Occult Philosophy", "Solar fortune variants", 1651),
                Ts("John Heydon", "Theomagia, or the Temple of Wisdome", "Lesser fortune", 1664)
            }
        };

        private static FigureData Fig16Via() => new FigureData
        {
            FigureID = "16",
            Name = "Via",
            EnglishName = "Way",
            OtherNames = "Wayfarer, candle, journey",
            Quality = "Mobile",
            Keyword = "Change",
            Imagery = "A road",
            StrongHouse = "Fourth",
            StrongHouseID = 4,
            WeakHouse = "Tenth",
            WeakHouseID = 10,
            Planet = "Moon",
            Sign = "Cancer",
            InnerEl = "Water",
            OuterEl = "Water",
            FireElement = "Active",
            AirElement = "Active",
            WaterElement = "Active",
            EarthElement = "Active",
            Anatomy = "The stomach",
            BodyType = "Butterfly stomach before flights; motion sickness aware; snacks in every bag; walks while on phone; moon face bloat when stressed.",
            CharacterType = "Nomad heart; mood tides; loyal but needs new horizons; cooks for road; journals at layovers.",
            Colors = "White, flecked with blue",
            Commentary = @"Via opens every line: Moon in Cancer, water inside and out, all elements active—tidal highway. Medieval authors called it the road; modern readers add fiber lines, supply chains, gender journey, migration policy. Nothing sits; even sleep is REM cycle. The diviner maps vectors not positions. Populus is crowd inertia; Via is crowd when everyone suddenly heads for the exit or parade.",
            DivinatoryMeaning = "Relocation, lane change, pivot, bus route, data migration, hormone transition, career hop. Favorable when movement itself solves; unfavorable for mortgages that need roots, or custody needing stability proof.",
            Tagline = "All lines go; river keeps cutting",
            CoreMeaning = new List<string> { "motion", "lane change", "migration", "iteration", "fleet", "tide" },
            FavorableFor = new List<string> { "Digital nomad visa", "Kubernetes rollout", "Solo motorcycle leg", "Gender care pathway", "Agile sprint" },
            UnfavorableFor = new List<string> { "Tenure committee", "Monastic stability", "Sealed deposition hold" },
            ElementalSynthesis = "All four active: fire, air, water, earth cycle without rest—complete permutation set; Cancerian water doubles intuition steering logistics.",
            TraditionalImagery = new List<string> { "Road fork", "Wayfarer", "Lunar journey glyphs" },
            Interpretation = new List<string>
            {
                "Via answers how not where—process dominates outcome until Judge freezes frame.",
                "Stack with Carcer to read commute jail; with Populus to read flash mob.",
                "Finance: cash flow yes, savings account maybe not."
            },
            InHouses = H(
                (1, "Identity in flux; name change pipeline."),
                (2, "Gig income streams; variable rent."),
                (3, "Rideshare thesis; newsletter pivot weekly."),
                (4, "Digital nomad parents; RV retrofit."),
                (5, "Touring band; dating travel mode."),
                (6, "Travel nurse contract; diet experiment loop."),
                (7, "Long-distance relating; import partner goods."),
                (8, "Crypto bridge hopping; shared custody rotation."),
                (9, "Study abroad; pilgrimage miles."),
                (10, "Consulting fly week; political campaign bus."),
                (11, "Activist carpool; hope on the march."),
                (12, "Ferry between hospitals; dream highway.")
            ),
            ModernExamples = new List<string>
            {
                "Four cities in a quarter—relationship app location chaos",
                "Container ship reroute due to canal backlog",
                "CI/CD pipeline changing nightly—stability elsewhere needed"
            },
            TraditionalSources = new List<TraditionalSourceEntry>
            {
                Ts("Pseudo-Agrippa", "Of Geomancy", "Via journey", 1655),
                Ts("Christopher Cattan", "The Geomancie of Maister Christopher Cattan", "Wayfarer", 1591),
                Ts("Franz Hartmann", "The Principles of Astrological Geomancy", "Total motion", 1889)
            }
        };
    }
}
