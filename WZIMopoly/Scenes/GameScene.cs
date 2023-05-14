using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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
using WZIMopoly.Scenes;

namespace WZIMopoly
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
        /// Initializes a new instance of the <see cref="GameScene"/> class.
        /// </summary>
        /// <param name="gameView">
        /// The view of the game scene.
        /// </param>
        /// <param name="gameModel">
        /// The model of the game scene.
        /// </param>
        internal GameScene(GameModel gameModel, GameView gameView)
            : base(gameModel, gameView) { }

        /// <summary>
        /// Starts the game.
        /// </summary>
        internal void StartGame()
        {
            Model.SetStartTime();
            Model.GameStatus = GameStatus.Running;

            _timerController = Model.InitializeChild<TimerModel, GUITimer, TimerController>();

            // A temporary code to add players to the game.
            var player1 = new PlayerModel("Player1", "Red");
            var player2 = new PlayerModel("Player2", "Yellow");
            var player3 = new PlayerModel("Player3", "Green");
            var player4 = new PlayerModel("Player4", "Blue");
            Model.Players.Add(player1);
            Model.Players.Add(player2);
            Model.Players.Add(player3);
            Model.Players.Add(player4);

            var mapModel = Model.GetModel<MapModel>();
            mapModel.CreatePawns(Model.Players);
            mapModel.SetPlayersOnStart(Model.Players);
            mapModel.UpdatePawnPositions();
        }

        /// <summary>
        /// Creates an interface for the game.
        /// </summary>
        internal void CreateInterface()
        {
            var infoWidth = 500;
            var infoHeight = 200;
            var positions = new List<Tuple<PlayerModel, Rectangle, GUIStartPoint>>()
            {
                new(Model.Players[0], new Rectangle(0, 10, infoWidth, infoHeight), GUIStartPoint.TopLeft),
                new(Model.Players[1], new Rectangle(0, 1070, infoWidth, infoHeight), GUIStartPoint.BottomLeft),
                new(Model.Players[2], new Rectangle(1920, 1070, infoWidth, infoHeight), GUIStartPoint.BottomRight),
                new(Model.Players[3], new Rectangle(1920, 10, infoWidth, infoHeight), GUIStartPoint.TopRight),
            };

            PlayerInfoModel model;
            GUIPlayerInfo view;
            PlayerInfoController controller;
            foreach (var (player, position, startPoint) in positions)
            {
                model = new PlayerInfoModel(player);
                view = new GUIPlayerInfo(model, () => Model.CurrentPlayer, position, startPoint);
                controller = new PlayerInfoController(model, view);
                Model.AddChild(controller);
            }

            Model.InitializeChild<DiceModel, GUIDice, DiceController>();
        }

        /// <summary>
        /// Creates all buttons on the game scene and adds them to the children list.
        /// </summary>
        internal void CreateButtons()
        {
            var diceModel = Model.GetModel<DiceModel>();
            var mapModel = Model.GetModel<MapModel>();

            // Mortage button
            Model.InitializeChild<MortgageButtonModel, GUIMortgageButton, MortgageButtonController>();

            // Sell button
            Model.InitializeChild<SellButtonModel, GUISellButton, SellButtonController>();

            // Dice button
            var diceButton = Model.InitializeChild<DiceButtonModel, GUIDiceButton, DiceButtonController>();
            diceButton.OnButtonClicked += () => diceModel.RollDice();
            diceButton.OnButtonClicked += () => mapModel.MovePlayer(Model.CurrentPlayer, diceModel.Sum);
            diceButton.OnButtonClicked += () => Model.NextPlayer();

            // Buy button
            Model.InitializeChild<BuyButtonModel, GUIBuyButton, BuyButtonController>();

            // Trade button
            Model.InitializeChild<TradeButtonModel, GUITradeButton, TradeButtonController>();

            // Settings button
            Model.InitializeChild<SettingsButtonModel, GUISettingsButton, SettingsButtonController>();
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            _timerController.UpdateTime(Model.ActualTime);
            var gameButtonModels = Model.GetAllModelsRecursively<IGameButtonModel>();
            var currentPlayerTile = Model.GetModelRecursively<TileModel>(x => x.Players.Contains(Model.CurrentPlayer));
            gameButtonModels.ForEach(x => x.Update(Model.CurrentPlayer, currentPlayerTile));
        }
    }
}
