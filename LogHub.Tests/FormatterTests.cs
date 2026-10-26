using System;
using LogHub.Formatting;
using LogHub.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogHub.Tests
{
    [TestClass]
    public class FormatterTests
    {
        private static LogRecord Sample()
        {
            return new LogRecord
            {
                Date = new DateTime(2026, 1, 15, 10, 0, 0, DateTimeKind.Unspecified),
                Severity = "Error",
                Message = "сбой \"модуля\""
            };
        }

        [TestMethod]
        public void Plain_HasExpectedShape()
        {
            var text = new PlainFormatter().Format(Sample());
            Assert.AreEqual("2026-01-15 10:00:00 [Error] сбой \"модуля\"", text);
        }

        [TestMethod]
        public void Json_LowercasesLevel_AndEscapesQuotes()
        {
            var text = new JsonFormatter().Format(Sample());

            Assert.IsTrue(text.Contains("\"level\":\"error\""), text);
            Assert.IsTrue(text.Contains("\\\"модуля\\\""), text);
        }

        [TestMethod]
        public void Csv_UsesSemicolonSeparator()
        {
            var text = new CsvFormatter().Format(Sample());
            Assert.IsTrue(text.Contains(";Error;"), text);
        }
    }
}
