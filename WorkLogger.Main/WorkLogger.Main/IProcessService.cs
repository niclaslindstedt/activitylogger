using System.Collections.Generic;

namespace WorkLogger.Main
{
    public interface IProcessService
    {
        string GetActiveProcessFromList(IEnumerable<string> processNamesOrRegexes, int allowedIdleSeconds);
        bool IsProcessActive(string processNameOrRegex, int allowedIdleSeconds);
    }
}
