using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the decline trade button view.
    /// </summary>
    internal sealed class GUITradeDeclineButton : GUIButton<TradeDeclineButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUITradeDeclineButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the decline trade button.
        /// </param>
        internal GUITradeDeclineButton(TradeDeclineButtonModel model)
            : base(model, new Rectangle(860, 923, 256, 88), GUIStartPoint.Right, disableTexture: false)
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
