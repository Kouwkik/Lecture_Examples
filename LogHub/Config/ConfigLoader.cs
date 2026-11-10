using System;
using System.Collections.Generic;
using System.IO;

namespace LogHub.Config
{
    // Чтение конфигурации вынесено из бизнес-логики в один класс с одной
    // причиной изменения — формат конфигурационного файла. В god-классе
    // этим занимался метод LoadConfig внутри того же класса, что и всё
    // остальное.
    public static class ConfigLoader
    {
        public static IReadOnlyDictionary<string, string> Load(string path)
        {
            var config = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "sink", "console" },
                { "minSeverity", "Info" },
                { "format", "plain" }
            };

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return config;
            }

            foreach (var line in File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#", StringComparison.Ordinal))
                {
                    continue;
                }

                var parts = line.Split('=', 2);
                if (parts.Length == 2)
                {
                    config[parts[0].Trim()] = parts[1].Trim();
                }
            }

            return config;
        }
    }
}
