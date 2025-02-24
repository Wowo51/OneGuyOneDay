using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SymbolicCholeskyDecomposition;

namespace SymbolicCholeskyDecompositionTest
{
    [TestClass]
    public class CholeskyDecomposerTests
    {
        [TestMethod]
        public void Decompose_NullMatrix_ReturnsEmptyMatrix()
        {
            SparseMatrix result = CholeskyDecomposer.Decompose(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Size);
            Assert.AreEqual(0, result.Data.Count);
        }

        [TestMethod]
        public void Decompose_SingleElementMatrix_ReturnsMatrixWithDiagonalEntry()
        {
            int size = 1;
            SparseMatrix A = new SparseMatrix(size);
            SparseMatrix L = CholeskyDecomposer.Decompose(A);
            Assert.IsNotNull(L);
            Assert.AreEqual(size, L.Size);
            // Expect a diagonal entry at (0,0) because the while loop always visits j.
            Assert.IsTrue(L.Data.ContainsKey(0));
            Assert.IsTrue(L.Data[0].ContainsKey(0));
            Assert.AreEqual(0.0, L.Data[0][0]);
        }

        [TestMethod]
        public void Decompose_ChainPatternMatrix_ReturnsCorrectStructure()
        {
            int size = 3;
            SparseMatrix A = new SparseMatrix(size);
            // Create a chain: entry (0,1) makes parent's[1]=0; entry (1,2) makes parent's[2]=1.
            A.AddValue(0, 1, 1.0);
            A.AddValue(1, 2, 1.0);
            SparseMatrix L = CholeskyDecomposer.Decompose(A);
            Assert.IsNotNull(L);
            Assert.AreEqual(size, L.Size);
            // For j=0: while loop only visits 0 => L(0,0) added.
            // For j=1: visits 1 then parent's[1] = 0 is visited but i starts at 1 so only L(1,1) is added.
            // For j=2: visits 2 and then parent's chain covers 1 and 0 but only i>=2 qualifies so only L(2,2) is added.
            Assert.IsTrue(L.Data.ContainsKey(0));
            Assert.IsTrue(L.Data[0].ContainsKey(0));
            Assert.IsTrue(L.Data.ContainsKey(1));
            Assert.IsTrue(L.Data[1].ContainsKey(1));
            Assert.IsTrue(L.Data.ContainsKey(2));
            Assert.IsTrue(L.Data[2].ContainsKey(2));
            // Ensure that every stored entry is in the lower triangular region (row >= col)
            foreach (KeyValuePair<int, Dictionary<int, double>> row in L.Data)
            {
                foreach (KeyValuePair<int, double> entry in row.Value)
                {
                    Assert.IsTrue(row.Key >= entry.Key);
                }
            }
        }

        [TestMethod]
        public void Decompose_RandomSparseMatrix_HasValidStructure()
        {
            int size = 100;
            SparseMatrix A = GenerateRandomSparseMatrix(size, 0.05);
            SparseMatrix L = CholeskyDecomposer.Decompose(A);
            Assert.IsNotNull(L);
            Assert.AreEqual(size, L.Size);
            // Verify that for every column j a diagonal entry (j, j) exists.
            for (int j = 0; j < size; j++)
            {
                bool found = false;
                if (L.Data.ContainsKey(j) && L.Data[j].ContainsKey(j))
                {
                    found = true;
                }

                Assert.IsTrue(found, "Diagonal entry missing for column " + j);
            }

            // Verify that every entry in L lies in the lower triangular region.
            foreach (KeyValuePair<int, Dictionary<int, double>> row in L.Data)
            {
                foreach (KeyValuePair<int, double> entry in row.Value)
                {
                    Assert.IsTrue(row.Key >= entry.Key, "Entry at (" + row.Key + ", " + entry.Key + ") is not in the lower triangular region.");
                }
            }
        }

        [TestMethod]
        public void Decompose_EmptyMatrix_ReturnsEmptyMatrix()
        {
            SparseMatrix A = new SparseMatrix(0);
            SparseMatrix L = CholeskyDecomposer.Decompose(A);
            Assert.IsNotNull(L);
            Assert.AreEqual(0, L.Size);
            Assert.AreEqual(0, L.Data.Count);
        }

        // Helper method: Generates a random sparse matrix with entries only for indices where i > j.
        private static SparseMatrix GenerateRandomSparseMatrix(int size, double density)
        {
            SparseMatrix matrix = new SparseMatrix(size);
            System.Random rnd = new System.Random(42);
            for (int j = 0; j < size; j++)
            {
                for (int i = j + 1; i < size; i++)
                {
                    if (rnd.NextDouble() < density)
                    {
                        double value = rnd.NextDouble();
                        matrix.AddValue(j, i, value);
                    }
                }
            }

            return matrix;
        }
    }
}