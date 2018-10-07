using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CoffeeManager.Api.Helper;
using CoffeeManager.Api.Mappers;

namespace CoffeeManager.Api.Controllers
{
    public class UpdateController : ApiController
    {
        [Route("api/update/getcurrentversion")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCurrenVersion(HttpRequestMessage message)
        {
            return Request.CreateResponse(HttpStatusCode.OK, 12);
        }


        [Route("api/update/GetAndroidPackage")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAndroidPackage(HttpRequestMessage message)
        {
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            string fullName = root + "/android/CoffeeManager.Droid.apk";

            if (!File.Exists(fullName))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "App does not exists");
            }

            var zip = new FileStreamEx(fullName);

            var response = Request.CreateResponse();

            response.Content = new PushStreamContent(zip.Write, new MediaTypeHeaderValue("file/zip"));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "CoffeeManager.Droid-Signed.apk"
            };
            return response;
        }
    }
}
