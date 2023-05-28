using Microsoft.Xna.Framework;
using WZIMopoly.Engine;
using WZIMopoly.Enums;

namespace WZIMopoly.GUI
{
    internal class GUIInnerWindow : GUITexture
    {
        internal GUIInnerWindow(string path, Rectangle defDstRect, GUIStartPoint startPoint = GUIStartPoint.TopLeft, float opacity = 1)
            : base(path, defDstRect, startPoint, opacity)
        {
        }
    }
}
