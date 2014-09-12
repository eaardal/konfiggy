using System;
using Konfiggy.Core;
using Konfiggy.Core.Exceptions;
using Konfiggy.Core.TagStrategies;
using Moq;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit.TagStrategiesTests
{
    [TestFixture]
    public class EnvironmentVariableTagStrategyTests
    {
        [Test]
        public void GetEnvironmentTag_WhenValueExistsInUserVariables_ReturnsValue()
        {
            var env = new Mock<ISystemEnvironment>();
            env.Setup(ctx => ctx.GetEnvironmentVariable("Konfiggy", EnvironmentVariableTarget.User)).Returns("Local");

            var tagStrat = new EnvironmentVariableTagStrategy { SystemEnvironment = env.Object };
            var tag = tagStrat.GetEnvironmentTag();

            Assert.AreEqual("Local", tag);
        }

        [Test]
        public void GetEnvironmentTag_WhenValueExistsInSystemVariables_ReturnsValue()
        {
            var env = new Mock<ISystemEnvironment>();
            env.Setup(ctx => ctx.GetEnvironmentVariable("Konfiggy", EnvironmentVariableTarget.User)).Returns(() => null);
            env.Setup(ctx => ctx.GetEnvironmentVariable("Konfiggy", EnvironmentVariableTarget.Machine)).Returns("QA");

            var tagStrat = new EnvironmentVariableTagStrategy { SystemEnvironment = env.Object };
            var tag = tagStrat.GetEnvironmentTag();
            
            Assert.AreEqual("QA", tag);
            env.Verify(ctx => ctx.GetEnvironmentVariable("Konfiggy", EnvironmentVariableTarget.User));
        }

        [Test]
        public void GetEnvironmentTag_WhenValueDoesNotExistInSystemEnvironmentVariables_ReturnsNull()
        {
            var env = new Mock<ISystemEnvironment>();
            env.Setup(ctx => ctx.GetEnvironmentVariable("Konfiggy", EnvironmentVariableTarget.User)).Returns(() => null);
            env.Setup(ctx => ctx.GetEnvironmentVariable("Konfiggy", EnvironmentVariableTarget.Machine)).Returns(() => null);

            var tagStrat = new EnvironmentVariableTagStrategy { SystemEnvironment = env.Object };
            var tag = tagStrat.GetEnvironmentTag();

            Assert.IsNull(tag);
            env.Verify(ctx => ctx.GetEnvironmentVariable("Konfiggy", EnvironmentVariableTarget.User));
            env.Verify(ctx => ctx.GetEnvironmentVariable("Konfiggy", EnvironmentVariableTarget.Machine));
        }

        [Test]
        public void GetEnvironmentTag_WhenEnvironmentIsNull_ThrowsException()
        {
            var tagStrat = new EnvironmentVariableTagStrategy {SystemEnvironment = null};

            Assert.Throws<KonfiggyEnvironmentNotSetException>(() => tagStrat.GetEnvironmentTag());
        }

        [Test]
        public void GetEnvironmentTag_WhenKonfiggyIdentifierIsNull_ThrowsException()
        {
            var tagStrat = new EnvironmentVariableTagStrategy{KonfiggyIdentifier = null};

            Assert.Throws<KonfiggyIdentifierNotSetException>(() => tagStrat.GetEnvironmentTag());
        }

        [Test]
        public void GeneralBehavior_WhenNoSystemEnvironmentIsSet_UsesDefault()
        {
            var tagStrat = new EnvironmentVariableTagStrategy();
            
            Assert.IsNotNull(tagStrat.SystemEnvironment);
            Assert.IsInstanceOf<SystemEnvironment>(tagStrat.SystemEnvironment);
        }

        [Test]
        public void GeneralBehavior_WhenNoKonfiggyIdentifierIsSet_UsesDefault()
        {
            var tagStrat = new EnvironmentVariableTagStrategy();

            Assert.IsNotNull(tagStrat.KonfiggyIdentifier);
            Assert.AreEqual("Konfiggy", tagStrat.KonfiggyIdentifier);
        }
    }
}
