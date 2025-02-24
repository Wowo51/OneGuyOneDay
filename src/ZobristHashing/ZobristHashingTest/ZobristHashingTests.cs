using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZobristHashing;

namespace ZobristHashingTest
{
    [TestClass]
    public class ZobristHashingTests
    {
        [TestMethod]
        public void ComputeHash_ValidBoard_ShouldBeConsistent()
        {
            int width = 8;
            int height = 8;
            int pieceTypes = 4;
            ZobristHasher hasher = new ZobristHasher(width, height, pieceTypes);
            int[, ] board = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    board[x, y] = (x + y) % pieceTypes;
                }
            }

            ulong hash1 = hasher.ComputeHash(board);
            ulong hash2 = hasher.ComputeHash(board);
            Assert.AreEqual(hash1, hash2, "Repeated hash computation should yield the same hash");
            int[, ] boardModified = (int[, ])board.Clone();
            boardModified[0, 0] = (boardModified[0, 0] + 1) % pieceTypes;
            ulong hashModified = hasher.ComputeHash(boardModified);
            Assert.AreNotEqual(hash1, hashModified, "Hash should change when board changes");
        }

        [TestMethod]
        public void ComputeHash_InvalidDimensions_ShouldReturnZero()
        {
            int width = 8;
            int height = 8;
            int pieceTypes = 4;
            ZobristHasher hasher = new ZobristHasher(width, height, pieceTypes);
            int[, ] boardWrongWidth = new int[width - 1, height];
            ulong hashWrongWidth = hasher.ComputeHash(boardWrongWidth);
            Assert.AreEqual(0UL, hashWrongWidth, "Board with wrong width should return 0");
            int[, ] boardWrongHeight = new int[width, height - 1];
            ulong hashWrongHeight = hasher.ComputeHash(boardWrongHeight);
            Assert.AreEqual(0UL, hashWrongHeight, "Board with wrong height should return 0");
        }

        [TestMethod]
        public void Constructor_NormalizesDimensions_ShouldComputeHashFor1x1Board()
        {
            ZobristHasher hasher = new ZobristHasher(-3, -4, -2);
            int[, ] board = new int[1, 1];
            board[0, 0] = 0;
            ulong hash1 = hasher.ComputeHash(board);
            ulong hash2 = hasher.ComputeHash(board);
            Assert.AreEqual(hash1, hash2, "Repeated hash computation for normalized board should yield the same hash");
        }

        [TestMethod]
        public void ComputeHash_IgnoreInvalidPieceTypes_ShouldProduceDifferentHash()
        {
            int width = 2;
            int height = 2;
            int pieceTypes = 1;
            ZobristHasher hasher = new ZobristHasher(width, height, pieceTypes);
            int[, ] boardValid = new int[width, height];
            ulong validHash = hasher.ComputeHash(boardValid);
            int[, ] boardInvalid = new int[width, height];
            boardInvalid[0, 0] = 5;
            ulong invalidHash = hasher.ComputeHash(boardInvalid);
            Assert.AreNotEqual(validHash, invalidHash, "Board with an invalid piece type should yield a different hash from board with all valid values");
        }

        [TestMethod]
        public void ComputeHash_LargeBoard_ShouldBeConsistent()
        {
            int width = 100;
            int height = 100;
            int pieceTypes = 5;
            ZobristHasher hasher = new ZobristHasher(width, height, pieceTypes);
            int[, ] board = new int[width, height];
            Random rng = new Random(42);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    board[x, y] = rng.Next(0, pieceTypes);
                }
            }

            ulong hash1 = hasher.ComputeHash(board);
            ulong hash2 = hasher.ComputeHash(board);
            Assert.AreEqual(hash1, hash2, "Large board hash should be consistent over multiple computations");
        }
    }
}