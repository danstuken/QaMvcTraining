namespace QAForum.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure;

    public class UserController:Controller
    {
        private readonly ForumRepository _forumRepository;

        public UserController(ForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [OutputCache(Duration = 10)]
        public PartialViewResult PartialUserDetails(Guid id)
        {
            var user = _forumRepository.GetUserById(id);
            return PartialView(user);
        }

        public JsonResult UserNameSearch(string searchTerm)
        {
            var query = from u in _forumRepository.GetAllUsers()
                        where u.UserName.ToLower().StartsWith(searchTerm.ToLower())
                        select u.UserName;
            return Json(query.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}