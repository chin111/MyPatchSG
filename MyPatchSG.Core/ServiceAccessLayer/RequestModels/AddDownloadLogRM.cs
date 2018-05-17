namespace MyPatchSG.SAL.RequestModels
{
    using System;
    using System.Collections.Generic;

    public class AddDownloadLogRM
    {
        public string DownloadDate { get; set; }
        public string LoginID { get; set; }
        public string Status { get; set; }
        public string DBPath { get; set; }
        public string TotalTime { get; set; }
        public string AppVersion { get; set; }
        public string OSType { get; set; }
        public string OSVersion { get; set; }
        public string MacAddr { get; set; }

        public AddDownloadLogRM(string DownloadDate, string LoginID, string Status, string DBPath, string TotalTime, string AppVersion, string OSType, string OSVersion, string MacAddr)
        {
            this.DownloadDate = DownloadDate;
            this.LoginID = LoginID;
            this.Status = Status;
            this.DBPath = DBPath;
            this.TotalTime = TotalTime;
            this.AppVersion = AppVersion;
            this.OSType = OSType;
            this.OSVersion = OSVersion;
            this.MacAddr = MacAddr;
        }
    }
}
