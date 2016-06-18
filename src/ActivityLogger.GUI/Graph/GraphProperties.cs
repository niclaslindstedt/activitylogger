using System.Drawing;

namespace AL.Gui.Graph
{
    public static class GraphProperties
    {
        public static readonly Pen AxisColor = Pens.Black;
        public static readonly SolidBrush BackgroundColor = new SolidBrush(Color.White);
        public static readonly Pen BorderColor = Pens.Gray;

        public const int Width = 554;
        public const int Height = 190;
        public const int Left = 15;
        public const int Top = 15;
        public const int Bottom = Top + Height;
        public const int BarWidth = 3;
        public const int AxisPaddingX = 25;
        public const int AxisPaddingY = 15;
    }
}
