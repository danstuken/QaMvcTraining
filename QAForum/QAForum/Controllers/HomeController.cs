namespace QAForum.Controllers
{
    using System.Web.Mvc;
    using Infrastructure;

    public class HomeController : Controller
    {
        private readonly ForumRepository _forumRepository;

        public HomeController(ForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

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
