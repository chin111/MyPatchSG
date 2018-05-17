using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusillade;
using System.Net.Http;

using MyPatchSG.SAL.DTO;
using MyPatchSG.SAL.RequestModels;

namespace MyPatchSG.SAL
{
    public interface IMyPatchSGService
    {
        Task<HttpResponseMessage> LoginSession(Priority priority, AccessTokenRM param);
        Task<HttpResponseMessage> RegisterUser(Priority priority, UserRegistrationRM param);
        Task<HttpResponseMessage> VerifyLogin(Priority priority, VerifyLoginRM param);
        Task<GetFileNameDto> GetFileName(Priority priority, GetFileNameRM param);
        Task<HttpResponseMessage> DownloadResource(Priority priority, ResourceDownloadRM param);
        Task<HttpResponseMessage> UploadResource(Priority priority, string LoginID, string FileName, byte[] resource);
        Task<HttpResponseMessage> AddDownloadLog(Priority priority, AddDownloadLogRM param);
        Task<HttpResponseMessage> AddUploadLog(Priority priority, AddUploadLogRM param);
    }
}