using System;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Tables.Config;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MobileAzureServerMVC.Startup))]

namespace MobileAzureServerMVC
{
    public partial class Startup
    {
        public  void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}
