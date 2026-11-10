namespace LogHub.Config
{
    // Загрузка профиля конфигурации из файла в обычный ConfigService.
    // Переключение профиля (console/file) — это другой файл конфигурации,
    // переданный при запуске; перекомпиляция не нужна.
    public static class ConfigProfiles
    {
        public static IConfigService Load(string path)
        {
            var values = ConfigLoader.Load(path);
            return new ConfigService(values);
        }
    }
}
