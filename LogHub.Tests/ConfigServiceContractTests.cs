using LogHub.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogHub.Tests
{
    // Все три реализации выполняют один контракт IConfigService.
    [TestClass]
    public class ConfigServiceContractTests
    {
        [TestMethod]
        public void Plain_SetThenGet_ReturnsValue()
        {
            var config = new ConfigService();
            config.SetValue("contract_plain", "v1");
            Assert.AreEqual("v1", config.GetValue("contract_plain"));
        }

        [TestMethod]
        public void Classic_SetThenGet_ReturnsValue()
        {
            ClassicConfigService.Instance().SetValue("contract_classic", "v2");
            Assert.AreEqual("v2", ClassicConfigService.Instance().GetValue("contract_classic"));
        }

        [TestMethod]
        public void Lazy_SetThenGet_ReturnsValue()
        {
            LazyConfigService.Instance.SetValue("contract_lazy", "v3");
            Assert.AreEqual("v3", LazyConfigService.Instance.GetValue("contract_lazy"));
        }

        [TestMethod]
        public void UnknownKey_ReturnsNull()
        {
            Assert.IsNull(new ConfigService().GetValue("missing"));
        }
    }
}
