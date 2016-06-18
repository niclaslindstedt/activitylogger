using System;
using System.Collections.Generic;
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
        private Mock<IObserver<IActivityReport>> _observerMock;

        private ActivityLogger _activityLogger;

        [SetUp]
        public void SetUp()
        {
            _activityReportMock = new Mock<IActivityReport>();
            _observerMock = new Mock<IObserver<IActivityReport>>();

            _activityLogger = ActivityLogger.Instance(_activityReportMock.Object);
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

        [Test]
        public void Log_IfReportsHaveComeIn_ReportToObserver()
        {
            var sectionsMock = new Mock<IDictionary<string, Section>>();
            _activityReportMock.Setup(x => x.Sections)
                .Returns(sectionsMock.Object);

            _activityLogger.ReportProcess(new ProcessReport());
            _activityLogger.ReportTime(new TimeReport());
            _activityLogger.ReportActivityType(new ActivityTypeReport());
            _activityLogger.Log();

            _observerMock.Verify(x => x.OnNext(_activityReportMock.Object));
        }
    }
}
