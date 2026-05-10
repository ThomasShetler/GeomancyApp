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
            Commentary = @"Acquisitio lifts the purse: Jupiter in Sagittarius, inner air over outer fire, with air and earth at work to catch what falls. The medieval authors paired the figure with prudence, since appetite must still sort the genuine from the false. Fire rests at the crown, granting vision without exhaustion so long as the querent's discipline holds. The practitioner tracks the figure's enlargement in many domains: market share, knowledge, body, audience. The contrast with Amissio is exact: there, the vessel pours out; here, it opens to receive.",
            DivinatoryMeaning = "Increase obtained from a recognizable source—a raise, a grant, an inheritance, the upward motion of an investment, the news of a child, the growth of an audience, the late harvest. The figure favors questions in which the gain is ethical and budgeted, and disfavors questions that require austerity, removal, or anonymity.",
            Tagline = "Open purse, widening horizon",
            CoreMeaning = new List<string> { "expansion", "profit", "blessing", "travel", "enrollment", "bonus" },
            FavorableFor = new List<string> { "Fundraising round", "Grad school admit", "Farm purchase", "Podcast sponsor", "Grant application" },
            UnfavorableFor = new List<string> { "Minimalist off-grid plan", "Witness protection", "Bankruptcy filing" },
            ElementalSynthesis = "Air and earth are active while fire and water remain passive: ideas and material cooperate, while raw heat and tear stay reserved—a Sagittarian arrow set to the bow but not yet released.",
            TraditionalImagery = new List<string> { "Upright bag", "Jupiter glyph luck", "Sagittarian arrow" },
            Interpretation = new List<string>
            {
                "The first practitioner question concerns what will be done with the surplus. Without Tristitia somewhere in the chart, Acquisitio can deliver weight as readily as wealth.",
                "Read in modern terms, the figure speaks of accumulation that can be measured—subscribers, recurring revenue, a growing audience—and the practitioner should look elsewhere in the chart for any sign of attrition.",
                "Paired with Carcer, the gain locks into structure; paired with Via, the gain accumulates while the querent is in motion."
            },
            InHouses = H(
                (1, "Querent levels up—confidence from new credential."),
                (2, "Raise, dividend, crypto green day."),
                (3, "Publisher accepts pitch; sibling shares lead."),
                (4, "Bigger apartment; family inheritance rumor true."),
                (5, "News of a child; an awarded grant; a small but welcome stroke of fortune."),
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
                "Compensation that arrives in the same month a major obligation comes due",
                "A small subscription audience growing at last to a sustainable size",
                "Welcoming an animal into the household who proves both companion and guardian"
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
            Tagline = "Walls, ledgers, slow clocks",
            CoreMeaning = new List<string> { "delay", "security", "hoarding", "focus", "isolation", "compliance" },
            FavorableFor = new List<string> { "Cold storage crypto", "Patent filing", "Monastic year", "Construction permit wait", "Pre-surgery fasting" },
            UnfavorableFor = new List<string> { "Viral launch", "Impulse travel", "Open relationship negotiation" },
            ElementalSynthesis = "Fire and earth move while air and water lie still: heat and matter meet without breath or moisture to soften the encounter—the logic of a vault and the logic of a kiln stand together in one figure.",
            TraditionalImagery = new List<string> { "Enclosure square", "Saturn lock", "Capricorn mountain" },
            Interpretation = new List<string>
            {
                "Carcer paired with Fortuna Major can name a promotion delayed but ultimately certain; paired with Via, the impulse to break out contests with habit.",
                "The practitioner should determine whether the lock under examination is external—a policy or contract—or internal, in the form of shame or reluctance the querent has not yet named.",
                "In matters of finance, the figure often points to the trap of locked liquidity: capital exists, but a binding rule prevents its use."
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
                "A required review period before a significant release",
                "Choosing a more modest residence in order to retire a debt",
                "A solitary record of credentials whose secrecy itself becomes a liability to the household"
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
            Tagline = "Weight down, root deep, slow truth",
            CoreMeaning = new List<string> { "gravitas", "decline", "foundation", "grief", "engineering", "silence" },
            FavorableFor = new List<string> { "Pouring concrete", "Deleting legacy code", "Grief work", "Bunker research", "Longitudinal study" },
            UnfavorableFor = new List<string> { "Grand opening balloons", "Honeymoon", "Influencer livestream" },
            ElementalSynthesis = "Earth alone acts beneath passive fire, air, and water: everything in the figure compresses toward soil. The Aquarian sky-ideas must wait until the ground itself confirms them.",
            TraditionalImagery = new List<string> { "Pit and stake", "Tower foundation", "Saturn in fixed air emblems" },
            Interpretation = new List<string>
            {
                "Where Tristitia and Laetitia stand together in the court positions, the practitioner may read the cyclical exhaustion and elation of a long undertaking, in which periods of weight alternate with periods of relief.",
                "The proper question is what the matter requires the querent to bury in order to grow—seeds, infrastructure, or grief that has waited too long to be acknowledged.",
                "In matters of justice, the figure points to slow proceedings, but to evidence that accumulates steadily in the querent's favor over time."
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
                "A wave of contraction announced on the same day as a long-anticipated release",
                "Working through years of records to substantiate a claim",
                "Accepting modest counsel within reach of one's means and finding it sufficient"
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
            Tagline = "Tower lights, lifted mood, bright risk",
            CoreMeaning = new List<string> { "ascent", "visibility", "elation", "faith", "virality", "relief" },
            FavorableFor = new List<string> { "Album drop", "Proposal flash mob", "Charity gala", "Recovery milestone party", "TikTok explainer" },
            UnfavorableFor = new List<string> { "Witness protection", "Silent retreat", "Short selling quietly" },
            ElementalSynthesis = "Only fire acts within a Piscean shell of water: enthusiasm pierces the fog while air and earth remain latent—a lifted balloon awaiting the ballast that another figure must supply.",
            TraditionalImagery = new List<string> { "Tower height", "Candelabrum joy", "Jupiter exaltation echoes" },
            Interpretation = new List<string>
            {
                "Laetitia welcomes the public stage. The practitioner should determine whether the querent is in fact ready for the scrutiny that arrives with the applause.",
                "Paired with Tristitia, the elation is followed by a corresponding contraction; paired with Fortuna Minor, the rise is rapid and the descent more rapid still.",
                "In matters of health, the figure prompts attention to sustainability. A debt of sleep or rest is often concealed behind the glow."
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
                "A successful release that exposes the limits of the system meant to support it",
                "A long-held truth shared publicly, met with warmth and followed by exhaustion",
                "An unexpected windfall whose disclosure brings unfamiliar relations to the door"
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
