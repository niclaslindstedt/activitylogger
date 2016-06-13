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
                _distance += Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
                _lastPoint = point;

                var mouseReport = new MouseReport
                {
                    Distance = _distance
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
