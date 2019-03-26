using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LuzFaltex.Utilities.Extensions;

namespace LuzFaltex.Utilities.Collections
{
    /// <summary>
    /// Represents a bi-directional collection of keys and values.
    /// </summary>
    /// <typeparam name="TFirst">The type of the keys in the dictionary</typeparam>
    /// <typeparam name="TSecond">The type of the values in the dictionary</typeparam>
    /// <inheritdoc />
    [System.Diagnostics.DebuggerDisplay("Count = {{{nameof(Count)}}}")]
    public class BiDictionary<TFirst, TSecond>
        : IDictionary<TFirst, TSecond>, IEquatable<BiDictionary<TFirst, TSecond>>
    {
        private Dictionary<TFirst, TSecond> _keyValue;

        private Dictionary<TSecond, TFirst> _valueKey;
        /// <summary>
        /// PropertyAccessor for Iterator over KeyValue-Relation
        /// </summary>
        public IReadOnlyDictionary<TFirst, TSecond> KeyValue => _keyValue;
        /// <summary>
        /// PropertyAccessor for Iterator over ValueKey-Relation
        /// </summary>
        public IReadOnlyDictionary<TSecond, TFirst> ValueKey => _valueKey;

        public int Count => _keyValue.Count;

        public ICollection<TFirst> Keys => _keyValue.Keys;
        public ICollection<TSecond> Values => _valueKey.Keys;

        public bool IsReadOnly => false;

        public BiDictionary()
        {
            _keyValue = new Dictionary<TFirst, TSecond>();
            _valueKey = new Dictionary<TSecond, TFirst>();
        }

        public BiDictionary(Dictionary<TFirst, TSecond> dictionary)
        {
            bool AllValuesUnique<T>(ICollection<T> collection)
            {
                var diffChecker = new HashSet<T>();
                return collection.All(diffChecker.Add);
            }

            if (!AllValuesUnique(dictionary.Values)) throw new ArgumentException("Dictionary must have a set of unique key-value pairs.", nameof(dictionary));

            _keyValue = new Dictionary<TFirst, TSecond>(dictionary);

            _valueKey = dictionary
                .ToDictionary(v => v.Value, k => k.Key);
        }

        public BiDictionary(KeyValuePair<TFirst, TSecond>[] array)
            : this(array.ToDictionary(k => k.Key, v => v.Value)) { }

        #region Implemented Members

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key. If the specified key is not found,
        ///     a get operation throws a <see cref="KeyNotFoundException"/>, and
        ///     a set operation creates a new element with the specified key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        /// <exception cref="KeyNotFoundException">The property is retrieved and <paramref name="key"/> does not exist in the collection.</exception>
        /// <exception cref="ArgumentException"> An element with the same key already exists in the <see cref="ValueKey"/> <see cref="Dictionary{TFirst,TSecond}"/>.</exception>
        public TSecond this[TFirst key]
        {
            get => KeyValue[key];
            set
            {
                _valueKey.Remove(KeyValue[key]);
                _keyValue[key] = value;
                _valueKey.Add(value, key);
            }
        }

        /// <summary>
        /// Gets or sets the key associated with the specified value.
        /// </summary>
        /// <param name="val">The value of the key to get or set.</param>
        /// <returns>The key associated with the specified value. If the specified value is not found,
        ///     a get operation throws a <see cref="KeyNotFoundException"/>, and
        ///     a set operation creates a new element with the specified value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="KeyNotFoundException">The property is retrieved and <paramref name="value"/> does not exist in the collection.</exception>
        /// <exception cref="ArgumentException">An element with the same value already exists in the <see cref="KeyValue"/> <see cref="Dictionary{TFirst,TSecond}"/>.</exception>
        public TFirst this[TSecond val]
        {
            get => _valueKey[val];
            set
            {
                _keyValue.Remove(_valueKey[val]);
                _valueKey[val] = value;
                _keyValue.Add(value, val);
            }
        }

        /// <summary>
        /// Adds the specified key and value to the dictionary. This is a nuclear operation.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> or <paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException">An element with the same key or value already exists in the <see cref="KeyValue"/> <see cref="Dictionary{TFirst,TSecond}"/>.</exception>
        public void Add(TFirst key, TSecond value)
        {
            // Ensure nuclear operation
            if (_keyValue.ContainsKey(key) || _valueKey.ContainsKey(value)) return;

            _keyValue.Add(key, value);
            _valueKey.Add(value, key);
        }

        /// <summary>
        /// Adds the specified key and value to the dictionary. This is a nuclear operation.
        /// </summary>
        /// <param name="keyValuePair">A <see cref="KeyValuePair{TFirst,TSecond}"/> containing the key and value of the element to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="keyValuePair"/> is null.</exception>
        /// <exception cref="ArgumentException">An element with the same key or value already exists in the <see cref="KeyValue"/> <see cref="Dictionary{TFirst,TSecond}"/>.</exception>
        public void Add(KeyValuePair<TFirst, TSecond> keyValuePair)
            => Add(keyValuePair.Key, keyValuePair.Value);

        /// <summary>
        /// Removes all keys and values from the <see cref="Dictionary{TFirst,TSecond}"/>.
        /// </summary>
        public void Clear()
        {
            _keyValue.Clear();
            _valueKey.Clear();;
        }

        /// <summary>
        /// Determines whether the <see cref="Dictionary{TFirst,TSecond}"/> contains the specified <see cref="KeyValuePair{TFirst,TSecond}"/>.
        /// </summary>
        /// <param name="key">The key of the KeyValuePair to locate in the <see cref="Dictionary{TFirst,TSecond}"/>.</param>
        /// <param name="value">The value of the KeyValuePair to locate in the <see cref="Dictionary{TFirst,TSecond}"/>.</param>
        /// <returns>True if the <see cref="Dictionary{TFirst,TSecond}"/> contains an element with the specified key which links to the specified value; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> or <paramref name="value"/> is null.</exception>
        public bool Contains(TFirst key, TSecond value)
            => _keyValue.ContainsKey(key) && _valueKey.ContainsKey(value);

        /// <summary>
        /// Determines whether the <see cref="Dictionary{TFirst,TSecond}"/> contains the specified <see cref="KeyValuePair{TFirst,TSecond}"/>.
        /// </summary>
        /// <param name="item">The KeyValuePair to locate in the <see cref="Dictionary{TFirst,TSecond}"/>.</param>
        /// <returns>True if the <see cref="Dictionary{TFirst,TSecond}"/> contains an element with the specified key which links to the specified value; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        public bool Contains(KeyValuePair<TFirst, TSecond> item)
            => Contains(item.Key, item.Value);

        /// <summary>
        /// Determines whether the the specified Key is contained within the <see cref="KeyValue"/> <see cref="Dictionary{TFirst,TSecond}"/>.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        /// <returns>True if the specified key is found; otherwise, false.</returns>
        public bool ContainsKey(TFirst key)
            => _keyValue.ContainsKey(key);

        /// <summary>
        /// Determines whether the the specified Key is contained within the <see cref="ValueKey"/> <see cref="Dictionary{TSecond,TFirst}"/>.
        /// </summary>
        /// <param name="value">The value to look up.</param>
        /// <returns>True if the specified value is found; otherwise, false.</returns>
        public bool ContainsValue(TSecond value)
            => _valueKey.ContainsKey(value);

        [Obsolete("Use RemoveByKey(TFirst key) instead.")]
        bool IDictionary<TFirst,TSecond>.Remove(TFirst key)
            => RemoveByKey(key);

        /// <summary>
        /// Removes the specified value with the specified key from the <see cref="BiDictionary{TFirst,TSecond}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>True if the element is successfully found and removed; otherwise, false.
        ///     This method returns false if the <paramref name="key"/> and its associated value is not found in the <see cref="Dictionary{TFirst,TSecond}"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        public bool RemoveByKey(TFirst key)
        {
            TSecond value = _keyValue[key];
            // Ensure nuclear operation
            if (!Contains(key, value)) return false;

            return _valueKey.Remove(value) && _keyValue.Remove(key);
        }

        /// <summary>
        /// Removes the specified value with the specified key from the <see cref="BiDictionary{TFirst,TSecond}"/>.
        /// </summary>
        /// <param name="value">The value of the element to remove.</param>
        /// <returns>True if the element is successfully found and removed; otherwise, false.
        ///     This method returns false if the <paramref name="value"/> and its associated key is not found in the <see cref="BiDictionary{TFirst,TSecond}"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public bool RemoveByValue(TSecond value)
        {
            TFirst key = _valueKey[value];
            // Ensure nuclear operation
            if (!Contains(key, value)) return false;

            return _valueKey.Remove(value) && _keyValue.Remove(key);
        }

        /// <Summary>Removes the specified KeyValuePair from the <see cref="BiDictionary{TFirst,TSecond}"/>.</Summary>
        /// <param name="item">The KeyValuePair to remove.</param>
        /// <Returns>true if the KeyValuePair is successfully found and removed; otherwise, false. This
        ///      method returns false if <paramref name="item"/> is not found in the <see cref="BiDictionary{TFirst,TSecond}"/>.</Returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        public bool Remove(KeyValuePair<TFirst, TSecond> item)
        {
            // Ensure nuclear operation
            if (!Contains(item.Key, item.Value)) return false;
            
            return _valueKey.Remove(item.Value) && _keyValue.Remove(item.Key);
        }

        /// <summary>
        /// Gets the key associated with the specified value.
        /// </summary>
        /// <param name="value">The value to look up.</param>
        /// <param name="key">When this method returns, contains the key associated with the specified value
        ///     if the value is found; otherwise, the default value for the type of <see cref="TFirst"/>.</param>
        /// <returns>True if the <see cref="ValueKey"/> <see cref="Dictionary{TSecond,TFirst}"/> contains an element with the specified value;
        ///     otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public bool TryGetKey(TSecond value, out TFirst key)
            => _valueKey.TryGetValue(value, out key);

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key.</param>
        /// <returns>True if the <see cref="KeyValue"/> <see cref="Dictionary{TFirst,TSecond}"/> contains an element with the specified key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        public bool TryGetValue(TFirst key, out TSecond value)
            => _keyValue.TryGetValue(key, out value);

        public void CopyTo(KeyValuePair<TFirst, TSecond>[] array, int arrayIndex)
            => _keyValue.ToList().CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<TFirst, TSecond>> GetEnumerator()
            => _keyValue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public bool Equals(BiDictionary<TFirst, TSecond> other)
            => Count.Equals(other.Count) && _keyValue.SequenceEqual(other._keyValue);

        #endregion

        public static explicit operator BiDictionary<TFirst, TSecond>(Dictionary<TFirst, TSecond> dictionary)
        {
            return new BiDictionary<TFirst, TSecond>(dic);
        }
    }
}
