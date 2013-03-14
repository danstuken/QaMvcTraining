namespace QAForum.Tags
{
    using System.Collections.Generic;

    public interface TagCloudProvider
    {
        IDictionary<string, int> GetTagCloud();
    }
}