using P5;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace P5.Tests
{

    [TestClass]
    public class turretQuirkyGuardTest
    {
        [TestMethod]
        public void guardSuccessBlok()
        {
            // Arrange
            int[] mockShields = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            int[] mockArtillery = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            IShieldProvider shieldProv = new shieldProvider(mockShields);
            IArtilleryProvider artilleryProv = new ArtilleryProvider(mockArtillery);
            var turretQuirkyGuard = new turretQuirkyGuard(5, 5, 100, 10, artilleryProv, shieldProv);
            int shieldIndex = 0;

            // Act
            bool result = turretQuirkyGuard.Block(shieldIndex);
            bool result1 = turretQuirkyGuard.Block(shieldIndex);
            bool result2 = turretQuirkyGuard.Block(shieldIndex);

            //uses the Quirkyguard block, so should return all true.
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);

        }

        [TestMethod]
        public void turretQuirkyGuardRetaliationSuccess()
        {
            // Arrange
            int[] mockShields = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            int[] mockArtillery = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            IShieldProvider shieldProv = new shieldProvider(mockShields);
            IArtilleryProvider artilleryProv = new ArtilleryProvider(mockArtillery);
            var turretQuirkyGuard = new turretQuirkyGuard(5, 5, 100, 10, artilleryProv, shieldProv);
            int shieldIndex = 0;

            // Act
            bool result = turretQuirkyGuard.selfDestruction(2, 2, 1, shieldIndex);
            bool result1 = turretQuirkyGuard.selfDestruction(4, 4, 4, shieldIndex);
            bool result2 = turretQuirkyGuard.selfDestruction(23, 23, 120, shieldIndex + 3);

            // it returns true for eliminateble that satify target and blocking, if not able to satify one it would be flase.
            Assert.IsTrue(result);
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);

        }
    }

}

