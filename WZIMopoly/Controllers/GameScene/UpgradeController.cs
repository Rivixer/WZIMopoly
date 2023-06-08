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
    /// Represents the upgrade tiles controller.
    /// </summary>
    internal class UpgradeController : Controller<UpgradeModel, GUIUpgrade>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the upgrade tiles controller.
        /// </param>
        /// <param name="view">
        /// The view of the upgrade tiles controller.
        /// </param>
        public UpgradeController(UpgradeModel model, GUIUpgrade view)
            : base(model, view) { }

        /// <summary>
        /// The delegate for the OnTileClicked event.
        /// </summary>
        public delegate void OnTileClickedHander();

        /// <summary>
        /// The event that is invoked when a tile is clicked.
        /// </summary>
        /// <remarks>
        /// The tile is clicked when the player
        /// wants and can upgrade the tile.
        /// </remarks>
        public event OnTileClickedHander OnTileClicked;

        /// <inheritdoc/>
        /// <remarks>
        /// Checks if the player wants to upgrade the tile
        /// and if the player can upgrade the tile, 
        /// it upgrades the tile.
        /// </remarks>
        public override void Update()
        {
            base.Update();
            PlayerModel player = Model.CurrentPlayer;
            if (player.PlayerStatus != PlayerStatus.UpgradingTiles
                || !MouseController.WasLeftBtnClicked()
                || WZIMopoly.GameType == GameType.Online && !player.Equals(GameSettings.Client))
            {
                return;
            }
            foreach (TileController tile in Model.TileControllers)
            {
                if (tile.Model is SubjectTileModel t
                    && t.CanUpgrade(Model.CurrentPlayer)
                    && MouseController.IsHover(tile.View.Position.ToCurrentResolution()))
                {
                    t.Upgrade();
                    Model.CurrentPlayer.PlayerStatus = PlayerStatus.BeforeRollingDice;
                    OnTileClicked?.Invoke();
                    break;
                }
            }
        }
    }
}
