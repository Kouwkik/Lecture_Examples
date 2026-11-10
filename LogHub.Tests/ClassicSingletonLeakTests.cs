using LogHub.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogHub.Tests
{
    // Демонстрация проблемы классического синглтона: статическое состояние
    // переносится между тестами, и результат зависит от порядка выполнения.
    // Это главный пункт работы — проблема воспроизводится, а не описывается.
    [TestClass]
    public class ClassicSingletonLeakTests
    {
        private const string Key = "leak_profile";

        // Мутирует общий статический экземпляр.
        [TestMethod]
        public void A_MutatesSharedSingleton()
        {
            ClassicConfigService.Instance().SetValue(Key, "file");
            Assert.AreEqual("file", ClassicConfigService.Instance().GetValue(Key));
        }

        // Ожидает чистое состояние. В ОДИНОЧКУ (--filter) проходит, в полном
        // НАБОРЕ после A_MutatesSharedSingleton — падает: синглтон переносит
        // состояние между тестами.
        //
        // Помечен [Ignore], чтобы CI оставался зелёным. Чтобы воспроизвести
        // падение, временно снимите атрибут [Ignore] и запустите весь набор:
        //   dotnet test
        // а затем в одиночку:
        //   dotnet test --filter "FullyQualifiedName~B_ExpectsCleanState"
        [Ignore("Тест зависит от порядка выполнения: в одиночку проходит, в наборе после A_MutatesSharedSingleton падает, т.к. ClassicConfigService хранит общее статическое состояние. Снимите [Ignore], чтобы увидеть падение, затем верните обратно, чтобы CI был зелёным.")]
        [TestMethod]
        public void B_ExpectsCleanState_ButSingletonLeaks()
        {
            Assert.IsNull(
                ClassicConfigService.Instance().GetValue(Key),
                "Ожидалось чистое состояние, но предыдущий тест оставил значение в общем синглтоне");
        }
    }
}
