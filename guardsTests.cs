using P5;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace P5.Tests
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void guardSuccessDecrement()
        {
            // Arrange
            int[] mockShields = new int[] { 2, 3 };
            IShieldProvider shieldProv = new shieldProvider(mockShields);
            var guard = new Guards(shieldProv);
            int shieldIndex = 0;

            // Act
            bool result = guard.Block(shieldIndex);
            bool result1 = guard.Block(shieldIndex);
            bool result2 = guard.Block(shieldIndex);

            //it decrements correctly, where if the shields inv is half not viable, then it is going to return false
            // otherwise it will output true as demonstrated.
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsFalse(result2);

        }
    }

}

