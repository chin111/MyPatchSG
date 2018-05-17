namespace MyPatchSG.SAL.RequestModels
{
    using System;

    public class UserRegistrationRM
    {
        public string username { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }

        public UserRegistrationRM(string param_username, string param_password)
        {
            this.username = param_username;
            this.password = param_password;
            this.confirmpassword = this.password;
        }
    }
}
