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
    }
}
