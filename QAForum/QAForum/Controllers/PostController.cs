namespace QAForum.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            var allThreads = _forumRepository.GetAllThreads();

            var allPostsVms = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(allPosts);
            var allThreadsVms = Mapper.Map<IEnumerable<Thread>, IEnumerable<ThreadViewModel>>(allThreads);

            ViewBag.ThreadId = new SelectList(allThreadsVms, "ThreadId", "ThreadTitle");
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

        public ActionResult FindPosts(string searchTerm)
        {
            IEnumerable<PostViewModel> postVms = new PostViewModel[] {};
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var foundPosts = _forumRepository.FindPosts(searchTerm);
                postVms = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(foundPosts);
                
            }

            if(Request.IsAjaxRequest())
                return PartialView("PartialPostList", postVms);
            return View(postVms);
        }

        public ActionResult PostsByThread(int id)
        {
            var posts = _forumRepository.GetPostsByThread(id);
            var postVms = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(posts);
            
            if (Request.IsAjaxRequest())
                return PartialView("PartialPostList", postVms);
            return View(postVms);
        }

        public ActionResult TagSearch(string tag)
        {
            IEnumerable<Post> posts;
            if (!string.IsNullOrEmpty(tag))
                posts = _forumRepository.FindPosts(tag);
            else
                posts = _forumRepository.GetAllPosts();
            
            ViewBag.Message = "Posts for tag: " + tag;
            var postsVms = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(posts);
            return View(postsVms);
        }

        public ActionResult DateSearch(int day, int month, int year)
        {
            var query = from p in _forumRepository.GetAllPosts()
                        where p.PostDateTime.Date == new DateTime(year, month, day)
                        select p;
            var postsVms = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(query.ToList());

            ViewBag.Message = string.Format("Posts posted on: {0}/{1}/{2}", day, month, year);
            return View(postsVms);
        }
    }
}
