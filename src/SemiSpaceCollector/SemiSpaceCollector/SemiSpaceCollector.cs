using System;

namespace SemiSpaceCollector
{
    public class SemiSpaceCollector
    {
        private object? [] _fromSpace;
        private object? [] _toSpace;
        private int _fromFree;
        private int _toFree;
        private int _capacity;
        private bool _usingFromSpace;
        public SemiSpaceCollector(int capacity)
        {
            if (capacity <= 0)
            {
                capacity = 1;
            }

            _capacity = capacity;
            _fromSpace = new object? [capacity];
            _toSpace = new object? [capacity];
            _fromFree = 0;
            _toFree = 0;
            _usingFromSpace = true;
        }

        public object? Allocate(object obj)
        {
            if (_usingFromSpace)
            {
                if (_fromFree >= _capacity)
                {
                    Collect();
                }
            }
            else
            {
                if (_toFree >= _capacity)
                {
                    Collect();
                }
            }

            if (_usingFromSpace)
            {
                if (_fromFree >= _capacity)
                {
                    return null;
                }

                _fromSpace[_fromFree] = obj;
                object? allocated = _fromSpace[_fromFree];
                _fromFree++;
                return allocated;
            }
            else
            {
                if (_toFree >= _capacity)
                {
                    return null;
                }

                _toSpace[_toFree] = obj;
                object? allocated = _toSpace[_toFree];
                _toFree++;
                return allocated;
            }
        }

        public void Collect()
        {
            if (_usingFromSpace)
            {
                _toFree = 0;
                for (int i = 0; i < _fromFree; i++)
                {
                    if (_fromSpace[i] != null)
                    {
                        _toSpace[_toFree] = _fromSpace[i];
                        _toFree++;
                        _fromSpace[i] = null;
                    }
                }

                _fromFree = 0;
                _usingFromSpace = false;
            }
            else
            {
                _fromFree = 0;
                for (int i = 0; i < _toFree; i++)
                {
                    if (_toSpace[i] != null)
                    {
                        _fromSpace[_fromFree] = _toSpace[i];
                        _fromFree++;
                        _toSpace[i] = null;
                    }
                }

                _toFree = 0;
                _usingFromSpace = true;
            }
        }

        public int ActiveSpaceAllocatedCount
        {
            get
            {
                return _usingFromSpace ? _fromFree : _toFree;
            }
        }

        public int Capacity
        {
            get
            {
                return _capacity;
            }
        }

        public bool IsUsingFromSpace
        {
            get
            {
                return _usingFromSpace;
            }
        }
    }
}