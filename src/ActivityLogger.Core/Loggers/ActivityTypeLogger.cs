using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AL.Core.Constants;
using AL.Core.Models;

namespace AL.Core.Loggers
{
    public class ActivityTypeLogger : Logger<ActivityType>, IActivityTypeLogger
    {
        private readonly Settings _settings;
        private ProcessReport _processReport;

        private readonly Dictionary<string, DateTime> _lastWorkActivity = new Dictionary<string, DateTime>();
        private readonly Dictionary<string, string> _processMatches = new Dictionary<string, string>();

        private bool _userIsActive;
        private DateTime _lastActivity = DateTime.Now;

        public ActivityTypeLogger(Settings settings)
        {
            _settings = settings;
        }

        public void DetermineActivityType(ProcessReport processReport, params IInputActivityReport[] inputActivityReports)
        {
            if (inputActivityReports != null && inputActivityReports.Any())
            {
                var reports = inputActivityReports.Where(x => x != null)
                    .OrderByDescending(x => x.LastActivity).ToList();

                if (reports.Any())
                    _lastActivity = reports.First().LastActivity;
            }

            _userIsActive = (DateTime.Now - _lastActivity).Seconds <= _settings.SecondsBeforeConsideredIdle;

            _processReport = processReport;
        }

        public override void Log()
        {
            ActivityType activityType;

            // Try getting active processes with the allowed idle seconds for
            // work processes (grace time before considered idle).
            var workProcess = GetActiveProcessFromList(
                _settings.WorkProcesses, _settings.AllowedIdleSecondsForWork);
            if (workProcess != null)
            {
                activityType = _userIsActive ? ActivityType.Work : ActivityType.None;
            }
            else
            {
                var workRelatedProcess = GetActiveProcessFromList(
                    _settings.WorkRelatedProcesses, _settings.AllowedIdleSecondsForWorkRelated);
                if (workRelatedProcess != null)
                    activityType = _userIsActive ? ActivityType.WorkRelated : ActivityType.None;
                else
                    activityType = _userIsActive ? ActivityType.NonWorkRelated : ActivityType.None;
            }

            Observer.OnNext(activityType);
        }

        private string GetActiveProcessFromList(IEnumerable<string> matchStrings, int allowedIdleSeconds)
        {
            var matchString = matchStrings.FirstOrDefault(x => IsProcessActive(x, allowedIdleSeconds));
            if (matchString == null)
                return null;

            return _processMatches[matchString];
        }

        private bool IsProcessActive(string matchString, int allowedIdleSeconds)
        {
            if (IsMatchToCurrentProcess(matchString))
            {
                _lastWorkActivity[matchString] = DateTime.Now;
                return true;
            }

            if (!_lastWorkActivity.ContainsKey(matchString))
                return false;

            return (DateTime.Now - _lastWorkActivity[matchString]) < TimeSpan.FromSeconds(allowedIdleSeconds);
        }

        private bool IsMatchToCurrentProcess(string matchString)
        {
            var regex = new Regex(matchString);
            if (_processReport.Name == matchString || regex.IsMatch(_processReport.WindowTitle) || regex.IsMatch(_processReport.Description))
            {
                if (!_processMatches.ContainsKey(matchString))
                    _processMatches.Add(matchString, _processReport.Description);
                return true;
            }

            return false;
        }
    }
}