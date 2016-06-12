using System;

namespace ActivityLogger.Core.Services
{
    public interface IActivityReporter
    {
        DateTime LastActivity { get; }
        bool UserIsActive { get; }
    }
}
