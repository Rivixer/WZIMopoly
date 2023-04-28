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
    internal class ConsoleScene : Scene
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
        public ConsoleScene() : base(new GameView(), new GameModel()) 
        {
            CreateConsole();

            var mapModel = new MapModel();
            var mapView = new MapView();
            var mapController = new MapController(mapView, mapModel);
            Model.MapController = mapController;
            Children.Add(Model.MapController);
        }

        /// <summary>
        /// Creates all buttons on the game scene and adds them to the children list.
        /// </summary>
        private void CreateConsole()
        {
            ConsoleModel model;
            GUIConsole view;
            ConsoleController controller;

            // Console
            model = new ConsoleModel("Console", new Rectangle(700, 700, 600, 300));
            view = new GUIConsole(model);
            controller = new ConsoleController(view, model);
            Children.Add(controller);
        }
    }
}
