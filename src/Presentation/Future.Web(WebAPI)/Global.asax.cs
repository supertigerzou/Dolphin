using AutoMapper;
using EF.Web.Unity;
using Future.Core.Domain.Course;
using Future.Core.Domain.Person;
using Future.Core.Domain.Sales;
using Future.Web.ViewModel.Course;
using Future.Web.ViewModel.Person;
using Future.Web.ViewModel.Sales;
using Future.Web_WebAPI_.App_Start;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace Future.Web_WebAPI_
{
    public class Global : UnityHttpApplication
    {
        private static IUnityContainer _container;

        internal new static IUnityContainer Container
        {
            get
            {
                if (_container != null)
                    return _container;

                return UnityHttpApplication.Container;
            }
            set { _container = value; }
        }

        protected override void OnApplicationStart()
        {
            base.OnApplicationStart();

            RegisterDependencies(Container);

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.CreateMap<Territory, TerritoryViewModel>();
            Mapper.CreateMap<CountryRegion, CountryRegionViewModel>()
                .ForMember(crm => crm.Territories, mo => mo.MapFrom(cr => cr.Territories.Select(t => t.ToModel()).ToArray()));
            Mapper.CreateMap<CourseUnit, CourseUnitViewModel>();
        }

        private static void RegisterDependencies(IUnityContainer container)
        {
            container.RegisterInstance(typeof(IUnityContainer), container);
        }
    }
}