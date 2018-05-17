namespace MyPatchSG.BL.Globals
{
    using System;
    using MyPatchSG.BL.Enums;
    using MyPatchSG.BL.Entities;
    using MyPatchSG.DL.Models;

    static public class Globals
    {
        // Declare global variables
        public static Int64 UserID = 0;
        public static string UserFirstName = "";
        public static string UserLastName = "";
        public static string AccessToken = "";
        public static string RefreshToken = "";

        public static string LoginUsername = "";
        public static string LoginPassword = "";
        public static string UserType = "";
        public static string UserMessage = "";

        public static AppInfo AppInformation = null;

        public static string AppID = "";
        public static string AppVersion = "";
        public static string OSType = "";
        public static string OSVersion = "";
        public static string MACAddress = "";

        public static int AndroidVersionCode = -1;
        public static string AndroidVersionName = "";
        public static bool IsUpdateNeeded = false;

        public static LoginStatusType LoginStatus = LoginStatusType.None;
        public static PasswordResetResultType PasswordResetResult = PasswordResetResultType.None;       
        public static GetFileNameResultType GetFileNameResult = GetFileNameResultType.None;
        public static DownloadDBResultType DownloadMasterDBResult = DownloadDBResultType.None;
        public static DownloadDBResultType DownloadAuditDBResult = DownloadDBResultType.None;

        public static UploadDBResultType UploadAuditDBResult = UploadDBResultType.None;
        public static string UploadDate = "";
        public static string FilePath = "";

        public static DBFileInfo AuditDBInfo = null;
        public static DBFileInfo MasterDBInfo = null;

        public static string ErrorTitle = "";
        public static string ErrorDescription = "";
    }
}
