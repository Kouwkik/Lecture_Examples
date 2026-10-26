using LogHub.Model;

namespace LogHub.Filtering
{
    // Абстракция фильтрации записи. Отдельная ответственность: решает
    // «пропускать запись дальше или нет», и больше ничего.
    public interface ILogFilter
    {
        bool ShouldKeep(LogRecord record);
    }
}
