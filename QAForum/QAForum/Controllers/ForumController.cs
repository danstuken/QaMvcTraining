namespace QAForum.Controllers
{
    using System;
    using System.Web.Mvc;
    using Filters.Diagnostics;
    using Infrastructure;
    using Models.Forum;

    public class ForumController : Controller
    {
        private readonly ForumRepository _forumRepository;

        public ForumController(ForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            var forums = _forumRepository.GetAllForums();
            ViewBag.HeaderMessage = "QA Forums List - " + DateTime.Now.ToString("HH:mm:ss");
            return View(forums);
        }

        public ActionResult Details(int id)
        {
            var forum = _forumRepository.GetForumById(id);
            ViewBag.HeaderMessage = "Forum Detail";
            return View(forum);
        }

        public ActionResult Create()
        {
            var newForum = new Forum();
            return View(newForum);
        }

        [HttpPost]
        public ActionResult Create(Forum forumToCreate)
        {
            _forumRepository.AddForum(forumToCreate);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var forumToEdit = _forumRepository.GetForumById(id);
            return View(forumToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Forum updatedForum)
        {
            _forumRepository.UpdateForum(updatedForum);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var forumToDelete = _forumRepository.GetForumById(id);
            return View(forumToDelete);
        }

        [HttpPost]
        public ActionResult Delete(Forum forumToDelete)
        {
            _forumRepository.DeleteForum(forumToDelete);
            return RedirectToAction("Index");
        }
    }
}
