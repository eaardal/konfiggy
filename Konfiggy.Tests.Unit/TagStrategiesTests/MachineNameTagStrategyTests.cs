using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Konfiggy.Exceptions;
using Konfiggy.Helpers;
using Konfiggy.TagStrategies;
using Moq;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit.TagStrategiesTests
{
    [TestFixture]
    public class MachineNameTagStrategyTests
    {
        private MachineNameTagStrategy _tagStrategy;

        [SetUp]
        public void SetUp()
        {
            _tagStrategy = new MachineNameTagStrategy(GetFakeServerNamesMap());
        }

        [Test]
        public void GetEnvironmentTag_WhenServerNamesMapIsNullOrEmpty_ThrowsException()
        {
            _tagStrategy.MachineNamesMap = null;
            Assert.Throws<KonfiggyServerNamesMapNotSetException>(() => _tagStrategy.GetEnvironmentTag());

            _tagStrategy.MachineNamesMap = new Dictionary<string, string>();
            Assert.Throws<KonfiggyServerNamesMapNotSetException>(() => _tagStrategy.GetEnvironmentTag());
        }

        [Test]
        public void GetEnvironmentTag_WhenSystemEnvironmentIsNull_ThrowsException()
        {
            _tagStrategy.SystemEnvironment = null;
            Assert.Throws<KonfiggyEnvironmentNotSetException>(() => _tagStrategy.GetEnvironmentTag());
        }

        [Test]
        public void Constructor_WhenNoSystemEnvironmentGiven_UsesDefault()
        {
            Assert.IsNotNull(_tagStrategy.SystemEnvironment);
            Assert.IsInstanceOf<SystemEnvironment>(_tagStrategy.SystemEnvironment);
        }

        [Test]
        public void GetEnvironmentTag_GetsNameOfCurrentMachineFromSystemEnvironment_ReturnsEnvironmentTagFromDictionary()
        {
            var environment = new Mock<ISystemEnvironment>();
            environment.Setup(ctx => ctx.GetMachineName()).Returns("server1");
            
            _tagStrategy.SystemEnvironment = environment.Object;

            var tag = _tagStrategy.GetEnvironmentTag();
            Assert.AreEqual("local", tag);
        }

        [Test]
        public void GetEnvironmentTag_WhenMachineNameAndServerNamesMapDoesNotMatchCase_ReturnsCorrectEnvironmentTag()
        {
            var environment = new Mock<ISystemEnvironment>();
            environment.Setup(ctx => ctx.GetMachineName()).Returns("SERVER3");

            _tagStrategy.SystemEnvironment = environment.Object;

            var tag = _tagStrategy.GetEnvironmentTag();
            Assert.AreEqual("qa", tag);
        }

        private IDictionary<string, string> GetFakeServerNamesMap()
        {
            return new Dictionary<string, string>
            {
                {"server1", "local"},
                {"server2", "dev"},
                {"server3", "qa"},
                {"server4", "prod"},
            };
        } 
    }
}

