using System;
using System.Collections.Generic;

namespace AlphaBetaPruning
{
    public static class AlphaBetaPruner
    {
        public static int AlphaBeta(IGameTreeNode node, int alpha, int beta, bool maximizingPlayer)
        {
            if (node == null)
            {
                return 0;
            }

            if (node.IsTerminal)
            {
                return node.Value;
            }

            if (maximizingPlayer)
            {
                int value = int.MinValue;
                IEnumerable<IGameTreeNode> children = node.GetChildren() ?? new List<IGameTreeNode>();
                foreach (IGameTreeNode child in children)
                {
                    int childValue = AlphaBeta(child, alpha, beta, false);
                    if (childValue > value)
                    {
                        value = childValue;
                    }

                    if (value > alpha)
                    {
                        alpha = value;
                    }

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return value;
            }
            else
            {
                int value = int.MaxValue;
                IEnumerable<IGameTreeNode> children = node.GetChildren() ?? new List<IGameTreeNode>();
                foreach (IGameTreeNode child in children)
                {
                    int childValue = AlphaBeta(child, alpha, beta, true);
                    if (childValue < value)
                    {
                        value = childValue;
                    }

                    if (value < beta)
                    {
                        beta = value;
                    }

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return value;
            }
        }
    }
}