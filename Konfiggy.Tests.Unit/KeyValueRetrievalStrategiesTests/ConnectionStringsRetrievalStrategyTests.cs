using System.Configuration;
using Konfiggy.Core;
using Konfiggy.Core.KeyValueRetrievalStrategies;
using Moq;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit.KeyValueRetrievalStrategiesTests
{
    [TestFixture]
    public class ConnectionStringsRetrievalStrategyTests
    {
        [Test]
        public void GetKeyValueCollection_ConfigurationKeeperIsCalledWithConnectionStringsArgument()
        {
            var configKeeper = new Mock<IConfigurationKeeper>();
            configKeeper.Setup(ctx => ctx.GetSection("connectionStrings")).Verifiable("Was not called with connectionStrings argument");
            configKeeper.Setup(ctx => ctx.GetSection("connectionStrings")).Returns(new ConnectionStringsSection());

            var keyValStrat = new ConnectionStringsRetrievalStrategy();
            keyValStrat.GetKeyValueCollection(configKeeper.Object);

            configKeeper.Verify();
        }
    }
}
