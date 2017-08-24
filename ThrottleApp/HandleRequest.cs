using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace ThrottleApp
{
    public class HandleRequest
    {
        private string Name { get; } = "Client1";

        private int Seconds { get; } = int.Parse(ConfigurationManager.AppSettings.Get("waitMillisecond"));
        

        public async Task<bool> IsValidRequest(HttpRequestMessage requestMessage)
        {
            var allowExecute = false;
            await Task.Factory.StartNew(() =>
            {
                var key = string.Concat(Name, "-", GetClientIp(requestMessage));
                if (HttpRuntime.Cache[key] == null)
                {
                    HttpRuntime.Cache.Add(key,
                        true, // is this the smallest data we can have?
                        null, // no dependencies
                        DateTime.Now.AddMilliseconds(Seconds), // absolute expiration
                        Cache.NoSlidingExpiration,
                        CacheItemPriority.Low,
                        null); // no callback

                    allowExecute = true;
                }
            });
            

            return allowExecute;
        }

        private string GetClientIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop;
                prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }

            return null;
        }
    }
}