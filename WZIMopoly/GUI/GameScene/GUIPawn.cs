using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a pawn view.
    /// </summary>
    internal class GUIPawn : GUITexture
    {
        /// <summary>
        /// The model of the pawn.
        /// </summary>
        private readonly PawnModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIPawn"/> class.
        /// </summary>
        internal GUIPawn(PawnModel model)
            : base("Images/Pawns/Pawn" + model.Color, new Rectangle(0, 0, 40, 40), GUIStartPoint.Center)
        {
            _model = model;
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            var player = GameSettings.Players.First(x => x.Color == _model.Color);
            if (player.PlayerType != PlayerType.None && player.PlayerStatus != PlayerStatus.Bankrupt)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}