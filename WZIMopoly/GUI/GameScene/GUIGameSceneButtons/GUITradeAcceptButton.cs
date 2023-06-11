using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the accept trade button view.
    /// </summary>
    internal sealed class GUITradeAcceptButton : GUIButton<TradeAcceptButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUITradeAcceptButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the accept trade button.
        /// </param>
        internal GUITradeAcceptButton(TradeAcceptButtonModel model)
            : base(model, new Rectangle(1060, 923, 256, 88), GUIStartPoint.Left, disableTexture: false)
        {
            SetButtonHoverArea(5, 0.8f);
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (GameSettings.ActivePlayers.Any(x => x.PlayerStatus == PlayerStatus.ReceivingTrade))
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
