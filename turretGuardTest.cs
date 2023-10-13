using P5;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace P5.Tests
{

    [TestClass]
    public class turretGuardTest
    {
        [TestMethod]
        public void guardSuccessBlok()
        {
            // Arrange
            int[] mockShields = new int[] { 5,5,5,5,5,5,5 };
            int[] mockArtillery = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            IShieldProvider shieldProv = new shieldProvider(mockShields);
            IArtilleryProvider artilleryProv = new ArtilleryProvider(mockArtillery);
            var turretGuard = new turretGuard(5,5,100,10,artilleryProv,shieldProv);
            int shieldIndex = 0;

            // Act
            bool result = turretGuard.Block(shieldIndex);
            bool result1 =turretGuard.Block(shieldIndex);
            bool result2 = turretGuard.Block(shieldIndex);

            //uses the guard block, so should return all true.
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);

        }

        [TestMethod]
        public void turretGuardRetaliationSuccess()
        {
            // Arrange
            int[] mockShields = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            int[] mockArtillery = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            IShieldProvider shieldProv = new shieldProvider(mockShields);
            IArtilleryProvider artilleryProv = new ArtilleryProvider(mockArtillery);
            var turretGuard = new turretGuard(5, 5, 100, 10, artilleryProv, shieldProv);
            int shieldIndex = 0;

            // Act
            bool result = turretGuard.retaliation(2,2,1,1,shieldIndex);
            bool result1 = turretGuard.retaliation(4,4,4,1,shieldIndex);
            bool result2 = turretGuard.retaliation(7, 7, 120, 40, shieldIndex + 3);

            // it returns true for eliminateble that satify target and blocking, if not able to satify one it would be flase.
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsFalse(result2);

        }
    }

}

