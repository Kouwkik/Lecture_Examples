using System;
using LogHub.Processing;
using LogHub.Reading;

namespace LogHub.App
{
    // Корневой объект приложения. Все зависимости приходят через конструктор,
    // контейнер внедряет их сам. Класс не знает, откуда взялись процессор и
    // источник, и не обращается к провайдеру.
    public sealed class LogHubApp
    {
        private readonly LogProcessor _processor;
        private readonly ILogReader _reader;

        public LogHubApp(LogProcessor processor, ILogReader reader)
        {
            _processor = processor;
            _reader = reader;
        }

        public int Run()
        {
            var result = _processor.Process(_reader);
            Console.WriteLine($"Записано: {result.Written}, пропущено: {result.Skipped}");
            return 0;
        }
    }
}
