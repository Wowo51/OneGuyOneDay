using System.Collections.Generic;

namespace StateSpaceSearch
{
    public interface IStateSpaceProblem<TState>
    {
        TState InitialState { get; }

        bool IsGoalState(TState state);
        IEnumerable<TState> GetNeighbors(TState state);
        int GetCost(TState fromState, TState toState);
        int Heuristic(TState state);
    }
}