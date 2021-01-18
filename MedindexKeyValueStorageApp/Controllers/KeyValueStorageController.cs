using MedindexKeyValueStorageApp.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MedindexKeyValueStorageApp.Api.Controllers
{
    [Route("api/storage")]
    public class KeyValueStorageController
    {
        private readonly IKeyValueStorage _storage;

        public KeyValueStorageController(IKeyValueStorage storage)
        {
            _storage = storage;
        }

        [HttpGet, Route("keys")]
        public IEnumerable<string> GetKeys()
        {
            return _storage.GetKeys();
        }

        [HttpGet, Route("{key}")]
        public object GetValue(string key)
        {
            return _storage.GetValue(key);
        }

        [HttpPost, Route("add-or-update")]
        public void AddOrUpdate([FromBody]KeyValuePair<string, object> item)
        {
            _storage.AddOrUpdate(item);
        }

        [HttpDelete, Route("{key}")]
        public void Delete(string key)
        {
            _storage.Delete(key);
        }
    }
}
