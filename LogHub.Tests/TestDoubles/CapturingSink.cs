using System.Collections.Generic;
using LogHub.Model;
using LogHub.Sinks;

namespace LogHub.Tests
{
    // Перехватывающий приёмник для тестов: ничего не пишет на диск или в
    // консоль, только запоминает, что ему передали. Доказывает, что
    // LogProcessor зависит от абстракции ILogSink, а не от конкретного вывода.
    internal sealed class CapturingSink : ILogSink
    {
        public List<string> Lines { get; } = new List<string>();
        public List<LogRecord> Records { get; } = new List<LogRecord>();
        public bool Disposed { get; private set; }

        public void Write(LogRecord record, string formattedText)
        {
            Records.Add(record);
            Lines.Add(formattedText);
        }

        public void Dispose()
        {
            Disposed = true;
        }
    }
}
