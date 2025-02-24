using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GeneralProblemSolver;

namespace GeneralProblemSolverTest
{
    [TestClass]
    public class GeneralProblemSolverUnitTests
    {
        [TestMethod]
        public void Test_Solve_NullInputs()
        {
            List<string> solutionSteps;
            Solver solver = new Solver(new List<Operator>());
            bool result = solver.Solve(null, new ProblemState("goal"), out solutionSteps);
            Assert.IsFalse(result);
            result = solver.Solve(new ProblemState("start"), null, out solutionSteps);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_Solve_AlreadySolved()
        {
            List<string> solutionSteps;
            Solver solver = new Solver(new List<Operator>());
            ProblemState state = new ProblemState("same");
            bool result = solver.Solve(state, state, out solutionSteps);
            Assert.IsTrue(result);
            Assert.AreEqual(0, solutionSteps.Count);
        }

        [TestMethod]
        public void Test_Solve_SimpleSequenceSuccess()
        {
            List<string> steps;
            Operator op1 = new Operator("Op1", (ProblemState s) => s.StateDescription == "start", (ProblemState s) => new ProblemState("middle"));
            Operator op2 = new Operator("Op2", (ProblemState s) => s.StateDescription == "middle", (ProblemState s) => new ProblemState("goal"));
            List<Operator> operators = new List<Operator>
            {
                op1,
                op2
            };
            Solver solver = new Solver(operators);
            bool result = solver.Solve(new ProblemState("start"), new ProblemState("goal"), out steps);
            Assert.IsTrue(result);
            Assert.AreEqual(2, steps.Count);
            Assert.AreEqual("Applied Op1", steps[0]);
            Assert.AreEqual("Applied Op2", steps[1]);
        }

        [TestMethod]
        public void Test_Solve_NoSolutionFails()
        {
            List<string> steps;
            Operator op = new Operator("OpNoGoal", (ProblemState s) => s.StateDescription == "start", (ProblemState s) => new ProblemState("notGoal"));
            List<Operator> operators = new List<Operator>
            {
                op
            };
            Solver solver = new Solver(operators);
            bool result = solver.Solve(new ProblemState("start"), new ProblemState("goal"), out steps);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_Solve_LongChain()
        {
            List<Operator> operators = new List<Operator>();
            for (int i = 0; i < 100; i++)
            {
                int current = i;
                Operator op = new Operator("Increment" + current, (ProblemState s) => s.StateDescription == current.ToString(), (ProblemState s) => new ProblemState((current + 1).ToString()));
                operators.Add(op);
            }

            Solver solver = new Solver(operators);
            List<string> steps;
            bool result = solver.Solve(new ProblemState("0"), new ProblemState("100"), out steps);
            Assert.IsTrue(result);
            Assert.AreEqual(100, steps.Count);
        }

        [TestMethod]
        public void Test_Solve_CyclicGraph()
        {
            List<Operator> operators = new List<Operator>();
            Operator opAB = new Operator("AtoB", (ProblemState s) => s.StateDescription == "A", (ProblemState s) => new ProblemState("B"));
            Operator opBA = new Operator("BtoA", (ProblemState s) => s.StateDescription == "B", (ProblemState s) => new ProblemState("A"));
            Operator opBC = new Operator("BtoC", (ProblemState s) => s.StateDescription == "B", (ProblemState s) => new ProblemState("C"));
            operators.Add(opAB);
            operators.Add(opBA);
            operators.Add(opBC);
            Solver solver = new Solver(operators);
            List<string> steps;
            bool result = solver.Solve(new ProblemState("A"), new ProblemState("C"), out steps);
            Assert.IsTrue(result);
            Assert.AreEqual(2, steps.Count);
            Assert.AreEqual("Applied AtoB", steps[0]);
            Assert.AreEqual("Applied BtoC", steps[1]);
        }

        [TestMethod]
        public void Test_Solve_LongChainLarge()
        {
            int chainLength = 500;
            List<Operator> operators = new List<Operator>();
            for (int i = 0; i < chainLength; i++)
            {
                int current = i;
                Operator op = new Operator("Increment" + current, (ProblemState s) => s.StateDescription == current.ToString(), (ProblemState s) => new ProblemState((current + 1).ToString()));
                operators.Add(op);
            }

            Solver solver = new Solver(operators);
            List<string> steps;
            bool result = solver.Solve(new ProblemState("0"), new ProblemState(chainLength.ToString()), out steps);
            Assert.IsTrue(result);
            Assert.AreEqual(chainLength, steps.Count);
        }

        [TestMethod]
        public void Test_Solve_BranchingGraph()
        {
            List<Operator> operators = new List<Operator>();
            Operator opStartTo1 = new Operator("StartTo1", (ProblemState s) => s.StateDescription == "start", (ProblemState s) => new ProblemState("1"));
            Operator opStartTo2 = new Operator("StartTo2", (ProblemState s) => s.StateDescription == "start", (ProblemState s) => new ProblemState("2"));
            Operator op1ToGoal = new Operator("1ToGoal", (ProblemState s) => s.StateDescription == "1", (ProblemState s) => new ProblemState("goal"));
            Operator op2ToGoal = new Operator("2ToGoal", (ProblemState s) => s.StateDescription == "2", (ProblemState s) => new ProblemState("goal"));
            operators.Add(opStartTo1);
            operators.Add(opStartTo2);
            operators.Add(op1ToGoal);
            operators.Add(op2ToGoal);
            Solver solver = new Solver(operators);
            List<string> steps;
            bool result = solver.Solve(new ProblemState("start"), new ProblemState("goal"), out steps);
            Assert.IsTrue(result);
            Assert.AreEqual(2, steps.Count);
            bool branch1 = (steps[0] == "Applied StartTo1" && steps[1] == "Applied 1ToGoal");
            bool branch2 = (steps[0] == "Applied StartTo2" && steps[1] == "Applied 2ToGoal");
            Assert.IsTrue(branch1 || branch2);
        }
    }
}