namespace EntityFrameworkDataLayer
{
    using System.Data.Entity;
    using QAModels.Forum;
    using QAModels.Membership;

    public class EntityFrameworkForumContext: DbContext, ForumContext
    {
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Forum>().ToTable("Forums");
            modelBuilder.Entity<User>().ToTable("aspnet_Users");
            modelBuilder.Entity<Member>().ToTable("aspnet_Membership");
            modelBuilder.Entity<Member>().HasKey(m => m.UserId);
        }

        void ForumContext.SaveChanges()
        {
            base.SaveChanges();
        }
    }
}