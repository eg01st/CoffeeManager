using System;
using System.Collections.Generic;

namespace CoffeManager.Common
{
    public interface IDataBaseProvider
    {
        void CreateTableIfNotExists(Type type);

        int Add<T>(T item) where T : new();

        void Remove<T>(T item) where T : new();

        IEnumerable<T> Get<T>() where T : new();

        void Update<T>(T item) where T : new();
    }
}
