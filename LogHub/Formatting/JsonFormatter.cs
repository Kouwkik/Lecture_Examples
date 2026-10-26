using LogHub.Model;

namespace LogHub.Formatting
{
    [FormatterName("json")]
    public sealed class JsonFormatter : ILogFormatter
    {
        public string Format(LogRecord record)
        {
            return "{\"date\":\"" + record.Date.ToString("O") + "\"," +
                   "\"level\":\"" + record.Severity.ToLowerInvariant() + "\"," +
                   "\"message\":\"" + record.Message.Replace("\"", "\\\"") + "\"}";
        }
    }
}
