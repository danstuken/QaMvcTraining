namespace QAForum.Controllers
{
    using System.Web.Mvc;
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the QA Forum";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
