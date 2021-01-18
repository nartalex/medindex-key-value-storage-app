using System.Collections.Generic;

namespace MedindexKeyValueStorageApp.Service
{
    public interface IKeyValueStorage
    {
        IEnumerable<string> GetKeys();

        object GetValue(string key);

        void AddOrUpdate(KeyValuePair<string, object> item);

        void Delete(string key);
    }
}
