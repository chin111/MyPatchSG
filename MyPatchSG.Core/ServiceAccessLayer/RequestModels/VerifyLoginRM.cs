namespace MyPatchSG.SAL.RequestModels
{
    using System;
    using System.Collections.Generic;

    public class VerifyLoginRM
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string MacAddr { get; set; }

        public VerifyLoginRM(string ID, string Pwd, string Addr)
        {
            this.UserID = ID;
            this.Password = Pwd;
            this.MacAddr = Addr;
        }
    }
}
