using System.Web.Mvc;

namespace Future.Web_WebAPI_.Controllers
{
    public class TestController : Controller
    {
         public ActionResult Index()
         {
             return Json("test");
         }
    }
}