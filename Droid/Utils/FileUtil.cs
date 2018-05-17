using System;
using System.IO;
using System.IO.Compression;

using MyPatchSG.Utils;
using MyPatchSG.Droid.Utils;

using SQLite;
using Xamarin.Forms;
using Android.Content;

[assembly: Dependency(typeof(FileUtil))]
namespace MyPatchSG.Droid.Utils
{
    public class FileUtil : IFileUtil
    {
        public FileUtil()
        {

        }
        public string GetTempZipFileName()
        {
            var tempFilename = "temp.zip";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentsPath, tempFilename);

            return path;
        }
        public string GetTempDBFileName()
        {
            var tempFilename = "temp.db";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentsPath, tempFilename);

            return path;
        }

        public string GetTempDirectoryPath()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            return documentsPath;
        }

        public string GetMasterDBPath()
        {
            var dbFilename = "Master_DB.db";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentsPath, dbFilename);

            return path;
        }
        public string GetAuditDBPath()
        {
            var dbFilename = "Audit_DB.db";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentsPath, dbFilename);

            return path;
        }
        public string GetAuditDBBlankPath()
        {
            var dbFilename = "AuditDB_Blank.db";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentsPath, dbFilename);

            return path;
        }
        public bool CopyAuditDBBlank()
        {
            var auditDBBlankPath = GetAuditDBBlankPath();
            var auditDBPath = GetAuditDBPath();

            try
            {
                File.Copy(auditDBBlankPath, auditDBPath, true);

                FileAttributes attributes = File.GetAttributes(auditDBPath);
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    File.SetAttributes(auditDBPath, attributes & ~FileAttributes.ReadOnly);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool ReplaceAuditDB()
        {
            try
            {
                var fileUtil = new FileUtil();
                string fileToWriteToAudit = fileUtil.GetTempZipFileName();

                using (ZipArchive archiveAudit = ZipFile.Open(fileToWriteToAudit, ZipArchiveMode.Read))
                {
                    string extractToAudit = fileUtil.GetTempDirectoryPath();
                    foreach (ZipArchiveEntry entry in archiveAudit.Entries)
                    {
                        string pathAudit = fileUtil.GetAuditDBPath();
                        if (File.Exists(pathAudit))
                        {
                            File.Delete(pathAudit);
                        }

                        entry.ExtractToFile(pathAudit);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);

                return false;
            }

            return true;
        }

        public static void ClearCache()
        {
            try
            {
                var cachePath = System.IO.Path.GetTempPath();

                // If exist, delete the cache directory and everything in it recursivly
                if (System.IO.Directory.Exists(cachePath))
                    System.IO.Directory.Delete(cachePath, true);

                // If not exist, restore just the directory that was deleted
                if (!System.IO.Directory.Exists(cachePath))
                    System.IO.Directory.CreateDirectory(cachePath);
            }
            catch (Exception) { }
        }

        public static void DeleteCache(Context context)
        {
            try
            {
                Java.IO.File dir = context.CacheDir;
                DeleteDir(dir);
            }
            catch (Exception ex) { }
        }

        public static bool DeleteDir(Java.IO.File dir)
        {
            if (dir != null && dir.IsDirectory)
            {
                String[] children = dir.List();
                for (int i = 0; i < children.Length; i++)
                {
                    bool success = DeleteDir(new Java.IO.File(dir, children[i]));
                    if (!success)
                    {
                        return false;
                    }
                }
                return dir.Delete();
            }
            else if (dir != null && dir.IsFile)
            {
                return dir.Delete();
            }
            else
            {
                return false;
            }
        }
    }
}
