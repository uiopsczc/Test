using System.Collections.Generic;
using System;

namespace CsCat
{
    /// <summary>
    /// WeakReferenceDictionary
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class WeakReferenceDictionary<K, V>
    {
        private Dictionary<K, WeakReference> dict = new Dictionary<K, WeakReference>();
        private List<K> toRemoveList = new List<K>();
        public List<V> valueList = new List<V>();


        public ICollection<K> Keys => this.dict.Keys;

        public ICollection<WeakReference> ReferenceValues => this.dict.Values;

        public List<V> Values
        {
            get
            {
                valueList.Clear();
                foreach (K current in this.dict.Keys)
                    if (TryGetValue(current, out var v))
                        valueList.Add(v);
                return valueList;
            }
        }

        public V this[K key]
        {
            get => TryGetValue(key, out var v) ? v : default;
            set => this.Add(key, value);
        }


        public void Add(K key, V value)
        {
            if (!this.dict.ContainsKey(key))
            {
                this.dict.Add(key, new WeakReference(value));
                return;
            }

            this.dict[key] = new WeakReference(value);
        }

        public void Clear()
        {
            this.dict.Clear();
        }

        public bool ContainsKey(K key)
        {
            return this.dict.ContainsKey(key);
        }

        public bool Remove(K key)
        {
            return this.dict.Remove(key);
        }

        public bool TryGetValue(K key, out V value)
        {
            if (this.dict.ContainsKey(key))
            {
                var valueResult = this.dict[key].GetValueResult<V>();
                value = valueResult.GetValue();
                return valueResult.GetIsHasValue();
            }

            value = default;
            return false;
        }

        public void GC()
        {
            toRemoveList.Clear();
            foreach (var e in dict.Keys)
            {
                if (!dict[e].IsAlive)
                    toRemoveList.Add(e);
            }

            if (toRemoveList.Count <= 0) return;
            foreach (var e in toRemoveList)
                dict.Remove(e);
            System.GC.Collect();
        }
    }
}