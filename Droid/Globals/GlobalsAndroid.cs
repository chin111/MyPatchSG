using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyPatchSG.DL;
using MyPatchSG.Droid.ViewModels;
using MyPatchSG.Droid.Utils;

namespace MyPatchSG.Droid.Globals
{
    static public class GlobalsAndroid
    {
        public static string FilterProjectCode = "";
        public static string SortBy = "";
        public static vwOutletListViewModel SelectedOutletListItem = null;

        public static MasterDB masterDB = null;

        public static bool AuditDBCleaned = true;

        public static AuditDB auditDB;

        public static MasterDB GetMasterDBInstance()
        {
            string masterDBPath = new FileUtil().GetMasterDBPath();
            if (File.Exists(masterDBPath))
            {
                if (masterDB == null)
                {
                    masterDB = new MasterDB();
                }
                
                masterDB.dbPath = masterDBPath;

                return masterDB;
            }
            else
            {
                masterDB = null;
                return null;
            }
        }
        private static AuditDB GetAuditDBInstance()
        {
            string auditDBPath = new FileUtil().GetAuditDBPath();
            if (File.Exists(auditDBPath))
            {
                if (auditDB == null)
                {
                    auditDB = new AuditDB();
                }
                
                auditDB.dbPath = auditDBPath;

                return auditDB;
            }
            else
            {
                string auditDBBlankPath = new FileUtil().GetAuditDBBlankPath();
                if (File.Exists(auditDBBlankPath))
                {
                    //bool copied = new FileUtil().CopyAuditDBBlank();
                    bool copied = new FileUtil().ReplaceAuditDB();

                    if (!copied)
                    {
                        return null;
                    }

                    if (File.Exists(auditDBPath))
                    {
                        if (auditDB != null)
                        {
                            auditDB.dbPath = auditDBPath;
                            return auditDB;
                        }

                        //auditDB = new AuditDB(auditDBPath);
                        auditDB = new AuditDB();
                        auditDB.dbPath = auditDBPath;

                        return auditDB;
                    }
                }
            }

            return null;
        }
        public static void SaveRCSTE(string FIELD_ID, string TE_ID, string FIELD_VALUE, string LAST_UPDATE)
        {
            var auditDB = GetAuditDBInstance();
            if (auditDB == null) return;

            if (AuditDBCleaned) AuditDBCleaned = false;

            auditDB.DeleteRCSTE(TE_ID);
            auditDB.InsertRCSTE(FIELD_ID, TE_ID, FIELD_VALUE, LAST_UPDATE);
        }

        public static void SaveRCSOutlet(string FIELD_ID, string CUSTOMER_ID, string FIELD_VALUE, string LAST_UPDATE)
        {
            var auditDB = GetAuditDBInstance();
            if (auditDB == null) return;

            if (AuditDBCleaned) AuditDBCleaned = false;

            auditDB.DeleteRCSOutletByID(CUSTOMER_ID);
            auditDB.InsertRCSOutlet(FIELD_ID, CUSTOMER_ID, FIELD_VALUE, LAST_UPDATE);
        }

        public static void DeleteAuditDB()
        {
            string auditDBPath = new FileUtil().GetAuditDBPath();
            if (File.Exists(auditDBPath))
            {
                //auditDB = null;
                //File.Delete(auditDBPath);

                if (auditDB != null)
                {
                    auditDB.CleanDatabase();
                    AuditDBCleaned = true;
                }
            }
        }
    }
}