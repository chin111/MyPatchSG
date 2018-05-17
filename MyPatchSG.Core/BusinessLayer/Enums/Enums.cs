namespace MyPatchSG.BL.Enums
{
    using System;

    public enum LoginStatusType
    {
        None = 0,
        Success = 1, 
        LoginError = 2,
        NetworkError = 3
    }

    public enum PasswordResetResultType
    {
        None = 0,
        Success = 1,
        InputError = 2,
        NetworkError = 3
    };

    public enum DownloadDBResultType
    {
        None = 0,
        Success = 1,
        NotFoundError = 2,
        BadRequestError = 3,
        NetworkError = 4
    };

    public enum UploadDBResultType
    {
        None = 0,
        Success = 1,
        BadRequestError = 2,
        NetworkError = 3
    };

    public enum GetFileNameResultType
    {
        None = 0,
        Success = 1,
        Error = 2
    };
}
