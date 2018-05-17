using System;

namespace MyPatchSG.SAL.RequestModels
{
    public class ResourceUploadRM
    {
        public string Username { get; set; }

        public ResourceUploadRM(string param_username)
        {
            this.Username = param_username;
        }
    }
}
