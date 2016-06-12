using System;
using ActivityLogger.Core.Constants;

namespace ActivityLogger.Core
{
    public interface ITimeReceiver
    {
        void ReportTime(Tuple<string, ActivityType, int> timeReport);
    }
}
