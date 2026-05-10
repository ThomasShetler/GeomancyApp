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
            Tagline = "Hot onset, thin patience, sharp edge",
            CoreMeaning = new List<string> { "initiative", "collision", "courage", "rashness", "contest", "breakout" },
            FavorableFor = new List<string> { "Emergency decisions", "Physical training or competition", "Starting a legal defense", "Breaking a deadlock", "Technical fixes under time pressure" },
            UnfavorableFor = new List<string> { "Partnership repair", "Long-term budgeting", "Anonymous whistleblowing", "Subtle office politics", "Slow diplomacy" },
            ElementalSynthesis = "Three active lines—fire, air, and earth—press outward while water lies passive, producing visible heat and articulation set against a dry emotional register. Inner air fans the outer fire, so that thought and speech accelerate action faster than feeling can refine it.",
            TraditionalImagery = new List<string> { "Sword or blade", "Youthful male signifier in old glyph sets", "Mars / Aries pairing in Renaissance tables" },
            Interpretation = new List<string>
            {
                "Read Puer as kinetic honesty: the chart is naming a place where the querent will not wait for consensus. In consultation, identify the cost of an impulsive message, an abrupt resignation, or a public statement that cannot be retracted.",
                "Because water sleeps in this figure, empathy tends to arrive late. Where the matter calls for harmony, pair Puer with softer witnesses such as Albus or Populus before extending any promise of accord.",
                "In questions of health and surgery, Puer can affirm a cut or a decisive treatment. In matters of love it marks pursuit and argument more often than steady union."
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
                (11, "Friends rally as a band of allies; hopes are pursued without hesitation."),
                (12, "Hidden irritants surface; self-sabotage through anger or late-night decisions.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin energy is martial pressure already on—the querent arrived hot, with adrenaline shaping the question more than reflection.",
                leftWitness: "External climate is escalating—rivals, deadlines, or systems are pushing the matter toward open contest.",
                judge: "Verdict by force: the matter resolves through a decisive thrust, but at the cost of bystanders and bridges.",
                reconciler: "Long tail is collateral—victory in the moment leaves a brittle edge that the querent will keep sharpening or apologizing for."
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
            BodyType = "A solid neck and a jaw that is either square or softly rounded, with a voice that carries. The shoulders are broader than the hips, and the hands handle weight with practiced ease, whether at an instrument, a kitchen, or any work that requires lifting and setting down. The skin flushes readily with feeling.",
            CharacterType = "Warm, candid, and given to attachment that runs deeper than pride will easily admit. Such a person dislikes being overlooked and oscillates between open generosity and blunt requests; reconciliation is sincere when it comes, but rarely arrives by the shortest road.",
            Colors = "White, flecked with citrine (greenish yellow)",
            Commentary = @"Amissio pictures the purse inverted: a Venusian appetite meeting an earth that cannot keep every coin. Fire and water flare without a steady mediating air, and earth at the rim lies passive, so what is gained slips because the vessel itself is open. Medieval writers placed loss alongside love-offerings and the relief of burdens, and the contemporary reader does well to hold both valences at once. The figure speaks not only of poverty but of release—what leaves makes room. Practitioners formed by Agrippa and Cattan will recognize outer wealth in outflow while the inner fire still desires the object; the tension is appetite without a tight lid.",
            DivinatoryMeaning = "Outflow, ransom, donation, separation, depreciation, the shedding of debt, or the loss of track of an asset. The figure favors questions that require letting go—divestment, candid confession, the exit from a binding contract—and disfavors questions that depend on retention, credit, or exclusive possession.",
            Tagline = "Letting go, open purse, shifting value",
            CoreMeaning = new List<string> { "release", "expense", "generosity", "scatter", "romantic surrender", "liquidity" },
            FavorableFor = new List<string> { "Paying down debt", "Donating or downsizing", "Ending a clingy relationship", "Medical detox", "Clearing inventory" },
            UnfavorableFor = new List<string> { "Raising capital", "Hoarding secrets", "Long-term lease lock-in", "Winning a retention bonus" },
            ElementalSynthesis = "Fire and water are both active while air and earth lie passive: appetite and feeling move, but neither mediation nor structure follows. Value slips between the fingers because no airy contract and no earthen vault close the circuit.",
            TraditionalImagery = new List<string> { "Bag or purse inverted", "Coins spilling in woodcut traditions", "Venus / Taurus correspondence in elemental wheels" },
            Interpretation = new List<string>
            {
                "Read Amissio as value in motion toward the exit. The first question is whether what leaves is being poured out by intention or drained by neglect.",
                "In matters of relationship the figure can bless a needed catharsis; in matters of finance it warns of thin margins unless Acquisitio answers it elsewhere in the chart.",
                "Held with care, the figure teaches that some losses purchase freedom. The practitioner should verify, however, that the querent is not mistaking waste for sacrifice."
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
            InCourtRoles = C(
                rightWitness: "The querent walked in already letting go—an asset, attachment, or expectation has been quietly slipping for months.",
                leftWitness: "External flow is outward—markets, partners, or platforms are draining value the querent does not yet see leaving.",
                judge: "Verdict is parting: the matter resolves by something leaving the querent's hands—a relief if intended, a setback if not.",
                reconciler: "Aftermath is lighter pockets and more honest shelves—what stays after the loss is the real holding."
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
            BodyType = "An upper torso held with quiet lift, breathing that remains steady under stress, and a preference for layered clothing. The fingers rest lightly at the keys or stain with ink, and the eyes visibly soften when the person is listening.",
            CharacterType = "One who speaks little but precisely, keeps notebooks, and avoids crowds while building a small network with deliberation. Such a person calms a room by lowering the volume rather than raising it, and may withdraw into study for days at a time.",
            Colors = "Brilliant white, flecked with red",
            Commentary = @"Albus is the bright cup raised without spill: Mercury in Gemini, outer air over inner water, an intellect that mirrors a quiet depth. Only water acts here while fire and earth lie at rest. The pattern is one of reflection preceding motion—a scholar's lamp rather than a forge. Renaissance texts link the figure to peace treaties and to snowfields that quiet the advance of armies. In practice Albus marks clarity won by stepping sideways from the fight: where Rubeus pours passion out, Albus collects dew, and insight condenses as the heat drops.",
            DivinatoryMeaning = "Peaceful progress, study, paperwork that clears, the truce, the diagnosis that finally clarifies, and the gentle rain that follows drought. The figure disfavors brawls and any matter that requires scandal to gather attention; it favors examinations, audits in which truth is the asset, and negotiations conducted in good faith.",
            Tagline = "Still surface, clear draft, soft power",
            CoreMeaning = new List<string> { "clarity", "study", "treaty", "chastity", "patience", "memo" },
            FavorableFor = new List<string> { "Writing briefs", "Meditation retreats", "Medical second opinions", "White-hat security review", "Apology letters" },
            UnfavorableFor = new List<string> { "Viral marketing stunts", "Hostile takeovers", "Street confrontation", "High-stakes bluffing" },
            ElementalSynthesis = "A single active water line within airy outer and inner shells: feeling and intuition move while fire and earth keep watch. Thought circulates after the manner of Mercury and Gemini, but embodiment and aggression remain temperate.",
            TraditionalImagery = new List<string> { "White cup upright", "Mercury caduceus echoes in later decks", "Peace embassy motifs in Cattan-era charts" },
            Interpretation = new List<string>
            {
                "Albus rewards the querent who will document, breathe, and verify. The figure is a vote for procedure over charisma.",
                "Because only water is active, momentum is subtle: progress here shows itself as errors caught early rather than as visible fireworks.",
                "Where the chart contains adverse martial figures, Albus suggests cooling protocols—formal mediation, deliberate pause, or the careful edit of a trusted mentor."
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
            InCourtRoles = C(
                rightWitness: "Origin is reflection: the querent has been studying, journaling, or quietly preparing rather than acting.",
                leftWitness: "Environment leans cooperative—mediators, paperwork, or institutional sympathy is moving toward the querent.",
                judge: "Verdict is settled by clarity: a paper, a ruling, or an honest conversation closes the matter without spectacle.",
                reconciler: "Aftermath is documented and durable—the resolution holds because it was reasoned, not improvised."
            ),
            ModernExamples = new List<string>
            {
                "Preparing clear documentation before a compliance review",
                "Choosing patient counsel over a public reaction one would later regret",
                "Returning to a budgeting practice after a long period of avoidance"
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
            Commentary = @"Populus is the throng: every line passive, water doubled within and without. The Moon's sign of Cancer underscores collective memory, tide, and the kitchen in which everyone speaks at once. Agrippa's lineage names the figure a mirror for whatever approaches—neither good nor ill until colored by its neighbors in the chart. Elementally, nothing initiates; inertia itself becomes stability. The practitioner should therefore map the network around the question: who is actually in the room, which feed shapes the opinion, and where the quorum, not the individual, rules the outcome.",
            DivinatoryMeaning = "The assembly, the audience, the poll, the household vote, the public mood, or any outcome decided by collective sentiment rather than by a single hand. The figure favors questions that require allies, sampling, or democratic cover, and disfavors questions that depend on a solitary decision or on stealth.",
            Tagline = "Many voices, shared mood, borrowed motion",
            CoreMeaning = new List<string> { "multitude", "mirror", "inertia", "public", "forum", "meme" },
            FavorableFor = new List<string> { "Crowdfunding", "Community organizing", "A/B testing", "Support groups", "Collective bargaining" },
            UnfavorableFor = new List<string> { "Solo entrepreneurship without team", "Secret elopement", "Whistleblowing without corroboration", "Patent stealth" },
            ElementalSynthesis = "All four lines lie passive, so no element leads. The doubling of water at inner and outer registers suggests that emotional and somatic fields hold the field while fire, air, and earth remain latent. Change enters only when another figure energizes the row.",
            TraditionalImagery = new List<string> { "Crowd glyph", "Double path in Renaissance keys", "Moon / Cancer pairing" },
            Interpretation = new List<string>
            {
                "Populus invites the question of whose tide the querent is swimming in. In the absence of a strong Judge elsewhere in the chart, drift should be expected.",
                "In contemporary readings the figure points to the distributed actor: the feed, the group chat, the collective response. Agency is dispersed, and a single villain is rarely the answer.",
                "When paired with Rubeus or Puer the crowd grows volatile; when paired with Albus or Fortuna Major it tends toward the benevolent."
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
            InCourtRoles = C(
                rightWitness: "Origin is the room the querent is standing in—family, team, audience—they did not arrive alone with this question.",
                leftWitness: "External momentum is collective: a crowd, algorithm, or quorum is shaping the trajectory more than any individual actor.",
                judge: "Verdict reflects the assembly—whatever the larger group decides becomes the answer; the querent has limited solo leverage.",
                reconciler: "Aftermath ripples through the network—reputation, group memory, and ongoing roles outlast the immediate event."
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
