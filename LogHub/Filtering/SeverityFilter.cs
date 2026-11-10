using LogHub.Model;

namespace LogHub.Filtering
{
    // Фильтр по минимальному уровню — та же логика, что была в god-классе
    // внутри цикла Run, но вынесенная в отдельный класс с одной причиной
    // изменения.
    public sealed class SeverityFilter : ILogFilter
    {
        private readonly int _minRank;

        public SeverityFilter(string minSeverity)
        {
            _minRank = SeverityScale.ToRank(minSeverity);
        }

        public bool ShouldKeep(LogRecord record)
        {
            return SeverityScale.ToRank(record.Severity) >= _minRank;
        }
    }
}
