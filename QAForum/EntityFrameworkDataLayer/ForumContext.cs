namespace EntityFrameworkDataLayer
{
    using System.Data.Entity;
    using QAModels;
    using QAModels.Forum;

    public interface ForumContext
    {
        DbSet<Forum> Forums { get; set; }
        DbSet<Thread> Threads { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<User> Users { get; set; }

        void SaveChanges();
    }
}