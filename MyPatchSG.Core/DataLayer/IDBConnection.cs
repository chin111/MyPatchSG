namespace MyPatchSG.DL
{
    using SQLite;

    public interface IDBConnection
    {
        SQLiteAsyncConnection GetConnection();
    }
}

