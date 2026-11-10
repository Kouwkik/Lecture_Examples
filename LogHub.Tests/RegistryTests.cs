using System;
using System.Collections.Generic;
using System.Linq;
using LogHub.Formatting;
using LogHub.Sinks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogHub.Tests
{
    [TestClass]
    public class RegistryTests
    {
        [TestMethod]
        public void SinkRegistry_DiscoversBuiltInSinks_ByReflection()
        {
            var names = SinkRegistry.Names().ToList();

            Assert.IsTrue(names.Contains("console"), "console должен находиться отражением");
            Assert.IsTrue(names.Contains("file"), "file должен находиться отражением");
        }

        [TestMethod]
        public void FormatterRegistry_CreatesByName()
        {
            Assert.AreEqual(typeof(PlainFormatter), FormatterRegistry.Create("plain").GetType());
            Assert.AreEqual(typeof(JsonFormatter), FormatterRegistry.Create("json").GetType());
            Assert.AreEqual(typeof(CsvFormatter), FormatterRegistry.Create("csv").GetType());
        }

        [TestMethod]
        public void FormatterRegistry_IsCaseInsensitive()
        {
            Assert.AreEqual(typeof(JsonFormatter), FormatterRegistry.Create("JSON").GetType());
        }

        [TestMethod]
        public void SinkRegistry_UnknownName_Throws()
        {
            try
            {
                SinkRegistry.Create("telegram", new Dictionary<string, string>());
                Assert.Fail("Ожидалось NotSupportedException для незарегистрированного приёмника");
            }
            catch (NotSupportedException)
            {
                // Ожидаемо: приёмника с таким именем нет.
            }
        }
    }
}
