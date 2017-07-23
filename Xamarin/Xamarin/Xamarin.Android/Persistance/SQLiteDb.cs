using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Persistance;
using SQLite;

[assembly: Dependency(typeof(SQLiteDb))]

namespace Xamarin.Persistance
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

