namespace QAForum.Tags
{
    using System.Collections.Generic;

    public class TagCloud
    {
        private readonly IDictionary<string, int> _tagsAndCounts = new Dictionary<string, int>();

        public void AddTagsFromText(string text)
        {
            foreach (var tag in text.Split(' '))
                AddTag(tag);
        }

        public IDictionary<string, int> ToDictionary()
        {
            return new Dictionary<string, int>(_tagsAndCounts);
        }

        private void AddTag(string tag)
        {
            if (_tagsAndCounts.ContainsKey(tag))
                _tagsAndCounts[tag] ++;
            else
                _tagsAndCounts[tag] = 1;
        }
    }
}