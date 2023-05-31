using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a pawn view.
    /// </summary>
    internal class GUIPawn : GUITexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIPawn"/> class.
        /// </summary>
        internal GUIPawn(PawnModel model)
            : base("Images/Pawns/Pawn" + model.Color, new Rectangle(0, 0, 30, 30), GUIStartPoint.Center) { }
    }
}