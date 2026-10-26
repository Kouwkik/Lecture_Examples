using System;
using System.Collections.Generic;
using System.IO;
using LogHub.Model;

namespace LogHub.Sinks
{
    [SinkName("file")]
    public sealed class FileSink : ILogSink
    {
        private readonly StreamWriter _writer;

        public FileSink(IReadOnlyDictionary<string, string> config)
        {
            string path;
            if (config == null || !config.TryGetValue("file", out path) || string.IsNullOrWhiteSpace(path))
            {
                // Имя по умолчанию — как в исходном god-классе.
                path = $"logs-{DateTime.Now:yyyyMMdd}.txt";
            }

            _writer = new StreamWriter(path, append: true);
        }

        public void Write(LogRecord record, string formattedText)
        {
            _writer.WriteLine(formattedText);
            _writer.Flush();
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
