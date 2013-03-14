namespace QAForum.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Models.ViewModels;
    using Providers;
    using QAModels.Forum;
    using Tags;

    public class PostController : Controller
    {
        private readonly ForumProvider _forumProvider;
        private readonly TagCloudProvider _tagProvider;

        public PostController(ForumProvider forumProvider, TagCloudProvider tagProvider)
        {
            _forumProvider = forumProvider;
            _tagProvider = tagProvider;
        }

        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            var allPosts = _forumProvider.GetAllPosts();
            var allThreads = _forumProvider.GetAllThreads();

            var allPostsVms = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(allPosts);
            var allThreadsVms = Mapper.Map<IEnumerable<Thread>, IEnumerable<ThreadViewModel>>(allThreads);

            ViewBag.ThreadId = new SelectList(allThreadsVms, "ThreadId", "ThreadTitle");
            ViewBag.Message = "QA Forums list [posts]";
            return View(allPostsVms);
        }

        [Authorize(Roles = "Users")]
        public ActionResult Details(int id)
        {
            var post = _forumProvider.GetPostById(id);
            var postVm = Mapper.Map<Post, PostViewModel>(post);

            ViewBag.Message = "Post detail";
            return View(postVm);
        }

        [Authorize(Roles = "Users")]
        public ActionResult Create()
        {
            var postVm = new PostViewModel();
            return View(postVm);
        }

        [HttpPost]
        [Authorize(Roles = "Users")]
        public ActionResult Create(PostViewModel postVm)
        {
            var post = Mapper.Map<PostViewModel, Post>(postVm);
            _forumProvider.AddPost(post);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrators, Moderators")]
        public ActionResult Edit(int id)
        {
            var post = _forumProvider.GetPostById(id);
            var postVm = Mapper.Map<Post, PostViewModel>(post);
            return View(postVm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrators, Moderators")]
        public ActionResult Edit(PostViewModel postVm)
        {
            var post = Mapper.Map<PostViewModel, Post>(postVm);
            _forumProvider.UpdatePost(post);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrators, Moderators")]
        public ActionResult Delete(int id)
        {
            var post = _forumProvider.GetPostById(id);
            var postVm = Mapper.Map<Post, PostViewModel>(post);
            return View(postVm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrators, Moderators")]
        public ActionResult Delete(PostViewModel postVm)
        {
            var post = Mapper.Map<PostViewModel, Post>(postVm);
            _forumProvider.DeletePost(post);
            return RedirectToAction("Index");
        }

        public ActionResult FindPosts(string searchTerm)
        {
            IEnumerable<PostViewModel> postVms = new PostViewModel[] {};
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var foundPosts = _forumProvider.FindPosts(searchTerm);
                postVms = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(foundPosts);
                
            }

            if(Request.IsAjaxRequest())
                return PartialView("PartialPostList", postVms);
            return View(postVms);
        }

        public ActionResult PostsByThread(int id)
        {
            var posts = _forumProvider.GetPostsByThread(id);
            var postVms = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(posts);
            
            if (Request.IsAjaxRequest())
                return PartialView("PartialPostList", postVms);
            return View(postVms);
        }

        public ActionResult TagSearch(string tag)
        {
            IEnumerable<Post> posts;
            if (!string.IsNullOrEmpty(tag))
                posts = _forumProvider.FindPosts(tag);
            else
                posts = _forumProvider.GetAllPosts();
            
            ViewBag.Message = "Posts for tag: " + tag;
            var postsVms = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(posts);
            return View(postsVms);
        }

        public ActionResult DateSearch(int day, int month, int year)
        {
            var query = from p in _forumProvider.GetAllPosts()
                        where p.PostDateTime.Date == new DateTime(year, month, day)
                        select p;
            var postsVms = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(query.ToList());

            ViewBag.Message = string.Format("Posts posted on: {0}/{1}/{2}", day, month, year);
            return View(postsVms);
        }

        public PartialViewResult PostsTagCloud()
        {
            var tags = _tagProvider.GetTagCloud();
            var tagsVms = Mapper.Map<IEnumerable<KeyValuePair<string, int>>, IEnumerable<PostTagViewModel>>(tags);
            return PartialView(tagsVms);
        }
    }
}
