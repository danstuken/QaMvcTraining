namespace QAModels.Forum
{
    using System;

    public class Thread
    {
        public int ThreadId { get; set; }
        public int ForumId { get; set; }
        public string ThreadTitle { get; set; }
        public Guid OwnerId { get; set; }
    }
}