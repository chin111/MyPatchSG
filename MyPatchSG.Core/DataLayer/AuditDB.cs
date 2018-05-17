using System;
using System.IO;

namespace MyPatchSG.DL
{
    using System.Collections.Generic;
    using System.Linq;
    using SQLite;
    using SQLitePCL;

    using MyPatchSG.DL.Models;
    public class AuditDB
    {
        //private SQLiteAsyncConnection database;
        public string dbPath = "";

        /// <summary>
        /// Initializes a new instance of the AuditDB class.
        /// </summary>
        //public AuditDB(string dbPath)
        //{
        //    database = new SQLiteAsyncConnection(dbPath);
        //}

        public AuditDB()
        {
        }

        public List<RCSTE> GetRCSTE()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<RCSTE>("SELECT * FROM RCS_TE");
                //return database.QueryAsync<RCSTE>("SELECT * FROM RCS_TE").Result;
            }
        }

        public List<RCSTE> GetRCSTEByID(string teID)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<RCSTE>("SELECT * FROM RCS_TE WHERE TE_ID='" + teID + "'");
                //return database.QueryAsync<RCSTE>("SELECT * FROM RCS_TE WHERE TE_ID='" + teID + "'").Result;
            }
        }
        public int DeleteRCSTE(string TE_ID)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                int rowAffected = database.Execute("DELETE FROM RCS_TE WHERE lower(trim(TE_ID))=lower(trim('" + TE_ID + "'))");
                Console.WriteLine("Master DB Table RCS_TE row deleted: " + rowAffected.ToString());
                return rowAffected;
            }
        }
        public int InsertRCSTE(string FIELD_ID, string TE_ID, string FIELD_VALUE, string LAST_UPDATE)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                int rowAffected = database.Execute("INSERT INTO RCS_TE(FIELD_ID, TE_ID, FIELD_VALUE, LAST_UPDATE) VALUES(?, ?, ?, ?)", new object[] { FIELD_ID, TE_ID, FIELD_VALUE, LAST_UPDATE });
                Console.WriteLine("Audit DB Table RCS_TE row inserted: " + rowAffected.ToString());
                return rowAffected;
            }
        }

        public void UpdateRCSTE(string remark, string updateDate, string teID)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                database.Execute("UPDATE RCS_TE SET FIELD_VALUE='" + remark + "', LAST_UPDATE='" + updateDate + "' WHERE TE_ID='" + teID + "'");
            }
        }

        public List<RCSOUTLET> GetRCSOUTLET()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<RCSOUTLET>("SELECT * FROM RCS_OUTLET");
                //return database.QueryAsync<RCSOUTLET>("SELECT * FROM RCS_OUTLET").Result;
            }
        }

        public List<RCSOUTLET> GetRCSOUTLETByID(string CustomerID)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<RCSOUTLET>("SELECT * FROM RCS_OUTLET WHERE CUSTOMER_ID='" + CustomerID + "'");
                //return database.QueryAsync<RCSOUTLET>("SELECT * FROM RCS_OUTLET WHERE CUSTOMER_ID='" + CustomerID + "'").Result;
            }
        }

        public int InsertRCSOutlet(string FIELD_ID, string CUSTOMER_ID, string FIELD_VALUE, string LAST_UPDATE)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                int rowAffected = 0;

                try
                {
                    rowAffected = database.Execute("INSERT INTO RCS_OUTLET(FIELD_ID, CUSTOMER_ID, FIELD_VALUE, LAST_UPDATE) VALUES(?, ?, ?, ?)", new object[] { FIELD_ID, CUSTOMER_ID, FIELD_VALUE, LAST_UPDATE });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("Audit DB Table RCS_OUTLET row inserted: " + rowAffected.ToString());
                return rowAffected;
            }
        }
        public int DeleteRCSOutletByID(string CUSTOMER_ID)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                int rowAffected = 0;

                try
                {
                    rowAffected = database.Execute("DELETE FROM RCS_OUTLET WHERE lower(trim(CUSTOMER_ID))=lower(trim('" + CUSTOMER_ID + "'))");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("Audit DB Table RCS_OUTLET row deleted: " + rowAffected.ToString());
                return rowAffected;
            }
        }

        public void UpdateRCSOutletByID(string remark, string updateDate, string CustomerID)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                database.Execute("UPDATE RCS_OUTLET SET FIELD_VALUE='" + remark + "', LAST_UPDATE='" + updateDate + "' WHERE CUSTOMER_ID='" + CustomerID + "'");
                //database.ExecuteAsync("UPDATE RCS_OUTLET SET FIELD_VALUE='" + remark + "', LAST_UPDATE='" + updateDate + "' WHERE CUSTOMER_ID='" + CustomerID + "'");
            }
        }

        public void CleanDatabase()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                int rowAffected = 0;

                rowAffected = database.Execute("DELETE FROM RCS_TE");
                Console.WriteLine("Audit DB Table RCS_TE row deleted: " + rowAffected.ToString());

                rowAffected = database.Execute("DELETE FROM RCS_OUTLET");
                Console.WriteLine("Audit DB Table RCS_OUTLET row deleted: " + rowAffected.ToString());
            }
        }
    }
}
