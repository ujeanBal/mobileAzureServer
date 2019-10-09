using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using AutoMapper;
using Local.DataObjects;
using Local.Models;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Tables.Config;
using Newtonsoft.Json;
using Owin;

namespace MobileAzureServerMVC
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
        
            HttpConfiguration config = new HttpConfiguration();

        //    config.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
        //    config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;

    

            config.MapHttpAttributeRoutes();


            // config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
             //   .WithMobileAppControllerConfigProvider(new MyConfigProvider())
                .MapApiControllers()
                .ApplyTo(config);

            var mapperConfig = new MapperConfiguration(cfg => {
                
                cfg.CreateMap<Order, BrownOnline>()
                    .ForMember(dst => dst.Id, map => map.MapFrom(src => SqlFuncs.StringConvert((double)src.OrderId).Trim()));

                cfg.CreateMap<BrownOnline, Order>();

                cfg.CreateMap<PersonEntity, Person>();

                cfg.CreateMap<Person, PersonEntity>();
            });

            Database.SetInitializer(new GreenInitializer());
            Database.SetInitializer(new BrownInitializer());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            //if (string.IsNullOrEmpty(settings.HostName))
            //{
            //    // This middleware is intended to be used locally for debugging. By default, HostName will
            //    // only have a value when running in an App Service application.
            //    app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
            //    {
            //        SigningKey = ConfigurationManager.AppSettings["SigningKey"],
            //        ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
            //        ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
            //        TokenHandler = config.GetAppServiceTokenHandler()
            //    });
            //}

            app.Use(async (environment, next) =>
            {
                Debug.WriteLine(environment.Request.Uri);
                await next();
                Debug.WriteLine(environment.Response.StatusCode);
            });

            app.UseWebApi(config);

          //  ConfigureSwagger(config);
        }
    }

    public class GreenInitializer : CreateDatabaseIfNotExists<GreenContext>
    {
        protected override void Seed(GreenContext context)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            byte[] version = Encoding.UTF8.GetBytes("version");
            List<Green> items = new List<Green>
            {
                new Green { Id = "1", CreatedAt = now, UpdatedAt = now, Version = version, Text = "Henrik" },
                new Green { Id = "2", CreatedAt = now, UpdatedAt = now, Version = version, Text = "Nora" },
                new Green { Id = "3", CreatedAt = now, UpdatedAt = now, Version = version, Text = "Benjamin" },
            };

            foreach (Green item in items)
            {
                context.Greens.Add(item);
            }

            base.Seed(context);
        }
    }

    public class BrownInitializer : CreateDatabaseIfNotExists<BrownContext>
    {
        protected override void Seed(BrownContext context)
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer { CustomerId = 1, Name = "Henrik", Orders = new Collection<Order> { new Order { OrderId = 10, Item = "Shoes", Quantity = 2 }}},
                new Customer { CustomerId = 2, Name = "Scott", Orders = new Collection<Order> { new Order { OrderId = 20, Item = "Polos", Quantity = 10 }}},
                new Customer { CustomerId = 3, Name = "Benjamin", Orders = new Collection<Order> { new Order { OrderId = 30, Item = "S'mores", Quantity = 20 }}},
            };

            foreach (Customer customer in customers)
            {
                context.Customers.Add(customer);
            }

            base.Seed(context);
        }
    }
    public class MyConfigProvider : MobileAppControllerConfigProvider
    {
        public override void Configure(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            base.Configure(controllerSettings, controllerDescriptor);
            // then tweak the default behavior

            controllerSettings.Formatters.JsonFormatter.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
        }
    }
}
