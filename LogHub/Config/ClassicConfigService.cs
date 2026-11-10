using System;
using System.Collections.Generic;

namespace LogHub.Config
{
    // Классический синглтон: приватный конструктор и статический Instance().
    // Хранит ИЗМЕНЯЕМОЕ СТАТИЧЕСКОЕ состояние — одно на весь процесс.
    // Именно поэтому состояние переносится между тестами (см.
    // ClassicSingletonLeakTests): это глобальная точка доступа, а не просто
    // «один экземпляр».
    public sealed class ClassicConfigService : IConfigService
    {
        private static ClassicConfigService _instance;

        private readonly Dictionary<string, string> _values =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private ClassicConfigService()
        {
        }

        public static ClassicConfigService Instance()
        {
            if (_instance == null)
            {
                _instance = new ClassicConfigService();
            }
            return _instance;
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
