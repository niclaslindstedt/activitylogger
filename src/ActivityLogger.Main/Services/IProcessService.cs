﻿using System.Collections.Generic;

namespace ActivityLogger.Main.Services
{
    public interface IProcessService
    {
        string GetActiveProcessFromList(IEnumerable<string> processNamesOrRegexes, int allowedIdleSeconds);
        bool IsProcessActive(string processNameOrRegex, int allowedIdleSeconds);
    }
}