using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using EspressoHeuristicMinimizer;

namespace EspressoHeuristicMinimizerTest
{
    [TestClass]
    public class EspressoMinimizerTests
    {
        [TestMethod]
        public void BooleanCube_Cover_Test()
        {
            BooleanCube cube1 = new BooleanCube("1010");
            BooleanCube cube2 = new BooleanCube("1-10");
            Assert.IsTrue(cube2.Covers(cube1), "Cube2 should cover Cube1");
        }

        [TestMethod]
        public void BooleanCube_Merge_Test()
        {
            BooleanCube cube1 = new BooleanCube("1010");
            BooleanCube cube2 = new BooleanCube("1110");
            BooleanCube? mergedCube = cube1.Merge(cube2);
            Assert.IsNotNull(mergedCube, "Merge should return a non-null cube");
            if (mergedCube != null)
            {
                Assert.AreEqual("1-10", mergedCube.ToString(), "Merged cube should have term '1-10'");
            }
        }

        [TestMethod]
        public void EspressoMinimizer_GetPrimeImplicants_Test()
        {
            List<BooleanCube> cubes = new List<BooleanCube>
            {
                new BooleanCube("1010"),
                new BooleanCube("1110"),
                new BooleanCube("1011"),
                new BooleanCube("0010")
            };
            EspressoMinimizer minimizer = new EspressoMinimizer();
            List<BooleanCube> primes = minimizer.GetPrimeImplicants(cubes);
            foreach (BooleanCube cube in cubes)
            {
                bool isCovered = primes.Any(prime => prime.Covers(cube));
                Assert.IsTrue(isCovered, $"Cube {cube} should be covered by a prime implicant");
            }
        }

        [TestMethod]
        public void EspressoMinimizer_Minimize_Test()
        {
            List<BooleanCube> cubes = new List<BooleanCube>
            {
                new BooleanCube("1010"),
                new BooleanCube("1110"),
                new BooleanCube("1011"),
                new BooleanCube("0010")
            };
            EspressoMinimizer minimizer = new EspressoMinimizer();
            List<BooleanCube> minimized = minimizer.Minimize(cubes);
            foreach (BooleanCube cube in cubes)
            {
                bool isCovered = minimized.Any(min => min.Covers(cube));
                Assert.IsTrue(isCovered, $"Cube {cube} should be covered by the minimized set");
            }
        }

        [TestMethod]
        public void EspressoMinimizer_Minimize_EmptyInput_Test()
        {
            List<BooleanCube> cubes = new List<BooleanCube>();
            EspressoMinimizer minimizer = new EspressoMinimizer();
            List<BooleanCube> minimized = minimizer.Minimize(cubes);
            Assert.AreEqual(0, minimized.Count, "Minimize should return an empty list for empty input");
        }

        [TestMethod]
        public void EspressoMinimizer_Minimize_NullInput_Test()
        {
            EspressoMinimizer minimizer = new EspressoMinimizer();
            List<BooleanCube> minimized = minimizer.Minimize(null);
            Assert.AreEqual(0, minimized.Count, "Minimize should return an empty list for null input");
        }

        [TestMethod]
        public void EspressoMinimizer_LargeDataSet_Test()
        {
            List<BooleanCube> cubes = new List<BooleanCube>();
            int length = 8;
            for (int i = 0; i < 100; i++)
            {
                string term = "";
                for (int j = 0; j < length; j++)
                {
                    term += ((i + j) % 2 == 0) ? "0" : "1";
                }

                cubes.Add(new BooleanCube(term));
            }

            EspressoMinimizer minimizer = new EspressoMinimizer();
            List<BooleanCube> minimized = minimizer.Minimize(cubes);
            foreach (BooleanCube cube in cubes)
            {
                bool isCovered = minimized.Any(min => min.Covers(cube));
                Assert.IsTrue(isCovered, $"Cube {cube} should be covered in large dataset minimization");
            }
        }
    }
}