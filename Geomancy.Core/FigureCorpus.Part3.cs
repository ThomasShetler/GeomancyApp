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
            BodyType = "Broad hips and strong thighs, with a stride that covers ground efficiently. The hands accept what is offered without ceremony, the laugh carries, and the weight is carried low in the body, after the manner of a wrestler or a working dancer.",
            CharacterType = "An optimistic dealmaker who tends to believe that growth resolves most difficulties. Such a person speaks plainly, teaches what they know without pretense, and parts with their materials freely.",
            Colors = "Red, yellow, or green",
            Commentary = @"Acquisitio lifts the purse: Jupiter in Sagittarius, inner air over outer fire, with air and earth at work to catch what falls. The medieval authors paired the figure with prudence, since appetite must still sort the genuine from the false. Fire rests at the crown, granting vision without exhaustion so long as the querent's discipline holds. The practitioner tracks the figure's enlargement in land held, lore gathered, health regained, and patronage reckoned plainly. The contrast with Amissio is exact: there, the vessel pours out; here, it opens to receive.",
            DivinatoryMeaning = "Increase obtained from a recognizable source—a raise, a grant, an inheritance, the upward motion of an investment, the news of a child, the growth of an audience, the late harvest. The figure favors questions in which the gain is ethical and budgeted, and disfavors questions that require austerity, removal, or anonymity.",
            Tagline = "Receiving vessel, widening fortune, prudent appetite",
            CoreMeaning = new List<string> { "increase", "patronage", "inheritance", "pilgrimage", "matriculation", "increment" },
            FavorableFor = new List<string> { "Capital campaign or endowment drive", "Admission to advanced study", "Purchase of arable land or workshop", "Sponsored broadcast or serial publication", "Grant, fellowship, or benefaction" },
            UnfavorableFor = new List<string> { "Radical voluntary poverty", "Relocation requiring anonymity", "Court-supervised insolvency" },
            ElementalSynthesis = "Air and earth are active while fire and water remain passive: ideas and material cooperate, while passion and deep feeling stay tempered—a Sagittarian arrow set to the bow but not yet released.",
            TraditionalImagery = new List<string> { "Upright purse or bag", "Jupiter's temperate abundance", "Archer's horizon line" },
            Interpretation = new List<string>
            {
                "The first practitioner question concerns what will be done with the surplus. Without Tristitia somewhere in the chart, Acquisitio can deliver weight as readily as wealth.",
                "Read in modern terms, the figure speaks of increase that admits an honest tally—steady yield from entrusted work, a circle of clients or hearers whose trust holds, inventories that swell—and the practitioner should look elsewhere in the chart for any sign of attrition.",
                "Paired with Carcer, the gain locks into structure; paired with Via, the gain accumulates while the querent is in motion."
            },
            InHouses = H(
                (1, "Standing rises—confidence rooted in credential, training, or recognized office."),
                (2, "Salary increase, dividend, or measured appreciation of held assets."),
                (3, "Publisher or committee accepts the matter; kin or neighbors furnish a trustworthy introduction."),
                (4, "More ample dwelling or grounds; credible news of inheritance or family transfer."),
                (5, "Word of a child; artistic prize; modest fortune arriving through pleasure or patronage."),
                (6, "Improved regimen or benefits; strength returning under disciplined care."),
                (7, "Partner or ally brings opportunity; joint venture equity or favorable compact."),
                (8, "Beneficial movement in shared finance; insurer's payout or patron capital."),
                (9, "Travel or study abroad permitted; advance against teaching or writing; pilgrimage opens."),
                (10, "Promotion, public honor, or invitation to board or governance."),
                (11, "Benefactors multiply; collective appeal exceeds its mark; mentor widens the circle of introductions."),
                (12, "Quiet benefaction; favorable settlement whose source remains partly concealed.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin lies in upward momentum—the querent arrives with wind at the back and means already gathered for use.",
                leftWitness: "External motion converges inward—offers, payments, or openings accumulate toward the querent.",
                judge: "Verdict favors gain: the matter concludes with something added—money, contract, rank, or tangible increase.",
                reconciler: "Aftermath enlarges the field—what is acquired reshapes obligation and widens the horizon of later choice."
            ),
            ModernExamples = new List<string>
            {
                "Compensation arriving when a large obligation comes due, easing the ledger without spectacle",
                "A modest readership or clientele at last crossing the threshold of sustainability",
                "Welcoming an animal into the household who proves comforter and quiet guardian alike"
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
            BodyType = "Pronounced knees, careful steps, and a tension that gathers in the jaw. Such a person tends toward sturdy footwear, organizes receipts with method, and visibly stiffens when the conversation turns to budgets or commitments.",
            CharacterType = "Frugal, stubborn, and loyal to whatever protocol they have accepted. The fear of disorder runs deeper than the fear of boredom; trust is slow to grant, and once a clear brief has been given, the same person becomes an excellent executor.",
            Colors = "White, russet, or dun (pale brown)",
            Commentary = @"Carcer is the lock and the ledger: Saturn in Capricorn, with earth doubled at the inner and outer registers, while fire and earth move and air and water lie still. Energy meets matter without mediating elements, and work narrows to a single tunnel. The medieval readers assigned to this figure delay, accumulation, and safekeeping at once. The contemporary practitioner is left to ask whether the walls in question are protecting or suffocating—a contract, a savings instrument, a vow, a procedural hold. The figure stabilizes whatever it touches, and the practitioner should verify that the querent in fact desires the matter to be held in place.",
            DivinatoryMeaning = "Delay, the held deposit, the quarantine, the binding agreement, the fortified store, the long-term reserve, the institutional clock, the legal hold. The figure favors questions in which structure preserves what would otherwise be lost, and disfavors questions in which speed, public exposure, or emotional movement is required.",
            Tagline = "Enclosure, ledger, measured time",
            CoreMeaning = new List<string> { "delay", "custody", "reserve", "concentration", "seclusion", "compliance" },
            FavorableFor = new List<string> { "Long-term custodial vault or archive", "Patent prosecution", "Structured cloister or retreat year", "Permit queue borne with patience", "Clinical fasting before surgery" },
            UnfavorableFor = new List<string> { "Launch timed for maximum publicity", "Impulsive journey without itinerary", "Negotiating radical openness in partnership" },
            ElementalSynthesis = "Fire and earth move while air and water lie still: heat and matter meet without breath or moisture to soften the encounter—the logic of a vault and the logic of a kiln stand together in one figure.",
            TraditionalImagery = new List<string> { "Squared enclosure", "Saturn's bond", "Mountain ascent, slow grade" },
            Interpretation = new List<string>
            {
                "Carcer paired with Fortuna Major can name a promotion delayed but ultimately certain; paired with Via, the impulse to break out contests with habit.",
                "The practitioner should determine whether the lock under examination is external—a policy or contract—or internal, in the form of shame or reluctance the querent has not yet named.",
                "In matters of finance, the figure often points to the trap of locked liquidity: capital exists, but a binding rule prevents its use."
            },
            InHouses = H(
                (1, "Self-definition narrows under duty; composure maintained through visible restraint."),
                (2, "Liquidity gated by term or covenant; thrift enforced by instrument and schedule."),
                (3, "Silence bound by agreement; distance from siblings or near kindred."),
                (4, "Mortgage chain or lien on domicile; elder care that fixes residence."),
                (5, "Creative constriction; vowed continence; fertility governed by calendar or protocol."),
                (6, "Office enclosure; chronic condition managed by regimen rather than cure."),
                (7, "Prenuptial instrument; non-compete; marriage sustained more by obligation than warmth."),
                (8, "Trust with maturity date; systematic reduction of encumbrance."),
                (9, "Higher study deferred by bureaucracy; visa or credential held in suspension."),
                (10, "Hierarchy and clearance define motion; promotion paced by institutional clock."),
                (11, "Collective statute freezes tiers; patron benefits locked by rule."),
                (12, "Involuntary hold; monitored residence; seclusion imposed for safety or remedy.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is constraint already binding—the querent entered the question inside contract, office, or long habit.",
                leftWitness: "The outer climate tightens—rules, gatekeepers, or material limits narrow what motion is permitted.",
                judge: "Verdict is closure or hold: the matter resolves by fixation—secured at last, or shut away.",
                reconciler: "Aftermath is structural—the boundary set here endures; the querent must furnish an interior life fit for the walls."
            ),
            ModernExamples = new List<string>
            {
                "A mandated review interval before publication or release may proceed",
                "Choosing humbler quarters so that debt retires on an honest schedule",
                "Licenses withheld in shame burdening the household more than truthful disclosure eventually would"
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
            BodyType = "Heavy, well-set ankles and a preference for grounded footwear, with a deliberate, settling walk. Such a person is drawn to weighted objects, gestures with the hands when thinking, and has a complexion that pales somewhat in cold seasons.",
            CharacterType = "Dry of wit, loyal once trust has been earned through long acquaintance, and inclined to think in systems. There is a strain of realism that can edge into the depressive register, and a habit of innovating precisely because older arrangements have hurt.",
            Colors = "Tawny or sky blue",
            Commentary = @"Tristitia drives the stake: Saturn in Aquarius, outer air over inner earth, with only earth active beneath three passive lines. The medieval authors named the figure sorrow, yet also recognized in it the foundation upon which towers may eventually stand if patience is allowed to do its work. Aquarius lifts the paradox into view: networks that freeze progress until the specifications improve. The practitioner should read this figure as downward motion—a price falling, a mood dropping, a foundation being dug—but should also recognize the same motion as the setting of roots upon which later structure depends.",
            DivinatoryMeaning = "A spell of low mood, a slide in valuation, the rumor of contraction, the work of digging, the long research, the funeral, the sober commitment. The figure favors construction, the careful preservation of what must be buried, and any matter served by sustained downward attention; it disfavors occasions and launches that depend on visible enthusiasm.",
            Tagline = "Weight downward, root deeper, truth slower",
            CoreMeaning = new List<string> { "gravitas", "contraction", "foundation", "mourning", "excavation", "reserve" },
            FavorableFor = new List<string> { "Pouring foundation or footing", "Retiring obsolete systems", "Structured mourning or therapy", "Shelter and contingency planning", "Longitudinal field study" },
            UnfavorableFor = new List<string> { "Grand-opening spectacle", "Honeymoon counted on for rescue", "Performative publicity tour" },
            ElementalSynthesis = "Earth alone acts beneath passive fire, air, and water: everything in the figure compresses toward soil. The Aquarian sky-ideas must wait until the ground itself confirms them.",
            TraditionalImagery = new List<string> { "Pit and driven stake", "Tower footing before rise", "Saturn in fixed-air emblems" },
            Interpretation = new List<string>
            {
                "Where Tristitia and Laetitia stand together in the court positions, the practitioner may read the cyclical exhaustion and elation of a long undertaking, in which periods of weight alternate with periods of relief.",
                "The proper question is what the matter requires the querent to bury in order to grow—seeds, infrastructure, or grief that has waited too long to be acknowledged.",
                "In matters of justice, the figure points to slow proceedings, but to evidence that accumulates steadily in the querent's favor over time."
            },
            InHouses = H(
                (1, "Heaviness of spirit; doubt persisting beneath outward competence."),
                (2, "Reduced income; austerity; household economy narrowed to essentials."),
                (3, "Dispiriting tidings; minor injury or hindrance on daily passage."),
                (4, "Substructure or cellar wants repair; ancestral sorrow surfaces in the dwelling."),
                (5, "Risk to conception; creative inhibition; pleasure withheld."),
                (6, "Chronic diagnosis; labor that earns little gratitude."),
                (7, "Partner withdrawn; litigation or negotiation draining by inches."),
                (8, "Collector's pressure; tax upon inheritance or shared obligation."),
                (9, "Scholarly rejection; visa or credential refused."),
                (10, "Whisper of demotion; advancement blocked by invisible ceiling."),
                (11, "Hope postponed; fatigue in common cause or fellowship."),
                (12, "Depression in confinement; secret burden that bends the inner life.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin is grief or fatigue—the querent bears weight that antedates the present question.",
                leftWitness: "The outer climate grinds—delay, scarcity, or institutional coolness wears the matter thin.",
                judge: "Verdict is heavy: resolution comes slowly and beneath hope's measure; what arrives is true and dear.",
                reconciler: "Aftermath is depth rather than mere harm—pain here schools a gravity the querent will wield as quiet authority."
            ),
            ModernExamples = new List<string>
            {
                "Public contraction announced the day a long-prepared work finally appears",
                "Years of records sifted to substantiate a claim that justice cannot hurry",
                "Counsel modest and nearby accepted—and found, against expectation, sufficient"
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
            BodyType = "A buoyant step and well-formed feet, a laugh that involves the whole torso, and hair worn somewhat freely. Such a person tends to go barefoot at home and to be visibly moved at thresholds—weddings, ordinations, departures, and arrivals.",
            CharacterType = "Possessed of an innocent enthusiasm and a capacity for genuine awe. The temperament is given to the gambler's optimism and the swift forgiveness, and is at its best in the company of a steadying companion who can supply weight when the figure provides lift.",
            Colors = "Glittering pale green",
            Commentary = @"Laetitia lifts the head: Jupiter in Pisces, inner fire over outer water, with fire alone active while its siblings rest—a torch raised against fog. The medieval joy figures warned that elevation invites visibility: the tower attracts the lightning. The practitioner therefore balances elation with discretion—the promotion announced too early, the pregnancy revealed before its time, the work released before it has been finished. The watery outer shell broadcasts empathy widely, which is beautiful so long as the querent's boundaries do not begin to blur.",
            DivinatoryMeaning = "Promotion, public success, the spiritual lift, the rallying of sentiment, the news of a child, the season of celebration. The figure favors undertakings that require visible joy and disfavors questions that require secrecy, stealth, or simple stillness.",
            Tagline = "Tower lights, uplifted spirit, bright hazard",
            CoreMeaning = new List<string> { "ascent", "visibility", "elation", "devotion", "public celebration", "relief" },
            FavorableFor = new List<string> { "Public première of creative work", "Betrothal openly declared", "Benefit supper or charitable levee", "Observance of recovery or remission", "Popular lecture or broad explanatory piece" },
            UnfavorableFor = new List<string> { "Life under witness protection", "Enclosed silent retreat", "Unadvertised dissent or quiet resistance alone" },
            ElementalSynthesis = "Only fire acts within a Piscean shell of water: enthusiasm pierces the fog while air and earth remain latent—a lifted balloon awaiting the ballast that another figure must supply.",
            TraditionalImagery = new List<string> { "Elevated tower", "Candelabrum of gladness", "Jovian light through mist" },
            Interpretation = new List<string>
            {
                "Laetitia welcomes the public stage. The practitioner should determine whether the querent is in fact ready for the scrutiny that arrives with the applause.",
                "Paired with Tristitia, the elation is followed by a corresponding contraction; paired with Fortuna Minor, the rise is rapid and the descent more rapid still.",
                "In matters of health, the figure prompts attention to sustainability. A debt of sleep or rest is often concealed behind the glow."
            },
            InHouses = H(
                (1, "The querent visibly lifts—renewed carriage, color, or reputation abroad."),
                (2, "Windfall bonus; equity rises on credible intelligence."),
                (3, "Correspondence finds wide reception; sibling or peer earns public distinction."),
                (4, "Festivity at home; favorable appraisal or generous estimate of grounds."),
                (5, "Joy of conception; exhibition or concert; pleasure crowned with notice."),
                (6, "Word of remission; new companion creature under the roof."),
                (7, "Betrothal celebrated; partnership praised in the circle that matters."),
                (8, "Beneficial inheritance movement; settlement concluded with relief."),
                (9, "Admission to chosen study; pilgrimage or devotional journey secured."),
                (10, "Chief officer applauded; nomination or laurel in the trade."),
                (11, "Collective patronage succeeds; fellowship tastes hope again."),
                (12, "Ecstasy of seclusion; secrecy strained by the impulse to testify.")
            ),
            InCourtRoles = C(
                rightWitness: "Origin lies in lifted spirits—the querent enters buoyed by recent grace, healing, or creative influx.",
                leftWitness: "Outer waters run buoyant—blessing, festivity, or favorable tide moves toward the querent.",
                judge: "Verdict is joyous: the matter resolves in relief, success, or liberty long withheld.",
                reconciler: "Aftermath is durable elevation—not accident; the new height becomes customary ground."
            ),
            ModernExamples = new List<string>
            {
                "A successful première that lays bare how slight the footing beneath applause proves to be",
                "Truth hoarded for years spoken aloud, welcomed, then paid for in weariness",
                "Unexpected gain whose announcement draws strangers and distant kin to the threshold"
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
