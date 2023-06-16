using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Enums;

namespace WZIMopoly.GUI.LobbyScene.PlayersList
{
    /// <summary>
    /// Represents a view for the host label.
    /// </summary>
    internal class GUIHostLabel : GUITexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIHostLabel"/> class.
        /// </summary>
        internal GUIHostLabel()
            : base("Images/PlayerHost", new Rectangle(910, 424, 40, 40), GUIStartPoint.Center) { }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (WZIMopoly.GameType == GameType.Online)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
