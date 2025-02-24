using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuddyMemoryAllocation;

namespace BuddyMemoryAllocationTest
{
    [TestClass]
    public class BuddyAllocatorTests
    {
        [TestMethod]
        public void TestAllocationReturnsValidOffset()
        {
            int totalSize = 16;
            BuddyAllocator allocator = new BuddyAllocator(totalSize);
            int? offset = allocator.Allocate(5);
            Assert.IsNotNull(offset, "Allocation returned null.");
            Assert.AreEqual(0, offset.Value, "First allocation offset should be 0.");
        }

        [TestMethod]
        public void TestAllocationExhaustion()
        {
            int totalSize = 16;
            BuddyAllocator allocator = new BuddyAllocator(totalSize);
            int? offset1 = allocator.Allocate(16);
            Assert.IsNotNull(offset1, "Expected allocation of full memory.");
            int? offset2 = allocator.Allocate(1);
            Assert.IsNull(offset2, "Expected allocation exhaustion to return null.");
        }

        [TestMethod]
        public void TestFreeAndAllocateAgain()
        {
            int totalSize = 16;
            BuddyAllocator allocator = new BuddyAllocator(totalSize);
            int? offset = allocator.Allocate(4);
            Assert.IsNotNull(offset, "Allocation failed.");
            allocator.Free(offset.Value, 4);
            int? offset2 = allocator.Allocate(4);
            Assert.IsNotNull(offset2, "Re-allocation after free failed.");
        }

        [TestMethod]
        public void TestFreeMerge()
        {
            int totalSize = 16;
            BuddyAllocator allocator = new BuddyAllocator(totalSize);
            int? block = allocator.Allocate(8);
            Assert.IsNotNull(block, "Allocation of block failed.");
            allocator.Free(block.Value, 8);
            int? fullBlock = allocator.Allocate(16);
            Assert.IsNotNull(fullBlock, "Allocation after merge failed.");
            Assert.AreEqual(0, fullBlock.Value, "Merged block offset should be 0.");
        }

        [TestMethod]
        public void TestAllocateZeroAndNegative()
        {
            int totalSize = 16;
            BuddyAllocator allocator = new BuddyAllocator(totalSize);
            int? offsetZero = allocator.Allocate(0);
            Assert.IsNotNull(offsetZero, "Allocation with 0 size should succeed as size 1.");
            int? offsetNegative = allocator.Allocate(-5);
            Assert.IsNotNull(offsetNegative, "Allocation with negative size should succeed as size 1.");
        }

        [TestMethod]
        public void TestMultipleAllocationsFreeRandomly()
        {
            int totalSize = 64;
            BuddyAllocator allocator = new BuddyAllocator(totalSize);
            List<Tuple<int, int>> allocations = new List<Tuple<int, int>>();
            int[] sizes = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };
            for (int i = 0; i < sizes.Length; i++)
            {
                int requestedSize = sizes[i];
                int? offset = allocator.Allocate(requestedSize);
                if (offset != null)
                {
                    allocations.Add(new Tuple<int, int>(offset.Value, requestedSize));
                }
            }

            allocations.Reverse();
            foreach (Tuple<int, int> alloc in allocations)
            {
                allocator.Free(alloc.Item1, alloc.Item2);
            }

            int? fullBlock = allocator.Allocate(64);
            Assert.IsNotNull(fullBlock, "After freeing all blocks, full allocation should succeed.");
            Assert.AreEqual(0, fullBlock.Value, "Full allocation offset should be 0.");
        }

        [TestMethod]
        public void TestSyntheticLargeDataAllocations()
        {
            int totalSize = 1024;
            BuddyAllocator allocator = new BuddyAllocator(totalSize);
            List<Tuple<int, int>> allocations = new List<Tuple<int, int>>();
            Random random = new Random(42);
            for (int i = 0; i < 100; i++)
            {
                int size = random.Next(1, 50);
                int? offset = allocator.Allocate(size);
                if (offset != null)
                {
                    allocations.Add(new Tuple<int, int>(offset.Value, size));
                }
            }

            foreach (Tuple<int, int> alloc in allocations)
            {
                allocator.Free(alloc.Item1, alloc.Item2);
            }

            int? fullBlock = allocator.Allocate(totalSize);
            Assert.IsNotNull(fullBlock, "After freeing all blocks, 1024 allocation should succeed.");
            Assert.AreEqual(0, fullBlock.Value, "Full block offset should be 0 after merging.");
        }
    }
}