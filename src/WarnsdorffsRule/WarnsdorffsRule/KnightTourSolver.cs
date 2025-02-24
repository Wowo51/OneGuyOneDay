using System;
using System.Collections.Generic;

namespace WarnsdorffsRule
{
    public struct Coordinate
    {
        public int X;
        public int Y;
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class KnightTourSolver
    {
        private static readonly int[] MovesX = new int[8]
        {
            2,
            1,
            -1,
            -2,
            -2,
            -1,
            1,
            2
        };
        private static readonly int[] MovesY = new int[8]
        {
            1,
            2,
            2,
            1,
            -1,
            -2,
            -2,
            -1
        };
        public List<Coordinate> Solve(int boardSize, int startX, int startY)
        {
            int totalCells = boardSize * boardSize;
            bool[, ] visited = new bool[boardSize, boardSize];
            List<Coordinate> path = new List<Coordinate>();
            visited[startX, startY] = true;
            path.Add(new Coordinate(startX, startY));
            int currentX = startX;
            int currentY = startY;
            for (int step = 1; step < totalCells; step++)
            {
                int nextX = -1;
                int nextY = -1;
                int minDegree = 9;
                for (int moveIndex = 0; moveIndex < 8; moveIndex++)
                {
                    int candidateX = currentX + MovesX[moveIndex];
                    int candidateY = currentY + MovesY[moveIndex];
                    if (IsValid(candidateX, candidateY, boardSize, visited))
                    {
                        int degree = CountOnwardMoves(candidateX, candidateY, boardSize, visited);
                        if (degree < minDegree)
                        {
                            minDegree = degree;
                            nextX = candidateX;
                            nextY = candidateY;
                        }
                    }
                }

                if (nextX == -1 || nextY == -1)
                {
                    break;
                }

                currentX = nextX;
                currentY = nextY;
                visited[currentX, currentY] = true;
                path.Add(new Coordinate(currentX, currentY));
            }

            if (path.Count == totalCells)
            {
                return path;
            }
            else
            {
                return new List<Coordinate>();
            }
        }

        private bool IsValid(int x, int y, int boardSize, bool[, ] visited)
        {
            if (x >= 0 && x < boardSize && y >= 0 && y < boardSize && !visited[x, y])
            {
                return true;
            }

            return false;
        }

        private int CountOnwardMoves(int x, int y, int boardSize, bool[, ] visited)
        {
            int count = 0;
            for (int moveIndex = 0; moveIndex < 8; moveIndex++)
            {
                int nextX = x + MovesX[moveIndex];
                int nextY = y + MovesY[moveIndex];
                if (IsValid(nextX, nextY, boardSize, visited))
                {
                    count++;
                }
            }

            return count;
        }
    }
}