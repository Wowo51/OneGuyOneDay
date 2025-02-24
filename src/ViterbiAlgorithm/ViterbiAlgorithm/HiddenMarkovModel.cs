using System.Collections.Generic;

namespace ViterbiAlgorithm
{
    public class HiddenMarkovModel
    {
        public List<string> States { get; set; }
        public List<string> Observations { get; set; }
        public double[] StartProbability { get; set; }
        public double[, ] TransitionProbability { get; set; }
        public double[, ] EmissionProbability { get; set; }

        public HiddenMarkovModel(List<string> states, List<string> observations, double[] startProbability, double[, ] transitionProbability, double[, ] emissionProbability)
        {
            this.States = (states != null) ? states : new List<string>();
            this.Observations = (observations != null) ? observations : new List<string>();
            this.StartProbability = (startProbability != null) ? startProbability : new double[0];
            this.TransitionProbability = (transitionProbability != null) ? transitionProbability : new double[0, 0];
            this.EmissionProbability = (emissionProbability != null) ? emissionProbability : new double[0, 0];
        }

        public int GetObservationIndex(string observation)
        {
            for (int index = 0; index < this.Observations.Count; index++)
            {
                if (this.Observations[index] == observation)
                {
                    return index;
                }
            }

            return -1;
        }
    }
}