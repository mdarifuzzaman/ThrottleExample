using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ThrottleApp.Controllers
{
    [Throttle]
    public class SampleController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get() => await Task.FromResult(Ok(Guid.NewGuid().ToString()));
    }
}