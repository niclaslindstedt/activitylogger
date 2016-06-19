using AL.Core.Models;

namespace AL.Core
{
    public interface IReportCentral
    {
        IActivityReport ActivityReport { get; set; }

        void Reset();
        void StartReporterThread();
    }
}