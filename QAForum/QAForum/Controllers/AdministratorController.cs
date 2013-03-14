namespace QAForum.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using Models.ViewModels;
    using Providers;
    using QAModels.Membership;

    [Authorize(Roles = "Administrators")]
    public class AdministratorController : Controller
    {
        private readonly ForumProvider _forumProvider;

        public AdministratorController(ForumProvider forumProvider)
        {
            _forumProvider = forumProvider;
        }

        public ActionResult Index()
        {
            var users = _forumProvider.GetAllUsers();
            var usersVms = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);

            ViewBag.Message = "QA Forums list [Users]";
            return View(usersVms);
        }

        public ActionResult Details(Guid id)
        {
            var member = _forumProvider.GetMemberById(id);
            var memberVm = Mapper.Map<Member, MemberViewModel>(member);

            ViewBag.Message = "Membership Detail";
            return View(memberVm);
        }
        
        public ActionResult Edit(Guid id)
        {
            var member = _forumProvider.GetMemberById(id);
            var memberVm = Mapper.Map<Member, MemberViewModel>(member);

            return View(memberVm);
        }

        [HttpPost]
        public ActionResult Edit(MemberViewModel memberVm)
        {
            var member = Mapper.Map<MemberViewModel, Member>(memberVm);
            _forumProvider.UpdateMember(member);
            return RedirectToAction("Index");
        }
    }
}
