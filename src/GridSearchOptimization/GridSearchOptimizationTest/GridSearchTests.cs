using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GridSearchOptimization;

namespace GridSearchOptimizationTest
{
    [TestClass]
    public class GridSearchTests
    {
        [TestMethod]
        public void TestSingleParameter()
        {
            List<Parameter> parameters = new List<Parameter>
            {
                new Parameter("x", 0, 10, 1)
            };
            GridSearch gridSearch = new GridSearch(parameters, (Dictionary<string, double> param) =>
            {
                double x;
                return param.TryGetValue("x", out x) ? (x - 7) * (x - 7) : 1000;
            });
            GridSearchResult result = gridSearch.Optimize();
            Assert.AreEqual(7.0, result.BestParameters["x"]);
            Assert.AreEqual(0.0, result.BestObjective);
        }

        [TestMethod]
        public void TestMultipleParameters()
        {
            List<Parameter> parameters = new List<Parameter>
            {
                new Parameter("x", 0, 5, 1),
                new Parameter("y", 0, 5, 1)
            };
            GridSearch gridSearch = new GridSearch(parameters, (Dictionary<string, double> param) =>
            {
                double x, y;
                return param.TryGetValue("x", out x) && param.TryGetValue("y", out y) ? (x - 3) * (x - 3) + (y - 4) * (y - 4) : 1000;
            });
            GridSearchResult result = gridSearch.Optimize();
            Assert.AreEqual(3.0, result.BestParameters["x"]);
            Assert.AreEqual(4.0, result.BestParameters["y"]);
            Assert.AreEqual(0.0, result.BestObjective);
        }

        [TestMethod]
        public void TestEmptyParameters()
        {
            List<Parameter> parameters = new List<Parameter>();
            GridSearch gridSearch = new GridSearch(parameters, (Dictionary<string, double> param) => 42);
            GridSearchResult result = gridSearch.Optimize();
            Assert.AreEqual(42.0, result.BestObjective);
            Assert.AreEqual(0, result.BestParameters.Count);
        }

        [TestMethod]
        public void TestRandomSingleParameter()
        {
            Random random = new Random(0);
            double lower = 0, upper = 100, step = 5;
            List<Parameter> parameters = new List<Parameter>
            {
                new Parameter("x", lower, upper, step)
            };
            int numSteps = (int)((upper - lower) / step);
            int randomIndex = random.Next(0, numSteps + 1);
            double target = lower + randomIndex * step;
            GridSearch gridSearch = new GridSearch(parameters, (Dictionary<string, double> param) =>
            {
                double x;
                return param.TryGetValue("x", out x) ? (x - target) * (x - target) : 1000;
            });
            GridSearchResult result = gridSearch.Optimize();
            Assert.AreEqual(target, result.BestParameters["x"]);
            Assert.AreEqual(0.0, result.BestObjective);
        }
    }
}