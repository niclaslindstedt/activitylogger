using System.Windows.Forms;

namespace ActivityLogger.Core.Services
{
    public class KeyReporter : ActivityReporter<Keys>
    {
        protected override void Act(Keys value)
        {
            // Nothing to do
        }

        public KeyReporter(Settings settings) : base(settings) { }
    }
}
