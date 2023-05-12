using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WZIMopoly.Controllers;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Enums;
using WZIMopoly.GUI;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;
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

            // A temporary code to add players to the game.
            var player1 = new PlayerModel("Player1", "Red");
            var player2 = new PlayerModel("Player2", "Yellow");
            var player3 = new PlayerModel("Player3", "Green");
            var player4 = new PlayerModel("Player4", "Blue");
            Model.Players.Add(player1);
            Model.Players.Add(player2);
            Model.Players.Add(player3);
            Model.Players.Add(player4);

            MapModel mapModel = GetController<MapController>().Model;
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
                AddChild(controller);
            }

            Model.InitializeChild<DiceModel, GUIDice, DiceController>();
        }

        /// <summary>
        /// Creates all buttons on the game scene and adds them to the children list.
        /// </summary>
        internal void CreateButtons()
        {
            ButtonModel model;
            GUIButton view;
            ButtonController controller;

            // Mortage button
            model = new ButtonModel("Mortgage");
            view = new GUIButton(model, new Rectangle(622, 930, 160, 160));
            view.SetButtonHoverArea(5, 0.8f);
            controller = new MortgageButton(model, view);
            AddChild(controller);

            // Sell button
            model = new ButtonModel("Sell");
            view = new GUIButton(model, new Rectangle(752, 930, 160, 160));
            view.SetButtonHoverArea(5, 0.8f);
            controller = new SellButton(model, view);
            AddChild(controller);

            // Dice button
            model = new ButtonModel("Dice");
            view = new GUIDiceButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            DiceModel diceModel = GetController<DiceController>().Model;
            MapModel mapModel = GetController<MapController>().Model;
            controller = new DiceButton(model, view as GUIDiceButton);
            controller.OnButtonClicked += () => diceModel.RollDice();
            controller.OnButtonClicked += () => mapModel.MovePlayer(Model.CurrentPlayer, diceModel.Sum);
            controller.OnButtonClicked += () => Model.NextPlayer();
            AddChild(controller);

            // Buy button
            model = new ButtonModel("Buy");
            view = new GUIButton(model, new Rectangle(1012, 930, 160, 160));
            view.SetButtonHoverArea(5, 0.8f);
            controller = new BuyButton(model, view);
            AddChild(controller);

            // Trade button
            model = new ButtonModel("Trade");
            view = new GUIButton(model, new Rectangle(1142, 930, 160, 160));
            view.SetButtonHoverArea(5, 0.8f);
            controller = new TradeButton(model, view);
            AddChild(controller);

            // Settings button
            model = new ButtonModel("Settings");
            view = new GUIButton(model, new Rectangle(60, 200, 160, 160));
            view.SetButtonHoverArea(5, 0.7f);
            controller = new SettingsButton(model, view);
            AddChild(controller);
        }
    }
}
