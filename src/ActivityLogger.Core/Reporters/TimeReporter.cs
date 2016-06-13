using AL.Core.Interfaces;
using AL.Core.Models;

namespace AL.Core.Reporters
{
    public class TimeReporter : Reporter<TimeReport>
    {
        private readonly ITimeReceiver _timeReceiver;

        public TimeReporter(ITimeReceiver timeReceiver)
        {
            _timeReceiver = timeReceiver;
        }

        protected override void Act(TimeReport timeReport)
        {
            _timeReceiver.ReportTime(timeReport);
        }
    }
}
