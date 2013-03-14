namespace QAForum.Models
{
    using System.Web.Profile;
    using System.Web.Security;

    public class ForumProfile : ProfileBase
    {
        private const string AvatarFilenameKey = @"AvatarFilename";

        public static ForumProfile GetUserProfile(string username)
        {
            return Create(username) as ForumProfile;
        }

        public static ForumProfile GetUserProfile()
        {
            var currentUser = Membership.GetUser();
            return currentUser == null ? null : GetUserProfile(currentUser.UserName);
        }

        [SettingsAllowAnonymous(false)]
        public string AvatarFilename
        {
            get { return base[AvatarFilenameKey] as string; }
            set { base[AvatarFilenameKey] = value; }
        }
    }
}