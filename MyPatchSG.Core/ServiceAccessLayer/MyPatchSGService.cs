using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Fusillade;
using Plugin.Connectivity;
using Polly;
using Refit;

using MyPatchSG.SAL.DTO;
using MyPatchSG.SAL.RequestModels;

namespace MyPatchSG.SAL
{
    public class MyPatchSGService : IMyPatchSGService
    {
        private readonly IAPIService _apiService;

        public MyPatchSGService(IAPIService apiService)
        {
            _apiService = apiService;
        }

        public async Task<HttpResponseMessage> LoginSession(Priority priority, AccessTokenRM param)
        {
            HttpResponseMessage response = null;
            Task<HttpResponseMessage> task;

            string contentType = "application/x-www-form-urlencoded";
            switch (priority)
            {
                case Priority.Background:
                    task = _apiService.Background.LoginSession(param.toDictionary(), contentType);
                    break;
                case Priority.UserInitiated:
                    task = _apiService.UserInitiated.LoginSession(param.toDictionary(), contentType);
                    break;
                case Priority.Speculative:
                    task = _apiService.Speculative.LoginSession(param.toDictionary(), contentType);
                    break;
                default:
                    task = _apiService.UserInitiated.LoginSession(param.toDictionary(), contentType);
                    break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                response = await Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync
                    (
                        retryCount: 3,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await task);
            }

            return response;
        }

        public async Task<HttpResponseMessage> RegisterUser(Priority priority, UserRegistrationRM param)
        {
            HttpResponseMessage response = null;
            Task<HttpResponseMessage> task;
            switch (priority)
            {
                case Priority.Background:
                    task = _apiService.Background.RegisterUser(param);
                    break;
                case Priority.UserInitiated:
                    task = _apiService.UserInitiated.RegisterUser(param);
                    break;
                case Priority.Speculative:
                    task = _apiService.Speculative.RegisterUser(param);
                    break;
                default:
                    task = _apiService.UserInitiated.RegisterUser(param);
                    break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                response = await Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync
                    (
                        retryCount: 3,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await task);
            }

            return response;
        }

        public async Task<HttpResponseMessage> VerifyLogin(Priority priority, VerifyLoginRM param)
        {
            HttpResponseMessage response = null;
            Task<HttpResponseMessage> loginTask;
            switch (priority)
            {
                case Priority.Background:
                    loginTask = _apiService.Background.VerifyLogin(param);
                    break;
                case Priority.UserInitiated:
                    loginTask = _apiService.UserInitiated.VerifyLogin(param);
                    break;
                case Priority.Speculative:
                    loginTask = _apiService.Speculative.VerifyLogin(param);
                    break;
                default:
                    loginTask = _apiService.UserInitiated.VerifyLogin(param);
                    break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                response = await Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync
                    (
                        retryCount: 5,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await loginTask);
            }

            return response;
        }

        public async Task<GetFileNameDto> GetFileName(Priority priority, GetFileNameRM param)
        {
            GetFileNameDto response = null;
            Task<GetFileNameDto> getTask;
            switch (priority)
            {
                case Priority.Background:
                    getTask = _apiService.Background.GetFileName(param);
                    break;
                case Priority.UserInitiated:
                    getTask = _apiService.UserInitiated.GetFileName(param);
                    break;
                case Priority.Speculative:
                    getTask = _apiService.Speculative.GetFileName(param);
                    break;
                default:
                    getTask = _apiService.UserInitiated.GetFileName(param);
                    break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                response = await Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync
                    (
                        retryCount: 5,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await getTask);
            }

            return response;
        }

        public async Task<HttpResponseMessage> DownloadResource(Priority priority, ResourceDownloadRM param)
        {
            HttpResponseMessage response = null;
            Task<HttpResponseMessage> task;
            switch (priority)
            {
                case Priority.Background:
                    task = _apiService.Background.DownloadResource(param);
                    break;
                case Priority.UserInitiated:
                    task = _apiService.UserInitiated.DownloadResource(param);
                    break;
                case Priority.Speculative:
                    task = _apiService.Speculative.DownloadResource(param);
                    break;
                default:
                    task = _apiService.UserInitiated.DownloadResource(param);
                    break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                response = await Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync
                    (
                        retryCount: 3,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await task);
            }

            return response;
        }

        public async Task<HttpResponseMessage> UploadResource(Priority priority,string LoginID, string FileName, byte[] resource)
        {
            HttpResponseMessage response = null;
            Task<HttpResponseMessage> task;

            var resourcePart = new ByteArrayPart(resource, FileName, "application/zip");

            switch (priority)
            {
                case Priority.Background:
                    task = _apiService.Background.UploadResource(LoginID, resourcePart);
                    break;
                case Priority.UserInitiated:
                    task = _apiService.UserInitiated.UploadResource(LoginID, resourcePart);
                    break;
                case Priority.Speculative:
                    task = _apiService.Speculative.UploadResource(LoginID, resourcePart);
                    break;
                default:
                    task = _apiService.UserInitiated.UploadResource(LoginID, resourcePart);
                    break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                response = await Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync
                    (
                        retryCount: 3,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await task);
            }

            return response;
        }
        public async Task<HttpResponseMessage> AddDownloadLog(Priority priority, AddDownloadLogRM param)
        {
            HttpResponseMessage response = null;
            Task<HttpResponseMessage> task;
            switch (priority)
            {
                case Priority.Background:
                    task = _apiService.Background.AddDownloadLog(param);
                    break;
                case Priority.UserInitiated:
                    task = _apiService.UserInitiated.AddDownloadLog(param);
                    break;
                case Priority.Speculative:
                    task = _apiService.Speculative.AddDownloadLog(param);
                    break;
                default:
                    task = _apiService.UserInitiated.AddDownloadLog(param);
                    break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                response = await Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync
                    (
                        retryCount: 3,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await task);
            }

            return response;
        }
        public async Task<HttpResponseMessage> AddUploadLog(Priority priority, AddUploadLogRM param)
        {
            HttpResponseMessage response = null;
            Task<HttpResponseMessage> task;
            switch (priority)
            {
                case Priority.Background:
                    task = _apiService.Background.AddUploadLog(param);
                    break;
                case Priority.UserInitiated:
                    task = _apiService.UserInitiated.AddUploadLog(param);
                    break;
                case Priority.Speculative:
                    task = _apiService.Speculative.AddUploadLog(param);
                    break;
                default:
                    task = _apiService.UserInitiated.AddUploadLog(param);
                    break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                response = await Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync
                    (
                        retryCount: 3,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await task);
            }

            return response;
        }
    }
}

