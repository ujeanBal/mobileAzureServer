using AutoMapper;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using MobilebACKEND.Models;
using MobilebACKEND.ViewModel;
using Owin;
using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace MobileAzureServerMVC
{
    public partial class Startup
    {
        public static MapperConfiguration MapperConfiguration;
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.MessageHandlers.Add(new DummyHandler());

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .MapApiControllers()
                .ApplyTo(config);

            var migrator = new DbMigrator(new MobilebACKEND.Migrations.Configuration());
            migrator.Update();

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FoodDTO, Food>()
                .ForPath(dest => dest.Description.Carbohydrates, opts => opts.MapFrom(src => Convert.ToInt32(src.Carbohydrates)))
                    .ForPath(dst => dst.Description.Fats, map => map.MapFrom(x => Convert.ToInt32(x.Fats)))
                    .ForPath(dst => dst.Description.Proteins, map => map.MapFrom(x => Convert.ToInt32(x.Proteins)))
                     .ForMember(dst => dst.Name, map => map.MapFrom(x => x.Name))
                      .ForMember(dst => dst.Kkal, map => map.MapFrom(x => x.Kkal))
                       .ForMember(dst => dst.Weight, map => map.MapFrom(x => Convert.ToInt32(x.Weight)));
                cfg.CreateMap<Food, FoodDTO>()
                    .ForMember(dst => dst.Carbohydrates, map => map.MapFrom(x => x.Description.Carbohydrates))
                    .ForMember(dst => dst.Fats, map => map.MapFrom(x => x.Description.Fats))
                    .ForMember(dst => dst.Proteins, map => map.MapFrom(x => x.Description.Proteins))
                 .ForMember(dst => dst.Kkal, map => map.MapFrom(x => (x.Kkal)));
            });

            app.UseWebApi(config);

            ConfigureSwagger(config);
        }
    }
    public class DummyHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // work on the request 
            Trace.WriteLine(request.RequestUri.ToString());
            try
            {
                var body = await request.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                var e = ex;
            }

            var response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add("X-Dummy-Header", Guid.NewGuid().ToString());
            return response;
        }
    }
}
