namespace EntityFrameworkDataLayer
{
    using System.Data.Entity;
    using QAModels.Forum;
    using QAModels.Membership;

    public interface ForumContext
    {
        DbSet<Forum> Forums { get; set; }
        DbSet<Thread> Threads { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Member> Members { get; set; }

        void SaveChanges();
    }
}