namespace MinimaxAlgorithm
{
    using System.Collections.Generic;

    public interface IGameState
    {
        bool IsTerminal { get; }

        int Evaluate();
        IEnumerable<IGameState> GetSuccessors();
    }
}