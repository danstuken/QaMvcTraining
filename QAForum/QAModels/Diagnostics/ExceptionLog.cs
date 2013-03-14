namespace QAModels.Diagnostics
{
    using System;

    public class ExceptionLog
    {
        public Guid ExceptionId { get; set; }
        public string ApplicationName { get; set; }
        public DateTime ExceptionTime { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string InnerText { get; set; }
    }
}