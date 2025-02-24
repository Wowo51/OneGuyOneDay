using HopcroftMooreBrzozowski.Models;

namespace HopcroftMooreBrzozowski.Minimizers
{
    public interface IDFAMinimizer
    {
        DeterministicFiniteAutomaton Minimize(DeterministicFiniteAutomaton dfa);
    }
}