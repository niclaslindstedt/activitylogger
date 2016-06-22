using System.Threading;
using System.Threading.Tasks;
using AL.Core.Interfaces;
using AL.Core.Loggers;
using AL.Core.Models;
using AL.Core.Reporters;
using AL.Core.Utilities;

namespace AL.Core
{
    public class ReportCentral : IReportCentral
    {
        public IActivityReport ActivityReport { get; set; }

        private readonly IActivityReceiver _activityReceiver;
        private ActivityLogger _activityLogger;

        public ReportCentral(IActivityReceiver activityReceiver)
        {
            _activityReceiver = activityReceiver;
            _activityReceiver.Register(this);
        }

        public void Reset()
        {
            _activityLogger.ReplaceReport(ActivityReport, this);
        }

        public void StartReporterThread()
        {
            var settings = new Settings(new SettingsReader("ActivityLogger.ini"));

            ActivityReport = new ActivityReport();
            _activityLogger = ActivityLogger.Instance(ActivityReport, settings);

            var mouseClickLogger = MouseClickLogger.Instance();
            var mouseClickReporter = MouseClickReporter.Instance(_activityLogger);
            mouseClickReporter.Subscribe(mouseClickLogger);

            var keyLogger = KeyLogger.Instance();
            var keyReporter = KeyReporter.Instance(_activityLogger);
            keyReporter.Subscribe(keyLogger);

            Task.Factory.StartNew(() =>
            {

                var activityReporter = new ActivityReporter(_activityReceiver);
                activityReporter.Subscribe(_activityLogger);

                var mouseLogger = new MouseLogger();
                var mouseReporter = new MouseReporter(_activityLogger);
                mouseReporter.Subscribe(mouseLogger);

                var processLogger = new ProcessLogger();
                var processReporter = new ProcessReporter(_activityLogger);
                processReporter.Subscribe(processLogger);

                var timeLogger = new TimeLogger();
                var timeReporter = new TimeReporter(_activityLogger);
                timeReporter.Subscribe(timeLogger);

                var activityTypeLogger = new ActivityTypeLogger(settings);
                var activityTypeReporter = new ActivityTypeReporter(_activityLogger);
                activityTypeReporter.Subscribe(activityTypeLogger);

                while (true)
                {
                    Thread.Sleep(1000);

                    // KeyLogger & MouseClickLogger will log when keystrokes/clicks are
                    // recorded, so no need to tell it to log here.

                    mouseLogger.Log();
                    processLogger.Log();
                    timeLogger.Log();

                    activityTypeLogger.DetermineActivityType(
                        processReporter.ProcessReport, mouseReporter.MouseReport,
                        MouseClickReporter.Instance().MouseClickReport, KeyReporter.Instance().KeyReport);
                    activityTypeLogger.Log();

                    _activityLogger.Log();
                }
            });
        }
    }
}
