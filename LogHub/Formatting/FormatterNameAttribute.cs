using System;

namespace LogHub.Formatting
{
    // Помечает реализацию ILogFormatter именем из конфигурации.
    // Реестр находит форматтер по этому имени отражением — добавление
    // нового формата не требует правки существующих файлов (OCP).
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class FormatterNameAttribute : Attribute
    {
        public string Name { get; }

        public FormatterNameAttribute(string name)
        {
            Name = name;
        }
    }
}
