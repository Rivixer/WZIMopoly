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
            var player2 = new Player("Player2", "Red");
            var player3 = new Player("Player3", "Red");
            var player4 = new Player("Player4", "Red");
            Model.Players.Add(player1);
            Model.Players.Add(player2);
            Model.Players.Add(player3);
            Model.Players.Add(player4);

            MapController.CreatePawns(Model.Players);
            MapController.SetPlayersOnStart(Model.Players);
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
            model = new ButtonModel("Dice", new Rectangle(1710, 680, 160, 160));
            view = new GUIButton(model);
            view.SetButtonHoverArea(5, 0.8f);
            controller = new DiceButton(view, model);
            Children.Add(controller);
        }

        /// <inheritdoc/>
        protected override void Load(ContentManager content)
        {
            View.Load(content);
        }
    }
}
