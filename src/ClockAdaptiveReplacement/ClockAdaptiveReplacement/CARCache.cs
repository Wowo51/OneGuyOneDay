using System;
using System.Collections.Generic;

namespace ClockAdaptiveReplacement
{
    public class CARCache<TKey, TValue>
        where TKey : notnull
    {
        private readonly int _capacity;
        private int _p;
        private readonly LinkedList<CacheEntry> _cacheList;
        private readonly Dictionary<TKey, LinkedListNode<CacheEntry>> _cacheMap;
        private readonly HashSet<TKey> _ghostB1;
        private readonly HashSet<TKey> _ghostB2;
        private LinkedListNode<CacheEntry>? _clockHand;
        public CARCache(int capacity)
        {
            _capacity = capacity;
            _p = 0;
            _cacheList = new LinkedList<CacheEntry>();
            _cacheMap = new Dictionary<TKey, LinkedListNode<CacheEntry>>();
            _ghostB1 = new HashSet<TKey>();
            _ghostB2 = new HashSet<TKey>();
            _clockHand = null;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_cacheMap.ContainsKey(key))
            {
                LinkedListNode<CacheEntry> node = _cacheMap[key];
                CacheEntry entry = node.Value;
                value = entry.Value;
                if (entry.IsInT1)
                {
                    entry.IsInT1 = false;
                }

                entry.ReferenceBit = true;
                return true;
            }

            value = default(TValue);
            return false;
        }

        public void Put(TKey key, TValue value)
        {
            if (_cacheMap.ContainsKey(key))
            {
                LinkedListNode<CacheEntry> node = _cacheMap[key];
                node.Value.Value = value;
                node.Value.ReferenceBit = true;
                if (node.Value.IsInT1)
                {
                    node.Value.IsInT1 = false;
                }

                return;
            }

            if (_ghostB1.Contains(key))
            {
                int adjustment = _ghostB2.Count > 0 ? Math.Max(_ghostB2.Count / _ghostB1.Count, 1) : 1;
                _p = _p + adjustment;
                if (_p > _capacity)
                {
                    _p = _capacity;
                }

                _ghostB1.Remove(key);
                ExecuteReplacement(key);
                CacheEntry newEntryB1 = new CacheEntry(key, value, true, false);
                LinkedListNode<CacheEntry> newNodeB1 = _cacheList.AddLast(newEntryB1);
                _cacheMap.Add(key, newNodeB1);
                if (_clockHand == null)
                {
                    _clockHand = newNodeB1;
                }

                return;
            }

            if (_ghostB2.Contains(key))
            {
                int adjustment = _ghostB1.Count > 0 ? Math.Max(_ghostB1.Count / _ghostB2.Count, 1) : 1;
                _p = _p - adjustment;
                if (_p < 0)
                {
                    _p = 0;
                }

                _ghostB2.Remove(key);
                ExecuteReplacement(key);
                CacheEntry newEntryB2 = new CacheEntry(key, value, true, false);
                LinkedListNode<CacheEntry> newNodeB2 = _cacheList.AddLast(newEntryB2);
                _cacheMap.Add(key, newNodeB2);
                if (_clockHand == null)
                {
                    _clockHand = newNodeB2;
                }

                return;
            }

            if (_cacheMap.Count >= _capacity)
            {
                ExecuteReplacement(key);
            }

            CacheEntry newEntry = new CacheEntry(key, value, false, true);
            LinkedListNode<CacheEntry> newNode = _cacheList.AddLast(newEntry);
            _cacheMap.Add(key, newNode);
            if (_clockHand == null)
            {
                _clockHand = newNode;
            }
        }

        private void ExecuteReplacement(TKey incomingKey)
        {
            int t1Count = 0;
            int t2Count = 0;
            LinkedListNode<CacheEntry>? nodeScan = _cacheList.First;
            while (nodeScan != null)
            {
                if (nodeScan.Value.IsInT1)
                {
                    t1Count++;
                }
                else
                {
                    t2Count++;
                }

                nodeScan = nodeScan.Next;
            }

            bool targetT1 = false;
            if (t1Count >= 1 && (((_ghostB2.Contains(incomingKey)) && (t1Count == _p)) || (t1Count > _p)))
            {
                targetT1 = true;
            }
            else
            {
                targetT1 = false;
            }

            while (true)
            {
                if (_clockHand == null)
                {
                    _clockHand = _cacheList.First!;
                }

                CacheEntry current = _clockHand.Value;
                if (current.ReferenceBit)
                {
                    current.ReferenceBit = false;
                    _clockHand = _clockHand.Next ?? _cacheList.First!;
                }
                else
                {
                    if (current.IsInT1 == targetT1)
                    {
                        TKey evictedKey = current.Key;
                        LinkedListNode<CacheEntry> nodeToRemove = _clockHand;
                        _clockHand = _clockHand.Next ?? _cacheList.First!;
                        _cacheList.Remove(nodeToRemove);
                        _cacheMap.Remove(evictedKey);
                        if (current.IsInT1)
                        {
                            _ghostB1.Add(evictedKey);
                        }
                        else
                        {
                            _ghostB2.Add(evictedKey);
                        }

                        break;
                    }
                    else
                    {
                        current.ReferenceBit = false;
                        _clockHand = _clockHand.Next ?? _cacheList.First!;
                    }
                }
            }
        }

        public void Clear()
        {
            _cacheList.Clear();
            _cacheMap.Clear();
            _ghostB1.Clear();
            _ghostB2.Clear();
            _clockHand = null;
        }

        public IEnumerable<TKey> GetKeys()
        {
            LinkedListNode<CacheEntry>? node = _cacheList.First;
            while (node != null)
            {
                yield return node.Value.Key;
                node = node.Next;
            }
        }

        public bool IsInGhostB1(TKey key)
        {
            return _ghostB1.Contains(key);
        }

        public bool IsInGhostB2(TKey key)
        {
            return _ghostB2.Contains(key);
        }

        public int T1Count
        {
            get
            {
                int count = 0;
                LinkedListNode<CacheEntry>? node = _cacheList.First;
                while (node != null)
                {
                    if (node.Value.IsInT1)
                    {
                        count++;
                    }

                    node = node.Next;
                }

                return count;
            }
        }

        public int T2Count
        {
            get
            {
                int count = 0;
                LinkedListNode<CacheEntry>? node = _cacheList.First;
                while (node != null)
                {
                    if (!node.Value.IsInT1)
                    {
                        count++;
                    }

                    node = node.Next;
                }

                return count;
            }
        }

        private class CacheEntry
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public bool ReferenceBit { get; set; }
            public bool IsInT1 { get; set; }

            public CacheEntry(TKey key, TValue value, bool referenceBit, bool isInT1)
            {
                Key = key;
                Value = value;
                ReferenceBit = referenceBit;
                IsInT1 = isInT1;
            }
        }
    }
}