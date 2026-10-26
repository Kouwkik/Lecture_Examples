using System;
using LogHub.Config;
using LogHub.Filtering;
using LogHub.Formatting;
using LogHub.Parsing;
using LogHub.Processing;
using LogHub.Reading;
using LogHub.Sinks;

namespace LogHub
{
    // Композиционный корень — единственное место, где собираются конкретные
    // реализации и где живут обращения к файлам и консоли. В ЛР2 эта сборка
    // переедет в DI-контейнер.
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Использование: dotnet run --project LogHub -- <путь-к-логу> [путь-к-конфигу]");
                Console.Error.WriteLine("Приёмники: " + string.Join(", ", SinkRegistry.Names()));
                Console.Error.WriteLine("Форматы:   " + string.Join(", ", FormatterRegistry.Names()));
                return 1;
            }

            var logPath = args[0];
            var configPath = args.Length > 1 ? args[1] : "loghub.config";

            var config = ConfigLoader.Load(configPath);

            ILogReader reader = new FileLogReader(logPath);
            ILogParser parser = new PipeLogParser();
            ILogFilter filter = new SeverityFilter(config["minSeverity"]);
            ILogFormatter formatter = FormatterRegistry.Create(config["format"]);

            using (var sink = SinkRegistry.Create(config["sink"], config))
            {
                var processor = new LogProcessor(parser, filter, formatter, sink);
                var result = processor.Process(reader);
                Console.WriteLine($"Записано: {result.Written}, пропущено: {result.Skipped}");
            }

            return 0;
        }
    }
}
