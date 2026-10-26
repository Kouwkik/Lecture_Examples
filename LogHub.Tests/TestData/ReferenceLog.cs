using System.Collections.Generic;

namespace LogHub.Tests
{
    // Эталонный набор строк для проверки, что рефакторинг не изменил
    // поведение. Счётчики посчитаны по логике исходного god-object
    // при minSeverity = Info.
    internal static class ReferenceLog
    {
        public static readonly List<string> Lines = new List<string>
        {
            "2026-01-15 08:00:00|Info|Сервис запущен",               // оставить
            "2026-01-15 08:00:01|Debug|Подробности инициализации",   // отфильтровать (Debug < Info)
            "2026-01-15 08:00:02|Warning|Мало места на диске",       // оставить
            "кривая строка без разделителей",                         // битая (1 поле)
            "2026-01-15 08:00:03|Error|NullReference в модуле X",     // оставить
            "2026-01-15 08:00:04|Info|Запрос обработан",             // оставить
            "2026-13-40 99:00:00|Info|битая дата",                   // битая дата
            "2026-01-15 08:00:05|Critical|Нет памяти",               // оставить
            "только|два поля",                                        // битая (2 поля)
            "2026-01-15 08:00:06|Debug|ещё debug",                   // отфильтровать
            "2026-01-15 08:00:07|Error|Повторная ошибка",            // оставить
            "#комментарий, не лог"                                    // битая (1 поле)
        };

        public const int ExpectedWritten = 6;
        public const int ExpectedSkipped = 6;
    }
}
