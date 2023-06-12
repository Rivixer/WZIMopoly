using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Enums;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents the view of a tile card.
    /// </summary>
    internal class GUITileCard : GUITexture
    {
        /// <summary>
        /// The text on card which represents the owner of the tile.
        /// </summary>
        internal GUIText OwnerOnCard;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUITileCard"/> class.
        /// </summary>
        /// <inheritdoc/>
        internal GUITileCard(string path, Rectangle defDstRect, GUIStartPoint startPoint = GUIStartPoint.TopLeft, float opacity = 1) 
            : base(path, defDstRect, startPoint, opacity)
        {
            OwnerOnCard = new GUIText("Fonts/WZIMFont", new Vector2(0), Color.Black, GUIStartPoint.Center, scale: 0.3f);
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            base.Load(content);

            OwnerOnCard.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            base.Recalculate();

            OwnerOnCard?.Recalculate();
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (IsVisible)
            {
                OwnerOnCard.Draw(spriteBatch);
            }
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();

            var pos = new Vector2(UnscaledDestinationRect.Center.X, UnscaledDestinationRect.Bottom - 35);
            OwnerOnCard.SetNewDefPosition(pos, GUIStartPoint.Center);
        }
    }
}
