using AL.Core.Models;

namespace AL.Core.Interfaces
{
    public interface IActivityReceiver
    {
        void ReportActivity(IActivityReport activityReport);
    }
}
