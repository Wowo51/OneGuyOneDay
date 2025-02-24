using System;
using System.Collections.Generic;

namespace AdaptiveReplacementCache
{
    public class AdaptiveReplacementCache<TKey, TValue>
        where TKey : notnull
    {
        private readonly int _capacity;
        private int _p;
        private Dictionary<TKey, TValue> _cache;
        private LinkedList<TKey> _T1;
        private LinkedList<TKey> _T2;
        private LinkedList<TKey> _B1;
        private LinkedList<TKey> _B2;
        private Dictionary<TKey, LinkedListNode<TKey>> _T1Nodes;
        private Dictionary<TKey, LinkedListNode<TKey>> _T2Nodes;
        private Dictionary<TKey, LinkedListNode<TKey>> _B1Nodes;
        private Dictionary<TKey, LinkedListNode<TKey>> _B2Nodes;
        public AdaptiveReplacementCache(int capacity)
        {
            if (capacity <= 0)
            {
                capacity = 1;
            }

            _capacity = capacity;
            _p = 0;
            _cache = new Dictionary<TKey, TValue>();
            _T1 = new LinkedList<TKey>();
            _T2 = new LinkedList<TKey>();
            _B1 = new LinkedList<TKey>();
            _B2 = new LinkedList<TKey>();
            _T1Nodes = new Dictionary<TKey, LinkedListNode<TKey>>();
            _T2Nodes = new Dictionary<TKey, LinkedListNode<TKey>>();
            _B1Nodes = new Dictionary<TKey, LinkedListNode<TKey>>();
            _B2Nodes = new Dictionary<TKey, LinkedListNode<TKey>>();
        }

        public TValue? Get(TKey key)
        {
            if (_cache.ContainsKey(key))
            {
                if (_T1Nodes.ContainsKey(key))
                {
                    LinkedListNode<TKey> node = _T1Nodes[key]!;
                    _T1.Remove(node);
                    _T1Nodes.Remove(key);
                    LinkedListNode<TKey> newNode = _T2.AddFirst(key);
                    _T2Nodes[key] = newNode;
                }
                else if (_T2Nodes.ContainsKey(key))
                {
                    LinkedListNode<TKey> node = _T2Nodes[key]!;
                    _T2.Remove(node);
                    LinkedListNode<TKey> newNode = _T2.AddFirst(key);
                    _T2Nodes[key] = newNode;
                }

                return _cache[key];
            }

            return default !;
        }

        public void Put(TKey key, TValue value)
        {
            if (_cache.ContainsKey(key))
            {
                _cache[key] = value;
                _ = Get(key);
                return;
            }

            if (_B1Nodes.ContainsKey(key))
            {
                _p = Math.Min(_capacity, _p + Math.Max(_B2.Count / Math.Max(_B1.Count, 1), 1));
                Replace(key);
                LinkedListNode<TKey> nodeB1 = _B1Nodes[key]!;
                _B1.Remove(nodeB1);
                _B1Nodes.Remove(key);
                LinkedListNode<TKey> nodeT2 = _T2.AddFirst(key);
                _T2Nodes[key] = nodeT2;
                _cache[key] = value;
                return;
            }

            if (_B2Nodes.ContainsKey(key))
            {
                _p = Math.Max(0, _p - Math.Max(_B1.Count / Math.Max(_B2.Count, 1), 1));
                Replace(key);
                LinkedListNode<TKey> nodeB2 = _B2Nodes[key]!;
                _B2.Remove(nodeB2);
                _B2Nodes.Remove(key);
                LinkedListNode<TKey> nodeT2 = _T2.AddFirst(key);
                _T2Nodes[key] = nodeT2;
                _cache[key] = value;
                return;
            }

            if ((_T1.Count + _T2.Count) == _capacity)
            {
                if (_T1.Count < _capacity)
                {
                    if (_B1.Count > 0)
                    {
                        LinkedListNode<TKey>? tailB1 = _B1.Last;
                        if (tailB1 != null)
                        {
                            TKey keyToRemove = tailB1.Value;
                            _B1.RemoveLast();
                            _B1Nodes.Remove(keyToRemove);
                        }
                    }

                    Replace(key);
                }
                else
                {
                    LinkedListNode<TKey>? tailT1 = _T1.Last;
                    if (tailT1 != null)
                    {
                        TKey keyToRemove = tailT1.Value;
                        _T1.RemoveLast();
                        _T1Nodes.Remove(keyToRemove);
                        _cache.Remove(keyToRemove);
                    }
                }
            }
            else
            {
                int total = _T1.Count + _T2.Count + _B1.Count + _B2.Count;
                if (total >= _capacity)
                {
                    if (total == (2 * _capacity))
                    {
                        if (_B2.Count > 0)
                        {
                            LinkedListNode<TKey>? tailB2 = _B2.Last;
                            if (tailB2 != null)
                            {
                                TKey keyToRemove = tailB2.Value;
                                _B2.RemoveLast();
                                _B2Nodes.Remove(keyToRemove);
                            }
                        }
                    }
                }
            }

            LinkedListNode<TKey> nodeT1 = _T1.AddFirst(key);
            _T1Nodes[key] = nodeT1;
            _cache[key] = value;
        }

        private void Replace(TKey key)
        {
            if (_T1.Count > 0 && ((_B2Nodes.ContainsKey(key) && _T1.Count == _p) || (_T1.Count > _p)))
            {
                LinkedListNode<TKey>? tailT1 = _T1.Last;
                if (tailT1 != null)
                {
                    TKey oldKey = tailT1.Value;
                    _T1.RemoveLast();
                    _T1Nodes.Remove(oldKey);
                    LinkedListNode<TKey> nodeB1 = _B1.AddFirst(oldKey);
                    _B1Nodes[oldKey] = nodeB1;
                    _cache.Remove(oldKey);
                }
            }
            else
            {
                if (_T2.Count > 0)
                {
                    LinkedListNode<TKey>? tailT2 = _T2.Last;
                    if (tailT2 != null)
                    {
                        TKey oldKey = tailT2.Value;
                        _T2.RemoveLast();
                        _T2Nodes.Remove(oldKey);
                        LinkedListNode<TKey> nodeB2 = _B2.AddFirst(oldKey);
                        _B2Nodes[oldKey] = nodeB2;
                        _cache.Remove(oldKey);
                    }
                }
            }
        }
    }
}