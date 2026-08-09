using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeomancyApp;

namespace GeomancyUnitTesting
{
    [TestClass]
    public class GeomanticAspectGeometryTests
    {
        [TestMethod]
        public void Table6_1_House1_MatchesGreerRow()
        {
            // Greer Table 6-1 row for house 1: Dex Sx 11, Dex Sq 10, Dex Tr 9, Opp 7, Sin Tr 5, Sin Sq 4, Sin Sx 3
            AssertPair(1, 11, AspectType.Sextile, "Dexter");
            AssertPair(1, 10, AspectType.Square, "Dexter");
            AssertPair(1, 9, AspectType.Trine, "Dexter");
            AssertPair(1, 7, AspectType.Opposition, "Opposition");
            AssertPair(1, 5, AspectType.Trine, "Sinister");
            AssertPair(1, 4, AspectType.Square, "Sinister");
            AssertPair(1, 3, AspectType.Sextile, "Sinister");
        }

        [TestMethod]
        public void HousesBetween_MatchesGreerWording()
        {
            Assert.AreEqual(1, GeomanticAspects.HousesBetween(AspectType.Sextile));
            Assert.AreEqual(2, GeomanticAspects.HousesBetween(AspectType.Square));
            Assert.AreEqual(3, GeomanticAspects.HousesBetween(AspectType.Trine));
            Assert.AreEqual(5, GeomanticAspects.HousesBetween(AspectType.Opposition));
        }

        [TestMethod]
        public void IntermediateHouses_SinisterSquare_HasTwoBetween()
        {
            // H1 sinister square H4 — houses between: 2, 3
            var mids = GeomanticAspects.IntermediateHouses(1, 4);
            CollectionAssert.AreEqual(new[] { 2, 3 }, mids.ToArray());
            Assert.AreEqual(2, mids.Count);
        }

        [TestMethod]
        public void IntermediateHouses_DexterSextile_HasOneBetween()
        {
            // H1 dexter sextile H11 — between: 12
            var mids = GeomanticAspects.IntermediateHouses(1, 11);
            CollectionAssert.AreEqual(new[] { 12 }, mids.ToArray());
        }

        [TestMethod]
        public void IntermediateHouses_Opposition_HasFiveBetween()
        {
            var mids = GeomanticAspects.IntermediateHouses(1, 7);
            Assert.AreEqual(5, mids.Count);
            CollectionAssert.AreEqual(new[] { 2, 3, 4, 5, 6 }, mids.ToArray());
        }

        [TestMethod]
        public void DescribeAspect_Opposition_MentionsFiveHousesAndNotDexter()
        {
            var text = GeomanticAspects.DescribeAspect(1, 7, AspectType.Opposition, "Opposition");
            StringAssert.Contains(text, "five houses");
            StringAssert.Contains(text, "not dexter");
        }

        [TestMethod]
        public void ShortLabel_DexterSquare()
        {
            Assert.AreEqual("Dex Sq", GeomanticAspects.ShortLabel(AspectType.Square, "Dexter"));
            Assert.AreEqual("Opp", GeomanticAspects.ShortLabel(AspectType.Opposition, "Opposition"));
        }

        [TestMethod]
        public void GetAspect_AgreesWithGetAspectWithDirection_OnType()
        {
            for (int from = 1; from <= 12; from++)
            for (int to = 1; to <= 12; to++)
            {
                if (from == to) continue;
                var table = GeomanticAspects.GetAspect(from, to);
                var (dist, _) = GeomanticAspects.GetAspectWithDirection(from, to);
                Assert.AreEqual(table, dist, $"Mismatch for H{from}→H{to}");
            }
        }

        private static void AssertPair(int from, int to, AspectType expected, string expectedDir)
        {
            Assert.AreEqual(expected, GeomanticAspects.GetAspect(from, to));
            var (asp, dir) = GeomanticAspects.GetAspectWithDirection(from, to);
            Assert.AreEqual(expected, asp);
            Assert.AreEqual(expectedDir, dir);
        }
    }
}
