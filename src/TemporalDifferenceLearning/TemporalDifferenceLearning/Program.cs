using System;

namespace TemporalDifferenceLearning
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TDLearner learner = new TDLearner(0.1, 0.9);
            learner.Update(0, 1, 5.0);
            double value = learner.GetValue(0);
            Console.WriteLine("Value of state 0: " + value);
        }
    }
}