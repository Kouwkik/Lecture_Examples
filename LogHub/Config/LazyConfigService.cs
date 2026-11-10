using System;
using System.Collections.Generic;

namespace LogHub.Config
{
    // Синглтон через Lazy<T>: потокобезопасная ленивая инициализация без
    // ручной двойной проверки блокировки с volatile. Инициализация аккуратнее,
    // чем у классического варианта, но суть та же — одно общее статическое
    // состояние на процесс, поэтому для тестируемости это ничего не меняет.
    public sealed class LazyConfigService : IConfigService
    {
        private static readonly Lazy<LazyConfigService> _instance =
            new Lazy<LazyConfigService>(() => new LazyConfigService());

        private readonly Dictionary<string, string> _values =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private LazyConfigService()
        {
        }

        public static LazyConfigService Instance
        {
            get { return _instance.Value; }
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
