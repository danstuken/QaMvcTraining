namespace EntityFrameworkDataLayer
{
    using System.Data.Entity;
    using QAModels;
    using QAModels.Forum;

    public class EntityFrameworkForumContext: DbContext, ForumContext
    {
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Forum>().ToTable("Forums");
            modelBuilder.Entity<User>().ToTable("aspnet_Users");
        }

        void ForumContext.SaveChanges()
        {
            base.SaveChanges();
        }
    }
}