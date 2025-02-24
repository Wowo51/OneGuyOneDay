using System;

namespace GeneralProblemSolver
{
    public class ProblemState
    {
        public string StateDescription { get; set; }

        public ProblemState(string stateDescription)
        {
            StateDescription = stateDescription;
        }
    }
}