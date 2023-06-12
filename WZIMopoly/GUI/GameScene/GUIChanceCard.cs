using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents the view of the chance card.
    /// </summary>
    internal class GUIChanceCard : GUITexture
    {
        /// <summary>
        /// The model of the chance card.
        /// </summary>
        private readonly ChanceCardModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIChanceCard"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the chance card.
        /// </param>
        internal GUIChanceCard(ChanceCardModel model)
            : base($"Images/{model.Type}Cards/{model.Id}", new Rectangle(960, 521, 640, 367), GUIStartPoint.Center)
        {
            _model = model;
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(var chanceTile in _model.ChanceTiles)
            {
                if (chanceTile.DrawnCard?.Equals(_model) ?? false)
                {
                    base.Draw(spriteBatch);
                    break;
                }
            }
        }
    }
}
