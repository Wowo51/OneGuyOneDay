using System;

namespace GeneralProblemSolver
{
    public class Operator
    {
        public string Name { get; set; }
        public Func<ProblemState, bool> Applicable { get; set; }
        public Func<ProblemState, ProblemState> Apply { get; set; }

        public Operator(string name, Func<ProblemState, bool> applicable, Func<ProblemState, ProblemState> apply)
        {
            Name = name;
            Applicable = applicable;
            Apply = apply;
        }
    }
}