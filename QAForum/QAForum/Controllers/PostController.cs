namespace QAForum.Controllers
{
    using System.Web.Mvc;
    using Infrastructure;
    using Models.Forum;

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
            ViewBag.Message = "QA Forums list [posts]";
            return View(allPosts);
        }

        public ActionResult Details(int id)
        {
            var post = _forumRepository.GetPostById(id);
            ViewBag.Message = "Post detail";
            return View(post);
        }

        public ActionResult Create()
        {
            var post = new Post();
            return View(post);
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            _forumRepository.AddPost(post);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var post = _forumRepository.GetPostById(id);
            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            _forumRepository.UpdatePost(post);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var post = _forumRepository.GetPostById(id);
            return View(post);
        }

        [HttpPost]
        public ActionResult Delete(Post post)
        {
            _forumRepository.DeletePost(post);
            return RedirectToAction("Index");
        }
    }
}
