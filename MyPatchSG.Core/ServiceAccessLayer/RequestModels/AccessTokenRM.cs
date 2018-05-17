namespace MyPatchSG.SAL.RequestModels
{
    using System;
    using System.Collections.Generic;

    public class AccessTokenRM
    {
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string client_os { get; set; }

        public AccessTokenRM(string param_username, string param_password, string clientos)
        {
            this.grant_type = "password";
            this.client_id = "TestNativeMyPatchSGClient";
            this.client_secret = "123@abc";
            this.username = param_username;
            this.password = param_password;
            this.client_os = clientos;
        }

        public Dictionary<string, string> toDictionary()
        {
            var result = new Dictionary<string, string>();

            result.Add("grant_type", this.grant_type);
            result.Add("client_id", this.client_id);
            result.Add("client_secret", this.client_secret);
            result.Add("username", this.username);
            result.Add("password", this.password);
            result.Add("client_os", this.client_os);

            return result;
        }
    }
}
