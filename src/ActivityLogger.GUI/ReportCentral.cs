using System;
using System.Threading;
using System.Threading.Tasks;
using ActivityLogger.Core;
using ActivityLogger.Core.Constants;
using ActivityLogger.Core.Services;

namespace ActivityLogger.GUI
{
    public class ReportCentral : ITimeReceiver
    {
        private readonly ITimeReceiver _timeReceiver; 

        public ReportCentral(ITimeReceiver timeReceiver)
        {
            _timeReceiver = timeReceiver;
        }

        public void StartThread()
        {
            Task.Factory.StartNew(() =>
            {
                var processService = new ProcessService();

                var keyReporter = new KeyReporter(Program.Settings);
                keyReporter.Subscribe(KeyLogger.Instance());

                var mouseReporter = new MouseReporter(Program.Settings);
                mouseReporter.Subscribe(MouseLogger.Instance());

                var timeLogger = new TimeLogger(processService, keyReporter, mouseReporter);
                var timeReporter = new TimeReporter(Program.Settings, _timeReceiver);
                timeReporter.Subscribe(timeLogger);

                while (true)
                {
                    Thread.Sleep(1000);

                    timeLogger.LogTime(Program.Settings);
                }
            });
        }

        public void ReportTime(Tuple<string, ActivityType, int> timeReport)
        {
            _timeReceiver.ReportTime(timeReport);
        }
    }
}
