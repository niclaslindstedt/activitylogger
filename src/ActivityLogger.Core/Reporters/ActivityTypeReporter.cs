using AL.Core.Interfaces;
using AL.Core.Models;

namespace AL.Core.Reporters
{
    public class ActivityTypeReporter : Reporter<ActivityTypeReport>
    {
        private readonly IActivityTypeReceiver _activityTypeReceiver;

        public ActivityTypeReporter(IActivityTypeReceiver activityTypeReceiver)
        {
            _activityTypeReceiver = activityTypeReceiver;
        }

        protected override void Act(ActivityTypeReport activityTypeReport)
        {
            _activityTypeReceiver.ReportActivityType(activityTypeReport);
        }
    }
}
