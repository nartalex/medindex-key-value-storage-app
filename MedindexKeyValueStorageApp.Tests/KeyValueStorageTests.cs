using System;
using System.Collections.Generic;
using System.Linq;
using MedindexKeyValueStorageApp.Service;
using MedindexKeyValueStorageApp.Service.ConcurrentDictionaryImpl;
using NUnit.Framework;

namespace MedindexKeyValueStorageApp.Tests
{
    public class KeyValueStorageTests
    {
        private IKeyValueStorage _storage;
        private Dictionary<string, object> _valuesToAdd;

        [SetUp]
        public void Setup()
        {
            _storage = new ConcurrentDictionaryKeyValueStorageImpl();
            _valuesToAdd = new()
            {
                { "1", 1 },
                { "2", 2d },
                { "3", "3" },
            };
        }

        [Test, Order(0)]
        public void InitialStateTest()
        {
            Assert.AreEqual(0, _storage.GetKeys().Count());
        }

        [Test, Order(1)]
        public void AddTest()
        {
            foreach (var valueToAdd in _valuesToAdd)
            {
                _storage.AddOrUpdate(valueToAdd);
            }

            var keysInStorage = _storage.GetKeys();
            Assert.AreEqual(_valuesToAdd.Count, keysInStorage.Count());

            foreach (var addedValue in _valuesToAdd)
            {
                Assert.IsTrue(keysInStorage.Contains(addedValue.Key));
            }
        }

        [Test, Order(2)]
        public void GetTest()
        {
            foreach (var valueToAdd in _valuesToAdd)
            {
                var valueInStorage = _storage.GetValue(valueToAdd.Key);

                Assert.AreEqual(valueToAdd.Value, valueInStorage);
            }
        }

        [Test, Order(3)]
        public void MissingValueGetTest()
        {
            try
            {
                _storage.GetValue("MissingValueGetTest");
                Assert.Fail();
            }
            catch (KeyNotFoundException)
            {
                Assert.Pass();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test, Order(4)]
        public void UpdateTest()
        {
            const string keyToUpdate = "3";
            const string newValue = "333";

            _storage.AddOrUpdate(new KeyValuePair<string, object>(keyToUpdate, newValue));
            var valueInStorage = _storage.GetValue(keyToUpdate);

            Assert.AreEqual(newValue, valueInStorage);
        }

        [Test, Order(5)]
        public void DeleteTest()
        {
            const string keyToDelete = "3";

            _storage.Delete(keyToDelete);

            Assert.AreEqual(_valuesToAdd.Count - 1, _storage.GetKeys().Count());

            try
            {
                _storage.GetValue(keyToDelete);
                Assert.Fail();
            }
            catch (KeyNotFoundException)
            {
                Assert.Pass();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}