using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonfiggyFramework.Exceptions;
using KonfiggyFramework.TagStrategies;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit.TagStrategiesTests
{
    [TestFixture]
    public class CodeTagStrategyTests
    {
        private CodeTagStrategy _tagStrategy;

        [SetUp]
        public void SetUp()
        {
            _tagStrategy = new CodeTagStrategy();
        }

        [Test]
        public void GetEnvironmentTag_WhenEnvironmentTagIsNullOrEmpty_ThrowsException()
        {
            _tagStrategy.EnvironmentTag = null;
            Assert.Throws<KonfiggyEnvironmentTagNotFoundException>(() => _tagStrategy.GetEnvironmentTag());

            _tagStrategy.EnvironmentTag = String.Empty;
            Assert.Throws<KonfiggyEnvironmentTagNotFoundException>(() => _tagStrategy.GetEnvironmentTag());
        }

        [Test]
        public void GetEnvironmentTag_WhenEnvironmentTagHasValue_ReturnsEnvironmentTag()
        {
            _tagStrategy.EnvironmentTag = "Local";
            string environmentTag = _tagStrategy.GetEnvironmentTag();
            Assert.AreEqual("Local", environmentTag);
        }

        [Test]
        public void Constructor_WhenSuppliedWithEnvironmentTag_EnvironmentTagPropertyIsSetWithContructorValue()
        {
            var tagStrat = new CodeTagStrategy("QA");
            Assert.AreEqual("QA", tagStrat.EnvironmentTag);
        }

        [Test]
        public void Constructor_WhenEnvironmentTagArgumentIsNullOrEmpty_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new CodeTagStrategy(null));
            Assert.Throws<ArgumentNullException>(() => new CodeTagStrategy(String.Empty));
        }
    }
}
