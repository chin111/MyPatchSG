using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Refit;

using MyPatchSG.SAL.DTO;
using MyPatchSG.SAL.RequestModels;

namespace MyPatchSG.SAL
{
    [Headers("Accept: application/json")]
    public interface IMyPatchSGAPI
    {
        [Post("/token")]
        Task<HttpResponseMessage> LoginSession([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> param, [Header("Content-Type")] string contentType);

        [Post("/api/account/register")]
        Task<HttpResponseMessage> RegisterUser(UserRegistrationRM param);

        [Post("/api/resource/verifylogin")]
        Task<HttpResponseMessage> VerifyLogin([Body] VerifyLoginRM param);

        [Post("/api/resource/getfilename")]
        Task<GetFileNameDto> GetFileName([Body] GetFileNameRM param);

        [Post("/api/resource/download")]
        [Headers("Accept: application/octet-stream")]
        Task<HttpResponseMessage> DownloadResource([Body] ResourceDownloadRM param);

        [Multipart]
        [Post("/api/resource/upload?loginid={loginid}")]
        Task <HttpResponseMessage> UploadResource(string loginid, ByteArrayPart fileParam);

        [Post("/api/resource/adddownloadlog")]
        Task<HttpResponseMessage> AddDownloadLog([Body] AddDownloadLogRM param);

        [Post("/api/resource/adduploadlog")]
        Task<HttpResponseMessage> AddUploadLog([Body] AddUploadLogRM param);
    }
}
