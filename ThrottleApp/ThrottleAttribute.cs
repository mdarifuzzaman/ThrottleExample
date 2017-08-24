using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ThrottleApp
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ThrottleAttribute:ActionFilterAttribute
    {
        private readonly HandleRequest _handleRequest;
        public ThrottleAttribute()
        {
            this._handleRequest = new HandleRequest();
        }

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var valid =  await this._handleRequest.IsValidRequest(actionContext.Request);
            if (!valid)
            {
                actionContext.Response =
                    new HttpResponseMessage((HttpStatusCode) 429) {ReasonPhrase = "Too many request"};
            }
        }
    }
}