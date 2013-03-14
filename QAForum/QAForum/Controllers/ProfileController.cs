namespace QAForum.Controllers
{
    using System.Web.Mvc;
    using Models;

    public class ProfileController : Controller
    {
        public ActionResult SelectAvatar()
        {
            var profileModel = new ProfileModel();
            ViewBag.AvatarList = profileModel.GetAllAvatars();
            return View();
        }

        public ActionResult UpdateProfileAvatar(string avatarName)
        {
            var currentUsersProfile = ForumProfile.GetUserProfile();
            currentUsersProfile.AvatarFilename = avatarName;
            currentUsersProfile.Save();
            return RedirectToAction("Index", "Home");
        }
    }
}
