using System;
using System.Globalization;
using LogHub.Model;

namespace LogHub.Parsing
{
    // Формат «дата|уровень|сообщение» — тот же, что был зашит в god-классе.
    // Теперь он за интерфейсом: другой формат входных логов = другая
    // реализация ILogParser, без правки конвейера. В ЛР3 выбор реализации
    // станет Стратегией, задаваемой конфигурацией.
    public sealed class PipeLogParser : ILogParser
    {
        public bool TryParse(string line, out LogRecord record)
        {
            record = null;

            if (line == null)
            {
                return false;
            }

            var parts = line.Split('|', 3);
            if (parts.Length != 3)
            {
                return false;
            }

            if (!DateTime.TryParse(parts[0], CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return false;
            }

            record = new LogRecord
            {
                Date = date,
                Severity = parts[1].Trim(),
                Message = parts[2].Trim()
            };
            return true;
        }
    }
}
