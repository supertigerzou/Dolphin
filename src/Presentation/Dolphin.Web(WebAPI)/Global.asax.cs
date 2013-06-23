using Dolphin.Services.Course;
using Dolphin.Services.Search;
using Dolphin.Web.WebAPI.App_Start;
using EF.Web.Unity;
using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Routing;

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

            UnityConfig.RegisterTypes(Container);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewModelConfig.RegisterMappings();

            Container.Resolve<ISearchService>().AddUpdateIndex(Container.Resolve<ICourseContentService>().GetAllUnits());
        }
    }
}