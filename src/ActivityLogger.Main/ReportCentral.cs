using System.Threading;
using System.Threading.Tasks;
using AL.Core.Interfaces;
using AL.Core.Loggers;
using AL.Core.Reporters;

namespace AL.Core
{
    public class ReportCentral
    {
        private readonly IActivityReceiver _activityReceiver;
        private readonly Settings _settings;

        public ReportCentral(IActivityReceiver activityReceiver, Settings settings)
        {
            _activityReceiver = activityReceiver;
            _settings = settings;
        }

        public void StartReporterThread()
        {
            Task.Factory.StartNew(() =>
            {
                var activityLogger = new ActivityLogger();
                var activityReporter = new ActivityReporter(_activityReceiver);
                activityReporter.Subscribe(activityLogger);

                var keyLogger = KeyLogger.Instance();
                var keyReporter = new KeyReporter(activityLogger);
                keyReporter.Subscribe(keyLogger);

                var mouseLogger = new MouseLogger();
                var mouseReporter = new MouseReporter(activityLogger);
                mouseReporter.Subscribe(mouseLogger);

                var processLogger = new ProcessLogger();
                var processReporter = new ProcessReporter(activityLogger);
                processReporter.Subscribe(processLogger);

                var timeLogger = new TimeLogger();
                var timeReporter = new TimeReporter(activityLogger);
                timeReporter.Subscribe(timeLogger);

                var activityTypeLogger = new ActivityTypeLogger(_settings);
                var activityTypeReporter = new ActivityTypeReporter(activityLogger);
                activityTypeReporter.Subscribe(activityTypeLogger);

                while (true)
                {
                    Thread.Sleep(1000);

                    // KeyLogger will log when keystrokes are recorded,
                    // so no need to tell it to log here.

                    mouseLogger.Log();
                    processLogger.Log();
                    timeLogger.Log();

                    activityTypeLogger.DetermineActivityType(
                        processReporter.ProcessReport, mouseReporter.MouseReport, keyReporter.KeyReport);
                    activityTypeLogger.Log();

                    activityLogger.Log();
                }
            });
        }
    }
}
