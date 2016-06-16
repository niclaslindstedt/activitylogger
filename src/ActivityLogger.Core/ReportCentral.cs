using System.Threading;
using System.Threading.Tasks;
using AL.Core.Interfaces;
using AL.Core.Loggers;
using AL.Core.Models;
using AL.Core.Reporters;
using AL.Core.Utilities;

namespace AL.Core
{
    public class ReportCentral
    {
        private readonly IActivityReceiver _activityReceiver;
        private readonly ILogReceiver _logReceiver;

        public ReportCentral(IActivityReceiver activityReceiver, ILogReceiver logReceiver)
        {
            _activityReceiver = activityReceiver;
            _logReceiver = logReceiver;
        }

        public void StartReporterThread()
        {
            var activityReport = new ActivityReport();
            var activityLogger = ActivityLogger.Instance(activityReport, _logReceiver);

            var mouseClickLogger = MouseClickLogger.Instance();
            var mouseClickReporter = MouseClickReporter.Instance(activityLogger);
            mouseClickReporter.Subscribe(mouseClickLogger);

            var keyLogger = KeyLogger.Instance();
            var keyReporter = KeyReporter.Instance(activityLogger);
            keyReporter.Subscribe(keyLogger);

            Task.Factory.StartNew(() =>
            {
                var settings = new Settings(new SettingsReader("ActivityLogger.ini"));

                var activityReporter = new ActivityReporter(_activityReceiver);
                activityReporter.Subscribe(activityLogger);

                var mouseLogger = new MouseLogger();
                var mouseReporter = new MouseReporter(activityLogger);
                mouseReporter.Subscribe(mouseLogger);

                var processLogger = new ProcessLogger();
                var processReporter = new ProcessReporter(activityLogger);
                processReporter.Subscribe(processLogger);

                var timeLogger = new TimeLogger();
                var timeReporter = new TimeReporter(activityLogger);
                timeReporter.Subscribe(timeLogger);

                var activityTypeLogger = new ActivityTypeLogger(settings);
                var activityTypeReporter = new ActivityTypeReporter(activityLogger);
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

                    activityLogger.Log();
                }
            });
        }
    }
}
