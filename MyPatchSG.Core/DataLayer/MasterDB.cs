using System;
using System.IO;

namespace MyPatchSG.DL
{
    using System.Collections.Generic;
    using System.Linq;
    using SQLite;

    using MyPatchSG.DL.Models;

    public class MasterDB
    {
        //private SQLiteAsyncConnection database;
        public string dbPath = "";

        /// <summary>
        /// Initializes a new instance of the MasterDB class.
        /// </summary>
        //public MasterDB(string dbPath)
        //{
        //    database = new SQLiteAsyncConnection(dbPath);
        //}
        public MasterDB()
        {
        }

        public List<vwOutletList> GetOutletList()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<vwOutletList>("SELECT * FROM vwOUTLET_LIST");
                //return database.QueryAsync<vwOutletList>("SELECT * FROM vwOUTLET_LIST").Result;
            }
        }

        public List<vwOutletList> GetOutletListSortedBy(string sortedby)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<vwOutletList>("SELECT * FROM vwOUTLET_LIST ORDER BY " + sortedby);
                //return database.QueryAsync<vwOutletList>("SELECT * FROM vwOUTLET_LIST ORDER BY " + sortedby).Result;
            }
        }

        public List<OutletTask> GetAllOutletTaskList()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<OutletTask>("SELECT * FROM OUTLET_TASK");
                //return database.QueryAsync<OutletTask>("SELECT * FROM OUTLET_TASK").Result;
            }
        }

        public List<OutletTask> GetOutletTaskListByProjectCode(string ProjectCode)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<OutletTask>("SELECT * FROM OUTLET_TASK WHERE PROJECT_CODE='" + ProjectCode + "'");
                //return database.QueryAsync<OutletTask>("SELECT * FROM OUTLET_TASK WHERE PROJECT_CODE='" + ProjectCode + "'").Result;
            }
        }

        public List<vwTE> GetViewTE()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<vwTE>("SELECT * FROM vwTE");
                //return database.QueryAsync<vwTE>("SELECT * FROM vwTE").Result;
            }
        }
        public List<vwSalesTE> GetViewSalesTE()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<vwSalesTE>("SELECT * FROM vwSales_TE");
                //return database.QueryAsync<vwSalesTE>("SELECT * FROM vwSales_TE").Result;
            }
        }
        public List<vwSalesTEChart> GetViewSalesTEChart()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<vwSalesTEChart>("SELECT * FROM vwSales_TE_CHART");
                //return database.QueryAsync<vwSalesTEChart>("SELECT * FROM vwSales_TE_CHART").Result;
            }
        }
        public List<vwSalesTEChart> GetViewSalesTEChartTotal()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<vwSalesTEChart>("SELECT * FROM vwSales_TE_CHART WHERE SALES_TYPE='Total' ORDER BY WK ASC");
                //return database.QueryAsync<vwSalesTEChart>("SELECT * FROM vwSales_TE_CHART WHERE SALES_TYPE='Total' ORDER BY WK ASC").Result;
            }
        }
        public List<RCSTE> GetRCSTE()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<RCSTE>("SELECT * FROM RCS_TE");
                //return database.QueryAsync<RCSTE>("SELECT * FROM RCS_TE").Result;
            }
        }
        public void UpdateRCSTE(string remark, string updateDate)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                int rowAffected = database.Execute("UPDATE RCS_TE SET FIELD_VALUE='" + remark + "', LAST_UPDATE='" + updateDate + "'");
                Console.WriteLine("Master DB Table RCS_TE row updated: " + rowAffected.ToString());
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
                Console.WriteLine("Master DB Table RCS_TE row inserted: " + rowAffected.ToString());
                return rowAffected;
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
        public void UpdateRCSOutletByID(string remark, string updateDate, string CustomerID)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                int rowAffected = database.Execute("UPDATE RCS_OUTLET SET FIELD_VALUE='" + remark + "', LAST_UPDATE='" + updateDate + "' WHERE CUSTOMER_ID='" + CustomerID + "'");
                Console.WriteLine("Master DB Table RCS_OUTLET row updated: " + rowAffected.ToString());
            }
        }
        public void DeleteRCSOutletByID(string CustomerID)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                int rowAffected = database.Execute("DELETE FROM RCS_OUTLET WHERE lower(trim(CUSTOMER_ID))=lower(trim('" + CustomerID + "'))");
                Console.WriteLine("Master DB Table RCS_OUTLET row deleted: " + rowAffected.ToString());
            }
        }
        public int InsertRCSOutlet(string FIELD_ID, string CUSTOMER_ID, string FIELD_VALUE, string LAST_UPDATE)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                int rowAffected = database.Execute("INSERT INTO RCS_OUTLET(FIELD_ID, CUSTOMER_ID, FIELD_VALUE, LAST_UPDATE) VALUES(?, ?, ?, ?)", new object[] { FIELD_ID, CUSTOMER_ID, FIELD_VALUE, LAST_UPDATE });
                Console.WriteLine("Master DB Table RCS_OUTLET row inserted: " + rowAffected.ToString());
                return rowAffected;
            }
        }

        public List<LKWk> GetLKWk()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<LKWk>("SELECT * FROM LK_Wk ORDER BY FIELD_ID");
                //return database.QueryAsync<LKWk>("SELECT * FROM LK_Wk ORDER BY FIELD_ID").Result;
            }
        }
        public List<vwSalesOutletChart> GetViewSalesOutletChart()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<vwSalesOutletChart>("SELECT * FROM vwSales_OUTLET_CHART");
                //return database.QueryAsync<vwSalesOutletChart>("SELECT * FROM vwSales_OUTLET_CHART").Result;
            }
        }
        public List<vwSalesOutletChart> GetViewSalesOutletChartByID(string CustomerID)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<vwSalesOutletChart>("SELECT * FROM vwSales_OUTLET_CHART WHERE CUSTOMER_ID='" + CustomerID + "' AND SALES_TYPE='Total' ORDER BY WK ASC");
                //return database.QueryAsync<vwSalesOutletChart>("SELECT * FROM vwSales_OUTLET_CHART WHERE CUSTOMER_ID='" + CustomerID + "' AND SALES_TYPE='Total' ORDER BY WK ASC").Result;
            }
        }
        public RCSOUTLET GetRCSOUTLETByID(string CustomerID)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                var result = database.Query<RCSOUTLET>("SELECT * FROM RCS_OUTLET WHERE CUSTOMER_ID='" + CustomerID + "'");
                if (result != null && result.Count > 0)
                {
                    return result.First();
                }

                return null;
            }
        }
        public List<OutletTask> GetOutletTaskByID(string CustomerID)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<OutletTask>("SELECT * FROM OUTLET_TASK WHERE CUSTOMER_ID='" + CustomerID + "'");
                //return database.QueryAsync<OutletTask>("SELECT * FROM OUTLET_TASK WHERE CUSTOMER_ID='" + CustomerID + "'").Result;
            }
        }
        public List<vwSalesOutlet> GetViewSalesOutletByID(string CustomerID)
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                return database.Query<vwSalesOutlet>("SELECT * FROM vwSales_OUTLET WHERE CUSTOMER_ID='" + CustomerID + "'");
                //return database.QueryAsync<vwSalesOutlet>("SELECT * FROM vwSales_OUTLET WHERE CUSTOMER_ID='" + CustomerID + "'").Result;
            }
        }
        public void CleanDatabase()
        {
            using (var database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite))
            {
                int rowAffected = 0;
                rowAffected = database.Execute("DELETE FROM AppSetting");
                Console.WriteLine("Master DB Table AppSetting row delete: " + rowAffected.ToString());
                rowAffected = database.Execute("DELETE FROM RCS_TE");
                Console.WriteLine("Master DB Table RCS_TE row delete: " + rowAffected.ToString());
                rowAffected = database.Execute("DELETE FROM RCS_OUTLET");
                Console.WriteLine("Master DB Table RCS_OUTLET row deleted: " + rowAffected.ToString());
            }
        }
    }
}
