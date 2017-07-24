using System;
using System.IO;
using Xamarin.Forms;
using FST.Persistance;
using FST.Droid.Persistance;
using SQLite;

[assembly: Dependency(typeof(SQLiteDb))]

namespace FST.Droid.Persistance
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite_FST.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}