using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using LogHub.Config;
using LogHub.Filtering;
using LogHub.Formatting;
using LogHub.Parsing;
using LogHub.Processing;
using LogHub.Reading;
using LogHub.Sinks;

namespace LogHub.App
{
    // Композиционный корень. Единственное место, где создаются конкретные
    // реализации (new / фабрики) и собирается граф зависимостей. Все сервисы
    // получают зависимости через конструктор; обращение к IServiceProvider —
    // одно, в точке входа (Program.Main). Service Locator не используется.
    public static class CompositionRoot
    {
        public static ServiceProvider Build(IConfigService config, ILogReader reader)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IConfigService>(config);
            services.AddSingleton<ILogReader>(reader);
            services.AddSingleton<ILogParser, PipeLogParser>();
            services.AddSingleton<ILogFilter>(new SeverityFilter(Or(config.GetValue("minSeverity"), "Info")));
            services.AddSingleton<ILogFormatter>(FormatterRegistry.Create(Or(config.GetValue("format"), "plain")));
            services.AddSingleton<ILogSink>(SinkRegistry.Create(Or(config.GetValue("sink"), "console"), SinkOptions(config)));
            services.AddSingleton<LogProcessor>();
            services.AddSingleton<LogHubApp>();

            return services.BuildServiceProvider();
        }

        private static string Or(string value, string fallback)
        {
            return string.IsNullOrWhiteSpace(value) ? fallback : value;
        }

        private static IReadOnlyDictionary<string, string> SinkOptions(IConfigService config)
        {
            var options = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var file = config.GetValue("file");
            if (!string.IsNullOrWhiteSpace(file))
            {
                options["file"] = file;
            }
            return options;
        }
    }
}
