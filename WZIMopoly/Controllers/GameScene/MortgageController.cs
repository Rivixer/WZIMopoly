using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.TileModels;
using WZIMopoly.Utils.PositionExtensions;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents the mortgage controller.
    /// </summary>
    internal class MortgageController : Controller<MortgageModel, GUIMortgage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MortgageController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the mortgage.
        /// </param>
        /// <param name="view">
        /// The view of the mortgage.
        /// </param>
        public MortgageController(MortgageModel model, GUIMortgage view)
            : base(model, view) { }

        /// <inheritdoc/>
        /// <remarks>
        /// Checks if the player wants to mortgage the tile or sell its grade,
        /// and if the player can mortgage the tile or sell its grade, 
        /// it mortgages the tile or sells its grade.
        /// </remarks>
        public override void Update()
        {
            base.Update();
            PlayerModel player = Model.CurrentPlayer;
            if (player.PlayerStatus != PlayerStatus.MortgagingTiles
                || !MouseController.WasLeftBtnClicked())
            {
                return;
            }
            foreach (TileController tile in Model.TileControllers)
            {
                if (tile.Model is not SubjectTileModel t
                    || !MouseController.IsHover(tile.View.Position.ToCurrentResolution()))
                {
                    continue;
                }

                if (t.IsMortgaged)
                {
                    t.Unmortgage();
                }
                else if (t.CanMortgage(player))
                {
                    t.Mortgage();
                }
                else if (t.CanSellGrade(player))
                {
                    t.SellGrade();
                }

                Model.CurrentPlayer.PlayerStatus = PlayerStatus.BeforeRollingDice;
                break;
            }
        }
    }
}
