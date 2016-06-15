using System;
using System.Drawing;
using AL.Core.Models;
using AL.Core.Utilities;

namespace AL.Core.Loggers
{
    public class MouseLogger : Logger<MouseReport>
    {
        private Point _lastPoint;
        private double _distance;

        public MouseLogger()
        {
            _lastPoint = GetCursorPosition();
        }

        public override void Log()
        {
            var point = GetCursorPosition();
            if (point.X != _lastPoint.X || point.Y != _lastPoint.Y)
            {
                var distanceX = Math.Abs(_lastPoint.X - point.X);
                var distanceY = Math.Abs(_lastPoint.Y - point.Y);
                var dDistance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
                _distance += dDistance;
                _lastPoint = point;

                var mouseReport = new MouseReport
                {
                    TotalDistance = _distance,
                    Distance = dDistance
                };

                Observer.OnNext(mouseReport);
            }
        }

        public static Point GetCursorPosition()
        {
            Point lpPoint;
            NativeMethods.GetCursorPos(out lpPoint);

            return lpPoint;
        }
    }
}
