using Xamarin.Forms;
using MyPatchSG.DL;
using MyPatchSG.Droid.DataLayer;

[assembly: Dependency(typeof(SQLiteClient))]
namespace MyPatchSG.Droid.DataLayer
{
    using System;
    using System.IO;
    using SQLite;

    public class SQLiteClient : IDBConnection
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var sqliteFilename = "mypatchsg.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentsPath, sqliteFilename);

            var connection = new SQLiteAsyncConnection(path);

            return connection;
        }
    }
}

