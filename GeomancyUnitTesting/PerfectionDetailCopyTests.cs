using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeomancyApp;

namespace GeomancyUnitTesting
{
    [TestClass]
    public class PerfectionDetailCopyTests
    {
        [TestMethod]
        public void CompanyAspect_TitleLeadsWithAspectNotCompany()
        {
            var title = PerfectionDetailCopy.ResolveModeTitle(
                mode: "Company",
                baseMode: "Aspect",
                companyType: "Simple",
                aspectType: "Opposition");

            Assert.AreEqual("Opposition · via Company Simple", title);
        }

        [TestMethod]
        public void CompanyAspect_PrimaryGlossaryIsAspectNotCompanyOverview()
        {
            var glossary = PerfectionDetailCopy.ResolvePrimaryGlossary(
                mode: "Company",
                baseMode: "Aspect",
                aspectType: "Opposition",
                aspectDirection: "Opposition",
                aspectGlossary: (type, dir) =>
                    type == "Opposition"
                        ? "Opposition — a six-house separation read as confrontation or denial."
                        : string.Empty,
                modeGlossary: _ => "Company overview should not win.");

            StringAssert.StartsWith(glossary, "Opposition");
            Assert.IsFalse(glossary.Contains("Company overview"));
        }

        [TestMethod]
        public void AspectListLabel_CompanyMediatedShowsViaCompany()
        {
            Assert.AreEqual("Sq · via Co. Simple",
                PerfectionDetailCopy.AspectListLabel("Square", true, "Simple"));
            Assert.AreEqual("Sq",
                PerfectionDetailCopy.AspectListLabel("Square", false, "None"));
        }

        [TestMethod]
        public void CompanyHoverText_PrefersTagline()
        {
            var hover = PerfectionDetailCopy.CompanyHoverText(
                "Simple — the companion is the same figure as its partner.",
                "Longer mechanism that should not be used.");

            Assert.AreEqual("Simple — the companion is the same figure as its partner.", hover);
        }

        [TestMethod]
        public void CompanyHoverText_PrefixesHousePair()
        {
            var hover = PerfectionDetailCopy.CompanyHoverText(
                "Compound — companions are structural opposites yoked together.",
                string.Empty,
                7,
                8);

            StringAssert.StartsWith(hover, "H7 with H8 — ");
            StringAssert.Contains(hover, "Compound");
        }

        [TestMethod]
        public void CompanyPairLabel_UsesFormatCompanyShort()
        {
            Assert.AreEqual("Co. Comp.", PerfectionDetailCopy.FormatCompanyShort("Compound"));
            Assert.AreEqual("Co. Demi", PerfectionDetailCopy.FormatCompanyShort("DemiSimple"));
        }

        [TestMethod]
        public void CompanyFormationReason_FromDescriptionParen()
        {
            Assert.AreEqual("opposite figures",
                PerfectionDetailCopy.CompanyFormationReason(
                    "Compound",
                    "Company Compound (opposite figures) — complementary partners."));
            Assert.AreEqual("same figure",
                PerfectionDetailCopy.CompanyFormationReason("Simple", string.Empty));
        }

        [TestMethod]
        public void DemiSimple_MechanismClause_PairedUnderPlanet()
        {
            Assert.AreEqual("Jupiter",
                PerfectionDetailCopy.ExtractCompanyBondPlanet(
                    "Company Demi-Simple (Caput Draconis with Jupiter) — reading."));
            Assert.AreEqual("paired under Jupiter",
                PerfectionDetailCopy.CompanyMechanismFormationClause(
                    "DemiSimple",
                    "Company Demi-Simple (Caput Draconis with Jupiter) — reading."));
            Assert.AreEqual("paired under Mercury",
                PerfectionDetailCopy.CompanyMechanismFormationClause(
                    "DemiSimple",
                    "Company Demi-Simple (same planet: Mercury) — reading."));
        }

        [TestMethod]
        public void DemiSimple_ThisChart_NamesHousesRoleAndPlanet()
        {
            var text = PerfectionDetailCopy.CompanyThisChartSentence(
                5, "Laetitia", "Quesited",
                6, "Caput Draconis",
                "DemiSimple",
                "Company Demi-Simple (Caput Draconis with Jupiter) — reading.");

            StringAssert.Contains(text, "House 5");
            StringAssert.Contains(text, "Laetitia");
            StringAssert.Contains(text, "quesited");
            StringAssert.Contains(text, "House 6");
            StringAssert.Contains(text, "Caput Draconis");
            StringAssert.Contains(text, "paired under Jupiter");
        }

