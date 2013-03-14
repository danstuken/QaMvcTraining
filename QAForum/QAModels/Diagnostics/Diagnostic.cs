namespace QAModels.Diagnostics
{
    using System;

    public class Diagnostic
    {     
        public Guid DiagnosticsId { get; set; }
        public string ApplicationName { get; set; }
        public DateTime DiagnosticsTime { get; set; }
        public string Data { get; set; }
    }
}