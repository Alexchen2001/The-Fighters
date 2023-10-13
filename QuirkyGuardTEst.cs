using P5;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace P5.Tests
{

    [TestClass]
    public class QuirkyGTest
    {
        [TestMethod]
        public void QuirkyGuardBlockingWithdifferentShields()
        {
           
            int[] mockShields = new int[] { 2, 3, 7, 8};
            IShieldProvider shieldProv = new shieldProvider(mockShields);
            var quirkyGuard = new quirkyGuard(shieldProv);
            int shieldIndex = 0;

            
            bool result = quirkyGuard.Block(shieldIndex);
            bool result1 = quirkyGuard.Block(shieldIndex);
            bool result2 = quirkyGuard.Block(shieldIndex);

            // Since it is quirky if it returns true for all, then it should be correct, becuase
            // it does not land on the same shield.
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);

        }
    }

}

