﻿using System;
using System.Diagnostics;
using System.Linq;
using AL.Core.Constants;
using AL.Core.Interfaces;
using AL.Core.Models;

namespace AL.Core.Loggers
{
    public class ActivityLogger :
        Logger<IActivityReport>,
        IKeyReceiver,
        IMouseReceiver,
        IMouseClickReceiver,
        IProcessReceiver,
        ITimeReceiver,
        IActivityTypeReceiver
    {
        private static ActivityLogger _instance;
        public static ActivityLogger Instance(IActivityReport activityReport, Settings settings)
        {
            return _instance ?? (_instance = new ActivityLogger(activityReport, settings));
        }

        private IActivityReport _activityReport;
        private readonly Settings _settings;

        private bool _replaceReport;
        private IReportCentral _reportReceiver;

        private bool _processReported;
        private bool _timeReported;
        private bool _activityTypeReported;
        
        private string _currentActivity;

        private ActivityLogger(IActivityReport activityReport, Settings settings)
        {
            _activityReport = activityReport;
            _settings = settings;
        }

        public override void Log()
        {
            if (_activityReport.UserIsActive)
                _activityReport.LastActive = DateTime.Now;

            SetTimeUntilIdle();

            if (_processReported && _timeReported & _activityTypeReported)
            {
                LogChangeInActivityType();
                ReportProcessWorkTime();
                Observer.OnNext(_activityReport);
            }
            else
            {
                Debug.Print("Reports are missing");
            }

            if (_replaceReport)
            {
                _activityReport = new ActivityReport();
                _reportReceiver.ActivityReport = _activityReport;
                _replaceReport = false;
            }
        }

        private void SetTimeUntilIdle()
        {
            var secondsBeforeConsideredIdle = _settings.GetActivitySettingAsInt(_activityReport.ActivityType,
                SettingStrings.SecondsBeforeConsideredIdle);
            var secondsSinceLastActivity = (DateTime.Now - _activityReport.LastInputActivity).TotalSeconds;
            var timeWhenConsideredIdle = DateTime.Now.AddSeconds(secondsBeforeConsideredIdle - secondsSinceLastActivity);
            var now = DateTime.Now;
            var secondsUntilIdle = (timeWhenConsideredIdle - DateTime.Now).TotalSeconds;
            var percentage = (int)(secondsUntilIdle * 100 / secondsBeforeConsideredIdle);

            if (percentage > 0 && percentage < 100)
            {
                
            }

            _activityReport.TimeUntilIdle = timeWhenConsideredIdle - DateTime.Now;
            _activityReport.TimeUntilIdlePercentage = Math.Min(100, Math.Max(0, 100-percentage));
        }

        private void LogChangeInActivityType()
        {
            var newActivity = _activityReport.ActivityType;
            if ((_currentActivity == string.Empty) || (newActivity == string.Empty))
            {
                var logMessage = string.Empty;
                if (_currentActivity == string.Empty && newActivity != string.Empty)
                {
                    if (!_activityReport.UserIsIdle)
                        logMessage = $"Idle => {newActivity}";
                }
                else if (_currentActivity != string.Empty)
                {
                    if (_activityReport.UserIsIdle)
                        logMessage = $"{_currentActivity} => Idle";
                }

                if (logMessage != string.Empty)
                    _activityReport.LogMessages.Add(logMessage);
            }

            _currentActivity = _activityReport.ActivityType;
        }

        private void ReportProcessWorkTime()
        {
            var activityType = _activityReport.ActivityType;
            var activities = _activityReport.Sections[activityType].Activities;

            var processName = _activityReport.ProcessName;
            var processDescription = _activityReport.ProcessDescription;
            var seconds = !_activityReport.UserIsIdle ? _activityReport.TimeSinceLastReport : 0;

            var activity = activities.FirstOrDefault(x => x.ProcessName == processName);
            if (activity != null)
                activity.Seconds += seconds;
            else
                activities.Add(new Activity
                {
                    ProcessName = processName,
                    ProcessDescription = processDescription,
                    Seconds = seconds
                });
        }
        
        public void ReportKey(KeyReport keyReport)
        {
            if (_activityReport.CurrentActivity != null)
                _activityReport.CurrentActivity.KeyStrokes += keyReport.KeyStrokes;

            var thisMinute = DateTime.Now.ToString("hh:mm");
            if (!_activityReport.KeyStrokesPerMinute.ContainsKey(thisMinute))
                _activityReport.KeyStrokesPerMinute.Add(thisMinute, keyReport.KeyStrokes);
            else
                _activityReport.KeyStrokesPerMinute[thisMinute] += keyReport.KeyStrokes;

            _activityReport.LastInputActivity = DateTime.Now;
            _activityReport.KeyReport = keyReport;
            // No need to log reported = true here since reports come
            // in when keyboard strokes are recorded.
        }

        public void ReportMouse(MouseReport mouseReport)
        {
            if (_activityReport.CurrentActivity != null)
                _activityReport.CurrentActivity.Distance += mouseReport.Distance;

            var thisMinute = DateTime.Now.ToString("hh:mm");
            if (!_activityReport.DistancePerMinute.ContainsKey(thisMinute))
                _activityReport.DistancePerMinute.Add(thisMinute, mouseReport.Distance);
            else
                _activityReport.DistancePerMinute[thisMinute] += mouseReport.Distance;

            if (mouseReport.Distance > 0)
                _activityReport.LastInputActivity = DateTime.Now;

            _activityReport.MouseReport = mouseReport;
            // No need to log reported = true here since reports
            // are only made when mouse actually moves.
        }

        public void ReportMouseClick(MouseClickReport mouseClickReport)
        {
            if (_activityReport.CurrentActivity != null)
                _activityReport.CurrentActivity.Clicks += mouseClickReport.Clicks;

            var thisMinute = DateTime.Now.ToString("hh:mm");
            if (!_activityReport.ClicksPerMinute.ContainsKey(thisMinute))
                _activityReport.ClicksPerMinute.Add(thisMinute, mouseClickReport.Clicks);
            else
                _activityReport.ClicksPerMinute[thisMinute] += mouseClickReport.Clicks;

            _activityReport.LastInputActivity = DateTime.Now;
            _activityReport.MouseClickReport = mouseClickReport;
            // No need to log reported = true here since reports come
            // in when mouse clicks are recorded.
        }

        public void ReportProcess(ProcessReport processReport)
        {
            _activityReport.ProcessReport = processReport;
            _processReported = true;
        }

        public void ReportTime(TimeReport timeReport)
        {
            _activityReport.TimeReport = timeReport;
            _timeReported = true;
        }

        public void ReportActivityType(ActivityTypeReport activityTypeReport)
        {
            _activityReport.ActivityType = activityTypeReport.ActivityType;
            _activityReport.UserIsActive = activityTypeReport.UserIsActive;
            _activityReport.UserIsIdle = activityTypeReport.UserIsIdle;

            if (_activityReport.CurrentSection == null)
            {
                _activityReport.CurrentSection = new Section
                {
                    SectionName = activityTypeReport.ActivityType,
                    HourGoal = activityTypeReport.ActivityHourGoal
                };
            }

            _activityTypeReported = true;
        }

        public void ReplaceReport(IActivityReport activityReport, IReportCentral returnTo)
        {
            _reportReceiver = returnTo;
            _replaceReport = true;
        }
    }
}