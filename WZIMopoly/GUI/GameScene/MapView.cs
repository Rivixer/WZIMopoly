using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a map view.
    /// </summary>
    internal sealed class MapView : GUIElement
    {
        internal override void Load(ContentManager content)
        {
            Texture = null; // TODO: Load the board texture here
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            // TODO: Remove this override after load the texture
        }
    }
}
