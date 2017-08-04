using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Plugins.Sqlite;
using SQLite;

namespace CoffeManager.Common
{
    public class DataBaseProvider
    {
        private readonly SQLiteConnection connection;

        public DataBaseProvider(IMvxSqliteConnectionFactory factory)
        {
            connection = factory.GetConnection("data.dat");
        }

        public void CreateTableIfNotExists(Type type)
        {
            bool tableExists = connection.TableMappings.Any(t => t.MappedType == type);
            if(tableExists)
            {
                return;
            }
            else
            {
                connection.CreateTable(type, CreateFlags.AllImplicit);
            }
        }

        public int Add<T>(T item) where T : new()
        {
            connection.BeginTransaction();
            var id = connection.Insert(item, typeof(T));
            connection.Commit();
            return id;
        }

        public void Remove<T>(T item) where T : new()
        {
            connection.BeginTransaction();
            connection.Delete(item);
            connection.Commit();
        }

        public IEnumerable<T> Get<T>() where T : new()
        {
            var table = connection.Table<T>();
            return table.AsEnumerable<T>();
        }

        public void Update<T>(T item) where T : new ()
        {
            connection.BeginTransaction();
            connection.Update(item, typeof(T));
            connection.Commit();
        }
    }
}
