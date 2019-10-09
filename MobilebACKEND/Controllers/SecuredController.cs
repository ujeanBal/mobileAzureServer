// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Local.Models;

using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;

namespace Local.Controllers
{
    /// <summary>
    /// The endpoints of this controller are secured
    /// </summary>

    [MobileAppController]
    public class CustomSecuredController : ApiController
    {
        [HttpGet, Route("api/Test")]
        public  IHttpActionResult Get()
        {
            //   FacebookCredentials fbCreds = await this.User.GetAppServiceIdentityAsync<FacebookCredentials>(this.Request);
            //   TwitterCredentials twitterCreds = await this.User.GetAppServiceIdentityAsync<TwitterCredentials>(this.Request);
            //  GoogleCredentials googCreds = await this.User.GetAppServiceIdentityAsync<GoogleCredentials>(this.Request);
            //  MicrosoftAccountCredentials msCreds = await this.User.GetAppServiceIdentityAsync<MicrosoftAccountCredentials>(this.Request);
            //  AzureActiveDirectoryCredentials aadCreds = await this.User.GetAppServiceIdentityAsync<AzureActiveDirectoryCredentials>(this.Request);

            /*  return new
              {
                  FacebookCreds = fbCreds,
                  TwitterCreds = twitterCreds,
                  GoogleCreds = googCreds,
                  MicrosoftAccountCreds = msCreds,
                  AadCreds = aadCreds,
                  Claims = (this.User as ClaimsPrincipal).Claims.Select(c => new { Type = c.Type, Value = c.Value })
              };*/
            return Ok(new Person { LastName = "Sin" });
        }

        [HttpPost, Route("api/Test/completeAll")]
        public string Post(string hej)
        {
            string retVal = "Hello World!" + hej;
            return retVal;
        }
    }
}