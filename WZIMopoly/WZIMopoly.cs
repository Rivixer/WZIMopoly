using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WZIMopoly.Engine;
using WZIMopoly.GUI;
using WZIMopoly.Models;
using WZIMopoly.Controllers;
using WZIMopoly.Models.GameScene;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Controllers.GameScene;

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

        /// <summary>
        /// The game scene.
        /// </summary>
        private GameScene _gameScene;

        /// <summary>
        /// Initializes a new instance of the <see cref="WZIMopoly"/> class.
        /// </summary>
        public WZIMopoly()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game.
        /// </summary>
        protected override void Initialize()
        {
            ScreenController.Initialize(_graphics);
            ScreenController.ChangeResolution(1280, 720, false);
            ScreenController.ApplyChanges();

            var gameView = new GameView();
            var gameModel = new GameModel();
            _gameScene = new GameScene(gameModel, gameView);
            _gameScene.Initialize();

            _currentScene = _gameScene;
            _currentScene.RecalculateAll();

            (_currentScene as GameScene)?.StartGame();

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
            _currentScene.LoadAll(Content);

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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardController.Update();
            MouseController.Update();

            if (KeyboardController.WasClicked(Keys.F))
            {
                ScreenController.Update();
                _currentScene.RecalculateAll();
            }

            _currentScene.UpdateAll();

            base.Update(gameTime);

            _currentScene.AfterUpdateAll();
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

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