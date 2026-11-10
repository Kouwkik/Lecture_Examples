namespace LogHub.Config
{
    // Контракт сервиса конфигурации. Один интерфейс — три реализации
    // (классический синглтон, синглтон через Lazy<T>, обычный класс),
    // чтобы на тестах увидеть разницу между «одним экземпляром» и
    // «глобальной точкой доступа».
    public interface IConfigService
    {
        string GetValue(string key);
        void SetValue(string key, string value);
    }
}
