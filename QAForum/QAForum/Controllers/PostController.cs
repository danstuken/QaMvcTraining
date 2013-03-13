namespace QAForum.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure;
    using Models.Forum;
    using Models.ViewModels;

    public class PostController : Controller
    {
        private readonly ForumRepository _forumRepository;

        public PostController(ForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            var allPosts = _forumRepository.GetAllPosts();
            var allPostsVms = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(allPosts);
            
            ViewBag.Message = "QA Forums list [posts]";
            return View(allPostsVms);
        }

        public ActionResult Details(int id)
        {
            var post = _forumRepository.GetPostById(id);
            var postVm = Mapper.Map<Post, PostViewModel>(post);

            ViewBag.Message = "Post detail";
            return View(postVm);
        }

        public ActionResult Create()
        {
            var postVm = new PostViewModel();
            return View(postVm);
        }

        [HttpPost]
        public ActionResult Create(PostViewModel postVm)
        {
            var post = Mapper.Map<PostViewModel, Post>(postVm);
            _forumRepository.AddPost(post);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var post = _forumRepository.GetPostById(id);
            var postVm = Mapper.Map<Post, PostViewModel>(post);
            return View(postVm);
        }

        [HttpPost]
        public ActionResult Edit(PostViewModel postVm)
        {
            var post = Mapper.Map<PostViewModel, Post>(postVm);
            _forumRepository.UpdatePost(post);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var post = _forumRepository.GetPostById(id);
            var postVm = Mapper.Map<Post, PostViewModel>(post);
            return View(postVm);
        }

        [HttpPost]
        public ActionResult Delete(PostViewModel postVm)
        {
            var post = Mapper.Map<PostViewModel, Post>(postVm);
            _forumRepository.DeletePost(post);
            return RedirectToAction("Index");
        }
    }
}
