using LogHub.Filtering;
using LogHub.Formatting;
using LogHub.Model;
using LogHub.Parsing;
using LogHub.Reading;
using LogHub.Sinks;

namespace LogHub.Processing
{
    // Единственная ответственность: прогнать строки через конвейер
    // чтение -> разбор -> фильтр -> формат -> запись и посчитать итог.
    //
    // Зависит только от абстракций (DIP): ни File, ни Console, ни БД здесь
    // нет, поэтому класс тестируется целиком в памяти — с InMemoryLogReader
    // на входе и перехватывающим приёмником на выходе.
    public sealed class LogProcessor
    {
        private readonly ILogParser _parser;
        private readonly ILogFilter _filter;
        private readonly ILogFormatter _formatter;
        private readonly ILogSink _sink;

        public LogProcessor(ILogParser parser, ILogFilter filter, ILogFormatter formatter, ILogSink sink)
        {
            _parser = parser;
            _filter = filter;
            _formatter = formatter;
            _sink = sink;
        }

        public ProcessingResult Process(ILogReader reader)
        {
            int written = 0;
            int skipped = 0;

            foreach (var line in reader.ReadLines())
            {
                LogRecord record;
                if (!_parser.TryParse(line, out record))
                {
                    skipped++;
                    continue;
                }

                if (!_filter.ShouldKeep(record))
                {
                    skipped++;
                    continue;
                }

                _sink.Write(record, _formatter.Format(record));
                written++;
            }

            return new ProcessingResult(written, skipped);
        }
    }
}
