using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        /// The color of the pawn.
        /// </summary>
        private string _color;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIPawn"/> class.
        /// </summary>
        internal GUIPawn() : base(new Rectangle(0, 0, 30, 30), GUIStartPoint.Center) { }

        /// <inheritdoc/>
        internal override void LoadDataFromModel(Models.Model model)
        {
            _color = (model as PawnModel).Color;
        }

        /// <inheritdoc/>
        internal override void Load(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Images/Pawns/Pawn" + _color);
        }

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