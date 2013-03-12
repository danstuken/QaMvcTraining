namespace QAForum.Models.Forum
{
    using System;

    public class Post
    {
        public int PostId { get; set; }
        public Guid UserId { get; set; }
        public int ThreadId { get; set; }
        public string PostTitle { get; set; }
        public DateTime PostDateTime { get; set; }
        public string PostBody { get; set; }
    }
}