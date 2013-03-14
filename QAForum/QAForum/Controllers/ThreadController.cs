namespace QAForum.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using Models.ViewModels;
    using Providers;
    using QAModels.Forum;

    public class ThreadController : Controller
    {
        private readonly ForumProvider _forumProvider;

        public ThreadController(ForumProvider forumProvider)
        {
            _forumProvider = forumProvider;
        }

        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            var allThreads = _forumProvider.GetAllThreads();
            var allThreadVms = Mapper.Map<IEnumerable<Thread>, IEnumerable<ThreadViewModel>>(allThreads);

            ViewBag.Message = "QA Forums list [threads]";
            return View(allThreadVms);
        }

        public ActionResult Details(int id)
        {
            var thread = _forumProvider.GetThreadById(id);
            var threadVm = Mapper.Map<Thread, ThreadViewModel>(thread);

            ViewBag.Message = "Thread detail";
            ViewBag.Posts = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_forumProvider.GetPostsByThread(id));
            return View(threadVm);
        }

        public ActionResult Create()
        {
            var threadVm = new ThreadViewModel();
            return View(threadVm);
        }

        [HttpPost]
        public ActionResult Create(ThreadViewModel threadVm)
        {
            var thread = Mapper.Map<ThreadViewModel, Thread>(threadVm);
            _forumProvider.AddThread(thread);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var thread = _forumProvider.GetThreadById(id);
            var threadVm = Mapper.Map<Thread, ThreadViewModel>(thread);

            return View(threadVm);
        }

        [HttpPost]
        public ActionResult Edit(ThreadViewModel threadVm)
        {
            var thread = Mapper.Map<ThreadViewModel, Thread>(threadVm);
            _forumProvider.UpdateThread(thread);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thread = _forumProvider.GetThreadById(id);
            var threadVm = Mapper.Map<Thread, ThreadViewModel>(thread);

            return View(threadVm);
        }

        [HttpPost]
        public ActionResult Delete(ThreadViewModel threadVm)
        {
            var thread = Mapper.Map<ThreadViewModel, Thread>(threadVm);
            _forumProvider.DeleteThread(thread);

            return RedirectToAction("Index");
        }
    }
}
