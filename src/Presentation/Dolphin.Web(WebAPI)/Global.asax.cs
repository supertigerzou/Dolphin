﻿using AutoMapper;
using Dolphin.Core.Domain.Course;
using Dolphin.Core.Domain.Person;
using Dolphin.Core.Domain.Sales;
using Dolphin.Web.ViewModel.Course;
using Dolphin.Web.ViewModel.Person;
using Dolphin.Web.ViewModel.Sales;
using Dolphin.Web_WebAPI_;
using Dolphin.Web_WebAPI_.App_Start;
using EF.Web.Unity;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Web.Http;

namespace Dolphin.Web.WebAPI
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