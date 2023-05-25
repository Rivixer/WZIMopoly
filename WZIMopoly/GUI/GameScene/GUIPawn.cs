using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a pawn view.
    /// </summary>
    internal sealed class GUIPawn : GUITexture, IGUIDynamicPosition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIPawn"/> class.
        /// </summary>
        public GUIPawn(PawnModel model)
            : base("Images/Pawns/Pawn" + model.Color, new Rectangle(0, 0, 30, 30), GUIStartPoint.Center) { }


        /// <inheritdoc/>
        public void UpdatePosition(Point point)
        {
            UpdateDefaultDestinationRect(this, point);
        }

        /// <inheritdoc/>
        public void UpdatePosition(Vector2 vector)
        {
            UpdateDefaultDestinationRect(this, vector);
        }
    }
}