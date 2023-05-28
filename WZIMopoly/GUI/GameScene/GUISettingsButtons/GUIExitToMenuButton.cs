using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models;

namespace WZIMopoly.GUI.GameScene.GUISettingsButtons
{
    internal class GUIExitToMenuButton : GUIButton
    {
        public GUIExitToMenuButton(ButtonModel model)
            : base(model, new Rectangle(1400, 800, 541, 125), GUIStartPoint.Right, disableTexture: false)
        {
            SetButtonHoverArea(5, 0.75f);
        }
    }
}
