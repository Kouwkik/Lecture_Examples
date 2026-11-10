using System;

namespace LogHub.Sinks
{
    // Помечает реализацию ILogSink именем из конфигурации. Реестр находит
    // приёмник по этому имени отражением — добавление нового приёмника не
    // требует правки существующих файлов (OCP).
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class SinkNameAttribute : Attribute
    {
        public string Name { get; }

        public SinkNameAttribute(string name)
        {
            Name = name;
        }
    }
}
