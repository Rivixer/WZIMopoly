using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the make trade button view.
    /// </summary>
    internal sealed class GUITradeMakeButton : GUIButton<TradeMakeButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUITradeMakeButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the make trade button.
        /// </param>
        internal GUITradeMakeButton(TradeMakeButtonModel model)
            : base(model, new Rectangle(1060, 923, 256, 88), GUIStartPoint.Left, disableTexture: false)
        {
            SetButtonHoverArea(5, 0.8f);
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (GameSettings.CurrentPlayer.PlayerStatus == PlayerStatus.Trading)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
