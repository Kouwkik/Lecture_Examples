using System;
using Microsoft.Extensions.DependencyInjection;
using LogHub.App;
using LogHub.Config;
using LogHub.Reading;

namespace LogHub
{
    // Точка входа и композиционный корень приложения.
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Использование: dotnet run --project LogHub -- <путь-к-логу> [профиль-конфигурации]");
                Console.Error.WriteLine("Профили: loghub.console.config (в консоль), loghub.file.config (в файл)");
                return 1;
            }

            var logPath = args[0];
            var configPath = args.Length > 1 ? args[1] : "loghub.console.config";

            // Загрузка выбранного профиля конфигурации. Смена профиля меняет
            // поведение без перекомпиляции — передаётся другой файл.
            var config = ConfigProfiles.Load(configPath);

            // new FileLogReader — единственное создание сервиса вне контейнера,
            // и оно здесь, в композиционном корне.
            using (var provider = CompositionRoot.Build(config, new FileLogReader(logPath)))
            {
                // Единственное за всё приложение обращение к IServiceProvider.
                var app = provider.GetRequiredService<LogHubApp>();
                return app.Run();
            }
        }
    }
}
