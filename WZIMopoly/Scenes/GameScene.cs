using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    internal class GameScene : Scene
    {
        /// <summary>
        /// Gets the view of the game scene.
        /// </summary>
        public new GameView View => (GameView)base.View;

        /// <summary>
        /// Gets the model of the game scene.
        /// </summary>
        public new GameModel Model => (GameModel)base.Model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameScene"/> class.
        /// </summary>
        public GameScene() : base(new GameView(), new GameModel()) 
        {

            CreateButtons();
            
            var mapModel = new MapModel();
            var mapView = new MapView();
            var mapController = new MapController(mapView, mapModel);
            Model.MapController = mapController;
            Children.Add(Model.MapController);
        }

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

            Model.MapController.CreatePawns(Model.Players);
            Model.MapController.SetPlayersOnStart(Model.Players);
            Model.MapController.UpdatePawnPositions();
        }

        /// <summary>
        /// Creates all buttons on the game scene and adds them to the children list.
        /// </summary>
        private void CreateButtons()
        {
            ButtonModel model;
            GUIButton view;
            ButtonController controller;

            // Dice button
            model = new ButtonModel("Dice", new Rectangle(882, 930, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            controller = new DiceButton(view, model);
            Children.Add(controller);

            // Settings button
            model = new ButtonModel("Settings", new Rectangle(60, 200, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            controller = new SettingsButton(view, model);
            Children.Add(controller);

            // Sell button
            model = new ButtonModel("Sell", new Rectangle(752, 930, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            controller = new SellButton(view, model);
            Children.Add(controller);

            // Mortage button
            model = new ButtonModel("Mortgage", new Rectangle(622, 930, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            controller = new MortgageButton(view, model);
            Children.Add(controller);

            // Trade button
            model = new ButtonModel("Trade", new Rectangle(1142, 930, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            controller = new TradeButton(view, model);
            Children.Add(controller);
            // Buy button
            model = new ButtonModel("Buy", new Rectangle(1012, 930, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            controller = new BuyButton(view, model);
            Children.Add(controller);
        }

        /// <inheritdoc/>
        protected override void Load(ContentManager content)
        {
            View.Load(content);
        }
    }
}
