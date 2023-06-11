using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WZIMopoly.Attributes;
using WZIMopoly.Controllers;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Controllers.GameScene.GameButtonControllers;
using WZIMopoly.Controllers.GameScene.GameSceneButtonControllers;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.GUI;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.GameButtonModels;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Scenes
{
    /// <summary>
    /// Represents a game scene.
    /// </summary>
    /// <remarks>
    /// Act as the primary controller.
    /// </remarks>
    internal class GameScene : Scene<GameModel, GameView>
    {
        /// <summary>
        /// The timer controller.
        /// </summary>
        private TimerController _timerController;

        /// <summary>
        /// The map controller.
        /// </summary>
        private MapController _mapController;

        /// <summary>
        /// The dice controller.
        /// </summary>
        private DiceController _diceController;

        /// <summary>
        /// The upgrade tiles controller.
        /// </summary>
        private UpgradeController _upgradeController;

        /// <summary>
        /// The mortgage tiles controller.
        /// </summary>
        private MortgageController _mortgageController;

        /// <summary>
        /// The trade controller.
        /// </summary>
        private TradeController _tradeController;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameScene"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the game scene.
        /// </param>
        /// <param name="view">
        /// The view of the game scene.
        /// </param>
        public GameScene(GameModel model, GameView view)
            : base(model, view) { }

        /// <inheritdoc/>
        public override void Initialize()
        {
            _mapController = Model.InitializeChild<MapModel, GUIMap, MapController>();
            List<TileController> tileControllers = _mapController.Model.LoadTiles();
            _mapController.Model.CreatePawns(GameSettings.Players);
            
            var model = new DiceModel();
            var view = new GUIDice(model);
            _diceController = new DiceController(model, view);

            Model.AddChildBefore<MapController>(_diceController);
            _timerController = Model.InitializeChild<TimerModel, GUITimer, TimerController>();

            _upgradeController = Model.InitializeChild<UpgradeModel, GUIUpgrade, UpgradeController>(tileControllers);
            _upgradeController.OnTileClicked += () => GameSettings.SendGameData(Model);

            _mortgageController = Model.InitializeChild<MortgageModel, GUIMortgage, MortgageController>(tileControllers);
            _mortgageController.OnTileClicked += () => GameSettings.SendGameData(Model);

            _mapController.Model.InitializeChanceCards(_mortgageController, Model);

            Model.InitializeChild<JailModel, GUIJail, JailController>();

            InitializePlayerInfo();

            _tradeController = Model.InitializeChild<TradeModel, GUITrade, TradeController>(tileControllers, Model.GetAllControllers<PlayerInfoController>());

            InitializeButtons();

            var buttons = Model.GetAllControllersRecursively<ButtonController>();
            foreach (var button in buttons)
            {
                if (Attribute.IsDefined(button.GetType(), typeof(UpdatesNetwork)))
                {
                    button.OnButtonClicked += () =>
                    {
                        GameSettings.SendGameData(Model);
                    };
                }
            }
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        internal void StartGame()
        {
            _mapController.Model.SetPlayersOnStart();
            _mapController.View.UpdatePawnPositions();

            Model.SetStartTime();
            Model.SetEndTime();

            Model.GameStatus = GameStatus.Running;
            GameSettings.ActivePlayers[0].PlayerStatus = PlayerStatus.BeforeRollingDice;
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            _timerController?.Model.UpdateTime(Model.ActualTime, Model.TimeToEnd);

            var currentPlayerTile = Model.GetModelRecursively<TileModel>(x => x.Players.Contains(GameSettings.CurrentPlayer));

            var gameUpdateModels = Model.GetAllModelsRecursively<IGameUpdateModel>();
            foreach (var model in gameUpdateModels)
            {
                try
                {
                    model.Update(GameSettings.CurrentPlayer, currentPlayerTile);
                }
                catch (InvalidOperationException ex)
                {
                    Debug.WriteLine($"Cannot update {model} model - {ex.Message}");
                }
            }

            var gameUpdateViews = Model.GetAllViewsRecursively<IGUIGameUpdate>();
            gameUpdateViews.ForEach(x => x.Update(GameSettings.CurrentPlayer, currentPlayerTile));

#if DEBUG
            // Click a key on the keyboard to move the current player a certain number of steps.
            List<Keys> clickedKeys = KeyboardController.GetAllClickedKeys();
            if (clickedKeys.Count > 0)
            {
                int stepToMove = clickedKeys[0] switch
                {
                    Keys.D1 => 1,
                    Keys.D2 => 2,
                    Keys.D3 => 3,
                    Keys.D4 => 4,
                    Keys.D5 => 5,
                    Keys.D6 => 6,
                    Keys.D7 => 7,
                    Keys.D8 => 8,
                    Keys.D9 => 9,
                    _ => 0,
                };
                if (stepToMove > 0)
                {
                    var jailModel = Model.GetModelRecursively<MandatoryLectureTileModel>();
                    if (jailModel.IsPrisoner(GameSettings.CurrentPlayer))
                    {
                        jailModel.ReleasePrisoner(GameSettings.CurrentPlayer);
                    }
                    var passedTiles = _mapController.Model.MovePlayer(GameSettings.CurrentPlayer, stepToMove);
                    MapModel.ActivateCrossableTiles(GameSettings.CurrentPlayer, passedTiles);
                    _mapController.Model.HandleBankrupt(
                        delegate { _mapController.Model.ActivateOnStandTile(GameSettings.CurrentPlayer); },
                        _mortgageController, Model);
                    _mapController.View.UpdatePawnPositions();
                    GameSettings.SendGameData(Model);
                }
            }

            // Click "+" or "-" to increase or decrease the current player's money.
            var isPressedPlusKey = KeyboardController.IsPressed(Keys.OemPlus) || KeyboardController.IsPressed(Keys.Add);
            if (isPressedPlusKey)
            {
                GameSettings.CurrentPlayer.Money++;
                GameSettings.SendGameData(Model);
            }
            var isPressedMinusKey = KeyboardController.IsPressed(Keys.OemMinus) || KeyboardController.IsPressed(Keys.Subtract);
            if (isPressedMinusKey)
            {
                GameSettings.CurrentPlayer.Money--;
                GameSettings.SendGameData(Model);
            }

            // Click F5 to bankrupt the current player.
            if (KeyboardController.WasClicked(Keys.F5))
            {
                GameSettings.CurrentPlayer.GoBankrupt();
            }

            // Click F6 to bankrupt the current player and
            // transfer all of their properties to the owner
            // of the tile they are currently on.
            if (KeyboardController.WasClicked(Keys.F6))
            {
                if (_mapController.Model.GetPlayerTile(GameSettings.CurrentPlayer).Model is PurchasableTileModel t)
                {
                    if (t.Owner is not null)
                    {
                        GameSettings.CurrentPlayer.GoBankrupt(t.Owner);
                    }
                }
            }

            // Click F8 to increase end time.
            if (KeyboardController.WasClicked(Keys.F8))
            {
                Model.IncreaseGameTime();
            }

            // Click F7 to decrease end time.
            if (KeyboardController.WasClicked(Keys.F7))
            {
                Model.DecreaseGameTime();
            }
#endif
        }

        /// <summary>
        /// Initializes the player information on the game scene and adds them to the children list.
        /// </summary>
        private void InitializePlayerInfo()
        {
            var infoWidth = 500;
            var infoHeight = 200;
            var rects = new List<Rectangle>()
            {
                new Rectangle(0, 10, infoWidth, infoHeight),
                new Rectangle(1920, 10, infoWidth, infoHeight),
                new Rectangle(1920, 1070, infoWidth, infoHeight),
                new Rectangle(0, 1070, infoWidth, infoHeight)
            };

            var pos = new List<GUIStartPoint>()
            {
                GUIStartPoint.TopLeft,
                GUIStartPoint.TopRight,
                GUIStartPoint.BottomRight,
                GUIStartPoint.BottomLeft
            };

            for (int i = 0; i < 4; i++)
            {
                var model = new PlayerInfoModel(GameSettings.Players[i]);
                var view = new GUIPlayerInfo(model, rects[i], pos[i]);
                var controller = new PlayerInfoController(model, view);
                Model.AddChildBefore<MapController>(controller);
            }
        }

        /// <summary>
        /// Initializes all buttons on the game scene and adds them to the children list.
        /// </summary>
        private void InitializeButtons()
        {
            var diceModel = _diceController.Model;
            var mapModel = _mapController.Model;
            var mapView = _mapController.View;

            // Mortage button
            var mortgageButton = Model.InitializeChild<MortgageButtonModel, GUIMortgageButton, MortgageButtonController>();
            mortgageButton.OnButtonClicked += () =>
            {
                if (GameSettings.CurrentPlayer.PlayerStatus == PlayerStatus.MortgagingTiles)
                {
                    GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.BeforeRollingDice;
                }
                else if (GameSettings.CurrentPlayer.PlayerStatus == PlayerStatus.BeforeRollingDice)
                {
                    _mortgageController.View.UpdateMask();
                    GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.MortgagingTiles;
                }
            };

            // Upgrade button
            var upgradeButton = Model.InitializeChild<UpgradeButtonModel, GUIUpgradeButton, UpgradeButtonController>();
            upgradeButton.OnButtonClicked += () =>
            {
                if (GameSettings.CurrentPlayer.PlayerStatus == PlayerStatus.UpgradingTiles)
                {
                    GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.BeforeRollingDice;
                }
                else if (GameSettings.CurrentPlayer.PlayerStatus == PlayerStatus.BeforeRollingDice)
                {
                    _upgradeController.View.UpdateMask();
                    GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.UpgradingTiles;
                }
            };

            // Dice and EndTurn button
            var diceButton = Model.InitializeChild<DiceButtonModel, GUIDiceButton, DiceButtonController>();
            var endTurnButton = Model.InitializeChild<EndTurnButtonModel, GUIEndTurnButton, EndTurnButtonController>();
            diceButton.Model.Conditions = () => !endTurnButton.Model.WasClickedInThisFrame;
            endTurnButton.Model.Conditions = () => !diceButton.Model.WasClickedInThisFrame;
            diceButton.OnButtonClicked += async () =>
            {
                GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.DuringRollingDice;
                diceModel.RollDice();

                await Task.Delay(350);

                var jailModel = mapModel.GetModel<MandatoryLectureTileModel>();
                if (jailModel.IsPrisoner(GameSettings.CurrentPlayer) && diceModel.LastRollWasDouble)
                {
                    jailModel.ReleasePrisoner(GameSettings.CurrentPlayer);
                }
                if (!jailModel.IsPrisoner(GameSettings.CurrentPlayer))
                {
                    if (diceModel.DoubleCounter == 3)
                    {
                        mapModel.TeleportPlayer(GameSettings.CurrentPlayer, jailModel);
                        jailModel.AddPrisoner(GameSettings.CurrentPlayer);
                    }
                    else
                    {
                        List<TileController> passedTiles = mapModel.MovePlayer(GameSettings.CurrentPlayer, diceModel.Sum);
                        MapModel.ActivateCrossableTiles(GameSettings.CurrentPlayer, passedTiles);
                    }
                    mapModel.HandleBankrupt(
                        delegate { mapModel.ActivateOnStandTile(GameSettings.CurrentPlayer); },
                         _mortgageController, Model);
                }
                if (GameSettings.CurrentPlayer.PlayerStatus != PlayerStatus.SavingFromBankruptcy)
                {
                    GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.AfterRollingDice;
                }
                GameSettings.SendGameData(Model);
                mapView.UpdatePawnPositions();
            };
            endTurnButton.OnButtonClicked += () =>
            {
                var jailModel = mapModel.GetModel<MandatoryLectureTileModel>();
                if (jailModel.IsPrisoner(GameSettings.CurrentPlayer))
                {
                    jailModel.AddPrisonerTurn(GameSettings.CurrentPlayer);
                    if (jailModel.CanPrisonerRelease(GameSettings.CurrentPlayer))
                    {
                        jailModel.ReleasePrisoner(GameSettings.CurrentPlayer);
                    }
                }

                var chanceTiles = mapModel.GetAllModels<ChanceTileModel>();
                chanceTiles.ForEach(x => x.DrawnCard = null);

                GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.WaitingForTurn;
                if (GameSettings.CurrentPlayer.AdditionalRoll)
                {
                    GameSettings.CurrentPlayer.AdditionalRoll = false;
                }
                else if (!diceModel.LastRollWasDouble || jailModel.Players.Contains(GameSettings.CurrentPlayer))
                {
                    GameSettings.NextPlayer();
                    diceModel.ResetDoubleCounter();
                }
                GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.BeforeRollingDice;
                diceModel.Reset();
            };

            // Buy button
            var buyButton = Model.InitializeChild<BuyButtonModel, GUIBuyButton, BuyButtonController>();
            buyButton.OnButtonClicked += () =>
            {
                var currentPlayerTileModel = Model.GetModelRecursively<PurchasableTileModel>(x => x.Players.Contains(GameSettings.CurrentPlayer));
                currentPlayerTileModel.Purchase(GameSettings.CurrentPlayer);
            };

            // Trade button
            var tradeButton = Model.InitializeChild<TradeButtonModel, GUITradeButton, TradeButtonController>();
            tradeButton.OnButtonClicked += () =>
            {
                if (GameSettings.CurrentPlayer.PlayerStatus == PlayerStatus.Trading)
                {
                    GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.BeforeRollingDice;
                }
                else if (GameSettings.CurrentPlayer.PlayerStatus == PlayerStatus.BeforeRollingDice)
                {
                    var tradeModel = _tradeController.Model;
                    tradeModel.Offeror = GameSettings.CurrentPlayer;
                    tradeModel.Recipient = null;
                    tradeModel.OfferedMoney = 0;
                    tradeModel.ChosenOfferorTiles.Clear();
                    tradeModel.ChosenRecipientTiles.Clear();
                    GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.Trading;
                }                
            };

            // Pay to leave jail button
            var leaveJailButton = Model.GetControllerRecursively<PayToLeaveJailButtonController>();
            leaveJailButton.OnButtonClicked += () =>
            {
                var jailModel = mapModel.GetModel<MandatoryLectureTileModel>();
                jailModel.ReleasePrisoner(GameSettings.CurrentPlayer);
                GameSettings.CurrentPlayer.Money -= 50;
            };

            // Use card to leave jail button
            var useCardButton = Model.GetControllerRecursively<UseCardToLeaveJailButtonController>();
            useCardButton.OnButtonClicked += () =>
            {
                var jailModel = mapModel.GetModel<MandatoryLectureTileModel>();
                jailModel.ReleasePrisoner(GameSettings.CurrentPlayer);
                GameSettings.CurrentPlayer.NumberOfLeaveJailCards--;
            };

            // Use elevator button
            var tepToElevButton = Model.InitializeChild<UseElevatorButtonModel, GUIUseElevatorButton, UseElevatorButtonController>();
            tepToElevButton.OnButtonClicked += () =>
            {
                var elevTile = mapModel.GetModel<ElevatorTileModel>((x) => !x.Players.Contains(GameSettings.CurrentPlayer));
                mapModel.TeleportPlayer(GameSettings.CurrentPlayer, elevTile);
                _mapController.View.UpdatePawnPositions();
            };

            //Exit button
            var exitButton = Model.InitializeChild<ExitButtonModel, GUIExitButton, ExitButtonController>();

            //Settings button
            var settingButton = Model.InitializeChild<SettingsButtonModel, GUISettingsButton, SettingsButtonController>();

            // Make trade button
            var makeTradeButton = Model.InitializeChild<TradeMakeButtonModel, GUITradeMakeButton, TradeMakeButtonController>();
            makeTradeButton.Model.Conditions += () => _tradeController.Model.TotalValue > 0;
            makeTradeButton.OnButtonClicked += () =>
            {
                PlayerModel recipient = _tradeController.Model.Recipient;
                recipient.PlayerStatus = PlayerStatus.ReceivingTrade;
                GameSettings.SetTemporaryPlayerAsCurrent(recipient);
            };

            // Accept trade button
            var acceptTradeButton = Model.InitializeChild<TradeAcceptButtonModel, GUITradeAcceptButton, TradeAcceptButtonController>();
            acceptTradeButton.Model.Conditions += () => !makeTradeButton.Model.WasClickedInThisFrame;
            acceptTradeButton.OnButtonClicked += () =>
            {
                PlayerModel offeror = _tradeController.Model.Offeror;
                PlayerModel recipient = _tradeController.Model.Recipient;
                foreach (var tile in _tradeController.Model.ChosenOfferorTiles)
                {
                    offeror.TransferTileTo(recipient, tile);
                }
                foreach (var tile in _tradeController.Model.ChosenRecipientTiles)
                {
                    recipient.TransferTileTo(offeror, tile);
                }
                offeror.TransferMoneyTo(recipient, _tradeController.Model.OfferedMoney);
                GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.WaitingForTurn;
                GameSettings.RestoreCurrentPlayer();
                GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.BeforeRollingDice;
            };

            // Decline trade button
            var declineTradeButton = Model.InitializeChild<TradeDeclineButtonModel, GUITradeDeclineButton, TradeDeclineButtonController>();
            declineTradeButton.OnButtonClicked += () =>
            {
                GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.WaitingForTurn;
                GameSettings.RestoreCurrentPlayer();
                GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.BeforeRollingDice;
            };

        }
    }
}
