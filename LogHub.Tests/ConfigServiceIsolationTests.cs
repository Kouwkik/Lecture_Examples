using System.Collections.Generic;
using LogHub.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogHub.Tests
{
    // Обратная сторона: обычный класс и его регистрация в контейнере
    // изолируемы. Каждый тест получает свежий экземпляр или свежий контейнер,
    // поэтому тест, зависящий от порядка, для них невозможен в принципе —
    // все тесты ниже проходят при любом порядке выполнения.
    [TestClass]
    public class ConfigServiceIsolationTests
    {
        [TestMethod]
        public void Plain_FreshInstance_StartsClean_EvenAfterAnotherMutates()
        {
            var first = new ConfigService();
            first.SetValue("profile", "file");

            var second = new ConfigService();
            Assert.IsNull(second.GetValue("profile"));
        }

        [TestMethod]
        public void ContainerSingleton_IsOnePerContainer()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IConfigService, ConfigService>()
                .BuildServiceProvider();

            var a = provider.GetRequiredService<IConfigService>();
            var b = provider.GetRequiredService<IConfigService>();

            Assert.AreSame(a, b); // AddSingleton — один экземпляр на контейнер
        }

        [TestMethod]
        public void ContainerSingleton_FreshContainer_StartsClean()
        {
            var first = new ServiceCollection()
                .AddSingleton<IConfigService, ConfigService>()
                .BuildServiceProvider();
            first.GetRequiredService<IConfigService>().SetValue("profile", "file");

            // Новый контейнер — новый экземпляр, состояние не переносится.
            var second = new ServiceCollection()
                .AddSingleton<IConfigService, ConfigService>()
                .BuildServiceProvider();

            Assert.IsNull(second.GetRequiredService<IConfigService>().GetValue("profile"));
        }
    }
}
