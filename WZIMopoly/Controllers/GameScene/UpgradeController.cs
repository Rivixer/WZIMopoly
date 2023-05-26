using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.GUI.GameScene;
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

        /// <inheritdoc/>
        /// <remarks>
        /// Checks if the player wants to upgrade the tile
        /// and if the player can upgrade the tile, 
        /// it upgrades the tile.
        /// </remarks>
        public override void Update()
        {
            base.Update();
            if (Model.CurrentPlayer.PlayerStatus == PlayerStatus.UpgradingFields
                && MouseController.WasLeftBtnClicked())
            {
                foreach (TileController tile in Model.TileControllers)
                {
                    if (tile.Model is SubjectTileModel t
                        && t.CanUpgrade(Model.CurrentPlayer, Model.TileModels)
                        && MouseController.IsHover(tile.View.Position.ToCurrentResolution()))
                    {
                        t.Upgrade();
                        Model.CurrentPlayer.PlayerStatus = PlayerStatus.BeforeRollingDice;
                        break;
                    }
                }
            }
        }
    }
}
