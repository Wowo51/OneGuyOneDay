using System.Collections.Generic;

namespace BcjrAlgorithm
{
    public class Trellis
    {
        public int NumStages { get; set; }
        public int NumStates { get; set; }
        public int InitialState { get; set; }
        public int FinalState { get; set; }
        public List<List<TrellisTransition>> Transitions { get; set; }

        public Trellis()
        {
            this.Transitions = new List<List<TrellisTransition>>();
        }
    }

    public class TrellisTransition
    {
        public int FromState { get; set; }
        public int ToState { get; set; }
        public double Metric { get; set; }
        public int Input { get; set; }
    }
}