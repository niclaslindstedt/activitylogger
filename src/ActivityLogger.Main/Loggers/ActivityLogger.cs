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
        
        private bool _processReported;
        private bool _timeReported;
        private bool _activityTypeReported;

        public override void Log()
        {
            if (_activityReport.UserIsWorking)
                _activityReport.LastActive = DateTime.Now;

            if (_processReported && _timeReported & _activityTypeReported)
            {
                ReportProcessWorkTime();
                Observer.OnNext(_activityReport);
            }
            else
            {
                Debug.Print("Reports are missing");
            }
        }

        private void ReportProcessWorkTime()
        {
            var processName = _activityReport.ProcessName;
            var activityType = _activityReport.ActivityType;
            var seconds = _activityReport.TimeSinceLastReport;

            switch (activityType)
            {
                case ActivityType.Work:
                case ActivityType.WorkRelated:
                    AddProcessTimeToList(_activityReport.WorkActivities, processName, seconds);
                    break;

                case ActivityType.NonWorkRelated:
                    AddProcessTimeToList(_activityReport.NonWorkActivities, processName, seconds);
                    break;

                case ActivityType.None:
                    AddProcessTimeToList(_activityReport.IdlingActivities, processName, seconds);
                    break;

                default:
                    throw new IndexOutOfRangeException();
            }
        }

        private void AddProcessTimeToList(ICollection<Activity> activities, string processName, int seconds)
        {
            var activity = activities.FirstOrDefault(x => x.ProcessName == processName);
            if (activity != null)
                activity.Seconds += seconds;
            else
                activities.Add(new Activity
                {
                    ProcessName = processName,
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