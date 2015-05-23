using KonfiggyFramework;
using KonfiggyFramework.Helpers;

namespace Konfiggy.Tests.Unit.Fixtures
{
    internal class ConfigurationBuilderFixture
    {
        public ConfigurationBuilder<T> CreateSut<T>()
        {
            return new ConfigurationBuilder<T>();  
        }

        public class TestConfig1
        {
            public string Foo { get; set; }

            public object A()
            {
                return new object();
            }
        }
    }
}