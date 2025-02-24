using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BeamSearch;

namespace BeamSearchTest
{
    [TestClass]
    public class BeamSearchSolverTests
    {
        private class DummyState
        {
            public int Value { get; set; }

            public DummyState(int value)
            {
                Value = value;
            }
        }

        [TestMethod]
        public void TestBeamWidthZeroReturnsNull()
        {
            DummyState start = new DummyState(0);
            DummyState? result = BeamSearchSolver.FindSolution<DummyState>(start, s => new List<DummyState>(), s => s.Value, s => false, 0);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestImmediateGoal()
        {
            DummyState start = new DummyState(5);
            DummyState? result = BeamSearchSolver.FindSolution<DummyState>(start, s => new List<DummyState> { new DummyState(s.Value + 1) }, s => Math.Abs(5 - s.Value), s => s.Value == 5, 3);
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result!.Value);
        }

        [TestMethod]
        public void TestFindSolution()
        {
            DummyState start = new DummyState(0);
            DummyState? result = BeamSearchSolver.FindSolution<DummyState>(start, s =>
            {
                List<DummyState> successors = new List<DummyState>();
                if (s.Value < 10)
                {
                    successors.Add(new DummyState(s.Value + 1));
                    successors.Add(new DummyState(s.Value + 2));
                }

                return successors;
            }, s => Math.Abs(10 - s.Value), s => s.Value == 10, 2);
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result!.Value);
        }

        [TestMethod]
        public void TestNoSolution()
        {
            DummyState start = new DummyState(0);
            DummyState? result = BeamSearchSolver.FindSolution<DummyState>(start, s => new List<DummyState>(), s => s.Value, s => false, 3);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestLargeSyntheticDataset()
        {
            DummyState start = new DummyState(0);
            DummyState? result = BeamSearchSolver.FindSolution<DummyState>(start, s =>
            {
                List<DummyState> successors = new List<DummyState>();
                if (s.Value < 100)
                {
                    for (int i = 1; i <= 50; i++)
                    {
                        successors.Add(new DummyState(s.Value + i));
                    }
                }

                return successors;
            }, s => Math.Abs(100 - s.Value), s => s.Value == 100, 10);
            Assert.IsNotNull(result);
            Assert.AreEqual(100, result!.Value);
        }
    }
}