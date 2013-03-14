namespace QAForum.Infrastructure
{
    using System.Data.Entity;
    using QAModels.Diagnostics;

    public interface DiagnosticsContext
    {
        DbSet<ExceptionLog> Exceptions { get; set; }
        DbSet<Diagnostic> Diagnostics { get; set; }

        void SaveChanges();
    }
}