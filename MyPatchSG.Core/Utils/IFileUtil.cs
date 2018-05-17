using System;

namespace MyPatchSG.Utils
{
    public interface IFileUtil
    {
        string GetTempZipFileName();
        string GetTempDBFileName();
        string GetTempDirectoryPath();
        string GetMasterDBPath();
        string GetAuditDBPath();
    }
}
