namespace QAForum.Infrastructure
{
    using System.Data.Entity;
    using Models.Diagnostics;

    public class EntityFrameworkDiagnosticsContext : DbContext, DiagnosticsContext
    {
        public DbSet<ExceptionLog> Exceptions { get; set; }
        public DbSet<Diagnostic> Diagnostics { get; set; }

        void DiagnosticsContext.SaveChanges()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(
                                DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ExceptionLog>().ToTable("Exceptions");
            modelBuilder.Entity<Diagnostic>()
                        .HasKey(d => d.DiagnosticsId)
                        .Property(d => d.DiagnosticsId)
                        .HasColumnName("DiagnosticsID");

            modelBuilder.Entity<ExceptionLog>()
                        .HasKey(e => e.ExceptionId)
                        .Property(e => e.ExceptionId)
                        .HasColumnName("ExceptionID");

        }
    }

}