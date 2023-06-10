using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.TileModels;
using WZIMopoly.Utils.PositionExtensions;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents the trade controller.
    /// </summary>
    internal class TradeController : Controller<TradeModel, GUITrade>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the trade controller.
        /// </param>
        /// <param name="view">
        /// The view of the trade controller.
        /// </param>
        public TradeController(TradeModel model, GUITrade view)
            : base(model, view) { }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            if (GameSettings.CurrentPlayer.PlayerStatus == PlayerStatus.Trading && MouseController.WasLeftBtnClicked())
            {
                foreach(var playerBox in Model.PlayerInfoCtrls)
                {
                    if (!playerBox.Model.Player.Equals(GameSettings.CurrentPlayer)
                        && MouseController.IsHover(playerBox.View.UnscaledDestinationRect.ToCurrentResolution()))
                    {
                        Model.Recipient = playerBox.Model.Player;
                        Model.ChosenRecipientTiles.Clear();
                    }
                }
                if (Model.Recipient is not null)
                {
                    foreach (var tile in Model.TileControllers)
                    {
                        if (tile.Model is not PurchasableTileModel tileModel)
                        {
                            continue;
                        }
                        if ((tileModel.Owner?.Equals(GameSettings.CurrentPlayer) ?? false)
                            && MouseController.IsHover(tile.View.Position.ToCurrentResolution()))
                        {
                            if (Model.ChosenOfferorTiles.Contains(tileModel))
                            {
                                Model.ChosenOfferorTiles.Remove(tileModel);
                            }
                            else
                            {
                                Model.ChosenOfferorTiles.Add(tileModel);
                            }
                        }
                        else if (((tile.Model as PurchasableTileModel)?.Owner?.Equals(Model.Recipient) ?? false)
                            && MouseController.IsHover(tile.View.Position.ToCurrentResolution()))
                        {
                            if (Model.ChosenRecipientTiles.Contains(tileModel))
                            {
                                Model.ChosenRecipientTiles.Remove(tileModel);
                            }
                            else
                            {
                                Model.ChosenRecipientTiles.Add(tileModel);
                            }
                        }
                    }
                }
            }
        }
    }
}
