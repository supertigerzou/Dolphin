using AutoMapper;
using Future.Core.Domain.Person;
using Future.Core.Domain.Sales;
using Future.Web.ViewModel.Person;
using Future.Web.ViewModel.Sales;
using Future.Web_WebAPI_.App_Start;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Future.Web_WebAPI_
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.CreateMap<Territory, TerritoryModel>();
            Mapper.CreateMap<CountryRegion, CountryRegionModel>()
                .ForMember(crm => crm.Territories, mo => mo.MapFrom(cr => cr.Territories.Select(t => t.ToModel()).ToArray()));
        }
    }
}