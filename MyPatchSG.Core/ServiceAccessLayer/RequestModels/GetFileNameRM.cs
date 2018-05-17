namespace MyPatchSG.SAL.RequestModels
{
    using System;
    using System.Collections.Generic;

    public class GetFileNameRM
    {
        public string UserName { get; set; }
        public string AppID { get; set; }
        public string OS { get; set; }

        public GetFileNameRM(string UserName, string AppID, string OS)
        {
            this.UserName = UserName;
            this.AppID = AppID;
            this.OS = OS;
        }
    }
}
