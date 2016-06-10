using System;

namespace WorkLogger.Main.Services
{
    public interface IActivityReporter
    {
        DateTime LastActivity { get; }
        bool UserIsActive { get; }
    }
}
