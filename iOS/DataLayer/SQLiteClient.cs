using Xamarin.Forms;
using MyPatchSG.DL;
using MyPatchSG.iOS.DataLayer;

[assembly: Dependency(typeof(SQLiteClient))]
namespace MyPatchSG.iOS.DataLayer
{
    using System;
    using System.IO;
    using SQLite;

    public class SQLiteClient : IDBConnection
    {
        public SQLiteClient()
        {
        }

        public SQLiteAsyncConnection GetConnection()
        {
            var sqliteFilename = "mypatchsg.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, sqliteFilename);

            var connection = new SQLiteAsyncConnection(path);

            return connection;
        }
    }
}

