using AL.Core.Models;

namespace AL.Core.Interfaces
{
    public interface IActivityReceiver
    {
        void Register(IReportCentral reporter);
        void ReportActivity(IActivityReport activityReport);
    }
}
