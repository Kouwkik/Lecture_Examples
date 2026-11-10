using LogHub.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogHub.Tests
{
    [TestClass]
    public class PipeLogParserTests
    {
        private readonly PipeLogParser _parser = new PipeLogParser();

        [TestMethod]
        public void TryParse_ValidLine_ReturnsRecordWithTrimmedFields()
        {
            var ok = _parser.TryParse("2026-01-15 10:00:00|Error| Диск заполнен ", out var record);

            Assert.IsTrue(ok);
            Assert.AreEqual("Error", record.Severity);
            Assert.AreEqual("Диск заполнен", record.Message);
            Assert.AreEqual(2026, record.Date.Year);
            Assert.AreEqual(10, record.Date.Hour);
        }

        [TestMethod]
        public void TryParse_WrongFieldCount_ReturnsFalseAndNull()
        {
            Assert.IsFalse(_parser.TryParse("нет разделителей вообще", out var r1));
            Assert.IsNull(r1);

            Assert.IsFalse(_parser.TryParse("2026-01-15 10:00:00|Info", out var r2));
            Assert.IsNull(r2);
        }

        [TestMethod]
        public void TryParse_BadDate_ReturnsFalse()
        {
            Assert.IsFalse(_parser.TryParse("не-дата|Info|текст", out var record));
            Assert.IsNull(record);
        }

        [TestMethod]
        public void TryParse_Null_ReturnsFalse()
        {
            Assert.IsFalse(_parser.TryParse(null, out var record));
            Assert.IsNull(record);
        }
    }
}
