namespace QAForum.Controllers
{
    using System;
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
    }
}