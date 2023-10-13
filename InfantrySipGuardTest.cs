using P5;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace P5.Tests
{

    [TestClass]
    public class infantrySkipGuardTest
    {
        [TestMethod]
        public void guardSuccessBlok()
        {
            // Arrange
            int[] mockShields = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            int[] mockArtillery = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            IShieldProvider shieldProv = new shieldProvider(mockShields);
            IArtilleryProvider artilleryProv = new ArtilleryProvider(mockArtillery);
            var infantrySkipGuard = new infantrySkipGuard(5, 5, 100, 10, artilleryProv, shieldProv);
            int shieldIndex = 0;

            // Ac
            bool result = infantrySkipGuard.Block(shieldIndex);
            bool result1 = infantrySkipGuard.Block(shieldIndex);
            bool result2 = infantrySkipGuard.Block(shieldIndex);

            //uses the guard block, so should return all true.
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);

        }

        [TestMethod]
        public void InfantryQuirkyGuardstriesSuccess()
        {
            // Arrange
            int[] mockShields = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            int[] mockArtillery = new int[] { 5, 5, 5, 5, 5, 5, 5 };
            IShieldProvider shieldProv = new shieldProvider(mockShields);
            IArtilleryProvider artilleryProv = new ArtilleryProvider(mockArtillery);
            var infantrySkipGuard = new infantrySkipGuard(5, 5, 100, 10, artilleryProv, shieldProv);
            int shieldIndex = 0;

            // Act
            bool result = infantrySkipGuard.dodgeAtk(1, 1, 2, shieldIndex);
            bool result1 = infantrySkipGuard.dodgeAtk(1, 1, 2, shieldIndex);
            bool result2 = infantrySkipGuard.dodgeAtk(23, 23, 200, shieldIndex + 7);

            // it returns true for eliminateble that satify target and blocking, if not able to satify one it would be flase.
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsFalse(result2);

        }
    }

}

