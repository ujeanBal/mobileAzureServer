﻿using Microsoft.Owin;
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
