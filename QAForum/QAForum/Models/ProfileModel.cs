namespace QAForum.Models
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;

    public class ProfileModel
    {
        private const string AvatarPath = @"/content/images/avatars/";

        public IEnumerable<AvatarData> GetAllAvatars()
        {
            var avatarDi = new DirectoryInfo(HttpContext.Current.Request.MapPath(AvatarPath));
            var query = from f in avatarDi.GetFiles()
                        select new AvatarData
                            {
                                AvatarName = f.Name,
                                FullPath = AvatarPath + f.Name
                            };
            return query.ToList();
        } 
    }
}