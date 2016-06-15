using AL.Core.Models;

namespace AL.Core.Interfaces
{
    public interface IActivityTypeReceiver
    {
        void ReportActivityType(ActivityTypeReport activityTypeReport);
    }
}
