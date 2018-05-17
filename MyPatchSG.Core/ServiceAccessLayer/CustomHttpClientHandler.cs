using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Refit;

namespace MyPatchSG.SAL
{
    class CustomHttpClientHandler : HttpClientHandler
    {
        public CustomHttpClientHandler()
        {

        }
    }
}
