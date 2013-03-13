namespace QAForum.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure;
    using Models.Forum;
    using Models.ViewModels;

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
            var forumVms = Mapper.Map<IEnumerable<Forum>, IEnumerable<ForumViewModel>>(forums);

            ViewBag.HeaderMessage = "QA Forums List - " + DateTime.Now.ToString("HH:mm:ss");
            return View(forumVms);
        }

        public ActionResult Details(int id)
        {
            var forum = _forumRepository.GetForumById(id);
            var forumVm = Mapper.Map<Forum, ForumViewModel>(forum);

            ViewBag.HeaderMessage = "Forum Detail";
            ViewBag.Threads = Mapper.Map<IEnumerable<Thread>, IEnumerable<ThreadViewModel>>(_forumRepository.GetThreadsByForum(id));
            return View(forumVm);
        }

        public ActionResult Create()
        {
            var newForumVm = new ForumViewModel();
            return View(newForumVm);
        }

        [HttpPost]
        public ActionResult Create(ForumViewModel forumVm)
        {
            var forumToCreate = Mapper.Map<ForumViewModel, Forum>(forumVm);

            _forumRepository.AddForum(forumToCreate);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var forumToEdit = _forumRepository.GetForumById(id);
            var forumVm = Mapper.Map<Forum, ForumViewModel>(forumToEdit);

            return View(forumVm);
        }

        [HttpPost]
        public ActionResult Edit(ForumViewModel forumVm)
        {
            var updatedForum = Mapper.Map<ForumViewModel, Forum>(forumVm);
            _forumRepository.UpdateForum(updatedForum);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var forumToDelete = _forumRepository.GetForumById(id);
            var forumVm = Mapper.Map<Forum, ForumViewModel>(forumToDelete);

            return View(forumVm);
        }

        [HttpPost]
        public ActionResult Delete(ForumViewModel forumVm)
        {
            var forumToDelete = Mapper.Map<ForumViewModel, Forum>(forumVm);
            _forumRepository.DeleteForum(forumToDelete);

            return RedirectToAction("Index");
        }
    }
}
