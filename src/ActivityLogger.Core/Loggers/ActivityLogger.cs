using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AL.Core.Constants;
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
        private ActivityType _currentActivity;

        public ActivityLogger(ILogReceiver logReceiver = null)
        {
            _logReceiver = logReceiver;
        }

        public override void Log()
        {
            if (_activityReport.UserIsWorking)
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
            if (newActivity != _currentActivity)
            {
                var logMessage = string.Empty;
                if (_currentActivity == ActivityType.None)
                {
                    logMessage += "User became active ";
                    if (newActivity == ActivityType.Work || newActivity == ActivityType.WorkRelated)
                        logMessage += "and started working.";
                    else
                        logMessage += "and started chilling.";
                }
                else if (_currentActivity == ActivityType.NonWorkRelated)
                {
                    logMessage += "User stopped chilling ";
                    if (newActivity == ActivityType.Work || newActivity == ActivityType.WorkRelated)
                        logMessage += "and started working.";
                    else
                        logMessage += "and became idle.";
                }
                else if (_currentActivity == ActivityType.Work || _currentActivity == ActivityType.WorkRelated)
                {
                    if (newActivity != ActivityType.Work && newActivity != ActivityType.WorkRelated)
                    {
                        logMessage += "User stopped working ";
                        if (newActivity == ActivityType.None)
                            logMessage += "and became idle.";
                        else
                            logMessage += "and started chilling.";
                    }
                }
                if (logMessage != string.Empty)
                    _logReceiver.Log(logMessage);
            }

            _currentActivity = _activityReport.ActivityType;
        }

        private void ReportProcessWorkTime()
        {
            var activityType = _activityReport.ActivityType;

            switch (activityType)
            {
                case ActivityType.Work:
                case ActivityType.WorkRelated:
                    AddProcessTimeToList(_activityReport.WorkActivities, _activityReport);
                    break;

                case ActivityType.NonWorkRelated:
                    AddProcessTimeToList(_activityReport.NonWorkActivities, _activityReport);
                    break;

                case ActivityType.None:
                    AddProcessTimeToList(_activityReport.IdlingActivities, _activityReport);
                    break;

                default:
                    throw new IndexOutOfRangeException();
            }
        }

        private void AddProcessTimeToList(ICollection<Activity> activities, ActivityReport activityReport)
        {
            var processName = _activityReport.ProcessName;
            var processDescription = _activityReport.ProcessDescription;
            var seconds = _activityReport.TimeSinceLastReport;

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

        public void ReportActivityType(ActivityType activityType)
        {
            _activityReport.ActivityType = activityType;
            _activityTypeReported = true;
        }
    }
}