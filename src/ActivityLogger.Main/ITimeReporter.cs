using System.Collections.Generic;

namespace ActivityLogger.Core
{
    public interface ITimeReporter
    {
        int GetWorkActivity(string processName);
        int GetNonWorkActivity(string processName);
        int GetIdleTime(string processName);
    }
}