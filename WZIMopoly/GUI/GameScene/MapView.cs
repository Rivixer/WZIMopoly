using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a map view.
    /// </summary>
    internal sealed class MapView : GUITexture
    {
        /// <sumary>
        /// Initializes a new instance of the <see cref="MapView"/> class.
        /// </summary>
        internal MapView() : base(new Rectangle(0, 0, 1920, 1080)) { }

        /// <inheritdoc/>
        internal override void Load(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Images/Board");
        }
    }
}
