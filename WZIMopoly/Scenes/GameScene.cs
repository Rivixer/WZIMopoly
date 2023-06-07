using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

            InitializePlayerInfo();
            InitializeButtons();

            var buttons = Model.GetAllControllersRecursively<ButtonController>();
            foreach(var button in buttons)
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
            Model.GameStatus = GameStatus.Running;
            GameSettings.ActivePlayers[0].PlayerStatus = PlayerStatus.BeforeRollingDice;
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            _timerController.UpdateTime(Model.ActualTime);

            var currentPlayerTile = Model.GetModelRecursively<TileModel>(x => x.Players.Contains(GameSettings.CurrentPlayer));

            var gameUpdateModels = Model.GetAllModelsRecursively<IGameUpdateModel>();
            gameUpdateModels.ForEach(x => x.Update(Model.CurrentPlayer, currentPlayerTile));

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
                    var passedTiles = _mapController.Model.MovePlayer(GameSettings.CurrentPlayer, (uint)stepToMove);
                    MapModel.ActivateCrossableTiles(GameSettings.CurrentPlayer, passedTiles);
                    _mapController.Model.ActivateOnStandTile(GameSettings.CurrentPlayer);
                    _mapController.View.UpdatePawnPositions();
                }
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
            var players = GameSettings.Players;
            var positions = new List<Tuple<PlayerModel, Rectangle, GUIStartPoint>>()
            {
                new(players[0], new Rectangle(0, 10, infoWidth, infoHeight), GUIStartPoint.TopLeft),
                new(players[1], new Rectangle(1920, 10, infoWidth, infoHeight), GUIStartPoint.TopRight),
                new(players[2], new Rectangle(1920, 1070, infoWidth, infoHeight), GUIStartPoint.BottomRight),
                new(players[3], new Rectangle(0, 1070, infoWidth, infoHeight), GUIStartPoint.BottomLeft),
            };

            foreach (var (player, position, startPoint) in positions)
            {
                var model = new PlayerInfoModel(player);
                var view = new GUIPlayerInfo(model, position, startPoint);
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
                    }
                    else
                    {
                        List<TileController> passedTiles = mapModel.MovePlayer(GameSettings.CurrentPlayer, diceModel.Sum);
                        MapModel.ActivateCrossableTiles(GameSettings.CurrentPlayer, passedTiles);
                    }
                    mapModel.ActivateOnStandTile(GameSettings.CurrentPlayer);
                }
                GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.AfterRollingDice;
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

                GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.WaitingForTurn;
                if (!diceModel.LastRollWasDouble || jailModel.Players.Contains(GameSettings.CurrentPlayer))
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
            Model.InitializeChild<TradeButtonModel, GUITradeButton, TradeButtonController>();

            // Leave jail button
            var leaveJailButton = Model.InitializeChild<LeaveJailButtonModel, GUILeaveJailButton, LeaveJailButtonController>();
            leaveJailButton.OnButtonClicked += () =>
            {
                var jailModel = mapModel.GetModel<MandatoryLectureTileModel>();
                jailModel.ReleasePrisoner(GameSettings.CurrentPlayer);
                GameSettings.CurrentPlayer.Money -= 50;
            };

            // Use elevator button
            var tepToElevButton = Model.InitializeChild<UseElevatorButtonModel, GUIUseElevatorButton, UseElevatorButtonController>();
            tepToElevButton.OnButtonClicked += () =>
            {
                var elevTile = mapModel.GetModel<ElevatorTileModel>((x) => !x.Players.Contains(GameSettings.CurrentPlayer));
                mapModel.TeleportPlayer(GameSettings.CurrentPlayer, elevTile);
                _mapController.View.UpdatePawnPositions();
            };
        }
    }
}
