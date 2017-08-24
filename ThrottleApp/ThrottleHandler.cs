using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ThrottleApp
{
    public class ThrottleHandler:DelegatingHandler
    {
        private readonly HandleRequest _handleRequest;
        public ThrottleHandler()
        {
            this._handleRequest = new HandleRequest();
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var allowRequest = await this._handleRequest.IsValidRequest(request);
            if (!allowRequest)
            {
                var response = request.CreateResponse((HttpStatusCode) 429, "Too many request");
                return response;
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}