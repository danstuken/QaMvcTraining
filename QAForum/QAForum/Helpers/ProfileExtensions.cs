namespace QAForum.Helpers
{
    using System.Web;
    using System.Web.Mvc;
    using Models;
    
    public static class ProfileExtensions
    {
        private const string AvatarRoot = "/content/images/avatars/";

        public static MvcHtmlString Avatar(this HtmlHelper helper)
        {
            var context = HttpContext.Current;
            var imagePath = AvatarRoot + "avatar1.jpg";
            if (context.User.Identity.IsAuthenticated)
                imagePath = GetOrSetProfileAvatar();
            return MvcHtmlString.Create(BuildAvatarForImagePath(imagePath));
        }

        private static string GetOrSetProfileAvatar()
        {
            var profile = ForumProfile.GetUserProfile();
            if (string.IsNullOrEmpty(profile.AvatarFilename))
            {
                profile.AvatarFilename = "avatar1.jpg";
                profile.Save();
            }
            return AvatarRoot + profile.AvatarFilename;
        }

        private static string BuildAvatarForImagePath(string imagePath)
        {
            var imageElement = string.Format(@"<img src=""{0}"" alt=""user avatar"" width=""40"" height=""40""/>",
                                             imagePath);
            return imageElement;
        }
    }
}