using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchreierSimsAlgorithm;
using System;
using System.Collections.Generic;

namespace SchreierSimsAlgorithmTest
{
    [TestClass]
    public class PermutationTests
    {
        [TestMethod]
        public void TestIdentityPermutation()
        {
            int size = 5;
            Permutation identity = Permutation.Identity(size);
            for (int i = 0; i < size; i++)
            {
                Assert.AreEqual(i, identity.Apply(i));
            }

            Assert.IsTrue(identity.IsIdentity());
        }

        [TestMethod]
        public void TestPermutationComposeWithIdentity()
        {
            int[] mapping = new int[]
            {
                2,
                0,
                1
            };
            Permutation p = new Permutation(mapping);
            Permutation identity = Permutation.Identity(mapping.Length);
            Permutation compose1 = p.Compose(identity);
            Permutation compose2 = identity.Compose(p);
            Assert.IsTrue(p.Equals(compose1));
            Assert.IsTrue(p.Equals(compose2));
        }

        [TestMethod]
        public void TestPermutationInverse()
        {
            int[] mapping = new int[]
            {
                2,
                0,
                1,
                4,
                3
            };
            Permutation p = new Permutation(mapping);
            Permutation inv = p.Inverse();
            Permutation identity = Permutation.Identity(mapping.Length);
            Permutation result = p.Compose(inv);
            Assert.IsTrue(result.IsIdentity());
            result = inv.Compose(p);
            Assert.IsTrue(result.IsIdentity());
        }

        [TestMethod]
        public void TestPermutationEqualsAndToString()
        {
            int[] mapping1 = new int[]
            {
                1,
                0,
                2
            };
            int[] mapping2 = new int[]
            {
                1,
                0,
                2
            };
            Permutation p1 = new Permutation(mapping1);
            Permutation p2 = new Permutation(mapping2);
            Assert.IsTrue(p1.Equals(p2));
            Assert.AreEqual(p1.GetHashCode(), p2.GetHashCode());
            string expected = "(1 0 2)";
            Assert.AreEqual(expected, p1.ToString());
        }

        [TestMethod]
        public void TestApplyInvalidIndex()
        {
            int[] mapping = new int[]
            {
                2,
                0,
                1
            };
            Permutation p = new Permutation(mapping);
            int negativeResult = p.Apply(-1);
            int outOfRangeResult = p.Apply(mapping.Length);
            Assert.AreEqual(-1, negativeResult, "Apply should return the index for negative inputs.");
            Assert.AreEqual(mapping.Length, outOfRangeResult, "Apply should return the index for out-of-range inputs.");
        }

        [TestMethod]
        public void TestNullMapping()
        {
            Permutation p = new Permutation(null);
            Assert.AreEqual(0, p.Length);
            Assert.IsTrue(p.IsIdentity());
        }
    }

    [TestClass]
    public class SchreierSimsTests
    {
        [TestMethod]
        public void TestComputeBSGST_TrivialGroup()
        {
            int domain = 4;
            Permutation identity = Permutation.Identity(domain);
            List<Permutation> generators = new List<Permutation>();
            generators.Add(identity);
            (List<int> basePoints, List<Permutation> strongGenerators) = SchreierSims.ComputeBSGS(generators, domain);
            Assert.AreEqual(0, basePoints.Count);
            Assert.AreEqual(0, strongGenerators.Count);
        }

        [TestMethod]
        public void TestComputeBSGST_S3()
        {
            int domain = 3;
            int[] mapping1 = new int[]
            {
                1,
                0,
                2
            };
            int[] mapping2 = new int[]
            {
                0,
                2,
                1
            };
            Permutation g1 = new Permutation(mapping1);
            Permutation g2 = new Permutation(mapping2);
            List<Permutation> generators = new List<Permutation>();
            generators.Add(g1);
            generators.Add(g2);
            (List<int> basePoints, List<Permutation> strongGenerators) = SchreierSims.ComputeBSGS(generators, domain);
            Assert.AreEqual(2, basePoints.Count);
            Assert.AreEqual(0, basePoints[0]);
            Assert.AreEqual(1, basePoints[1]);
            Assert.AreEqual(1, strongGenerators.Count);
            Assert.IsTrue(g2.Equals(strongGenerators[0]));
        }

        [TestMethod]
        public void TestComputeBSGST_RandomGenerators()
        {
            int domain = 4;
            int seed = 12345;
            Random random = new Random(seed);
            List<Permutation> generators = new List<Permutation>();
            for (int j = 0; j < 5; j++)
            {
                int[] array = new int[domain];
                for (int i = 0; i < domain; i++)
                {
                    array[i] = i;
                }

                for (int i = domain - 1; i > 0; i--)
                {
                    int k = random.Next(i + 1);
                    int temp = array[i];
                    array[i] = array[k];
                    array[k] = temp;
                }

                Permutation p = new Permutation(array);
                generators.Add(p);
            }

            (List<int> basePoints, List<Permutation> strongGenerators) = SchreierSims.ComputeBSGS(generators, domain);
            Assert.IsNotNull(basePoints);
            Assert.IsNotNull(strongGenerators);
            Assert.IsTrue(strongGenerators.Count <= generators.Count);
            for (int i = 0; i < strongGenerators.Count; i++)
            {
                Assert.IsFalse(strongGenerators[i].IsIdentity());
            }
        }

        [TestMethod]
        public void TestComputeBSGS_NullGenerators()
        {
            int domain = 5;
            (List<int> basePoints, List<Permutation> strongGenerators) = SchreierSims.ComputeBSGS(null, domain);
            Assert.AreEqual(0, basePoints.Count, "For null generators, basePoints should be empty.");
            Assert.AreEqual(0, strongGenerators.Count, "For null generators, strongGenerators should be empty.");
        }

        [TestMethod]
        public void TestComputeBSGS_LargeRandomGenerators()
        {
            int domain = 10;
            int seed = 54321;
            Random random = new Random(seed);
            List<Permutation> generators = new List<Permutation>();
            for (int j = 0; j < 20; j++)
            {
                int[] array = new int[domain];
                for (int i = 0; i < domain; i++)
                {
                    array[i] = i;
                }

                for (int i = domain - 1; i > 0; i--)
                {
                    int k = random.Next(i + 1);
                    int temp = array[i];
                    array[i] = array[k];
                    array[k] = temp;
                }

                Permutation p = new Permutation(array);
                generators.Add(p);
            }

            (List<int> basePoints, List<Permutation> strongGenerators) = SchreierSims.ComputeBSGS(generators, domain);
            Assert.IsNotNull(basePoints);
            Assert.IsNotNull(strongGenerators);
            foreach (Permutation perm in strongGenerators)
            {
                Assert.IsFalse(perm.IsIdentity(), "Strong generator should not be identity.");
            }
        }
    }
}