using System.Collections.Generic;

namespace LogHub.Reading
{
    // Источник для тестов: ни байта на диск. Именно он позволяет проверять
    // разбор и маршрутизацию без файловой системы.
    public sealed class InMemoryLogReader : ILogReader
    {
        private readonly IEnumerable<string> _lines;

        public InMemoryLogReader(IEnumerable<string> lines)
        {
            _lines = lines;
        }

        public IEnumerable<string> ReadLines()
        {
            return _lines;
        }
    }
}
