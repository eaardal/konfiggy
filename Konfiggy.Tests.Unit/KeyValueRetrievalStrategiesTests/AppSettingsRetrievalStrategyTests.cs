using System.Collections.Specialized;
using Konfiggy.Core;
using Konfiggy.Core.KeyValueRetrievalStrategies;
using Moq;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit.KeyValueRetrievalStrategiesTests
{
    [TestFixture]
    public class AppSettingsRetrievalStrategyTests
    {
        [Test]
        public void GetKeyValueCollection_ConfigurationKeeperIsCalledWithAppSettingsArgument()
        {
            var configKeeper = new Mock<IConfigurationKeeper>();
            configKeeper.Setup(ctx => ctx.GetSection("appSettings")).Verifiable("Was not called with appSettings argument");
            configKeeper.Setup(ctx => ctx.GetSection("appSettings")).Returns(new NameValueCollection());
            
            var keyValStrat = new AppSettingsRetrievalStrategy();
            keyValStrat.GetKeyValueCollection(configKeeper.Object);

            configKeeper.Verify();
        }
    }
}
