using LogHub.Model;

namespace LogHub.Formatting
{
    // Абстракция форматирования записи в строку вывода.
    public interface ILogFormatter
    {
        string Format(LogRecord record);
    }
}
