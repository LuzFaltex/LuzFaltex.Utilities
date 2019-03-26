using System;
using System.Collections.Generic;
using System.Linq;

namespace LuzFaltex.Utilities.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds the values from the specified Dictionary to this dictionary's collection.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="second">The dictionary to merge into this one.</param>
        /// <param name="mergeOptions">An enum which determines how merge conflicts should be handled.</param>
        /// <returns>The modified dictionary</returns>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> source, Dictionary<TKey, TValue> second,
            DictionaryMergeOptions mergeOptions)
        {
            switch (mergeOptions)
            {
                case DictionaryMergeOptions.IgnoreDuplicates:
                    foreach (var kvp in second)
                    {
                        if (!source.ContainsKey(kvp.Key))
                            source.Add(kvp);
                    }
                    break;
                case DictionaryMergeOptions.Overwrite:
                    foreach (var kvp in second)
                    {
                        // Overwite the value if the key exists
                        if (source.ContainsKey(kvp.Key))
                            source[kvp.Key] = kvp.Value;
                        else
                            source.Add(kvp);
                    }
                    break;
                case DictionaryMergeOptions.Throw:
                    List<Exception> exceptions = new List<Exception>();

                    // In order to make this option atomic, we're just going to calculate 
                    var duplicates = source.Keys.Where(x => second.ContainsKey(x));

                    foreach (var key in duplicates)
                    {
                        exceptions.Add(new ArgumentException("Duplicate key found", nameof(key)));
                    }

                    if (exceptions.Count > 0)
                        throw new AggregateException(exceptions);
                    else
                    {
                        foreach (var kvp in second)
                            source.Add(kvp);
                    }
                    break;
            }

            return source;
        }

        public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> source, KeyValuePair<TKey, TValue> pair)
            => source?.Add(pair.Key, pair.Value);

        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> source, KeyValuePair<TKey, TValue> pair)
            => source.TryAdd(pair.Key, pair.Value);
        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, TValue value)
        {
            if (source.ContainsKey(key))
                return false;

            source?.Add(key, value);
            return true;
        }
    }

    public enum DictionaryMergeOptions
    {
        /// <summary>
        /// Duplicate keys will be ignored and skipped
        /// </summary>
        IgnoreDuplicates,
        /// <summary>
        /// Existing values will be overwritten by the value being added. New keys will be added as normal.
        /// </summary>
        Overwrite,
        /// <summary>
        /// Throw an exception as if trying to add a duplicate key
        /// </summary>
        Throw
    }
}
