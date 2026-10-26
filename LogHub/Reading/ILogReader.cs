using System.Collections.Generic;

namespace LogHub.Reading
{
    // Абстракция источника строк. Благодаря ей конвейер не знает, откуда
    // пришли данные — с диска, из памяти или из сети (DIP).
    public interface ILogReader
    {
        IEnumerable<string> ReadLines();
    }
}
