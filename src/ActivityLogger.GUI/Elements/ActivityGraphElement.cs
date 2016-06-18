using System;
using System.Drawing;
using System.Linq;
using AL.Core.Models;
using AL.Gui.Graph;

namespace AL.Gui.Elements
{
    public class ActivityGraphElement : IControlUpdater
    {
        private IActivityReport _activityReport;

        private readonly Graphics _coupledGraphics;

        public ActivityGraphElement(Graphics coupledGraphics)
        {
            _coupledGraphics = coupledGraphics;
        }

        public void Update(IActivityReport activityReport)
        {
            _activityReport = activityReport;

            DrawBorder();
            DrawBackground();
            DrawAxisY();
            DrawAxisX();
            DrawKpmGraph();
        }

        private void DrawKpmGraph()
        {
            if (_activityReport?.KeyStrokesPerMinute == null || !_activityReport.KeyStrokesPerMinute.Any())
                return;

            var kpmList = ActivityGraphHelper.GetKpmList(_activityReport);

            var maxKeyStrokes = kpmList.Max();

            for (var i = 0; i < kpmList.Length; ++i)
            {
                var keyStrokes = kpmList[i];

                Color color;
                if ((float)keyStrokes / maxKeyStrokes > 0.9)
                    color = Color.Green;
                else if ((float)keyStrokes / maxKeyStrokes > 0.6)
                    color = Color.YellowGreen;
                else if ((float)keyStrokes / maxKeyStrokes > 0.3)
                    color = Color.DarkOrange;
                else
                    color = Color.OrangeRed;

                var x = i * GraphProperties.Width + GraphProperties.Left + GraphProperties.AxisPaddingX + 1;
                var y = GraphProperties.Bottom - Math.Min((float)keyStrokes / maxKeyStrokes * GraphProperties.Bottom, GraphProperties.Bottom) + GraphProperties.Top + GraphProperties.AxisPaddingY;
                var height = Math.Max(0, GraphProperties.Bottom - y) - GraphProperties.AxisPaddingY;

                _coupledGraphics.FillRectangle(new SolidBrush(color), x, y, GraphProperties.BarWidth, height);
            }
        }

        private void DrawAxisX()
        {
            _coupledGraphics.DrawLine(
                GraphProperties.AxisColor,
                GraphProperties.Left + GraphProperties.AxisPaddingX,
                GraphProperties.Top + GraphProperties.Height - GraphProperties.AxisPaddingY,
                GraphProperties.Left + GraphProperties.Width - GraphProperties.AxisPaddingX,
                GraphProperties.Top + GraphProperties.Height - GraphProperties.AxisPaddingY);
        }

        private void DrawBorder()
        {
            _coupledGraphics.DrawRectangle(
                GraphProperties.BorderColor,
                GraphProperties.Left - 1,
                GraphProperties.Top - 1,
                GraphProperties.Width + 2,
                GraphProperties.Height + 2);
        }

        private void DrawBackground()
        {
            _coupledGraphics.FillRectangle(
                GraphProperties.BackgroundColor,
                GraphProperties.Left,
                GraphProperties.Top,
                GraphProperties.Width,
                GraphProperties.Height);
        }

        private void DrawAxisY()
        {
            _coupledGraphics.DrawLine(
                GraphProperties.AxisColor,
                GraphProperties.Left + GraphProperties.AxisPaddingX,
                GraphProperties.Top + GraphProperties.AxisPaddingY,
                GraphProperties.Left + GraphProperties.AxisPaddingX,
                GraphProperties.Top + GraphProperties.Height - GraphProperties.AxisPaddingY);
        }
    }
}
