using System;

namespace LogHub.Model
{
    // Одна запись лога. Та же тройка полей, что и в исходном god-классе,
    // но вынесена в отдельный файл и не знает ни про разбор, ни про вывод.
    public sealed class LogRecord
    {
        public DateTime Date { get; set; }
        public string Severity { get; set; }
        public string Message { get; set; }
    }
}
