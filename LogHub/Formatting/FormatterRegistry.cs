using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LogHub.Formatting
{
    // OCP: новый формат вывода = новый класс с атрибутом [FormatterName(...)].
    // Реестр находит его отражением по сборке, поэтому этот файл при добавлении
    // формата править не нужно.
    public static class FormatterRegistry
    {
        public static ILogFormatter Create(string name)
        {
            var type = Discover().FirstOrDefault(t =>
                string.Equals(
                    t.GetCustomAttribute<FormatterNameAttribute>().Name,
                    name,
                    StringComparison.OrdinalIgnoreCase));

            if (type == null)
            {
                throw new NotSupportedException($"Формат '{name}' не зарегистрирован");
            }

            return (ILogFormatter)Activator.CreateInstance(type);
        }

        public static IEnumerable<string> Names()
        {
            return Discover().Select(t => t.GetCustomAttribute<FormatterNameAttribute>().Name);
        }

        private static IEnumerable<Type> Discover()
        {
            return typeof(FormatterRegistry).Assembly.GetTypes()
                .Where(t => typeof(ILogFormatter).IsAssignableFrom(t)
                            && !t.IsAbstract
                            && !t.IsInterface
                            && t.GetCustomAttribute<FormatterNameAttribute>() != null);
        }
    }
}
