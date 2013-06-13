using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using EFSchools.Englishtown.UnityET;
using EFSchools.Englishtown.Web.Troop;
using Microsoft.Practices.Unity;

namespace Dolphin.Web_Troop_
{
    public class Global : System.Web.HttpApplication
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IUnityContainer Container { get; private set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            InitializeUnityForTroop();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private static void InitializeUnityForTroop()
        {
            Container = ETUnityContainerFactory.CreateContainer(string.Empty);
            ResourceRouteRegistration.RegisterRoutes(Container, RouteTable.Routes);
        }
    }
}