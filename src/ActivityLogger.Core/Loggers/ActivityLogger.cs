using System;
using System.Diagnostics;
using System.Linq;
using AL.Core.Interfaces;
using AL.Core.Models;

namespace AL.Core.Loggers
{
    public class ActivityLogger :
        Logger<ActivityReport>,
        IKeyReceiver,
        IMouseReceiver,
        IMouseClickReceiver,
        IProcessReceiver,
        ITimeReceiver,
        IActivityTypeReceiver
    {
        private static ActivityLogger _instance;
        public static ActivityLogger Instance(ILogReceiver logReceiver = null)
        {
            return _instance ?? (_instance = new ActivityLogger(logReceiver));
        }

        private readonly ActivityReport _activityReport = new ActivityReport();

        private readonly ILogReceiver _logReceiver;

        private bool _processReported;
        private bool _timeReported;
        private bool _activityTypeReported;
        
        private string _currentActivity;

        private ActivityLogger(ILogReceiver logReceiver = null)
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

            _activityReport.KeyReport = keyReport;
            // No need to log reported = true here since reports come
            // in when keyboard strokes are recorded.
        }

        public void ReportMouse(MouseReport mouseReport)
        {
            if (_activityReport.CurrentActivity != null)
                _activityReport.CurrentActivity.Distance += mouseReport.Distance;

            _activityReport.MouseReport = mouseReport;
            // No need to log reported = true here since reports
            // are only made when mouse actually moves.
        }

        public void ReportMouseClick(MouseClickReport mouseClickReport)
        {
            if (_activityReport.CurrentActivity != null)
                _activityReport.CurrentActivity.Clicks += mouseClickReport.Clicks;

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
    }
}