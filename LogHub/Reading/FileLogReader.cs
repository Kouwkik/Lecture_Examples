using System.Collections.Generic;
using System.IO;

namespace LogHub.Reading
{
    // Единственное место LogHub, которое читает лог с диска.
    public sealed class FileLogReader : ILogReader
    {
        private readonly string _path;

        public FileLogReader(string path)
        {
            _path = path;
        }

        public IEnumerable<string> ReadLines()
        {
            return File.ReadLines(_path);
        }
    }
}
