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
            Tagline = "Exit stamp, tail risk, burnt bridge",
            CoreMeaning = new List<string> { "closure", "exhaust", "migration", "karma", "write-off", "last mile" },
            FavorableFor = new List<string> { "Deleting account", "Ending lease", "Hospice acceptance", "Sunsetting app", "Leaving cult" },
            UnfavorableFor = new List<string> { "Renewing vows", "IPO lockup extension", "Tenure clock freeze" },
            ElementalSynthesis = "Three active lines press above a passive earth: heat, idea, and tear all move outward without soil to receive them—the figure releases what it carries, after the manner of a stage discarded once its work is done.",
            TraditionalImagery = new List<string> { "Footprints away", "Dragon tail south node", "Sagittarian departure" },
            Interpretation = new List<string>
            {
                "Read with Caput Draconis, the figure tells a complete lifecycle question: the head proposes, and the tail disposes.",
                "In matters of finance, the figure marks write-offs, refunded transactions, and the long aftermath of an investment that did not hold.",
                "In matters of the spirit, the figure points to the initiation that requires the querent to leave an old name at the door before crossing."
            },
            InHouses = H(
                (1, "An old identity is set aside; a former name is formally retired."),
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
            InCourtRoles = C(
                rightWitness: "Origin is something concluding—the question rises out of an exit, divorce, or closing chapter the querent has been navigating.",
                leftWitness: "External climate is closing doors—platforms shutting, partners departing, options narrowing in the trajectory.",
                judge: "Verdict is ending: the matter resolves by termination—a release if needed, a loss if clung to.",
                reconciler: "Aftermath is the empty room—what remains after the door shuts becomes the querent's new starting point."
            ),
            ModernExamples = new List<string>
            {
                "Closing a long-standing public account whose climate has decisively turned",
                "A final shift at a workplace being phased out by a successor system",
                "A concluding session of long counsel, at which the work at last finds its rest"
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
            Tagline = "Threshold in, fertile rough start",
            CoreMeaning = new List<string> { "ingress", "upgrade", "apprenticeship", "visa", "seed", "mentor" },
            FavorableFor = new List<string> { "First day oncall", "Adoption placement call", "Scholarship start", "Green card biometrics", "Sabbatical plan" },
            UnfavorableFor = new List<string> { "Final paycheck", "Deleting data", "Closing franchise" },
            ElementalSynthesis = "Air, water, and earth all act while fire waits in passivity: craft, care, and soil cooperate while the raw ignition holds in reserve—the seedling held in greenhouse conditions until the season permits its planting outdoors.",
            TraditionalImagery = new List<string> { "Footprints inward", "North node head", "Virgo harvest start" },
            Interpretation = new List<string>
            {
                "Caput Draconis tends to favor the paperwork that unlocks. The practitioner should verify deadlines carefully, so that the door is not closed by a lapse the querent could have prevented.",
                "Where Cauda Draconis appears in other houses of the same chart, the larger story is one of migration—an outbreath in one quarter, an inbreath in another.",
                "In matters of resource, the grant or award most often arrives accompanied by conditions, and those conditions deserve the same attention as the gift itself."
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
            InCourtRoles = C(
                rightWitness: "Origin is fresh ground—the querent walked in with a new identity, residence, or commitment recently taken.",
                leftWitness: "External climate offers a threshold—openings, invitations, or undiscovered paths waiting in the trajectory.",
                judge: "Verdict is beginning: the matter resolves by initiating something—a chapter opens that will define the next phase.",
                reconciler: "Aftermath is genesis—the choice made here seeds a project, relationship, or vocation that grows over years."
            ),
            ModernExamples = new List<string>
            {
                "Receiving access to one's new institutional environment on the first day of work",
                "A residency status confirmed and the long process of formal arrangement begun",
                "An unexpected call to receive a child, met with the practical preparation of the household"
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
            Tagline = "Flash win, borrowed spotlight",
            CoreMeaning = new List<string> { "velocity", "patron", "hype", "surface", "gift", "volatility" },
            FavorableFor = new List<string> { "Launch week spike", "Rescue helicopter", "VIP line skip", "24h hackathon prize" },
            UnfavorableFor = new List<string> { "Century bond", "Monastic stability", "Undercover years" },
            ElementalSynthesis = "Fire and air move while water and earth lie still: heat and wind cooperate, while moisture and soil wait their turn—a banner upon the battlements rather than a root within the slope.",
            TraditionalImagery = new List<string> { "Mountain staff", "Lesser fortune", "Leo air outer emblems" },
            Interpretation = new List<string>
            {
                "The first practitioner question is who owns the platform beneath the moment of visibility, since the answer determines how long the visibility itself can be sustained.",
                "Where the Reconciler in the chart is unfavorable, the promotion conferred by this figure tends to feel hollow over time, and the practitioner does well to revisit the matter at a later date.",
                "The instructive contrast is with Fortuna Major: the same solar sign, but very different depths of soil beneath the same crown."
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
            InCourtRoles = C(
                rightWitness: "Origin is borrowed momentum—the querent arrived leaning on someone else's wave or a recent lucky break.",
                leftWitness: "External flow is fast and favorable but conditional—help arrives quickly with strings or short shelf life.",
                judge: "Verdict is yes for now: the matter resolves favorably in the short term but rests on assistance the querent does not control.",
                reconciler: "Aftermath thins as the loaned wind dies—what remains is what the querent built while the breeze was free."
            ),
            ModernExamples = new List<string>
            {
                "Borrowed computing resources from a mentor that make a demonstration possible",
                "A small group booked to perform on the strength of a brief recording that traveled widely",
                "An award won inside a brief application window that few were prepared to meet"
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
            TraditionalImagery = new List<string> { "Road fork", "Wayfarer", "Lunar journey glyphs" },
            Interpretation = new List<string>
            {
                "Via answers the question of how rather than the question of where, and process tends to dominate outcome until the Judge of the chart freezes the frame.",
                "Read with Carcer, the figure can describe a commute that has become a confinement; read with Populus, it describes the moment in which a great many people set out at once.",
                "In matters of finance, the figure points more readily to cash flow than to savings: value moves through the querent's hands rather than accumulating within them."
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
            InCourtRoles = C(
                rightWitness: "Origin is transit—the querent walked in already in motion, between places, jobs, or commitments.",
                leftWitness: "External climate is in flux—routes, schedules, and circumstances keep moving in the trajectory.",
                judge: "Verdict is travel: the matter resolves through movement—a relocation, pivot, or literal trip that changes everything.",
                reconciler: "Aftermath is a new address—where the querent ends up, geographically or in life, is the lasting answer."
            ),
            ModernExamples = new List<string>
            {
                "Four cities in a single quarter, with personal life adjusted to the constant motion",
                "A shipment rerouted across continents because of a distant logistical bottleneck",
                "A working system that changes nightly, requiring stability to be sourced from elsewhere"
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
