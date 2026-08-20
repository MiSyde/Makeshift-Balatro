using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models
{
    public class ObservableDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : notnull
    {
        private Dictionary<TKey, TValue> _dictionary;
        public event Action ValueChanged;

        public ObservableDictionary(Action ValueChanged) { _dictionary = new Dictionary<TKey, TValue>(); this.ValueChanged = ValueChanged; }

        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set
            {
                _dictionary.TryGetValue(key, out var oldValue);
                bool existed = _dictionary.ContainsKey(key);

                if (!existed || !EqualityComparer<TValue>.Default.Equals(oldValue!, value))
                {
                    _dictionary[key] = value;
                    ValueChanged?.Invoke();
                }
                else
                {
                    _dictionary[key] = value;
                }
            }
        }

        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);
        public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value!);
        public bool Remove(TKey key) => _dictionary.Remove(key);
        public int Count => _dictionary.Count;

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Dictionary<TKey, TValue>.ValueCollection getValues() { return _dictionary.Values; } 
        public Dictionary<TKey, TValue>.KeyCollection getKeys() { return _dictionary.Keys; } 

    }
}
