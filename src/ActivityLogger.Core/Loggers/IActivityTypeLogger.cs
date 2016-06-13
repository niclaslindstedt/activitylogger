using AL.Core.Models;

namespace AL.Core.Loggers
{
    public interface IActivityTypeLogger
    {
        void DetermineActivityType(ProcessReport processReport, params IInputActivityReport[] inputActivityReports);
    }
}
