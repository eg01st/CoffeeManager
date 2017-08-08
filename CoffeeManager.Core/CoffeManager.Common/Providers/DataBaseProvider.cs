using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Plugins.Sqlite;
using SQLite;

namespace CoffeManager.Common
{
    public class DataBaseProvider : IDataBaseProvider
    {
        private SQLiteConnection connection;
        readonly IMvxSqliteConnectionFactory factory;

        public DataBaseProvider(IMvxSqliteConnectionFactory factory)
        {
            this.factory = factory;
        }

        public void InitConnection()
        {
            connection = factory.GetConnection("data.dat");
        }

        public void CreateTableIfNotExists<T>()
        {
            connection.CreateTable(typeof(T), CreateFlags.AutoIncPK);
        }

        public int Add<T>(T item) where T : new()
        {
            return connection.Insert(item, typeof(T));
        }

        public void Remove<T>(T item) where T : new()
        {
            connection.Delete(item);
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

        public void ClearTable<T>() where T : new()
        {
            connection.DeleteAll<T>();
        }

        public void DeleteTable<T>() where T : new()
        {
            connection.DropTable<T>();
        }
    }
}
