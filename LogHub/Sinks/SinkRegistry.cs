using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LogHub.Sinks
{
    // OCP: новый приёмник = новый класс с атрибутом [SinkName(...)].
    // Реестр находит его отражением по сборке, поэтому добавление приёмника
    // не требует правки ни этого файла, ни любого другого существующего —
    // только появляется новый файл (проверяется `git diff --name-only`).
    public static class SinkRegistry
    {
        public static ILogSink Create(string name, IReadOnlyDictionary<string, string> config)
        {
            var type = Discover().FirstOrDefault(t =>
                string.Equals(
                    t.GetCustomAttribute<SinkNameAttribute>().Name,
                    name,
                    StringComparison.OrdinalIgnoreCase));

            if (type == null)
            {
                throw new NotSupportedException($"Приёмник '{name}' не зарегистрирован");
            }

            return (ILogSink)Activator.CreateInstance(type, config);
        }

        public static IEnumerable<string> Names()
        {
            return Discover().Select(t => t.GetCustomAttribute<SinkNameAttribute>().Name);
        }

        private static IEnumerable<Type> Discover()
        {
            return typeof(SinkRegistry).Assembly.GetTypes()
                .Where(t => typeof(ILogSink).IsAssignableFrom(t)
                            && !t.IsAbstract
                            && !t.IsInterface
                            && t.GetCustomAttribute<SinkNameAttribute>() != null);
        }
    }
}
