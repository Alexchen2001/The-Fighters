using P5;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace P5.Tests
{

    [TestClass]
    public class skipGuardTerst
    {
        [TestMethod]
        public void skipGuardSuccessDecrement()
        {
            // Arrange
            int[] mockShields = new int[] { 2, 3 ,7,8};
            IShieldProvider shieldProv = new shieldProvider(mockShields);
            var skipGuard = new skipGuards(shieldProv);
            int shieldIndex = 0;

            // Act
            bool result = skipGuard.Block(shieldIndex);
            bool result1 = skipGuard.Block(shieldIndex);
            bool result2 = skipGuard.Block(shieldIndex + 1);
            bool result3 = skipGuard.Block(shieldIndex + 1);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result2);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void skipGuardFailtoBlock()
        {
            // Arrange
            int[] mockShields = new int[] { 2, 3, 7, 8 };
            IShieldProvider shieldProv = new shieldProvider(mockShields);
            var skipGuard = new skipGuards(shieldProv);
            int shieldIndex = 0;

            // Act
            bool result = skipGuard.Block(shieldIndex);
            bool result1 = skipGuard.Block(shieldIndex);
            bool result2 = skipGuard.Block(shieldIndex + 1);
            bool result3 = skipGuard.Block(shieldIndex + 1);
            bool result4 = skipGuard.Block(shieldIndex + 3);

            // if the number is offseting to a non-existent shield then it is false,
            // where it demonstrate that it is offsetting.
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result2);
            Assert.IsTrue(result2);
            Assert.IsFalse(result4);


        }
    }

}

