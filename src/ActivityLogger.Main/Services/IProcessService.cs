using System.Collections.Generic;

namespace ActivityLogger.Core.Services
{
    public interface IProcessService
    {
        string CurrentProcessDescription { get; }
        string CurrentModuleName { get; }
        string CurrentWindowTitle { get; }
        string CurrentProcessName { get; }

        string GetActiveProcessFromList(IEnumerable<string> processNamesOrRegexes, int allowedIdleSeconds);
        bool IsProcessActive(string processNameOrRegex, int allowedIdleSeconds);
    }
}
