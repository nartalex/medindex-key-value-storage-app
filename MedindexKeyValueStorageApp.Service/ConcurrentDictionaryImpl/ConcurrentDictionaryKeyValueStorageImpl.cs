using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MedindexKeyValueStorageApp.Service.ConcurrentDictionaryImpl
{
    public class ConcurrentDictionaryKeyValueStorageImpl : IKeyValueStorage
    {
        private static readonly Lazy<ConcurrentDictionary<string, object>> _storage = new(() => new());

        public IEnumerable<string> GetKeys()
        {
            return _storage.Value.Keys;
        }

        public object GetValue(string key)
        {
            return _storage.Value[key];
        }

        public void AddOrUpdate(KeyValuePair<string, object> item)
        {
            _storage.Value[item.Key] = item.Value;
        }

        public void Delete(string key)
        {
            _storage.Value.TryRemove(key, out _);
        }
    }
}
