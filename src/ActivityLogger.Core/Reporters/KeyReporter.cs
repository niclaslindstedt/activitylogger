using System;
using AL.Core.Interfaces;
using AL.Core.Models;

namespace AL.Core.Reporters
{
    public class KeyReporter : Reporter<KeyReport>
    {
        private static KeyReporter _instance;
        public static KeyReporter Instance(IKeyReceiver keyReceiver = null)
        {
            return _instance ?? (_instance = new KeyReporter(keyReceiver));
        }

        public KeyReport KeyReport { get; private set; }

        private readonly IKeyReceiver _keyReceiver;

        private KeyReporter(IKeyReceiver keyReceiver)
        {
            if (keyReceiver == null)
                throw new ArgumentNullException(nameof(keyReceiver));

            _keyReceiver = keyReceiver;
        }
        
        protected override void Act(KeyReport keyReport)
        {
            KeyReport = keyReport;

            _keyReceiver.ReportKey(keyReport);
        }
    }
}
