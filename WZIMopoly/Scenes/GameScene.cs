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
            var player2 = new PlayerModel("Player2", "Blue");
            var player3 = new PlayerModel("Player3", "Green");
            var player4 = new PlayerModel("Player4", "Yellow");
            Model.Players.Add(player1);
            Model.Players.Add(player2);
            Model.Players.Add(player3);
            Model.Players.Add(player4);

            player1.PlayerStatus = PlayerStatus.BeforeRollingDice;

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
                new(Model.Players[1], new Rectangle(1920, 10, infoWidth, infoHeight), GUIStartPoint.TopRight),
                new(Model.Players[2], new Rectangle(1920, 1070, infoWidth, infoHeight), GUIStartPoint.BottomRight),
                new(Model.Players[3], new Rectangle(0, 1070, infoWidth, infoHeight), GUIStartPoint.BottomLeft),
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

            var currentPlayerTile = Model.GetModelRecursively<TileModel>(x => x.Players.Contains(Model.CurrentPlayer));

            var gameUpdateModels = Model.GetAllModelsRecursively<IGameUpdateModel>();
            gameUpdateModels.ForEach(x => x.Update(Model.CurrentPlayer, currentPlayerTile));

            var gameUpdateViews = Model.GetAllViewsRecursively<IGUIGameUpdate>();
            gameUpdateViews.ForEach(x => x.Update(Model.CurrentPlayer, currentPlayerTile));
        }
    }
}
