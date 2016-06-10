using System;

namespace ActivityLogger.Main.Services
{
    public interface IActivityReporter
    {
        DateTime LastActivity { get; }
        bool UserIsActive { get; }
    }
}
