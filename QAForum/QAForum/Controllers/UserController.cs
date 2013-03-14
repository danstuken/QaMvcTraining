namespace QAForum.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Models.ViewModels;
    using Providers;
    using QAModels.Membership;

    public class UserController:Controller
    {
        private readonly ForumProvider _forumProvider;

        public UserController(ForumProvider forumProvider)
        {
            _forumProvider = forumProvider;
        }

        [OutputCache(Duration = 10)]
        public PartialViewResult PartialUserDetails(Guid id)
        {
            var user = _forumProvider.GetUserById(id);
            var userVm = Mapper.Map<User, UserViewModel>(user);
            return PartialView(userVm);
        }

        public JsonResult UserNameSearch(string searchTerm)
        {
            var query = from u in _forumProvider.GetAllUsers()
                        where u.UserName.ToLower().StartsWith(searchTerm.ToLower())
                        select u.UserName;
            return Json(query.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}