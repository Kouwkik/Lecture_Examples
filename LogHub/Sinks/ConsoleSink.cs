using System;
using System.Collections.Generic;
using LogHub.Model;

namespace LogHub.Sinks
{
    [SinkName("console")]
    public sealed class ConsoleSink : ILogSink
    {
        // Единый конструктор для всех приёмников, чтобы реестр создавал их
        // единообразно. Консоли конфигурация не нужна — она её игнорирует.
        public ConsoleSink(IReadOnlyDictionary<string, string> config)
        {
        }

        public void Write(LogRecord record, string formattedText)
        {
            var previous = Console.ForegroundColor;
            Console.ForegroundColor =
                record.Severity == "Error" || record.Severity == "Critical"
                    ? ConsoleColor.Red
                    : ConsoleColor.Gray;
            Console.WriteLine(formattedText);
            Console.ForegroundColor = previous;
        }

        public void Dispose()
        {
            // Консоль ресурсов не держит — освобождать нечего.
        }
    }
}
