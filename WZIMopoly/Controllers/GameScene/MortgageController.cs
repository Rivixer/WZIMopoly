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

        /// <summary>
        /// The delegate for the OnTileClicked event.
        /// </summary>
        public delegate void OnTileClickedHander();

        /// <summary>
        /// The event that is invoked when a tile is clicked.
        /// </summary>
        /// <remarks>
        /// The tile is clicked when the player wants and
        /// can to mortgage the tile or sell its grade.
        /// </remarks>
        public event OnTileClickedHander OnTileClicked;

        /// <inheritdoc/>
        /// <remarks>
        /// Checks if the player wants to mortgage the tile or sell its grade,
        /// and if they can, it does so.
        /// </remarks>
        public override void Update()
        {
            base.Update();
            PlayerModel player = Model.CurrentPlayer;
            if (player.PlayerStatus != PlayerStatus.MortgagingTiles
                && player.PlayerStatus != PlayerStatus.SavingFromBankruptcy
                || !MouseController.WasLeftBtnClicked()
                || WZIMopoly.GameType == GameType.Online && !player.Equals(GameSettings.Client))
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

                if (t.CanUnmortgage(player))
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
                else
                {
                    return;
                }

                if (player.PlayerStatus == PlayerStatus.MortgagingTiles)
                {
                    Model.CurrentPlayer.PlayerStatus = PlayerStatus.BeforeRollingDice;
                }
                
                OnTileClicked?.Invoke();
                break;
            }
        }
    }
}
