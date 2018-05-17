namespace MyPatchSG.SAL.DTO
{
    using System;

    public class LoginResponseDTO
    {
        public string USER_ID { get; set; }
        public string USER_TYPE { get; set; }
        public string User_Msg { get; set; }
        public int IsActive { get; set; }
    }
}