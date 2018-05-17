using System;
using System.Net.Http;
using Refit;
using Fusillade;

namespace MyPatchSG.SAL
{
    public class MyPatchSGAPIService : IAPIService
    {
        //      public const string APIBaseURL = "http://192.168.2.222:9810/api";
        public const string APIBaseURL = "http://127.0.0.1:8080/api";

        public MyPatchSGAPIService(string BaseURL = null)
        {
            Func<HttpMessageHandler, IMyPatchSGAPI> createClient = messageHandler => {
                var client = new HttpClient(messageHandler)
                {
                    BaseAddress = new Uri(BaseURL ?? APIBaseURL)
                };
                
                return RestService.For<IMyPatchSGAPI>(client);
            };

            _background = new Lazy<IMyPatchSGAPI>(() => createClient(new RateLimitedHttpMessageHandler(new HttpClientHandler(), Priority.Background)));

            _userInitiated = new Lazy<IMyPatchSGAPI>(() => createClient(new RateLimitedHttpMessageHandler(new HttpClientHandler(), Priority.UserInitiated)));

            _speculative = new Lazy<IMyPatchSGAPI>(() => createClient(new RateLimitedHttpMessageHandler(new HttpClientHandler(), Priority.Speculative)));

        }

        private readonly Lazy<IMyPatchSGAPI> _background;
        private readonly Lazy<IMyPatchSGAPI> _userInitiated;
        private readonly Lazy<IMyPatchSGAPI> _speculative;

        public IMyPatchSGAPI Background
        {
            get { return _background.Value; }
        }

        public IMyPatchSGAPI UserInitiated
        {
            get { return _userInitiated.Value; }
        }

        public IMyPatchSGAPI Speculative
        {
            get { return _speculative.Value; }
        }
    }
}

