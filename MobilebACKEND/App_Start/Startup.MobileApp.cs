using AutoMapper;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using MobilebACKEND.Models;
using MobilebACKEND.ViewModel;
using Owin;
using System;
using System.Data.Entity.Migrations;
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

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .MapApiControllers()
                .ApplyTo(config);

            var migrator = new DbMigrator(new MobilebACKEND.Migrations.Configuration());
            migrator.Update();

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FoodMobile, Food>()
                .ForPath(dest => dest.Description.Carbohydrates, opts => opts.MapFrom(src => Convert.ToInt32(src.Carbohydrates)))
                    .ForPath(dst => dst.Description.Fats, map => map.MapFrom(x => Convert.ToInt32(x.Fats)))
                    .ForPath(dst => dst.Description.Proteins, map => map.MapFrom(x => Convert.ToInt32(x.Proteins)))
                     .ForMember(dst => dst.Name, map => map.MapFrom(x => x.Name))
                      .ForMember(dst => dst.Kkal, map => map.MapFrom(x => Convert.ToInt32(x.Kkal)))
                       .ForMember(dst => dst.Weight, map => map.MapFrom(x => Convert.ToInt32(x.Weight)));
                cfg.CreateMap<Food, FoodMobile>()
                    .ForMember(dst => dst.Carbohydrates, map => map.MapFrom(x => x.Description.Carbohydrates))
                    .ForMember(dst => dst.Fats, map => map.MapFrom(x => x.Description.Fats))
                    .ForMember(dst => dst.Proteins, map => map.MapFrom(x => x.Description.Proteins));
            });

            app.UseWebApi(config);

            ConfigureSwagger(config);
        }
    }
}
