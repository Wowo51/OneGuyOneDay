namespace MinimaxAlgorithm
{
    using System;
    using System.Collections.Generic;

    public class MinimaxSolver
    {
        public MinimaxResult Minimax(IGameState state, bool isMaximizing, int depth)
        {
            if (depth == 0 || state.IsTerminal)
            {
                MinimaxResult result = new MinimaxResult();
                result.Score = state.Evaluate();
                result.BestMove = null;
                return result;
            }

            if (isMaximizing)
            {
                int bestScore = Int32.MinValue;
                IGameState? bestMove = null;
                foreach (IGameState child in state.GetSuccessors())
                {
                    MinimaxResult childResult = Minimax(child, false, depth - 1);
                    if (childResult.Score > bestScore)
                    {
                        bestScore = childResult.Score;
                        bestMove = child;
                    }
                }

                MinimaxResult result = new MinimaxResult();
                result.Score = bestScore;
                result.BestMove = bestMove;
                return result;
            }
            else
            {
                int bestScore = Int32.MaxValue;
                IGameState? bestMove = null;
                foreach (IGameState child in state.GetSuccessors())
                {
                    MinimaxResult childResult = Minimax(child, true, depth - 1);
                    if (childResult.Score < bestScore)
                    {
                        bestScore = childResult.Score;
                        bestMove = child;
                    }
                }

                MinimaxResult result = new MinimaxResult();
                result.Score = bestScore;
                result.BestMove = bestMove;
                return result;
            }
        }
    }
}