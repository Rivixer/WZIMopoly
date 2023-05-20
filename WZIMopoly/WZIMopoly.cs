using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WZIMopoly.Engine;
using WZIMopoly.GUI;
using WZIMopoly.Models;
using WZIMopoly.Controllers;
using WZIMopoly.Scenes;
using WZIMopoly.Controllers.MenuScene;

#if DEBUG
using WZIMopoly.DebugUtils;
#endif

namespace WZIMopoly
{
    /// <summary>
    /// Represents a WZIMopoly game.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The game is based on the Monopoly board game.
    /// </para>
    /// <para>
    /// TODO: more info
    /// </para>
    /// </remarks>
    public class WZIMopoly : Game
    {
        /// <summary>
        /// The GraphicsDeviceManager responsible for managing graphics in MonoGame.
        /// </summary>
        private GraphicsDeviceManager _graphics;

        /// <summary>
        /// The SpriteBatch object resposible for rendering graphics in Monogame.
        /// </summary>
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// The current scene manages the application's view and model.
        /// </summary>
        /// <remarks>
        /// The scene must implement the <see cref="IPrimaryController"/> interface.
        /// </remarks>
        private IPrimaryController _currentScene;

        #region Scenes
        /// <summary>
        /// The main menu scene.
        /// </summary>
        private readonly MenuScene _menuScene;

        /// <summary>
        /// The game scene.
        /// </summary>
        private readonly GameScene _gameScene;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="WZIMopoly"/> class.
        /// </summary>
        public WZIMopoly()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var mainMenuModel = new MenuModel();
            var mainMenuView = new MenuView();
            _menuScene = new MenuScene(mainMenuModel, mainMenuView);

            var gameView = new GameView();
            var gameModel = new GameModel();
            _gameScene = new GameScene(gameModel, gameView);
        }

        /// <summary>
        /// Initializes the game.
        /// </summary>
        protected override void Initialize()
        {
            ScreenController.Initialize(_graphics);
            ScreenController.ChangeResolution(1280, 720, false);
            ScreenController.ApplyChanges();

            _menuScene.Initialize();
            var newGameButton = _menuScene.Model.GetController<NewGameButtonController>();
            newGameButton.OnButtonClicked += () =>
            {
                _gameScene.StartGame();
                _currentScene = _gameScene;
                _currentScene.RecalculateAll();
            };
            var quitButton = _menuScene.Model.GetController<QuitButtonController>();
            quitButton.OnButtonClicked += Exit;

            _gameScene.Initialize();

            _currentScene = _menuScene;
            _currentScene.RecalculateAll();
            base.Initialize();
        }

        /// <summary>
        /// Loads the content of the game.
        /// </summary>
        /// <remarks>
        /// This method is called once per game and is used to load all content.
        /// </remarks>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _menuScene.LoadAll(Content);
            _gameScene.LoadAll(Content);

            base.LoadContent();
        }

        /// <summary>
        /// Updates the game.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame and is used to update the game logic.
        /// </remarks>
        /// <param name="gameTime">
        /// The time since the application launched.
        /// </param>
        protected override void Update(GameTime gameTime)
        {
            _currentScene.BeforeUpdateAll();

            KeyboardController.Update();
            MouseController.Update();

            if (KeyboardController.WasClicked(Keys.F))
            {
                ScreenController.Update();
                _currentScene.RecalculateAll();
            }

            _currentScene.UpdateAll();
            _currentScene.AfterUpdateAll();

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame and is used to draw the game.
        /// </remarks>
        /// <param name="gameTime">
        /// The time since the application launched.
        /// </param>
        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            
            _currentScene.DrawAll(_spriteBatch);
#if DEBUG
            ShowCursorPosition.Draw(_spriteBatch, Content);
#endif
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}