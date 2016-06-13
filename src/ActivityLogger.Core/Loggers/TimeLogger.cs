using System;
using AL.Core.Models;

namespace AL.Core.Loggers
{
    public class TimeLogger : Logger<TimeReport>
    {
        private DateTime _lastReportTime;

        public TimeLogger()
        {
            _lastReportTime = DateTime.Now;
        }

        public override void Log()
        {
            var timeDifference = (DateTime.Now - _lastReportTime).Seconds;
            _lastReportTime = DateTime.Now;
            
            var timeReport = new TimeReport
            {
                Seconds = timeDifference
            };

            Observer.OnNext(timeReport);
        }
    }
}
