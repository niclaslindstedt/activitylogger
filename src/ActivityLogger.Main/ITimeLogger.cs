using System.Collections.Generic;

namespace ActivityLogger.Main
{
    public interface ITimeLogger
    {
        Dictionary<string, int> ActiveSeconds { get; }
        Dictionary<string, int> ProcrastinationSeconds { get; }
        Dictionary<string, int> IdleSeconds { get; }

        void LogTime();
    }
}