namespace QAForum.Filters.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.Web;
    using System.Web.Mvc;
    using Infrastructure;
    using QAModels.Diagnostics;

    public class DiagnosticFilter : ActionFilterAttribute
    {
        private readonly Stopwatch _timer = new Stopwatch();
        private const string FormatString = @"<h2>Diagnostic information</h2><div ><table><tr><td>Web Server:</td><td>{0}</td></tr><tr><td>Browser:</td><td>{1}</td></tr><tr><td>Controller</td><td>{2}</td></tr><tr><td>Action:</td><td>{3}</td></tr><tr><td>Execution Time(ms):</td><td>{4}</td></tr></table></div>";

        public DiagnosticsContext DiagnosticsContext { get; set; }
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _timer.Start();
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _timer.Stop();
            var browser = context.HttpContext.Request.Browser;
            var entry = string.Format(
                FormatString,
                HttpContext.Current.Request.ServerVariables["SERVER_NAME"],
                String.Format("{0} ({1})", browser.Browser, browser.Version),
                context.RouteData.Values["controller"],
                context.RouteData.Values["action"],
                _timer.ElapsedMilliseconds);
            SaveDiagnosticsEntry(entry);
        }

        private void SaveDiagnosticsEntry(string entry)
        {
            if (DiagnosticsContext == null)
                throw new ArgumentNullException("DiagnosticsContext");

            DiagnosticsContext.Diagnostics.Add(new Diagnostic
                {
                    DiagnosticsId = Guid.NewGuid(),
                    ApplicationName = "QA Forum",
                    Data = entry,
                    DiagnosticsTime = DateTime.UtcNow
                });
            DiagnosticsContext.SaveChanges();
        }
    }
}