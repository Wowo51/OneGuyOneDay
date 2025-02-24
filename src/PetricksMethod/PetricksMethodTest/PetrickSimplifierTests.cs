using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using PetricksMethod;

namespace PetricksMethodTest
{
    [TestClass]
    public class PetrickSimplifierTests
    {
        public TestContext TestContext { get; set; } = null !;

        [TestMethod]
        public void TestNullChart()
        {
            TestContext.WriteLine("Running TestNullChart");
            IList<IList<string>>? chart = null;
            IList<IList<string>> result = PetrickSimplifier.Simplify(chart);
            Assert.IsNotNull(result, "Simplify should return a non-null list for null chart.");
            Assert.AreEqual(0, result.Count, "Simplify should return 0 solutions for null chart.");
        }

        [TestMethod]
        public void TestEmptyChart()
        {
            TestContext.WriteLine("Running TestEmptyChart");
            IList<IList<string>> chart = new List<IList<string>>();
            IList<IList<string>> result = PetrickSimplifier.Simplify(chart);
            Assert.IsNotNull(result, "Simplify should return a non-null list for empty chart.");
            Assert.AreEqual(0, result.Count, "Simplify should return 0 solutions for empty chart.");
        }

        [TestMethod]
        public void TestRowWithEmptyList()
        {
            TestContext.WriteLine("Running TestRowWithEmptyList");
            IList<IList<string>> chart = new List<IList<string>>
            {
                new List<string>
                {
                    "A",
                    "B"
                },
                new List<string>()
            };
            IList<IList<string>> result = PetrickSimplifier.Simplify(chart);
            Assert.IsNotNull(result, "Simplify should return a non-null list for row with empty list.");
            Assert.AreEqual(0, result.Count, "Simplify should return 0 solutions if any row is empty.");
        }

        [TestMethod]
        public void TestRowWithAllNulls()
        {
            TestContext.WriteLine("Running TestRowWithAllNulls");
            IList<IList<string>> chart = new List<IList<string>>
            {
                new List<string>
                {
                    "A",
                    "B"
                },
                new List<string>
                {
                    null !,
                    null !
                }
            };
            IList<IList<string>> result = PetrickSimplifier.Simplify(chart);
            Assert.IsNotNull(result, "Simplify should return a non-null list for row with all nulls.");
            Assert.AreEqual(0, result.Count, "Simplify should return 0 solutions if any row contains only nulls.");
        }

        [TestMethod]
        public void TestSimpleChart()
        {
            TestContext.WriteLine("Running TestSimpleChart");
            IList<IList<string>> chart = new List<IList<string>>
            {
                new List<string>
                {
                    "A",
                    "B"
                },
                new List<string>
                {
                    "A",
                    "C"
                },
                new List<string>
                {
                    "B",
                    "C"
                }
            };
            IList<IList<string>> result = PetrickSimplifier.Simplify(chart);
            Assert.IsNotNull(result, "Simplify should return a non-null list for a simple chart.");
            Assert.AreEqual(2, result.Count, "Simplify should return 2 solutions for the simple chart.");
            foreach (IList<string> solution in result)
            {
                Assert.AreEqual(2, solution.Count, "Each solution in a simple chart should have 2 elements.");
                CollectionAssert.Contains(solution.ToList(), "A", "Each solution should contain 'A'.");
            }
        }

        [TestMethod]
        public void TestChartWithCommonElement()
        {
            TestContext.WriteLine("Running TestChartWithCommonElement");
            IList<IList<string>> chart = new List<IList<string>>
            {
                new List<string>
                {
                    "COMMON",
                    "A"
                },
                new List<string>
                {
                    "COMMON",
                    "B"
                },
                new List<string>
                {
                    "COMMON",
                    "C"
                }
            };
            IList<IList<string>> result = PetrickSimplifier.Simplify(chart);
            Assert.IsNotNull(result, "Simplify should return a non-null list for a chart with common element.");
            Assert.AreEqual(1, result.Count, "Simplify should return 1 solution when a common element exists.");
            Assert.AreEqual(1, result[0].Count, "The solution should contain exactly one element.");
            Assert.AreEqual("COMMON", result[0].First(), "The common element should be 'COMMON'.");
        }

        [TestMethod]
        public void TestSyntheticChartWithoutCommon()
        {
            TestContext.WriteLine("Running TestSyntheticChartWithoutCommon");
            IList<IList<string>> chart = new List<IList<string>>();
            chart.Add(new List<string> { "A", "B" });
            for (int i = 0; i < 10; i++)
            {
                chart.Add(new List<string> { "B", "C" });
            }

            IList<IList<string>> result = PetrickSimplifier.Simplify(chart);
            Assert.IsNotNull(result, "Simplify should return a non-null list for synthetic chart without common element.");
            Assert.AreEqual(1, result.Count, "Simplify should return 1 solution for synthetic chart without common element.");
            Assert.AreEqual(1, result[0].Count, "The single solution should contain 1 element.");
            Assert.AreEqual("B", result[0].First(), "The solution should be 'B'.");
        }

        [TestMethod]
        public void TestRandomLargeChart()
        {
            TestContext.WriteLine("Running TestRandomLargeChart");
            Random random = new Random(0);
            IList<IList<string>> chart = new List<IList<string>>();
            for (int i = 0; i < 50; i++)
            {
                List<string> row = new List<string>
                {
                    "5"
                };
                List<string> pool = new List<string>
                {
                    "1",
                    "2",
                    "3",
                    "4",
                    "6",
                    "7",
                    "8",
                    "9",
                    "10"
                };
                pool = pool.OrderBy(delegate (string s)
                {
                    return random.Next();
                }).ToList();
                row.Add(pool[0]);
                row.Add(pool[1]);
                chart.Add(row);
            }

            IList<IList<string>> result = PetrickSimplifier.Simplify(chart);
            Assert.IsNotNull(result, "Simplify should return a non-null list for a random large chart.");
            Assert.AreEqual(1, result.Count, "Simplify should return 1 solution for a random large chart.");
            Assert.AreEqual(1, result[0].Count, "The solution for a random large chart should have 1 element.");
            Assert.AreEqual("5", result[0].First(), "The solution should be '5'.");
        }

        [TestMethod]
        public void TestRowWithDuplicateImplicants()
        {
            TestContext.WriteLine("Running TestRowWithDuplicateImplicants");
            IList<IList<string>> chart = new List<IList<string>>
            {
                new List<string>
                {
                    "A",
                    "B",
                    "A"
                },
                new List<string>
                {
                    "B",
                    "C"
                }
            };
            IList<IList<string>> result = PetrickSimplifier.Simplify(chart);
            Assert.IsNotNull(result, "Simplify should return a non-null list for chart with duplicate implicants.");
            Assert.AreEqual(1, result.Count, "Simplify should return 1 minimal solution when duplicates are present.");
            Assert.AreEqual(1, result[0].Count, "The minimal solution should have 1 element.");
            Assert.AreEqual("B", result[0].First(), "The solution should be 'B'.");
        }
    }
}