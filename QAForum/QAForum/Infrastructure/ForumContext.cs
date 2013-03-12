namespace QAForum.Infrastructure
{
    using System.Data.Entity;
    using Models;
    using Models.Forum;

    public interface ForumContext
    {
        DbSet<Forum> Forums { get; set; }
        DbSet<Thread> Threads { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<User> Users { get; set; }

        void SaveChanges();
    }
}