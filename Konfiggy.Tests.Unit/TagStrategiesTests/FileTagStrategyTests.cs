using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Konfiggy.Exceptions;
using Konfiggy.TagStrategies;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit.TagStrategiesTests
{
    [TestFixture]
    public class FileTagStrategyTests
    {
        [Test]
        public void GetEnvironmentTag_WhenFilePathIsNullOrEmpty_ThrowsException()
        {
            var tagStrat = new KonfiggyXmlFileTagStrategy();
            Assert.Throws<KonfiggyFilePathNotSetException>(() => tagStrat.GetEnvironmentTag());

            tagStrat.FilePath = String.Empty;
            Assert.Throws<KonfiggyFilePathNotSetException>(() => tagStrat.GetEnvironmentTag());
        }
    }
}
