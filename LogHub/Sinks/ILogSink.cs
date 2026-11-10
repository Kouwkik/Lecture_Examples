using System;
using LogHub.Model;

namespace LogHub.Sinks
{
    // Узкий интерфейс (ISP). В god-классе ILogSink требовал восемь методов
    // (Write, Flush, BeginTransaction, Commit, Rollback, Rotate, GetFileSize,
    // SetConsoleColor) — реализовать их целиком не мог ни один приёмник:
    // консоль не знает транзакций, база не знает ротации файлов.
    //
    // Здесь остаётся ровно то, что обязан уметь любой приёмник: записать
    // уже отформатированную строку и освободить свои ресурсы. Транзакции,
    // ротация и раскраска — внутреннее дело конкретных приёмников.
    //
    // Запись получает и саму запись, и готовый текст: форматирование не его
    // забота, а record нужен приёмникам, которым важен уровень (консоль — цвет).
    public interface ILogSink : IDisposable
    {
        void Write(LogRecord record, string formattedText);
    }
}
