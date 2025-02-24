using System;

namespace ZobristHashing
{
    public class ZobristHasher
    {
        private readonly int boardWidth;
        private readonly int boardHeight;
        private readonly int pieceTypesCount;
        private readonly ulong[,, ] randomTable;
        private readonly Random random;
        public ZobristHasher(int boardWidth, int boardHeight, int pieceTypesCount)
        {
            if (boardWidth <= 0)
            {
                boardWidth = 1;
            }

            if (boardHeight <= 0)
            {
                boardHeight = 1;
            }

            if (pieceTypesCount <= 0)
            {
                pieceTypesCount = 1;
            }

            this.boardWidth = boardWidth;
            this.boardHeight = boardHeight;
            this.pieceTypesCount = pieceTypesCount;
            this.randomTable = new ulong[boardWidth, boardHeight, pieceTypesCount];
            this.random = new Random();
            InitializeRandomTable();
        }

        private void InitializeRandomTable()
        {
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    for (int p = 0; p < pieceTypesCount; p++)
                    {
                        randomTable[x, y, p] = NextUInt64();
                    }
                }
            }
        }

        private ulong NextUInt64()
        {
            uint low = ((uint)random.Next(0, 65536) << 16) | (uint)random.Next(0, 65536);
            uint high = ((uint)random.Next(0, 65536) << 16) | (uint)random.Next(0, 65536);
            return ((ulong)high << 32) | low;
        }

        public ulong ComputeHash(int[, ] board)
        {
            if (board.GetLength(0) != boardWidth || board.GetLength(1) != boardHeight)
            {
                return 0UL;
            }

            ulong hash = 0UL;
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    int pieceType = board[x, y];
                    if (pieceType >= 0 && pieceType < pieceTypesCount)
                    {
                        hash ^= randomTable[x, y, pieceType];
                    }
                }
            }

            return hash;
        }
    }
}