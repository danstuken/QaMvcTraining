namespace QAForum.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using Models.ViewModels;
    using Providers;
    using QAModels.Forum;

    public class ForumController : Controller
    {
        private readonly ForumProvider _forumProvider;

        public ForumController(ForumProvider forumProvider)
        {
            _forumProvider = forumProvider;
        }

        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            var forums = _forumProvider.GetAllForums();
            var forumVms = Mapper.Map<IEnumerable<Forum>, IEnumerable<ForumViewModel>>(forums);

            ViewBag.HeaderMessage = "QA Forums List - " + DateTime.Now.ToString("HH:mm:ss");
            return View(forumVms);
        }

        public ActionResult Details(int id)
        {
            var forum = _forumProvider.GetForumById(id);
            var forumVm = Mapper.Map<Forum, ForumViewModel>(forum);

            ViewBag.HeaderMessage = "Forum Detail";
            ViewBag.Threads = Mapper.Map<IEnumerable<Thread>, IEnumerable<ThreadViewModel>>(_forumProvider.GetThreadsByForum(id));
            return View(forumVm);
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult Create()
        {
            var newForumVm = new ForumViewModel();
            return View(newForumVm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public ActionResult Create(ForumViewModel forumVm)
        {
            var forumToCreate = Mapper.Map<ForumViewModel, Forum>(forumVm);

            _forumProvider.AddForum(forumToCreate);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrators, Moderators")]
        public ActionResult Edit(int id)
        {
            var forumToEdit = _forumProvider.GetForumById(id);
            var forumVm = Mapper.Map<Forum, ForumViewModel>(forumToEdit);

            return View(forumVm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrators, Moderators")]
        public ActionResult Edit(ForumViewModel forumVm)
        {
            var updatedForum = Mapper.Map<ForumViewModel, Forum>(forumVm);
            _forumProvider.UpdateForum(updatedForum);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int id)
        {
            var forumToDelete = _forumProvider.GetForumById(id);
            var forumVm = Mapper.Map<Forum, ForumViewModel>(forumToDelete);

            return View(forumVm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(ForumViewModel forumVm)
        {
            var forumToDelete = Mapper.Map<ForumViewModel, Forum>(forumVm);
            _forumProvider.DeleteForum(forumToDelete);

            return RedirectToAction("Index");
        }
    }
}
