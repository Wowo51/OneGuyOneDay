using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GramSchmidtProcess;

namespace GramSchmidtProcessTest
{
    [TestClass]
    public class GramSchmidtProcessUnitTests
    {
        [TestMethod]
        public void TestOrthogonalize_NullInput()
        {
            List<Vector> result = Orthogonalizer.Orthogonalize(null !);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestOrthogonalize_NullVectorsInList()
        {
            List<Vector> vectors = new List<Vector>();
            vectors.Add(null !);
            vectors.Add(new Vector(new double[] { 1, 1 }));
            List<Vector> result = Orthogonalizer.Orthogonalize(vectors);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestOrthogonalize_SimpleCase()
        {
            List<Vector> vectors = new List<Vector>()
            {
                new Vector(new double[] { 1, 0 }),
                new Vector(new double[] { 0, 1 })
            };
            List<Vector> orthogonal = Orthogonalizer.Orthogonalize(vectors);
            Assert.AreEqual(2, orthogonal.Count);
            double dot = orthogonal[0].Dot(orthogonal[1]);
            Assert.IsTrue(Math.Abs(dot) < 1e-10, "Orthogonal vectors are not orthogonal");
        }

        [TestMethod]
        public void TestOrthogonalize_RepeatedVectors()
        {
            List<Vector> vectors = new List<Vector>()
            {
                new Vector(new double[] { 1, 1 }),
                new Vector(new double[] { 1, 1 })
            };
            List<Vector> orthogonal = Orthogonalizer.Orthogonalize(vectors);
            Assert.AreEqual(1, orthogonal.Count);
        }

        [TestMethod]
        public void TestOrthonormalize()
        {
            List<Vector> vectors = new List<Vector>()
            {
                new Vector(new double[] { 3, 1 }),
                new Vector(new double[] { 2, 2 })
            };
            List<Vector> orthonormal = Orthogonalizer.Orthonormalize(vectors);
            Assert.IsTrue(orthonormal.Count > 0);
            foreach (Vector v in orthonormal)
            {
                double norm = v.Norm();
                Assert.IsTrue(Math.Abs(norm - 1.0) < 1e-10, "Vector not normalized");
            }

            for (int i = 0; i < orthonormal.Count; i++)
            {
                for (int j = i + 1; j < orthonormal.Count; j++)
                {
                    double dot = orthonormal[i].Dot(orthonormal[j]);
                    Assert.IsTrue(Math.Abs(dot) < 1e-10, "Vectors are not orthogonal");
                }
            }
        }

        [TestMethod]
        public void TestVectorOperations()
        {
            Vector v1 = new Vector(new double[] { 1, 2, 3 });
            Vector v2 = new Vector(new double[] { 4, 5, 6 });
            double dot = v1.Dot(v2);
            Assert.AreEqual(32, dot, 1e-10);
            Vector add = v1.Add(v2);
            CollectionAssert.AreEqual(new double[] { 5, 7, 9 }, add.Components);
            Vector subtract = v1.Subtract(v2);
            CollectionAssert.AreEqual(new double[] { -3, -3, -3 }, subtract.Components);
            Vector scale = v1.Scale(2);
            CollectionAssert.AreEqual(new double[] { 2, 4, 6 }, scale.Components);
            Vector normalized = v1.Normalize();
            double norm = normalized.Norm();
            Assert.IsTrue(Math.Abs(norm - 1.0) < 1e-10);
        }

        [TestMethod]
        public void TestSyntheticDataOrthonormalization()
        {
            List<Vector> vectors = new List<Vector>();
            Random random = new Random(42);
            int count = 50;
            int dimension = 5;
            for (int i = 0; i < count; i++)
            {
                double[] comps = new double[dimension];
                for (int j = 0; j < dimension; j++)
                {
                    comps[j] = random.NextDouble() * 100 - 50;
                }

                vectors.Add(new Vector(comps));
            }

            List<Vector> orthonormal = Orthogonalizer.Orthonormalize(vectors);
            for (int i = 0; i < orthonormal.Count; i++)
            {
                double norm = orthonormal[i].Norm();
                Assert.IsTrue(Math.Abs(norm - 1.0) < 1e-8, "Normalized vector does not have unit norm");
                for (int j = i + 1; j < orthonormal.Count; j++)
                {
                    double dot = orthonormal[i].Dot(orthonormal[j]);
                    Assert.IsTrue(Math.Abs(dot) < 1e-8, "Vectors are not orthogonal");
                }
            }
        }
    }
}