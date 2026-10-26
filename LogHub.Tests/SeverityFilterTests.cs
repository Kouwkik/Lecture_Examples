using LogHub.Filtering;
using LogHub.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogHub.Tests
{
    [TestClass]
    public class SeverityFilterTests
    {
        [TestMethod]
        public void ShouldKeep_AtOrAboveMin_ReturnsTrue()
        {
            var filter = new SeverityFilter("Warning");

            Assert.IsTrue(filter.ShouldKeep(new LogRecord { Severity = "Warning" }));
            Assert.IsTrue(filter.ShouldKeep(new LogRecord { Severity = "Error" }));
            Assert.IsTrue(filter.ShouldKeep(new LogRecord { Severity = "Critical" }));
        }

        [TestMethod]
        public void ShouldKeep_BelowMin_ReturnsFalse()
        {
            var filter = new SeverityFilter("Warning");

            Assert.IsFalse(filter.ShouldKeep(new LogRecord { Severity = "Info" }));
            Assert.IsFalse(filter.ShouldKeep(new LogRecord { Severity = "Debug" }));
        }

        [TestMethod]
        public void ShouldKeep_UnknownSeverity_TreatedAsInfo()
        {
            var filter = new SeverityFilter("Info");

            // Неизвестный уровень трактуется как Info (rank 1) — как в god-классе.
            Assert.IsTrue(filter.ShouldKeep(new LogRecord { Severity = "Trace?" }));
        }
    }
}
