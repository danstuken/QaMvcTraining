namespace QAForum.Tags
{
    using System.Collections.Generic;
    using System.Linq;
    using Providers;

    public class SqlPostsTagCloudProvider : TagCloudProvider
    {
        private readonly ForumProvider _forumProvider;

        public SqlPostsTagCloudProvider(ForumProvider forumProvider)
        {
            _forumProvider = forumProvider;
        }

        public IDictionary<string, int> GetTagCloud()
        {
            var postBodies = _forumProvider.GetAllPosts().Select(p => p.PostBody);
            var tagCloud = new TagCloud();
            foreach (var postBody in postBodies)
                tagCloud.AddTagsFromText(postBody);
            return tagCloud.ToDictionary();
        }
    }
}