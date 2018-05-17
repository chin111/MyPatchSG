namespace MyPatchSG.SAL.RequestModels
{
    using System;
    using System.Collections.Generic;

    public class AddUploadLogRM
    {
        public string UploadDate { get; set; }
        public string LoginID { get; set; }
        public string Status { get; set; }
        public string FilePath { get; set; }
        public string TotalTime { get; set; }
        public string AppVersion { get; set; }
        public string OSType { get; set; }
        public string OSVersion { get; set; }
        public string MacAddr { get; set; }

        public AddUploadLogRM(string UploadDate, string LoginID, string Status, string FilePath, string TotalTime, string AppVersion, string OSType, string OSVersion, string MacAddr)
        {
            this.UploadDate = UploadDate;
            this.LoginID = LoginID;
            this.Status = Status;
            this.FilePath = FilePath;
            this.TotalTime = TotalTime;
            this.AppVersion = AppVersion;
            this.OSType = OSType;
            this.OSVersion = OSVersion;
            this.MacAddr = MacAddr;
        }
    }
}
