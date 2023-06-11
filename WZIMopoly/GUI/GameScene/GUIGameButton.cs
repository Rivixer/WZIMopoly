using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Enums;
using WZIMopoly.Models;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents the view of a button which is on the game scene.
    /// </summary>
    internal class GUIGameButton : GUIButton
    {
        /// <summary>
        /// The auxiliary text informing the player about button's functions.
        /// </summary>
        /// <remarks>
        /// The text is displayed on the board when a button is hovered.<br/>
        /// The default color is black.
        /// </remarks>
        protected readonly GUIText AuxText;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIGameButton"/> class.
        /// </summary>
        /// <inheritdoc cref="GUIButton(ButtonModel, Rectangle, GUIStartPoint, bool, bool)"/>
        public GUIGameButton(
            ButtonModel model,
            Rectangle defDstRect,
            GUIStartPoint startPoint = GUIStartPoint.TopLeft,
            bool hoverTexture = true,
            bool disableTexture = true) 
            : base(model, defDstRect, startPoint, hoverTexture, disableTexture)
        {
            AuxText = new GUIText("Fonts/WZIMFont", new Vector2(960, 720), Color.Black, GUIStartPoint.Center, scale: 0.3f);
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            base.Load(content);

            AuxText.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            base.Recalculate();

            AuxText.Recalculate();
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (IsHovered && Model.IsActive)
            {
                AuxText.Draw(spriteBatch);
            }
        }
    }

    /// <summary>
    /// Represents the view of a button with a specific model
    /// which is on the game scene.
    /// </summary>
    /// <typeparam name="M">
    /// The type of the model of the button.
    /// </typeparam>
    internal class GUIGameButton<M> : GUIGameButton
        where M : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIGameButton{M}"/> class.
        /// </summary>
        /// <inheritdoc cref="GUIGameButton(ButtonModel, Rectangle, GUIStartPoint, bool, bool)"/>
        public GUIGameButton(
            M model,
            Rectangle defDstRect,
            GUIStartPoint startPoint = GUIStartPoint.TopLeft,
            bool hoverTexture = true,
            bool disableTexture = true
        ) : base(model, defDstRect, startPoint, hoverTexture, disableTexture) { }

        /// <summary>
        /// Gets the model of the button.
        /// </summary>
        protected new M Model => (M)base.Model;
    }
}
