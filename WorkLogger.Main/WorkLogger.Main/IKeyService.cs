using System;

namespace WorkLogger.Main
{
    public interface IActivityService
    {
        DateTime LastActivity { get; }
        bool UserIsActive { get; }
    }
}
