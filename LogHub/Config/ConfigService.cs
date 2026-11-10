using System;
using System.Collections.Generic;

namespace LogHub.Config
{
    // Обычный класс: публичный конструктор, никакого статического состояния.
    // Временем жизни управляет DI-контейнер (AddSingleton/AddScoped/...).
    // Каждый тест может создать свой экземпляр — состояние не переносится.
    public sealed class ConfigService : IConfigService
    {
        private readonly Dictionary<string, string> _values;

        public ConfigService()
        {
            _values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        public ConfigService(IReadOnlyDictionary<string, string> initial)
        {
            _values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (initial != null)
            {
                foreach (var pair in initial)
                {
                    _values[pair.Key] = pair.Value;
                }
            }
        }

        public string GetValue(string key)
        {
            return _values.TryGetValue(key, out var value) ? value : null;
        }

        public void SetValue(string key, string value)
        {
            _values[key] = value;
        }
    }
}
