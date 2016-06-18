using System.Drawing;
using AL.Core.Models;

namespace AL.Gui
{
    public interface IGraphicsUpdater
    {
        void Update(IActivityReport activityReport, Graphics graphics);
    }
}
