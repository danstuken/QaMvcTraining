namespace QAForum.Controllers
{
    using System.Web.Mvc;
    using Infrastructure;
    using Models.Forum;

    public class ThreadController : Controller
    {
        private readonly ForumRepository _forumRepository;

        public ThreadController(ForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            var allThreads = _forumRepository.GetAllThreads();
            ViewBag.Message = "QA Forums list [threads]";
            return View(allThreads);
        }

        public ActionResult Details(int id)
        {
            var thread = _forumRepository.GetThreadById(id);
            ViewBag.Message = "Thread detail";
            ViewBag.Posts = _forumRepository.GetPostsByThread(id);
            return View(thread);
        }

        public ActionResult Create()
        {
            var thread = new Thread();
            return View(thread);
        }

        [HttpPost]
        public ActionResult Create(Thread thread)
        {
            _forumRepository.AddThread(thread);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var thread = _forumRepository.GetThreadById(id);
            return View(thread);
        }

        [HttpPost]
        public ActionResult Edit(Thread thread)
        {
            _forumRepository.UpdateThread(thread);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var result = _forumRepository.GetThreadById(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Delete(Thread thread)
        {
            _forumRepository.DeleteThread(thread);
            return RedirectToAction("Index");
        }
    }
}
