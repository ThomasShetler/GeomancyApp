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
    }
}
