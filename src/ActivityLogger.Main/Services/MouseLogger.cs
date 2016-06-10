using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ActivityLogger.Main.Services
{
    public class MouseLogger : ActivityLogger<Point>
    {
        private static MouseLogger _instance;
        public static MouseLogger Instance()
        {
            return _instance ?? (_instance = new MouseLogger());
        }

        private Point _lastPoint;

        public MouseLogger()
        {
            _lastPoint = GetCursorPosition();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);

                    var point = GetCursorPosition();
                    if (point.X != _lastPoint.X || point.Y != _lastPoint.Y)
                    {
                        _lastPoint = point;
                        Observer.OnNext(point);
                    }
                }
            });
        }
        
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        public static Point GetCursorPosition()
        {
            Point lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }
    }
}
