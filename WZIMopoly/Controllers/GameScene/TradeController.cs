using WZIMopoly.Controllers.GameScene.GameSceneButtonControllers;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;
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
            : base(model, view)
        {
            // The step of the money to be added or subtracted.
            int step = 10;

            var addMoney = Model.InitializeChild<TradeAddMoneyButtonModel, GUITradeAddMoneyButton, TradeAddMoneyButtonController>();
            addMoney.Model.Conditions += () => Model.Recipient is not null && Model.OfferedMoney <= Model.Offeror.Money - step;
            addMoney.OnButtonClicked += () => Model.OfferedMoney += step;

            var subtractMoney = Model.InitializeChild<TradeSubtractMoneyButtonModel, GUITradeSubtractMoneyButton, TradeSubtractMoneyButtonController>();
            subtractMoney.Model.Conditions += () => Model.Recipient is not null && -Model.OfferedMoney <= Model.Recipient.Money - step;
            subtractMoney.OnButtonClicked += () => Model.OfferedMoney -= step;
        }

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
