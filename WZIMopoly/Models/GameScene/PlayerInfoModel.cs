using Microsoft.Xna.Framework;
using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene
{
    internal class PlayerInfoModel : Model
    {
        internal Player Player { get; private set; }

        internal Rectangle DefRectangle { get; private set; }

        internal GUIStartPoint StartPoint { get; private set; }

        internal PlayerInfoModel(Player player, Rectangle defRect, GUIStartPoint startPoint)
        {
            Player = player;
            DefRectangle = defRect;
            StartPoint = startPoint;
        }
    }
}
