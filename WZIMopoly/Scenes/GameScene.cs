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
            var player1 = new Player("Player1", "Red");
            var player2 = new Player("Player2", "Yellow");
            var player3 = new Player("Player3", "Green");
            var player4 = new Player("Player4", "Blue");
            Model.Players.Add(player1);
            Model.Players.Add(player2);
            Model.Players.Add(player3);
            Model.Players.Add(player4);

            MapController mapController = GetController<MapController>();
            mapController.CreatePawns(Model.Players);
            mapController.SetPlayersOnStart(Model.Players);
            mapController.UpdatePawnPositions();
        }

        /// <summary>
        /// Creates an interface for the game.
        /// </summary>
        internal void CreateInterface()
        {
            var infoWidth = 500;
            var infoHeight = 200;
            var positions = new List<Tuple<Player, Rectangle, GUIStartPoint>>()
            {
                new(Model.Players[0], new Rectangle(0, 10, infoWidth, infoHeight), GUIStartPoint.TopLeft),
                new(Model.Players[1], new Rectangle(0, 1070, infoWidth, infoHeight), GUIStartPoint.BottomLeft),
                new(Model.Players[2], new Rectangle(1920, 1070, infoWidth, infoHeight), GUIStartPoint.BottomRight),
                new(Model.Players[3], new Rectangle(1920, 10, infoWidth, infoHeight), GUIStartPoint.TopRight),
            };

            PlayerInfoModel model;
            GUIPlayerInfo view;
            PlayerInfo controller;
            foreach (var (player, position, startPoint) in positions)
            {
                model = new PlayerInfoModel(player, position, startPoint);
                view = new GUIPlayerInfo(model);
                controller = new PlayerInfo(model, view);
                AddChild(controller);
            }
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
            model = new ButtonModel("Mortgage", new Rectangle(622, 930, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            controller = new MortgageButton(model, view);
            AddChild(controller);

            // Sell button
            model = new ButtonModel("Sell", new Rectangle(752, 930, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            controller = new SellButton(model, view);
            AddChild(controller);

            // Dice button
            model = new ButtonModel("Dice", new Rectangle(882, 930, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            controller = new DiceButton(model, view);
            AddChild(controller);

            // Buy button
            model = new ButtonModel("Buy", new Rectangle(1012, 930, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            controller = new BuyButton(model, view);
            AddChild(controller);

            // Trade button
            model = new ButtonModel("Trade", new Rectangle(1142, 930, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            controller = new TradeButton(model, view);
            AddChild(controller);

            // Settings button
            model = new ButtonModel("Settings", new Rectangle(60, 200, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.7f);
            controller = new SettingsButton(model, view);
            AddChild(controller);
        }
    }
}
