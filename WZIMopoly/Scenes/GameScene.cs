using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Controllers.GameScene.GameButtonControllers;
using WZIMopoly.Controllers.GameScene.GameSceneButtonControllers;
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
            _mapController.Model.LoadTiles();
            _mapController.Model.CreatePawns(GameSettings.Players);

            _timerController = Model.InitializeChild<TimerModel, GUITimer, TimerController>();
            _diceController = Model.InitializeChild<DiceModel, GUIDice, DiceController>();

            InitializePlayerInfo();
            InitializeButtons();
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        internal void StartGame()
        {
            _mapController.Model.SetPlayersOnStart();
            _mapController.Model.UpdatePawnPositions();
            
            Model.SetStartTime();
            Model.GameStatus = GameStatus.Running;
            GameSettings.ActivePlayers[0].PlayerStatus = PlayerStatus.BeforeRollingDice;
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            _timerController.UpdateTime(Model.ActualTime);

            var currentPlayerTile = Model.GetModelRecursively<TileModel>(x => x.Players.Contains(Model.CurrentPlayer));

            var gameUpdateModels = Model.GetAllModelsRecursively<IGameUpdateModel>();
            gameUpdateModels.ForEach(x => x.Update(Model.CurrentPlayer, currentPlayerTile));

            var gameUpdateViews = Model.GetAllViewsRecursively<IGUIGameUpdate>();
            gameUpdateViews.ForEach(x => x.Update(Model.CurrentPlayer, currentPlayerTile));
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
                Model.AddChild(controller);
            }
        }

        /// <summary>
        /// Initializes all buttons on the game scene and adds them to the children list.
        /// </summary>
        private void InitializeButtons()
        {
            var diceModel = _diceController.Model;
            var mapModel = _mapController.Model;

            // Mortage button
            Model.InitializeChild<MortgageButtonModel, GUIMortgageButton, MortgageButtonController>();

            // Upgrade button
            Model.InitializeChild<UpgradeButtonModel, GUIUpgradeButton, UpgradeButtonController>();

            // Dice and EndTurn button
            var diceButton = Model.InitializeChild<DiceButtonModel, GUIDiceButton, DiceButtonController>();
            var endTurnButton = Model.InitializeChild<EndTurnButtonModel, GUIEndTurnButton, EndTurnButtonController>();
            diceButton.Model.Conditions = () => !endTurnButton.Model.WasClickedInThisFrame;
            endTurnButton.Model.Conditions = () => !diceButton.Model.WasClickedInThisFrame;
            diceButton.OnButtonClicked += async () =>
            {
                Model.CurrentPlayer.PlayerStatus = PlayerStatus.DuringRollingDice;
                diceModel.RollDice();

                await Task.Delay(350);

                Model.CurrentPlayer.PlayerStatus = PlayerStatus.AfterRollingDice;
                mapModel.MovePlayer(Model.CurrentPlayer, diceModel.Sum);
            };
            endTurnButton.OnButtonClicked += () =>
            {
                Model.CurrentPlayer.PlayerStatus = PlayerStatus.WaitingForTurn;
                Model.NextPlayer();
                Model.CurrentPlayer.PlayerStatus = PlayerStatus.BeforeRollingDice;
                diceModel.Reset();
            };

            // Buy button
            var buyButton = Model.InitializeChild<BuyButtonModel, GUIBuyButton, BuyButtonController>();
            buyButton.OnButtonClicked += () =>
            {
                var currentPlayerTile = Model.GetModelRecursively<PurchasableTileModel>(x => x.Players.Contains(Model.CurrentPlayer));
                currentPlayerTile.Purchase(Model.CurrentPlayer);
            };

            // Trade button
            Model.InitializeChild<TradeButtonModel, GUITradeButton, TradeButtonController>();

            // Settings button
            Model.InitializeChild<SettingsButtonModel, GUISettingsButton, SettingsButtonController>();
        }
    }
}
