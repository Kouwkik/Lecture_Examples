using System.Collections.Generic;
using LogHub.Filtering;
using LogHub.Formatting;
using LogHub.Parsing;
using LogHub.Processing;
using LogHub.Reading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogHub.Tests
{
    [TestClass]
    public class LogProcessorTests
    {
        private static LogProcessor Build(CapturingSink sink, string minSeverity)
        {
            return new LogProcessor(
                new PipeLogParser(),
                new SeverityFilter(minSeverity),
                new PlainFormatter(),
                sink);
        }

        [TestMethod]
        public void Process_RoutesEveryKeptRecord_ToSink()
        {
            var sink = new CapturingSink();
            var reader = new InMemoryLogReader(new List<string>
            {
                "2026-01-15 10:00:00|Info|старт",
                "2026-01-15 10:00:01|Error|ошибка"
            });

            var result = Build(sink, "Info").Process(reader);

            Assert.AreEqual(2, result.Written);
            Assert.AreEqual(0, result.Skipped);
            Assert.AreEqual(2, sink.Lines.Count);
            Assert.AreEqual("2026-01-15 10:00:00 [Info] старт", sink.Lines[0]);
        }

        [TestMethod]
        public void Process_CountsSkipped_ForMalformedAndFiltered()
        {
            var sink = new CapturingSink();
            var reader = new InMemoryLogReader(new List<string>
            {
                "2026-01-15 10:00:00|Info|держим",       // оставить
                "2026-01-15 10:00:01|Debug|тихо",         // отфильтровать
                "мусор",                                   // битая
                "2026-01-15 10:00:02|Critical|падаем",    // оставить
                "не|хватает",                              // битая (2 поля)
                "плохая-дата|Info|текст"                   // битая дата
            });

            var result = Build(sink, "Info").Process(reader);

            Assert.AreEqual(2, result.Written);
            Assert.AreEqual(4, result.Skipped);
        }

        [TestMethod]
        public void Process_PreservesGodObjectBehaviour_OnReferenceInput()
        {
            var sink = new CapturingSink();
            var reader = new InMemoryLogReader(ReferenceLog.Lines);

            var result = Build(sink, "Info").Process(reader);

            Assert.AreEqual(ReferenceLog.ExpectedWritten, result.Written);
            Assert.AreEqual(ReferenceLog.ExpectedSkipped, result.Skipped);
        }

        [TestMethod]
        public void Process_RunsEntirelyInMemory_NoDiskNeeded()
        {
            // Если бы процессор обращался к файлу или консоли напрямую,
            // этот тест был бы невозможен без диска. Он проходит в памяти (DIP).
            var sink = new CapturingSink();
            var reader = new InMemoryLogReader(new[] { "2026-01-15 10:00:00|Debug|x" });

            var result = Build(sink, "Debug").Process(reader);

            Assert.AreEqual(1, result.Written);
            Assert.AreEqual(1, sink.Records.Count);
        }
    }
}
