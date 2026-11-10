using LogHub.Model;

namespace LogHub.Parsing
{
    // Абстракция разбора строки в запись. Явный bool вместо «молча вернуть
    // null», как было в god-классе: вызывающий обязан обработать неуспех,
    // и это видно из сигнатуры.
    public interface ILogParser
    {
        bool TryParse(string line, out LogRecord record);
    }
}
