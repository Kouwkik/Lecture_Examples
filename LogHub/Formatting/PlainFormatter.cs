using LogHub.Model;

namespace LogHub.Formatting
{
    [FormatterName("plain")]
    public sealed class PlainFormatter : ILogFormatter
    {
        public string Format(LogRecord record)
        {
            return $"{record.Date:yyyy-MM-dd HH:mm:ss} [{record.Severity}] {record.Message}";
        }
    }
}
