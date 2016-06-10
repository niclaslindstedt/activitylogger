using NUnit.Framework;
using WorkLogger.Main;

namespace WorkLogger.Tests
{
    [TestFixture]
    public class ProcessFinderTest
    {
        private ProcessFinder _processFinder;

        [SetUp]
        public void SetUp()
        {
            _processFinder = new ProcessFinder();
        }

        [Test]
        public void IsProcessActiveTest()
        {
            Assert.IsTrue(_processFinder.IsProcessActive("devenv.exe", true));
        }
    }
}
