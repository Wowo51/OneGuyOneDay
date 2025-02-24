using System;
using System.Collections.Generic;

namespace OddsAlgorithm
{
    public class Simulator
    {
        public void RunSimulation()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            int trialCount = 20;
            double eventProbability = 0.3;
            List<bool> outcomes = new List<bool>();
            Random random = new Random();
            for (int i = 0; i < trialCount; i++)
            {
                outcomes.Add(random.NextDouble() < eventProbability);
            }

            Console.WriteLine("Outcomes:");
            for (int i = 0; i < outcomes.Count; i++)
            {
                Console.WriteLine("Trial " + (i + 1) + ": " + outcomes[i]);
            }

            int predictedIndex = algorithm.PredictLastOccurrence(outcomes, eventProbability);
            if (predictedIndex != -1)
            {
                Console.WriteLine("Predicted stop at trial: " + (predictedIndex + 1));
            }
            else
            {
                Console.WriteLine("No stop predicted.");
            }

            int lastOccurrence = -1;
            for (int i = 0; i < outcomes.Count; i++)
            {
                if (outcomes[i])
                {
                    lastOccurrence = i;
                }
            }

            if (predictedIndex == lastOccurrence)
            {
                if (predictedIndex == -1)
                {
                    Console.WriteLine("Prediction successful: no event occurred.");
                }
                else
                {
                    Console.WriteLine("Prediction successful! It is the last occurrence.");
                }
            }
            else
            {
                Console.WriteLine("Prediction failed.");
            }
        }
    }
}