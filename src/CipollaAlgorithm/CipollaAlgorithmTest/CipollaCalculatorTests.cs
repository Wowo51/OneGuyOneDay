using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CipollaAlgorithm;

namespace CipollaAlgorithmTest
{
    [TestClass]
    public class CipollaCalculatorTests
    {
        [TestMethod]
        public void TestComputeSquareRoot_Zero()
        {
            int p = 7;
            int? result = CipollaCalculator.ComputeSquareRoot(0, p);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Value);
        }

        [TestMethod]
        public void TestComputeSquareRoot_NonResidue()
        {
            int p = 7;
            int? result = CipollaCalculator.ComputeSquareRoot(3, p);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestComputeSquareRoot_Residue()
        {
            int p = 7;
            int n = 2;
            int? result = CipollaCalculator.ComputeSquareRoot(n, p);
            Assert.IsNotNull(result);
            int r = result.Value;
            int square = (r * r) % p;
            if (square < 0)
            {
                square += p;
            }

            Assert.AreEqual(n % p, square);
        }

        [TestMethod]
        public void TestComputeSquareRoot_AllResidues()
        {
            int p = 29;
            for (int n = 0; n < p; n++)
            {
                int? result = CipollaCalculator.ComputeSquareRoot(n, p);
                if (result != null)
                {
                    int r = result.Value;
                    int square = (r * r) % p;
                    if (square < 0)
                    {
                        square += p;
                    }

                    Assert.AreEqual(n % p, square);
                }
                else
                {
                    bool hasSolution = false;
                    for (int i = 0; i < p; i++)
                    {
                        int square = (i * i) % p;
                        if (square < 0)
                        {
                            square += p;
                        }

                        if (square == n % p)
                        {
                            hasSolution = true;
                            break;
                        }
                    }

                    Assert.IsFalse(hasSolution, string.Format("Expected a solution for n={0} but found null.", n));
                }
            }
        }

        [TestMethod]
        public void TestSyntheticLargeData()
        {
            int p = 101;
            System.Random random = new System.Random();
            for (int i = 0; i < 100; i++)
            {
                int n = random.Next(p);
                int? result = CipollaCalculator.ComputeSquareRoot(n, p);
                if (result != null)
                {
                    int r = result.Value;
                    int square = (r * r) % p;
                    if (square < 0)
                    {
                        square += p;
                    }

                    Assert.AreEqual(n % p, square, string.Format("n={0}, r={1} failed.", n, r));
                }
                else
                {
                    bool hasSolution = false;
                    for (int j = 0; j < p; j++)
                    {
                        int square = (j * j) % p;
                        if (square < 0)
                        {
                            square += p;
                        }

                        if (square == n % p)
                        {
                            hasSolution = true;
                            break;
                        }
                    }

                    Assert.IsFalse(hasSolution, string.Format("n={0} has a solution but returned null.", n));
                }
            }
        }
    }
}