using AL.Core.Interfaces;
using AL.Core.Models;

namespace AL.Core.Reporters
{
    public class KeyReporter : Reporter<KeyReport>
    {
        public KeyReport KeyReport { get; private set; }

        private readonly IKeyReceiver _keyReceiver;

        public KeyReporter(IKeyReceiver keyReceiver)
        {
            _keyReceiver = keyReceiver;
        }
        
        protected override void Act(KeyReport keyReport)
        {
            KeyReport = keyReport;

            _keyReceiver.ReportKey(keyReport);
        }
    }
}
