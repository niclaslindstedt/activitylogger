using System;
using System.Collections.Generic;
using System.Linq;
using ActivityLogger.Main.Services;

namespace ActivityLogger.Main
{
    public class TimeLogger : ITimeLogger
    {
        public Dictionary<string, int> ActiveSeconds { get; } = new Dictionary<string, int>();
        public Dictionary<string, int> ProcrastinationSeconds { get; } = new Dictionary<string, int>();
        public Dictionary<string, int> IdleSeconds { get; } = new Dictionary<string, int>();
        
        private readonly IProcessService _processService;
        private readonly List<IActivityReporter> _activityReporters;
        private DateTime _lastReportTime;

        public TimeLogger(IProcessService processService, params IActivityReporter[] activityReporters)
        {
            _processService = processService;
            _activityReporters = activityReporters.ToList();
            _lastReportTime = DateTime.Now;
        }

        public void LogTime()
        {
            var activeProcess = _processService.GetActiveProcessFromList(Program.Settings.WorkProcesses,
                Program.Settings.AllowedIdleSecondsForWork)
                ?? _processService.GetActiveProcessFromList(Program.Settings.WorkRelatedProcesses,
                    Program.Settings.AllowedIdleSecondsForWorkRelated);
            if (activeProcess != null)
            {
                if (ActiveSeconds.ContainsKey(activeProcess))
                    ActiveSeconds[activeProcess] += GetTimeDifference();
                else
                    ActiveSeconds[activeProcess] = GetTimeDifference();
            }
            else
            {
                activeProcess = _processService.CurrentProcessDescription;
                if (activeProcess != null)
                {
                    if (_activityReporters.Any(x => x.UserIsActive))
                    {
                        if (ProcrastinationSeconds.ContainsKey(activeProcess))
                            ProcrastinationSeconds[activeProcess] += GetTimeDifference();
                        else
                            ProcrastinationSeconds[activeProcess] = GetTimeDifference();
                    }
                    else
                    {
                        if (IdleSeconds.ContainsKey(activeProcess))
                            IdleSeconds[activeProcess] += GetTimeDifference();
                        else
                            IdleSeconds[activeProcess] = GetTimeDifference();
                    }
                }
            }

            _lastReportTime = DateTime.Now;
        }

        private int GetTimeDifference()
        {
            return (DateTime.Now - _lastReportTime).Seconds;
        }
    }
}
