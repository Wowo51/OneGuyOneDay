using HopcroftMooreBrzozowski.Models;

namespace HopcroftMooreBrzozowski.Minimizers
{
    public class BrzozowskiMinimizer : IDFAMinimizer
    {
        public DeterministicFiniteAutomaton Minimize(DeterministicFiniteAutomaton dfa)
        {
            var reversed1 = AutomataUtils.ReverseDFA(dfa);
            var dfa1 = AutomataUtils.DeterminizeNFA(reversed1);
            var reversed2 = AutomataUtils.ReverseDFA(dfa1);
            return AutomataUtils.DeterminizeNFA(reversed2);
        }
    }
}