        [TestMethod]
        public void Compound_ThisChart_NamesOppositePairWorking()
        {
            var text = PerfectionDetailCopy.CompanyThisChartSentence(
                1, "Amissio", "Querent",
                2, "Acquisitio",
                "Compound",
                "Company Compound (opposite figures) — reading.");

            StringAssert.Contains(text, "House 1");
            StringAssert.Contains(text, "Amissio");
            StringAssert.Contains(text, "querent");
            StringAssert.Contains(text, "House 2");
            StringAssert.Contains(text, "Acquisitio");
            StringAssert.Contains(text, "compound opposite figures");
            StringAssert.Contains(text, "Amissio ↔ Acquisitio");
        }

        [TestMethod]
        public void Compound_MatchOppositePair_IncludesNodes()
        {
            var match = PerfectionDetailCopy.MatchCompoundOppositePair("Caput Draconis", "Cauda Draconis");
            Assert.IsNotNull(match);
            Assert.AreEqual("Cauda Draconis", match.Value.Left);
            Assert.AreEqual("Caput Draconis", match.Value.Right);
        }

        [TestMethod]
        public void FormatCompanyPairLabel_IncludesFigures()
        {
            Assert.AreEqual("H7 · Caput Draconis ↔ H8 · Cauda Draconis",
                PerfectionDetailCopy.FormatCompanyPairLabel(7, 8, "Caput Draconis", "Cauda Draconis"));
        }

        [TestMethod]
        public void AspectGlossary_Opposition_UsesGreerFiveHouses()
        {
            var line = PerfectionDetailCopy.AspectGlossary("Opposition", "Opposition");
            StringAssert.Contains(line, "five houses");
            StringAssert.Contains(line, "not dexter");
        }

        [TestMethod]
        public void AspectGeometryTable_HasFourGreerRows()
        {
            Assert.AreEqual(4, PerfectionDetailCopy.AspectGeometryTable.Count);
            Assert.AreEqual(1, PerfectionDetailCopy.MatchAspectGeometryRow("Sextile")?.HousesBetween);
            Assert.AreEqual(2, PerfectionDetailCopy.MatchAspectGeometryRow("Square")?.HousesBetween);
            Assert.AreEqual(3, PerfectionDetailCopy.MatchAspectGeometryRow("Trine")?.HousesBetween);
            Assert.AreEqual(5, PerfectionDetailCopy.MatchAspectGeometryRow("Opposition")?.HousesBetween);
        }

        [TestMethod]
        public void AspectGeometryRow_ActiveMatcher()
        {
            Assert.IsTrue(PerfectionDetailCopy.IsAspectGeometryRowActive("Sextile", "Sextile"));
            Assert.IsTrue(PerfectionDetailCopy.IsAspectGeometryRowActive("Trine", "Trine"));
            Assert.IsTrue(PerfectionDetailCopy.IsAspectGeometryRowActive("Square", "Square"));
            Assert.IsTrue(PerfectionDetailCopy.IsAspectGeometryRowActive("Opposition", "Opposition"));
            Assert.IsFalse(PerfectionDetailCopy.IsAspectGeometryRowActive("Sextile", "Trine"));
        }

        [TestMethod]
        public void AspectThisChart_NamesHousesFiguresAndDexterSextile()
        {
            var text = PerfectionDetailCopy.AspectThisChart(
                fromHouse: 1,
                fromFigure: "Laetitia",
                toHouse: 3,
                toFigure: "Amissio",
                aspectType: "Sextile",
                direction: "Dexter",
                querentHouse: 1,
                quesitedHouse: 5);

            StringAssert.Contains(text, "House 1");
            StringAssert.Contains(text, "Laetitia");
            StringAssert.Contains(text, "House 3");
            StringAssert.Contains(text, "Amissio");
            StringAssert.Contains(text, "Dexter Sextile");
            StringAssert.Contains(text, "querent");
        }
    }
}
