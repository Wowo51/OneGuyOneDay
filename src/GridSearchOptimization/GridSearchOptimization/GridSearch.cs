using System;
using System.Collections.Generic;

namespace GridSearchOptimization
{
    public class Parameter
    {
        public string Name { get; set; }
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double Step { get; set; }

        public Parameter(string name, double lowerBound, double upperBound, double step)
        {
            Name = name;
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Step = step;
        }
    }

    public class GridSearchResult
    {
        public Dictionary<string, double> BestParameters { get; set; }
        public double BestObjective { get; set; }

        public GridSearchResult(Dictionary<string, double> bestParameters, double bestObjective)
        {
            BestParameters = bestParameters;
            BestObjective = bestObjective;
        }
    }

    public class GridSearch
    {
        public List<Parameter> Parameters { get; set; }
        public Func<Dictionary<string, double>, double> ObjectiveFunction { get; set; }

        private double bestObjective;
        private Dictionary<string, double> bestParameters;
        public GridSearch(List<Parameter> parameters, Func<Dictionary<string, double>, double> objectiveFunction)
        {
            Parameters = parameters;
            ObjectiveFunction = objectiveFunction;
            bestObjective = double.MaxValue;
            bestParameters = new Dictionary<string, double>();
        }

        public GridSearchResult Optimize()
        {
            Dictionary<string, double> initialParameters = new Dictionary<string, double>();
            Search(0, initialParameters);
            return new GridSearchResult(bestParameters, bestObjective);
        }

        private void Search(int index, Dictionary<string, double> current)
        {
            if (index == Parameters.Count)
            {
                double objective = ObjectiveFunction(current);
                if (objective < bestObjective)
                {
                    bestObjective = objective;
                    bestParameters = new Dictionary<string, double>(current);
                }

                return;
            }

            Parameter param = Parameters[index];
            for (double value = param.LowerBound; value <= param.UpperBound; value += param.Step)
            {
                Dictionary<string, double> nextParameters = new Dictionary<string, double>(current)
                {
                    [param.Name] = value
                };
                Search(index + 1, nextParameters);
            }
        }
    }
}