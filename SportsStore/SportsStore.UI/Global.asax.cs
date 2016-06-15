using SportsStore.Domain.Entites;
using SportsStore.UI.Infrastructure;

using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
