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
            BodyType = "Compact, athletic frame; quick gestures; flushed or weathered complexion; jaw and brow pronounced; hairline often uneven; hands calloused or nicked from tools, keyboards, or contact sports.",
            CharacterType = "Acts first and edits later; competitive; blunt speech; loyal in bursts; restless curiosity; dislikes being managed; calms down only after motion or competition has burned off adrenaline.",
            Colors = "White, flecked with red",
            Commentary = @"Puer names the martial flash that leaps before calculation. In the old books the figure is read with Mars and the Ram: outward ignition, inner air—the mind racing ahead of consequence. Elementally, fire and air strike while earth holds a purchase on outcome, yet water sleeps: there is heat and edge without the tempering of reception. The image is not romance but mechanics—pressure seeking a channel. Where medieval authors spoke of the young soldier or the duelist, the diviner today should think of vectors: who initiates, who escalates, where the first blow lands. Puer rewards questions that need decisive intervention; it punishes those that need patience, secrecy, or fine diplomacy.",
            DivinatoryMeaning = "Swift motion, initiation, quarrel, lawsuit, surgery, mechanical repair, athletic contest, or any matter where force applied early decides the field. Good when you must cross a threshold before doubt erodes will; poor for reconciliation, long finance, or delicate reputation. Traditionally tied to justice in the sense of contest, not calm judgment.",
            Tagline = "Hot onset, thin patience, sharp edge",
            CoreMeaning = new List<string> { "initiative", "collision", "courage", "rashness", "contest", "breakout" },
            FavorableFor = new List<string> { "Emergency decisions", "Physical training or competition", "Starting a legal defense", "Breaking a deadlock", "Technical fixes under time pressure" },
            UnfavorableFor = new List<string> { "Partnership repair", "Long-term budgeting", "Anonymous whistleblowing", "Subtle office politics", "Slow diplomacy" },
            ElementalSynthesis = "Three active lines (fire, air, earth) push outward while water rests passive: outward heat and articulation with a dry emotional register. Inner air fans outer fire—ideas and speech accelerate action more than feeling refines it.",
            TraditionalImagery = new List<string> { "Sword or blade", "Youthful male signifier in old glyph sets", "Mars / Aries pairing in Renaissance tables" },
            Interpretation = new List<string>
            {
                "Treat Puer as kinetic honesty: the chart is showing where someone will not wait for consensus. In consultations, name the risk of impulsive email, abrupt resignation, or a public post that cannot be recalled.",
                "Because water is passive, empathy may arrive late; pair this figure with witnesses that soften (Albus, Populus) before promising harmony.",
                "In health and surgery questions, Puer can affirm cutting or intervention; in romance it more often marks pursuit or argument than stable union."
            },
            InHouses = H(
                (1, "The querent is primed to act; self-presentation reads as bold or confrontational."),
                (2, "Money moves quickly—spending spikes, haggling, or a fight over cash flow."),
                (3, "Siblings or neighbors become triggers; messages escalate tone."),
                (4, "Domestic scene heats—repairs, boundary disputes, or restless household energy."),
                (5, "Creative risk, flirtation, or speculative bets favored by daring."),
                (6, "Work intensity, acute symptoms, or clash with subordinates/service staff."),
                (7, "Open rivalry or chemistry with the other party; contracts need hard terms."),
                (8, "Shared resources become a battleground; surgery or crisis management themes."),
                (9, "Travel or study plans rush forward; debate on principles turns sharp."),
                (10, "Career visibility spikes—promotion push or public friction with authority."),
                (11, "Friends rally as a war-band; hopes are pursued aggressively."),
                (12, "Hidden irritants surface; self-sabotage through anger or late-night decisions.")
            ),
            ModernExamples = new List<string>
            {
                "Sending a heated reply-all before reading the thread fully",
                "Signing up for a tournament, trial shift, or coding sprint to prove a point",
                "Choosing urgent surgery versus watchful waiting on a test result"
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
            BodyType = "Solid neck, square or soft jaw, voice carries; shoulders wider than hips; hands good at holding or releasing weight—musicians, movers, cooks; skin tends to flush with emotion or wine.",
            CharacterType = "Warm, possessive, candid; hates being ignored; oscillates between generosity and blunt demands; attachment runs deep but pride complicates apologies.",
            Colors = "White, flecked with citrine (greenish yellow)",
            Commentary = @"Amissio pictures the purse inverted: Venusian appetite meeting earth that cannot keep every coin. Fire and water flare without steady air to negotiate between them, and earth at the rim stays passive—gain slips because the vessel is open. Medieval writers paired loss with love-offerings and with relief from burdens; the modern reader should keep both valences. The figure is not only poverty but release—what leaves makes room. Diviners trained on Agrippa and Cattan will recall outer wealth flowing away while inner fire still wants the object; the tension is desire without a tight lid.",
            DivinatoryMeaning = "Outflow, ransom, ransom paid, donation, breakup, depreciation, shedding debt, emptying a storage unit, or losing track of an asset. Favorable when you need to lose weight, sell stock, exit a contract, or confess; unfavorable when retention, credit, or exclusive possession is the goal.",
            Tagline = "Letting go, open purse, shifting value",
            CoreMeaning = new List<string> { "release", "expense", "generosity", "scatter", "romantic surrender", "liquidity" },
            FavorableFor = new List<string> { "Paying down debt", "Donating or downsizing", "Ending a clingy relationship", "Medical detox", "Clearing inventory" },
            UnfavorableFor = new List<string> { "Raising capital", "Hoarding secrets", "Long-term lease lock-in", "Winning a retention bonus" },
            ElementalSynthesis = "Fire and water both active while air and earth sleep: appetite and feeling move, but structure and mediation lag. Things slip between fingers because no airy contract and no earthy vault close the circuit.",
            TraditionalImagery = new List<string> { "Bag or purse inverted", "Coins spilling in woodcut traditions", "Venus / Taurus correspondence in elemental wheels" },
            Interpretation = new List<string>
            {
                "Read Amissio as value in motion toward the exit. Ask what is being poured out on purpose versus what is draining through neglect.",
                "In relationship charts it can bless catharsis; in finance it warns of thin margins unless paired with Acquisitio elsewhere.",
                "Charitable tone: the figure teaches that some losses purchase freedom—verify the querent is not confusing waste with sacrifice."
            },
            InHouses = H(
                (1, "Identity softens—rebranding, haircut, or confession strips an old mask."),
                (2, "Cash leaves; salary cut, fine, or splurge; watch subscriptions and fees."),
                (3, "News disperses; rumor mill drops a name or a secret slips."),
                (4, "Family asset sold; heirlooms leave the house; caregiver burnout."),
                (5, "Affair cools, creative draft scrapped, or gambling loss."),
                (6, "Illness depletes reserves; pet rehomed; job hours trimmed."),
                (7, "Partner walks away share, divorce settlement, or client churn."),
                (8, "Shared fund drained; insurance payout; inheritance dispersed."),
                (9, "Tuition paid, pilgrimage cost, or publisher rejects—knowledge bought with price."),
                (10, "Title or stock options slip; reputation trade-off for integrity."),
                (11, "Crowdfunding misses goal; friend group splinters after loan."),
                (12, "Anonymous leak; hospital bill; hidden subscription cancels savings.")
            ),
            ModernExamples = new List<string>
            {
                "Closing a brokerage account to fund a move abroad",
                "Deleting a dating profile after a candid talk",
                "Auctioning tools after a career pivot"
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
            BodyType = "Upper torso rounded or lifted posture; breath steady; prefers layered clothing; fingers ink-stained or keyboard-light; eyes soften when listening.",
            CharacterType = "Speaks little but precisely; keeps notebooks; avoids crowds yet networks selectively; calms rooms by lowering volume; can withdraw into study for days.",
            Colors = "Brilliant white, flecked with red",
            Commentary = @"Albus is the bright cup raised without spill: Mercury in Gemini, outer air, inner water—mind mirroring a quiet depth. Only water acts; fire and earth rest. The pattern is reflection before motion, a scholar's lamp rather than a forge. Renaissance texts link the figure to peace treaties and to snowfields that slow armies. Practically, Albus marks clarity obtained by stepping sideways from the fight. Where Rubeus pours passion out, Albus collects dew: insight condenses when heat drops.",
            DivinatoryMeaning = "Peaceful progress, study, paperwork that clears, truce, diagnosis that clarifies, gentle rain after drought. Weak for brawls, hype launches, or anything needing scandal energy; strong for exams, audits that favor truth, and white-hat negotiations.",
            Tagline = "Still surface, clear draft, soft power",
            CoreMeaning = new List<string> { "clarity", "study", "treaty", "chastity", "patience", "memo" },
            FavorableFor = new List<string> { "Writing briefs", "Meditation retreats", "Medical second opinions", "White-hat security review", "Apology letters" },
            UnfavorableFor = new List<string> { "Viral marketing stunts", "Hostile takeovers", "Street confrontation", "High-stakes bluffing" },
            ElementalSynthesis = "Single active water line inside airy shells: feeling and intuition move while fire and earth wait. Thought circulates (Gemini/Mercury) but embodiment and aggression stay cool.",
            TraditionalImagery = new List<string> { "White cup upright", "Mercury caduceus echoes in later decks", "Peace embassy motifs in Cattan-era charts" },
            Interpretation = new List<string>
            {
                "Albus rewards the querent who will document, breathe, and verify. It is a vote for procedure over charisma.",
                "Because only water is active, momentum is subtle—progress shows as errors caught early, not fireworks.",
                "Pair with unfavorable martial figures to suggest cooling protocols: legal mediation, cooling-off clauses, or a mentor's edit."
            },
            InHouses = H(
                (1, "Querent chooses restraint; reputation reads as thoughtful, not flashy."),
                (2, "Budget stabilizes through accounting; small surplus from careful cuts."),
                (3, "Neighborly truce; siblings fact-check instead of feud."),
                (4, "Home becomes sanctuary; quiet renovation or elder care planning."),
                (5, "Chaste date, craft refinement, or low-key creative residency."),
                (6, "Healing regimen, gentle therapy animal, HR policy that actually helps."),
                (7, "Mediator enters partnership dispute; contract reviewed line by line."),
                (8, "Estate planning without drama; therapy breakthrough on shame."),
                (9, "Visa paperwork clears; thesis advisor approves outline."),
                (10, "Boardroom calm; mentor shields a junior from gossip."),
                (11, "Book club or union steward offers wise counsel; hopes modest but sound."),
                (12, "Dream journal yields insight; insomnia eases when phone leaves bedroom.")
            ),
            ModernExamples = new List<string>
            {
                "Switching to plaintext documentation before a compliance audit",
                "Choosing therapy over subtweeting an ex",
                "Using a budgeting app after months of avoidance"
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
            BodyType = "Torso carries tension in diaphragm; gait syncs with whoever walks beside; complexion shifts with tides, sleep, or hormones; breasts or belly emphasized in classical sketches.",
            CharacterType = "Mirrors company; moods contagious; loyal to tribe; indecisive alone; collects stories; remembers who sat where at the wedding.",
            Colors = "Sea green or dark russet brown",
            Commentary = @"Populus is the throng: every line passive, doubled water inside and out. The Moon's sign Cancer underscores collective memory, tides, and kitchens where everyone talks at once. Agrippa's lineage calls the figure a mirror for whatever approaches—neither good nor ill until colored by neighbors in the chart. Elementally, nothing initiates; inertia itself becomes stability. The diviner should map networks: who is actually in the room, which algorithmic feed shapes opinion, where quorum rules the outcome.",
            DivinatoryMeaning = "Assembly, audience, poll results, HOA vote, union card check, social proof, or any outcome decided by mass mood. Favorable when you need allies, samples, or democratic cover; unfavorable when a lone decision or stealth is required.",
            Tagline = "Many voices, shared mood, borrowed motion",
            CoreMeaning = new List<string> { "multitude", "mirror", "inertia", "public", "forum", "meme" },
            FavorableFor = new List<string> { "Crowdfunding", "Community organizing", "A/B testing", "Support groups", "Collective bargaining" },
            UnfavorableFor = new List<string> { "Solo entrepreneurship without team", "Secret elopement", "Whistleblowing without corroboration", "Patent stealth" },
            ElementalSynthesis = "All four passive: no element leads; water bookends imply emotional or somatic fields dominate while fire, air, and earth stay latent. Change enters only when another figure energizes the row.",
            TraditionalImagery = new List<string> { "Crowd glyph", "Double path in Renaissance keys", "Moon / Cancer pairing" },
            Interpretation = new List<string>
            {
                "Populus asks whose tide you are swimming. Without a strong Judge, expect drift.",
                "In tech-age readings, think feeds, group chats, and review bombs—distributed agency, not a single villain.",
                "When paired with Rubeus or Puer, the crowd turns volatile; with Albus or Fortuna Major, it leans benevolent."
            },
            InHouses = H(
                (1, "Self is shaped by roles—parent, partner, job title—more than solo will."),
                (2, "Income tied to platform, tips, or household pooling."),
                (3, "Local chatter decides facts; Nextdoor energy."),
                (4, "Family table, tenants, cohousing vote."),
                (5, "Fan community, theater troupe, sports stands."),
                (6, "Shift workers, clinic waiting room, open-plan office."),
                (7, "Jury, marriage as public performance, business partnership optics."),
                (8, "Collective debt, HOA reserve, shared insurance risk pool."),
                (9, "University cohort, pilgrimage caravan, Discord study server."),
                (10, "Org chart crowd; middle management consensus."),
                (11, "Mutuals, Patreon backers, activist listserv."),
                (12, "Hospital ward, subreddit lurkers, unseen online pile-on.")
            ),
            ModernExamples = new List<string>
            {
                "Kickstarter crossing because of influencer signal boost",
                "Neighborhood app deciding if a new shop is 'welcome'",
                "Remote team voting on async Slack poll"
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
