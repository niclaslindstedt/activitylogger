using AL.Core.Interfaces;
using AL.Core.Models;

namespace AL.Core.Reporters
{
    public class ActivityReporter : Reporter<IActivityReport>
    {
        private readonly IActivityReceiver _activityReceiver;
        
        public ActivityReporter(IActivityReceiver activityReceiver)
        {
            _activityReceiver = activityReceiver;
        }

        protected override void Act(IActivityReport activityReport)
        {
            _activityReceiver.ReportActivity(activityReport);
        }
    }
}
