using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoneMethodSIP;

namespace StoneMethodSIPTest
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void NormTest()
        {
            Vector vector = new Vector(2);
            vector.Values[0] = 3;
            vector.Values[1] = 4;
            double norm = vector.Norm();
            Assert.AreEqual(5.0, norm, 1e-6);
        }

        [TestMethod]
        public void CloneTest()
        {
            Vector vector = new Vector(3);
            vector.Values[0] = 1;
            vector.Values[1] = 2;
            vector.Values[2] = 3;
            Vector clone = vector.Clone();
            Assert.AreEqual(vector.Size, clone.Size);
            for (int i = 0; i < vector.Size; i++)
            {
                Assert.AreEqual(vector.Values[i], clone.Values[i], 1e-6);
            }

            clone.Values[0] = 10;
            Assert.AreNotEqual(vector.Values[0], clone.Values[0]);
        }

        [TestMethod]
        public void AddSubtractMultiplyTest()
        {
            Vector a = new Vector(3);
            Vector b = new Vector(3);
            a.Values[0] = 1;
            a.Values[1] = 2;
            a.Values[2] = 3;
            b.Values[0] = 4;
            b.Values[1] = 5;
            b.Values[2] = 6;
            Vector add = Vector.Add(a, b);
            Assert.AreEqual(5, add.Values[0], 1e-6);
            Assert.AreEqual(7, add.Values[1], 1e-6);
            Assert.AreEqual(9, add.Values[2], 1e-6);
            Vector sub = Vector.Subtract(b, a);
            Assert.AreEqual(3, sub.Values[0], 1e-6);
            Assert.AreEqual(3, sub.Values[1], 1e-6);
            Assert.AreEqual(3, sub.Values[2], 1e-6);
            Vector mult = Vector.Multiply(2.0, a);
            Assert.AreEqual(2, mult.Values[0], 1e-6);
            Assert.AreEqual(4, mult.Values[1], 1e-6);
            Assert.AreEqual(6, mult.Values[2], 1e-6);
        }
    }

    [TestClass]
    public class SparseMatrixTests
    {
        [TestMethod]
        public void SetGetTest()
        {
            SparseMatrix matrix = new SparseMatrix(3, 3);
            matrix.Set(1, 1, 10);
            double value = matrix.Get(1, 1);
            Assert.AreEqual(10, value, 1e-6);
        }

        [TestMethod]
        public void GetUnsetTest()
        {
            SparseMatrix matrix = new SparseMatrix(3, 3);
            double value = matrix.Get(0, 2);
            Assert.AreEqual(0, value, 1e-6);
        }
    }

    [TestClass]
    public class StoneSIPSolverTests
    {
        [TestMethod]
        public void IdentityMatrixSolverTest()
        {
            int n = 5;
            SparseMatrix A = new SparseMatrix(n, n);
            for (int i = 0; i < n; i++)
            {
                A.Set(i, i, 1);
            }

            Vector b = new Vector(n);
            for (int i = 0; i < n; i++)
            {
                b.Values[i] = i + 1;
            }

            StoneSIPSolver solver = new StoneSIPSolver();
            Vector x = solver.Solve(A, b, 10, 1e-6);
            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(b.Values[i], x.Values[i], 1e-5);
            }
        }

        [TestMethod]
        public void DiagonallyDominantSolverTest()
        {
            int n = 10;
            SparseMatrix A = new SparseMatrix(n, n);
            System.Random rand = new System.Random(0);
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        double value = rand.NextDouble();
                        A.Set(i, j, value);
                        sum += Math.Abs(value);
                    }
                }

                A.Set(i, i, sum + 1);
            }

            Vector b = new Vector(n);
            for (int i = 0; i < n; i++)
            {
                b.Values[i] = rand.NextDouble();
            }

            StoneSIPSolver solver = new StoneSIPSolver();
            Vector x = solver.Solve(A, b, 1000, 1e-6);
            Vector r = new Vector(n);
            for (int i = 0; i < n; i++)
            {
                double Ax = 0;
                for (int j = 0; j < n; j++)
                {
                    Ax += A.Get(i, j) * x.Values[j];
                }

                r.Values[i] = b.Values[i] - Ax;
            }

            Assert.IsTrue(r.Norm() < 1e-4);
        }

        [TestMethod]
        public void KnownMatrixSolverTest()
        {
            int n = 3;
            SparseMatrix A = new SparseMatrix(n, n);
            A.Set(0, 0, 4);
            A.Set(0, 1, -1);
            A.Set(1, 0, -1);
            A.Set(1, 1, 4);
            A.Set(1, 2, -1);
            A.Set(2, 1, -1);
            A.Set(2, 2, 4);
            Vector b = new Vector(n);
            b.Values[0] = 3;
            b.Values[1] = 4;
            b.Values[2] = 5;
            StoneSIPSolver solver = new StoneSIPSolver();
            Vector x = solver.Solve(A, b, 100, 1e-6);
            Vector r = new Vector(n);
            for (int i = 0; i < n; i++)
            {
                double Ax = 0;
                for (int j = 0; j < n; j++)
                {
                    Ax += A.Get(i, j) * x.Values[j];
                }

                r.Values[i] = b.Values[i] - Ax;
            }

            Assert.IsTrue(r.Norm() < 1e-4);
        }
    }
}