using P5;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace P5.Tests
{

    [TestClass]
    public class turretSkipGuardTest
    {
        [TestMethod]
        public void guardSuccessBlok()
        {
            // Arrange
            int[] mockShields = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            int[] mockArtillery = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            IShieldProvider shieldProv = new shieldProvider(mockShields);
            IArtilleryProvider artilleryProv = new ArtilleryProvider(mockArtillery);
            var turretSkipGuard = new turretSkipGuard(5, 5, 100, 10, artilleryProv, shieldProv);
            int shieldIndex = 0;

            // Act
            bool result = turretSkipGuard.Block(shieldIndex);
            bool result1 = turretSkipGuard.Block(shieldIndex);
            bool result2 = turretSkipGuard.Block(shieldIndex);

            //uses the guard block, so should return all true.
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);

        }

        [TestMethod]
        public void turretSkipGuardRetaliationSuccess()
        {
            // Arrange
            int[] mockShields = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            int[] mockArtillery = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            IShieldProvider shieldProv = new shieldProvider(mockShields);
            IArtilleryProvider artilleryProv = new ArtilleryProvider(mockArtillery);
            var turretSkipGuard = new turretSkipGuard(5, 5, 100, 10, artilleryProv, shieldProv);
            int shieldIndex = 0;

            // Act
            bool result = turretSkipGuard.feignedTerror(2, 2, shieldIndex+1);
            bool result1 = turretSkipGuard.feignedTerror(4, 4, shieldIndex);
            bool result2 = turretSkipGuard.feignedTerror(7, 7, shieldIndex + 6);

            // it returns true for eliminateble that satify target and blocking, if not able to satify one it would be flase.
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsFalse(result2);

        }
    }

}

