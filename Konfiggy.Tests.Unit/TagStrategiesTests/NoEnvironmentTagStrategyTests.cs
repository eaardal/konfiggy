using KonfiggyFramework.TagStrategies;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit.TagStrategiesTests
{
    [TestFixture]
    public class NoEnvironmentTagStrategyTests
    {
        [Test]
        public void ReturnsNull()
        {
            Assert.Null(new NoEnvironmentTagStrategy().GetEnvironmentTag());
        }
    }
}
