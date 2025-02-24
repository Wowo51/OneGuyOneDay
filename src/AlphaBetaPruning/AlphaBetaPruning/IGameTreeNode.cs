using System.Collections.Generic;

namespace AlphaBetaPruning
{
    public interface IGameTreeNode
    {
        bool IsTerminal { get; }

        int Value { get; }

        IEnumerable<IGameTreeNode> GetChildren();
    }
}