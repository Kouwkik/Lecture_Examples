using LogHub.Model;

namespace LogHub.Formatting
{
    [FormatterName("csv")]
    public sealed class CsvFormatter : ILogFormatter
    {
        public string Format(LogRecord record)
        {
            return $"{record.Date:O};{record.Severity};{record.Message}";
        }
    }
}
