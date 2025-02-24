using System;
using System.Collections.Generic;

namespace BuddyMemoryAllocation
{
    public class BuddyAllocator
    {
        private int _totalSize;
        private Dictionary<int, List<int>> _freeBlocks = new Dictionary<int, List<int>>();
        public BuddyAllocator(int totalSize)
        {
            _totalSize = totalSize;
            int initialSize = NextPowerOfTwo(totalSize);
            if (initialSize != totalSize)
            {
                _totalSize = initialSize;
            }

            _freeBlocks[_totalSize] = new List<int>
            {
                0
            };
        }

        public int? Allocate(int size)
        {
            int reqSize = NextPowerOfTwo(size);
            int foundSize = 0;
            int offset = 0;
            bool found = false;
            int blockSize = reqSize;
            while (blockSize <= _totalSize)
            {
                if (_freeBlocks.ContainsKey(blockSize) && _freeBlocks[blockSize].Count > 0)
                {
                    foundSize = blockSize;
                    offset = _freeBlocks[blockSize][0];
                    _freeBlocks[blockSize].RemoveAt(0);
                    found = true;
                    break;
                }

                blockSize *= 2;
            }

            if (!found)
            {
                return null;
            }

            while (foundSize > reqSize)
            {
                foundSize /= 2;
                int buddyOffset = offset + foundSize;
                AddFreeBlock(foundSize, buddyOffset);
            }

            return offset;
        }

        public void Free(int offset, int size)
        {
            int blockSize = NextPowerOfTwo(size);
            int currentOffset = offset;
            int currentSize = blockSize;
            while (true)
            {
                int buddyOffset = currentOffset ^ currentSize;
                bool merged = false;
                if (_freeBlocks.ContainsKey(currentSize))
                {
                    List<int> blockList = _freeBlocks[currentSize];
                    int index = blockList.IndexOf(buddyOffset);
                    if (index != -1)
                    {
                        blockList.RemoveAt(index);
                        currentOffset = currentOffset < buddyOffset ? currentOffset : buddyOffset;
                        currentSize *= 2;
                        merged = true;
                    }
                }

                if (!merged)
                {
                    AddFreeBlock(currentSize, currentOffset);
                    break;
                }
            }
        }

        private void AddFreeBlock(int size, int offset)
        {
            if (_freeBlocks.ContainsKey(size))
            {
                _freeBlocks[size].Add(offset);
            }
            else
            {
                _freeBlocks[size] = new List<int>
                {
                    offset
                };
            }
        }

        private static int NextPowerOfTwo(int n)
        {
            if (n < 1)
            {
                return 1;
            }

            int power = 1;
            while (power < n)
            {
                power *= 2;
            }

            return power;
        }
    }
}