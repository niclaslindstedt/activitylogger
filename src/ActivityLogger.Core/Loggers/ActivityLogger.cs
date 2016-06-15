using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AL.Core.Interfaces;
using AL.Core.Models;
using Activity = AL.Core.Models.ActivityReport.Activity;

namespace AL.Core.Loggers
{
    public class ActivityLogger :
        Logger<ActivityReport>,
        IKeyReceiver,
        IMouseReceiver,
        IProcessReceiver,
        ITimeReceiver,
        IActivityTypeReceiver
    {
        private readonly ActivityReport _activityReport = new ActivityReport();

        private readonly ILogReceiver _logReceiver;

        private bool _processReported;
        private bool _timeReported;
        private bool _activityTypeReported;
        
        private string _currentActivity;

        public ActivityLogger(ILogReceiver logReceiver = null)
        {
            _logReceiver = logReceiver;
        }

        public override void Log()
        {
            if (_activityReport.UserIsActive)
                _activityReport.LastActive = DateTime.Now;

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
        }

        private void LogChangeInActivityType()
        {
            if (_logReceiver == null)
                return;

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
                    _logReceiver.Log(logMessage);
            }

            _currentActivity = _activityReport.ActivityType;
        }

        private void ReportProcessWorkTime()
        {
            var activityType = _activityReport.ActivityType;
            var activities = _activityReport.SectionActivities[activityType];

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
            _activityReport.KeyReport = keyReport;
            // No need to log reported = true here since reports come
            // in when keyboard strokes are recorded.
        }

        public void ReportMouse(MouseReport mouseReport)
        {
            _activityReport.MouseReport = mouseReport;
            // No need to log reported = true here since reports
            // are only made when mouse actually moves.
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

            if (!_activityReport.SectionActivities.ContainsKey(_activityReport.ActivityType))
                _activityReport.SectionActivities[_activityReport.ActivityType] = new List<Activity>();

            if (!_activityReport.SectionHourGoals.ContainsKey(_activityReport.ActivityType))
                _activityReport.SectionHourGoals[_activityReport.ActivityType] = activityTypeReport.ActivityHourGoal;

            _activityTypeReported = true;
        }
    }
}