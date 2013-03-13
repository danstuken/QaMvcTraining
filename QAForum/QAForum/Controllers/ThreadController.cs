namespace QAForum.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure;
    using Models.Forum;
    using Models.ViewModels;

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
            var allThreadVms = Mapper.Map<IEnumerable<Thread>, IEnumerable<ThreadViewModel>>(allThreads);

            ViewBag.Message = "QA Forums list [threads]";
            return View(allThreadVms);
        }

        public ActionResult Details(int id)
        {
            var thread = _forumRepository.GetThreadById(id);
            var threadVm = Mapper.Map<Thread, ThreadViewModel>(thread);

            ViewBag.Message = "Thread detail";
            ViewBag.Posts = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_forumRepository.GetPostsByThread(id));
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
            _forumRepository.AddThread(thread);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var thread = _forumRepository.GetThreadById(id);
            var threadVm = Mapper.Map<Thread, ThreadViewModel>(thread);

            return View(threadVm);
        }

        [HttpPost]
        public ActionResult Edit(ThreadViewModel threadVm)
        {
            var thread = Mapper.Map<ThreadViewModel, Thread>(threadVm);
            _forumRepository.UpdateThread(thread);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thread = _forumRepository.GetThreadById(id);
            var threadVm = Mapper.Map<Thread, ThreadViewModel>(thread);

            return View(threadVm);
        }

        [HttpPost]
        public ActionResult Delete(ThreadViewModel threadVm)
        {
            var thread = Mapper.Map<ThreadViewModel, Thread>(threadVm);
            _forumRepository.DeleteThread(thread);

            return RedirectToAction("Index");
        }
    }
}
