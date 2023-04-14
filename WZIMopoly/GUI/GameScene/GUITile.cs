using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Controllers.GameScene;
using Microsoft.Xna.Framework;

namespace WZIMopoly.GUI.GameScene
{
    internal sealed class GUITile : GUIText
    {
        private readonly Tile _tile;

        internal GUITile(Tile tile) : base(Vector2.Zero)
        {
            _tile = tile;
        }

        internal override void Load(ContentManager content)
        {
            Font = content.Load<SpriteFont>("Fonts/WZIMFont");
        }
    }
}