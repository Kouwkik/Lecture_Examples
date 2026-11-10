using System.Collections.Generic;
using LogHub.App;
using LogHub.Config;
using LogHub.Processing;
using LogHub.Reading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogHub.Tests
{
    // Проверка, что композиционный корень действительно собирает рабочий граф:
    // контейнер разрешает корневой объект со всеми зависимостями через
    // конструктор, а время жизни Singleton даёт один экземпляр на контейнер.
    [TestClass]
    public class CompositionRootTests
    {
        private static IConfigService Config()
        {
            return new ConfigService(new Dictionary<string, string>
            {
                { "sink", "console" },
                { "format", "plain" },
                { "minSeverity", "Info" }
            });
        }

        [TestMethod]
        public void Build_ResolvesApp_WithAllDependencies()
        {
            using (var provider = CompositionRoot.Build(Config(), new InMemoryLogReader(new string[0])))
            {
                Assert.IsNotNull(provider.GetRequiredService<LogHubApp>());
                Assert.IsNotNull(provider.GetRequiredService<LogProcessor>());
            }
        }

        [TestMethod]
        public void Build_ProcessorIsSingleton_WithinContainer()
        {
            using (var provider = CompositionRoot.Build(Config(), new InMemoryLogReader(new string[0])))
            {
                Assert.AreSame(
                    provider.GetRequiredService<LogProcessor>(),
                    provider.GetRequiredService<LogProcessor>());
            }
        }

        [TestMethod]
        public void Build_WiredProcessor_CountsCorrectly()
        {
            var reader = new InMemoryLogReader(new[]
            {
                "2026-01-15 10:00:00|Info|ok",
                "мусор"
            });

            using (var provider = CompositionRoot.Build(Config(), reader))
            {
                var processor = provider.GetRequiredService<LogProcessor>();
                var result = processor.Process(provider.GetRequiredService<ILogReader>());

                Assert.AreEqual(1, result.Written);
                Assert.AreEqual(1, result.Skipped);
            }
        }
    }
}
