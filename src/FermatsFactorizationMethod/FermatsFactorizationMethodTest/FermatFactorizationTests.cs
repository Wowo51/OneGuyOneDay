using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FermatsFactorizationMethod;

namespace FermatsFactorizationMethodTest
{
    [TestClass]
    public class FermatFactorizationTests
    {
        [TestMethod]
        public void TestFactorizeForOne()
        {
            (long factor1, long factor2) = FermatFactorization.Factorize(1);
            Assert.AreEqual(1L, factor1, "Factor1 for 1 should be 1");
            Assert.AreEqual(1L, factor2, "Factor2 for 1 should be 1");
        }

        [TestMethod]
        public void TestFactorizeForPrime()
        {
            long n = 3;
            (long factor1, long factor2) = FermatFactorization.Factorize(n);
            Assert.AreEqual(1L, factor1, "For a prime number, the smaller factor should be 1");
            Assert.AreEqual(n, factor2, "For a prime number, the larger factor should be the number itself");
        }

        [TestMethod]
        public void TestFactorizeForComposite()
        {
            long n = 15;
            (long factor1, long factor2) = FermatFactorization.Factorize(n);
            Assert.AreEqual(3L, factor1, "15 should factor to 3 and 5");
            Assert.AreEqual(5L, factor2, "15 should factor to 3 and 5");
            n = 21;
            (factor1, factor2) = FermatFactorization.Factorize(n);
            Assert.AreEqual(3L, factor1, "21 should factor to 3 and 7");
            Assert.AreEqual(7L, factor2, "21 should factor to 3 and 7");
            n = 9;
            (factor1, factor2) = FermatFactorization.Factorize(n);
            Assert.AreEqual(3L, factor1, "9 should factor to 3 and 3");
            Assert.AreEqual(3L, factor2, "9 should factor to 3 and 3");
        }

        [TestMethod]
        public void TestFactorizeRandomOddNumbers()
        {
            Random random = new Random(42);
            for (int i = 0; i < 50; i++)
            {
                long randomValue = random.Next(1, 5000);
                long n = randomValue * 2 + 1;
                (long factor1, long factor2) = FermatFactorization.Factorize(n);
                Assert.AreEqual(n, factor1 * factor2, "Product of factors must equal the original number");
                Assert.IsTrue(factor1 <= factor2, "The first factor should be less than or equal to the second factor");
            }
        }
    }
}