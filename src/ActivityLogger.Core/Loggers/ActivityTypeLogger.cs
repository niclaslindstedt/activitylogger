using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AL.Core.Constants;
using AL.Core.Models;

namespace AL.Core.Loggers
{
    public class ActivityTypeLogger : Logger<ActivityTypeReport>, IActivityTypeLogger
    {
        private readonly Settings _settings;
        private ProcessReport _processReport;

        private readonly IDictionary<string, IDictionary<string, DateTime>> _latestProcessActivity = new Dictionary<string, IDictionary<string, DateTime>>();
        private readonly IDictionary<string, IDictionary<string, string>> _processMatches = new Dictionary<string, IDictionary<string, string>>();

        private bool _userIsActive;
        private bool _userIsIdle;
        private DateTime _latestActivity = DateTime.Now;
        private string _activeSection = string.Empty;

        public ActivityTypeLogger(Settings settings)
        {
            _settings = settings;
        }

        public void DetermineActivityType(ProcessReport processReport, params IInputActivityReport[] inputActivityReports)
        {
            if (inputActivityReports != null && inputActivityReports.Any())
            {
                var reports = inputActivityReports.Where(x => x != null)
                    .OrderBy(x => x.LatestActivity).ToList();

                if (reports.Any())
                    _latestActivity = reports.Last().LatestActivity;
            }
            
            _processReport = processReport;
        }

        public override void Log()
        {
            var activityType = string.Empty;

            var sections = _settings.Sections;
            foreach (var section in sections)
            {
                var idleSeconds = _settings.GetActivitySettingAsInt(section, SettingStrings.SecondsBeforeConsideredIdle);

                if (!_processMatches.ContainsKey(section))
                    _processMatches[section] = new Dictionary<string, string>();

                if (!_latestProcessActivity.ContainsKey(section))
                    _latestProcessActivity[section] = new Dictionary<string, DateTime>();

                var process = GetActiveProcessFromList(section, _settings.SectionActivities[section], idleSeconds);
                
                if (process != null)
                {
                    _activeSection = section;
                    activityType = section;
                    _userIsActive = true;
                    _userIsIdle = (DateTime.Now - _latestActivity).Seconds >= idleSeconds;
                }
            }
            
            var activityTypeReport = new ActivityTypeReport
            {
                ActivityType = activityType,
                ActivityHourGoal = _settings.GetActivitySettingAsFloat(activityType, SettingStrings.DailyHourGoal),
                UserIsActive = !_userIsIdle && _userIsActive,
                UserIsIdle = _userIsIdle
            };

            Observer.OnNext(activityTypeReport);
        }

        private string GetActiveProcessFromList(string section, IEnumerable<string> matchStrings, int allowedIdleSeconds)
        {
            var matchString = matchStrings.FirstOrDefault(x => IsProcessActive(section, x, allowedIdleSeconds));
            if (matchString == null)
                return null;

            return _processMatches[section][matchString];
        }

        private bool IsProcessActive(string section, string matchString, int allowedIdleSeconds)
        {
            if (IsMatchToCurrentProcess(section, matchString))
            {
                _latestProcessActivity[section][matchString] = DateTime.Now;
                return true;
            }

            if (_activeSection == string.Empty)
                return false;

            if (!_latestProcessActivity[_activeSection].ContainsKey(matchString))
                return false;

            _userIsActive = (DateTime.Now - _latestProcessActivity[_activeSection][matchString]) < TimeSpan.FromSeconds(allowedIdleSeconds);

            return _userIsActive;
        }

        private bool IsMatchToCurrentProcess(string section, string matchString)
        {
            var match = false;
            var regex = new Regex(matchString);

            if (_processReport.Name == matchString)
                match = true;
            else if (!string.IsNullOrEmpty(_processReport.WindowTitle) && regex.IsMatch(_processReport.WindowTitle))
                match = true;
            else if (!string.IsNullOrEmpty(_processReport.Description) && regex.IsMatch(_processReport.Description))
                match = true;

            if (match)
            {
                if (!_processMatches[section].ContainsKey(matchString))
                    _processMatches[section].Add(matchString, _processReport.Description);
                return true;
            }

            return false;
        }
    }
}