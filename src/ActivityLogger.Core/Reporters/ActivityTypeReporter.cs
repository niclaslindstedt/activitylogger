using AL.Core.Constants;
using AL.Core.Interfaces;

namespace AL.Core.Reporters
{
    public class ActivityTypeReporter : Reporter<ActivityType>
    {
        private readonly IActivityTypeReceiver _activityTypeReceiver;

        public ActivityTypeReporter(IActivityTypeReceiver activityTypeReceiver)
        {
            _activityTypeReceiver = activityTypeReceiver;
        }

        protected override void Act(ActivityType activityType)
        {
            _activityTypeReceiver.ReportActivityType(activityType);
        }
    }
}
