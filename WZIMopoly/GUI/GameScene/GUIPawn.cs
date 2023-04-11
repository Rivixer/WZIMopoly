using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a pawn view.
    /// </summary>
    internal sealed class GUIPawn : GUIElement, IGUIDynamicPosition
    {
        /// <summary>
        /// The color of the pawn.
        /// </summary>
        private readonly string _color;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIPawn"/> class.
        /// </summary>
        /// <param name="color"></param>
        internal GUIPawn(string color)
            : base() // TODO: Set the default destination rectangle here
        {
            _color = color;
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