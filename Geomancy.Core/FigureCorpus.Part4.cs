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
            BodyType = "A left arm that bears the marks of a long history, a posture that subtly turns away from a crowded room, and shoulders held a little unevenly from years of carrying what might be needed at short notice.",
            CharacterType = "A generosity that runs to depletion, an instinct to give until little remains and then to withdraw, and a guarded skepticism toward what may be received in return. Such a person learns through loss, and their humor tends toward the dry and the dark.",
            Colors = "Green, white, dark crimson, or pale tawny brown",
            Commentary = @"Cauda Draconis marks the threshold of the southern lunar node: three active lines stand against a passive earth, so that fire, air, and water rush outward while the ground withholds its receipt. The medieval authors mixed good and evil in a single breath under this figure, because endings, properly understood, fertilize the field they leave behind. Sagittarius adds the dimension of travel and of doctrine: the leaving of a church, a country, a working paradigm. The practitioner should read this figure as egress, as the final invoice, as the last episode of a long undertaking. The motion is not cruelty; it is completion with teeth.",
            DivinatoryMeaning = "Closure, the residual risk that gathers at an exit, the formal end of a binding tie, the graduation, the decommissioning of a system, the resignation from work that has worn the querent down. The figure favors questions in which release, detoxification, or a final reckoning is required, and disfavors questions that depend on retention, renewal, or quiet preservation.",
            Tagline = "Closure at the threshold; what exits carries consequence.",
            CoreMeaning = new List<string> { "closure", "release", "migration", "karma", "reckoning", "completion" },
            FavorableFor = new List<string> { "Formally ending what honesty no longer sustains", "Closing a lease or tenancy with clear papers", "Accepting palliative or hospice transition", "Sunsetting a product or duty by design", "Stepping away from restrictive alliances or dogmas" },
            UnfavorableFor = new List<string> { "Renewing vows or bonds meant to endure", "Extending provisional locks or freezes indefinitely", "Preserving tenure or standing when integrity asks release" },
            ElementalSynthesis = "Three active lines press above a passive earth: heat, idea, and tear all move outward without soil to receive them—the figure releases what it carries, after the manner of a stage discarded once its work is done.",
            TraditionalImagery = new List<string> { "Footprints leading outward", "The lunar south node", "Departure under Sagittarius" },
            Interpretation = new List<string>
            {
                "Read with Caput Draconis, the figure tells a complete lifecycle question: the head proposes, and the tail disposes.",
                "In matters of finance, the figure marks write-offs, refunded transactions, and the long aftermath of an investment that did not hold.",
                "In matters of the spirit, the figure points to the initiation that requires the querent to leave an old name at the door before crossing."
            },
            InHouses = H(
                (1, "An established self is laid down; a former name or role is honorably retired."),
                (2, "Formal discharge of debt; losses booked and acknowledged without denial."),
                (3, "Correspondence or kin ties shortened by deliberate, humane closure."),
                (4, "Household clearing; elders downsizing or leaving the family home."),
                (5, "A creative arc concludes; grief or acceptance after loss of work or pregnancy."),
                (6, "Resignation from ill-suited employment; a course of care reaches its natural end."),
                (7, "Partnership dissolution settled; steady patrons or allies quietly drift away."),
                (8, "Will executed; debt forgiven or estate matters closed."),
                (9, "Credential completed; visa or study permission reaches lawful expiry."),
                (10, "Leadership handed off; public standing pivots away from an old narrative."),
                (11, "A hope consciously relinquished; the circle of allies thoughtfully narrowed."),
                (12, "Discharge after acute care; a hidden truth surfaces, then the chapter seals.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is conclusion—the matter arises from an exit, severance, or chapter the querent has been closing with care.",
                leftWitness: "The outer field narrows—openings shutter, allies withdraw, and the trajectory favors ending over extension.",
                judge: "Verdict is termination: resolution comes by release; clinging turns closure into needless loss.",
                reconciler: "Aftermath is the cleared space—what remains after the threshold is crossed becomes honest ground for what follows."
            ),
            ModernExamples = new List<string>
            {
                "Closing a long-held public affiliation once its climate has turned decisively hostile",
                "A last scheduled shift before a workplace is folded into a successor organization",
                "The final meeting of sustained counsel, when the work is honored and laid to rest"
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
            BodyType = "A right arm conditioned to carry whatever new responsibility has been accepted—the working tool, the bag of provisions, the child. The nails are kept clean, and the carriage of the body visibly improves when the figure encounters opportunity.",
            CharacterType = "An earnest improver, given to method and to small acts of considered kindness. There is a recognizable anxiety before a major beginning, and a temperament that learns its own shape through service rendered to others.",
            Colors = "White, flecked with citrine",
            Commentary = @"Caput Draconis greets the threshold of the northern lunar node: earth doubled within and without, while air and water move and fire keeps watch—a fertile furrow. The medieval texts promise new doors here, but doors with thresholds that are seldom smooth: a scholarship, an arrival, a major commitment, the first stage of a long work. Virgo refines appetite into craft. The practitioner should ask what seed is actually being planted, since the earth in the figure is rich while the sun stays passive—until the labor chosen by the querent supplies the necessary heat.",
            DivinatoryMeaning = "The new offer of work, the news of a child, the formal invitation, the first stage of a project, the arrival of a mentor. The figure favors beginnings, upgrades, and the careful work of taking up new responsibility, and disfavors questions in which the matter requires endings, silence, or the discharge of an existing obligation.",
            Tagline = "A fertile threshold; beginnings ask disciplined tending.",
            CoreMeaning = new List<string> { "ingress", "foundation", "apprenticeship", "permission", "seed", "mentorship" },
            FavorableFor = new List<string> { "First day carrying formal duty or on-call responsibility", "Call confirming adoption or guardianship placement", "Commencement of scholarship or funded study", "Immigration milestone such as biometrics or interview", "Structuring a sabbatical or renewal leave with intent" },
            UnfavorableFor = new List<string> { "Final severance or terminal payout scenes", "Erasing archives contrary to obligation", "Shuttering an institution meant to endure" },
            ElementalSynthesis = "Air, water, and earth all act while fire waits in passivity: craft, care, and soil cooperate while the raw ignition holds in reserve—the seedling held in greenhouse conditions until the season permits its planting outdoors.",
            TraditionalImagery = new List<string> { "Footprints toward the door", "The lunar north node", "First furrow under Virgo" },
            Interpretation = new List<string>
            {
                "Caput Draconis tends to favor the paperwork that unlocks. The practitioner should verify deadlines carefully, so that the door is not closed by a lapse the querent could have prevented.",
                "Where Cauda Draconis appears in other houses of the same chart, the larger story is one of migration—an outbreath in one quarter, an inbreath in another.",
                "In matters of resource, the grant or award most often arrives accompanied by conditions, and those conditions deserve the same attention as the gift itself."
            },
            InHouses = H(
                (1, "Renewed presentation of self; first therapeutic intake or intentional habit."),
                (2, "Initial wages from new employment; grant funds deposited."),
                (3, "Admission letter or credential advance; kin or peer opens vocational door."),
                (4, "Keys to a new dwelling; room prepared for arrival or growth."),
                (5, "Confirmed conception or creative acceptance to a residency or cohort."),
                (6, "Structured onboarding; partnership with coach or clinician for upkeep."),
                (7, "Meaningful first union under sincerity; compact among founders signed."),
                (8, "Investment instrument executed; corrective surgery booked with clarity."),
                (9, "Travel permission stamped; study begins under a mapped syllabus."),
                (10, "Elevation of role; outward narrative drafted with care."),
                (11, "Trusted ally elevated within community; initiative dear to hope gains sanction."),
                (12, "Dream or ordeal redirects toward admitted care—a sanctioned beginning.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is fresh ground—the querent arrives carrying a recent arrival: identity, home, or binding pledge.",
                leftWitness: "The field presents thresholds—invitations, openings, or roads not yet walked.",
                judge: "Verdict is inception: resolution favors beginning—the chapter opened here shapes what follows.",
                reconciler: "Aftermath is the planted seed—the commitment taken roots into vocation, bond, or long craft."
            ),
            ModernExamples = new List<string>
            {
                "Badge or credential admitting entry on the first day of service",
                "Residency status affirmed and the slow lattice of lawful paperwork commenced",
                "Word that a child will join the household, met with proportionate preparation"
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
            BodyType = "A straight spine and a quick pivot at the hip, with eyes that habitually read the horizon. The handshake is firm, but the attention is rarely settled in the room for long.",
            CharacterType = "A proud helper, willing to shoulder the rescue when the moment calls for it. Such a person rises sharply on praise, accepts correction with humility after a misstep, and tends to share what they have generously and then to move on before their generosity can be returned.",
            Colors = "Gold or yellow",
            Commentary = @"Fortuna Minor crowns the ridge: Sun in Leo, yet with outer air over inner fire—fire and air move while water and earth lie still. The classical contrast with the valley of Fortuna Major is exact: here, fortune rides on the wind. The gift comes from a referral, a sudden spotlight, a willing patron, or a moment of public attention. It is easily won and easily spent. The practitioner should look carefully at the source: borrowed influence, lent resources, the favor of a third party. The dignity of Leo without the ballast of inner earth carries less weight than it appears to.",
            DivinatoryMeaning = "The brief win, the surge of attention, the borrowed ladder, the bonus that lands at the right moment, the moment of public favor. The figure favors questions in which speed matters more than depth, and disfavors instruments that require decades to mature, such as long marriages, long bonds, or research conducted away from view.",
            Tagline = "Swift favor from without; depth must be earned afterward.",
            CoreMeaning = new List<string> { "velocity", "patronage", "visibility", "surface", "gift", "volatility" },
            FavorableFor = new List<string> { "Launch windows where momentum decides outcomes", "Emergency extraction or urgent rescue logistics", "Expedited access granted by trusted gatekeepers", "Short competitive sprints with transparent criteria" },
            UnfavorableFor = new List<string> { "Instruments meant to mature across generations", "Communities built on stable enclosure", "Undetected long-running assignments" },
            ElementalSynthesis = "Fire and air move while water and earth lie still: heat and wind cooperate, while moisture and soil wait their turn—a banner upon the battlements rather than a root within the slope.",
            TraditionalImagery = new List<string> { "Staff upon the summit", "Fortune that rides the ridge", "Leo crowned by outward air" },
            Interpretation = new List<string>
            {
                "The first practitioner question is who owns the platform beneath the moment of visibility, since the answer determines how long the visibility itself can be sustained.",
                "Where the Reconciler in the chart is unfavorable, the promotion conferred by this figure tends to feel hollow over time, and the practitioner does well to revisit the matter at a later date.",
                "The instructive contrast is with Fortuna Major: the same solar sign, but very different depths of soil beneath the same crown."
            },
            InHouses = H(
                (1, "Brief public notice; reputation sharpened by a deliberate showing."),
                (2, "Secondary income spikes; gratuities pile on one fortunate evening."),
                (3, "Sudden amplification of message; transport or gig earnings cluster."),
                (4, "Competitive offers over a single open weekend."),
                (5, "Sudden chart attention; invitation to perform before a wide crowd."),
                (6, "Premium pay for surge hours; expedited care when timing favors."),
                (7, "Celebration underwritten by patrons; partnership riding borrowed visibility."),
                (8, "Patron intervenes; bridging finance arrives under pressure."),
                (9, "Unexpected podium or lecture; reputation lifts on one gesture."),
                (10, "Executive praise spreads quickly through professional networks."),
                (11, "Crowdfunding crests in days; supporters rally in a burst."),
                (12, "Anonymous transfer or gift—due diligence before trust.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is lent momentum—the querent enters riding another's wave or a recent stroke of luck.",
                leftWitness: "Outside motion runs fast and kindly yet carries conditions—help is timely but not guaranteed to hold.",
                judge: "Verdict favors the near horizon: success now rests on patronage or circumstance beyond full ownership.",
                reconciler: "Aftermath thins when the borrowed wind stills—only what was anchored in skill or ethic endures."
            ),
            ModernExamples = new List<string>
            {
                "Equipment or hosting lent by a mentor until a proof can be shown",
                "An ensemble engaged after one recording traveled farther than years of local work",
                "A prize captured in a narrow window few rivals had prepared to meet"
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
            BodyType = "A stomach sensitive to anticipation, a body practiced at handling motion, and the small habits of one who travels often—provisions in the bag, a phone consulted while walking, and a face that softens visibly with stress.",
            CharacterType = "A nomadic heart and a temperament that moves with its own tides. Such a person is loyal but needs new horizons to remain whole, prepares carefully for the road, and writes most clearly when between one place and the next.",
            Colors = "White, flecked with blue",
            Commentary = @"Via opens every line: Moon in Cancer, water doubled inside and out, with all four elements active—a tidal highway. The medieval authors named the figure the road; the contemporary reader may add to that the fiber line, the supply chain, the long migration, the journey of identity. Nothing in this figure remains still; even sleep is the rhythm of cycles. The practitioner maps vectors here rather than positions. Where Populus is the inertia of the crowd, Via is the same crowd at the moment in which everyone moves at once—toward the exit, the parade, or the ferry that has just been announced.",
            DivinatoryMeaning = "Relocation, the change of lane, the pivot, the route, the migration of data or person, the long transition. The figure favors questions in which the movement itself is the solution, and disfavors questions that require the proof of roots—long instruments, custody arguments, or any matter that demands stability under examination.",
            Tagline = "All lines go; river keeps cutting",
            CoreMeaning = new List<string> { "motion", "lane change", "migration", "iteration", "fleet", "tide" },
            FavorableFor = new List<string> { "Digital nomad visa", "Kubernetes rollout", "Solo motorcycle leg", "Gender care pathway", "Agile sprint" },
            UnfavorableFor = new List<string> { "Tenure committee", "Monastic stability", "Sealed deposition hold" },
            ElementalSynthesis = "All four lines act: fire, air, water, and earth cycle without rest. The figure presents the complete permutation, and the doubled water of Cancer asks intuition itself to do much of the steering as the logistics unfold.",
            TraditionalImagery = new List<string> { "Forked road under moonlight", "The wayfarer's staff", "Lunar glyphs of passage" },
            Interpretation = new List<string>
            {
                "Via answers the question of how rather than the question of where, and process tends to dominate outcome until the Judge of the chart freezes the frame.",
                "Read with Carcer, the figure can describe a commute that has become a confinement; read with Populus, it describes the moment in which a great many people set out at once.",
                "In matters of finance, the figure points more readily to cash flow than to savings: value moves through the querent's hands rather than accumulating within them."
            },
            InHouses = H(
                (1, "Selfhood under revision; lawful name or presentation change in progress."),
                (2, "Income from shifting assignments; housing cost that follows the route."),
                (3, "Commute as thesis; publishing rhythm that retrains weekly."),
                (4, "Parents raising children across bases; dwelling retrofitted for travel."),
                (5, "Creative life on circuit; affection sustained across distance."),
                (6, "Contract care across facilities; regimen tuned by successive trials."),
                (7, "Partnership spanning regions; goods sourced through mobile alliance."),
                (8, "Assets moved across jurisdictions; custody rhythm that alternates households."),
                (9, "Education abroad; devotional miles accumulated deliberately."),
                (10, "Profession lived airport to airport; vocation staged from vehicles and halls."),
                (11, "Shared transport toward shared aim; collective hope expressed as march."),
                (12, "Transfer between healing centers; dream imagery dense with roads.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is passage—the querent arrives mid-transition among homes, roles, or vows.",
                leftWitness: "The outer field keeps reshaping—timelines, corridors, and contingencies refuse stasis.",
                judge: "Verdict is motion: resolution travels—by removal, pivot, or journey that resets the frame.",
                reconciler: "Aftermath is a new coordinate—where body or calling settles after the miles is the durable lesson."
            ),
            ModernExamples = new List<string>
            {
                "Several cities in one season, domestic rhythm bent around continual relocation",
                "Freight rerouted across continents after disruption far upstream",
                "Operations revised nightly so steadiness must be borrowed from ritual or bond outside the task"
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
