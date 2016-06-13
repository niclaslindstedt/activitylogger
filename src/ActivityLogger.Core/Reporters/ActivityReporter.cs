using AL.Core.Interfaces;
using AL.Core.Models;

namespace AL.Core.Reporters
{
    public class ActivityReporter : Reporter<ActivityReport>
    {
        private readonly IActivityReceiver _activityReceiver;
        
        public ActivityReporter(IActivityReceiver activityReceiver)
        {
            _activityReceiver = activityReceiver;
        }

        protected override void Act(ActivityReport activityReport)
        {
            _activityReceiver.ReportActivity(activityReport);
        }
    }
}
