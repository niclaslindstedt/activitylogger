using NUnit.Framework;
using ActivityLogger.Main.Services;

namespace ActivityLogger.Tests
{
    [TestFixture]
    public class ProcessFinderTest
    {
        private ProcessService _processService;

        [SetUp]
        public void SetUp()
        {
            _processService = new ProcessService();
        }

        [Test]
        public void IsProcessActiveTest()
        {
            Assert.IsTrue(_processService.IsProcessActive("devenv.exe", 10));
        }
    }
}
