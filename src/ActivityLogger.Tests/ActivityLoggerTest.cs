using System;
using AL.Core.Interfaces;
using AL.Core.Loggers;
using AL.Core.Models;
using AL.Tests.Helpers;
using Moq;
using NUnit.Framework;

namespace AL.Tests
{
    [TestFixture]
    public class ActivityLoggerTest
    {
        private Mock<IActivityReport> _activityReportMock;
        private Mock<ILogReceiver> _logReceiverMock;
        private Mock<IObserver<ActivityReport>> _observerMock;

        private ActivityLogger _activityLogger;

        [SetUp]
        public void SetUp()
        {
            _activityReportMock = new Mock<IActivityReport>();
            _logReceiverMock = new Mock<ILogReceiver>();
            _observerMock = new Mock<IObserver<ActivityReport>>();

            _activityLogger = ActivityLogger.Instance(_activityReportMock.Object, _logReceiverMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            SingletonHelper.Dispose(_activityLogger);
        }

        [Test]
        public void Log_IfUserIsActive_SetLastActiveToNow()
        {
            var lastActive = DateTime.MinValue;
            _activityReportMock.Setup(x => x.UserIsActive)
                .Returns(true);
            _activityReportMock.SetupSet(r => r.LastActive = It.IsAny<DateTime>())
                .Callback<DateTime>(r => lastActive = r);
            
            _activityLogger.Log();
            
            Assert.IsTrue(lastActive >= DateTime.Now.AddSeconds(-10), "LastActive was not set during Log");
        }
    }
}
