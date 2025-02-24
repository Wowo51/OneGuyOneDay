using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaffAlgorithm;

namespace ChaffAlgorithmTest
{
    [TestClass]
    public class ChaffSolverTests
    {
        [TestMethod]
        public void TestSimpleSatisfiable()
        {
            List<List<int>> clauses = new List<List<int>>
            {
                new List<int>
                {
                    1,
                    -2
                },
                new List<int>
                {
                    -1,
                    2,
                    3
                }
            };
            ChaffSolver solver = new ChaffSolver();
            Dictionary<int, bool>? solution = solver.Solve(clauses);
            Assert.IsNotNull(solution, "Solution should not be null for satisfiable formula.");
            foreach (List<int> clause in clauses)
            {
                bool clauseSatisfied = false;
                foreach (int literal in clause)
                {
                    int variable = Math.Abs(literal);
                    Assert.IsTrue(solution.ContainsKey(variable), "Solution should contain variable.");
                    bool value = solution[variable];
                    if ((literal > 0 && value) || (literal < 0 && !value))
                    {
                        clauseSatisfied = true;
                        break;
                    }
                }

                Assert.IsTrue(clauseSatisfied, "Clause is not satisfied.");
            }
        }

        [TestMethod]
        public void TestUnsatisfiable()
        {
            List<List<int>> clauses = new List<List<int>>
            {
                new List<int>
                {
                    1
                },
                new List<int>
                {
                    -1
                }
            };
            ChaffSolver solver = new ChaffSolver();
            Dictionary<int, bool>? solution = solver.Solve(clauses);
            Assert.IsNull(solution, "Solution should be null for unsatisfiable formula.");
        }

        [TestMethod]
        public void TestEmptyClauseList()
        {
            List<List<int>> clauses = new List<List<int>>();
            ChaffSolver solver = new ChaffSolver();
            Dictionary<int, bool>? solution = solver.Solve(clauses);
            Assert.IsNotNull(solution, "Solution should not be null for empty CNF.");
            Assert.AreEqual(0, solution.Count, "Solution for empty CNF should be empty.");
        }

        [TestMethod]
        public void TestLargeSatisfiableCnf()
        {
            int numberOfVariables = 10;
            int numberOfClauses = 50;
            Dictionary<int, bool> knownAssignment = new Dictionary<int, bool>();
            for (int i = 1; i <= numberOfVariables; i++)
            {
                knownAssignment.Add(i, (i % 2 == 0));
            }

            List<List<int>> clauses = new List<List<int>>();
            Random random = new Random(42);
            for (int i = 0; i < numberOfClauses; i++)
            {
                int clauseLength = random.Next(2, 5);
                List<int> clause = new List<int>();
                HashSet<int> chosenVars = new HashSet<int>();
                for (int j = 0; j < clauseLength; j++)
                {
                    int variable = 0;
                    do
                    {
                        variable = random.Next(1, numberOfVariables + 1);
                    }
                    while (chosenVars.Contains(variable));
                    chosenVars.Add(variable);
                    bool pos = (random.Next(0, 2) == 0);
                    int literal = pos ? variable : -variable;
                    clause.Add(literal);
                }

                bool satisfied = false;
                foreach (int literal in clause)
                {
                    int variable = Math.Abs(literal);
                    bool assignmentForVar = knownAssignment[variable];
                    if ((literal > 0 && assignmentForVar) || (literal < 0 && !assignmentForVar))
                    {
                        satisfied = true;
                        break;
                    }
                }

                if (!satisfied && clause.Count > 0)
                {
                    int variable = Math.Abs(clause[0]);
                    bool assignmentForVar = knownAssignment[variable];
                    clause[0] = assignmentForVar ? variable : -variable;
                }

                clauses.Add(clause);
            }

            ChaffSolver solver = new ChaffSolver();
            Dictionary<int, bool>? solution = solver.Solve(clauses);
            Assert.IsNotNull(solution, "Solution should not be null for large satisfiable CNF.");
            foreach (List<int> clause in clauses)
            {
                bool clauseSatisfied = false;
                foreach (int literal in clause)
                {
                    int variable = Math.Abs(literal);
                    Assert.IsTrue(solution.ContainsKey(variable), "Solution should contain variable.");
                    bool value = solution[variable];
                    if ((literal > 0 && value) || (literal < 0 && !value))
                    {
                        clauseSatisfied = true;
                        break;
                    }
                }

                Assert.IsTrue(clauseSatisfied, "Clause is not satisfied in large CNF test.");
            }
        }
    }
}