using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Fusillade;
using Plugin.Connectivity;
using Polly;

namespace MyPatchSG.SAL
{
    public class CustomAPIService
    {
        public CustomAPIService()
        {

        }

        public ICustomAPI CreateClient(string BaseURL)
        {
            var client = new HttpClient(new CustomHttpClientHandler())
            {
                BaseAddress = new Uri(BaseURL)
            };

            return RestService.For<ICustomAPI>(client);
        }

        public async Task<HttpResponseMessage> DownloadFile(string url)
        {
            var apiClient = CreateClient(url);

            HttpResponseMessage response = null;
            Task<HttpResponseMessage> task;

            task = apiClient.DownloadFile();

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
