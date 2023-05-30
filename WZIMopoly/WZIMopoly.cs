using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Engine;
using WZIMopoly.GUI;
using WZIMopoly.Models;
using WZIMopoly.Controllers;
using WZIMopoly.Scenes;
using WZIMopoly.Controllers.MenuScene;
using WZIMopoly.Controllers.LobbyScene;
using WZIMopoly.Enums;

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
        #region Scenes
        /// <summary>
        /// The menu scene.
        /// </summary>
        private readonly MenuScene _menuScene;

        /// <summary>
        /// The lobby scene.
        /// </summary>
        private readonly LobbyScene _lobbyScene;

        /// <summary>
        /// The game scene.
        /// </summary>
        private readonly GameScene _gameScene;

        /// <summary>
        /// The settings scene.
        /// </summary>
        private readonly SettingsScene _settingsScene;
        #endregion

        /// <summary>
        /// The GraphicsDeviceManager responsible for managing graphics in MonoGame.
        /// </summary>
        private readonly GraphicsDeviceManager _graphics;

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
        /// Initializes a new instance of the <see cref="WZIMopoly"/> class.
        /// </summary>
        public WZIMopoly()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var menuModel = new MenuModel();
            var menuView = new MenuView();
            _menuScene = new MenuScene(menuModel, menuView);

            var lobbyModel = new LobbyModel();
            var lobbyView = new LobbyView();
            _lobbyScene = new LobbyScene(lobbyModel, lobbyView);

            var gameView = new GameView();
            var gameModel = new GameModel();
            _gameScene = new GameScene(gameModel, gameView);

            var settingsView = new SettingsView();
            var settingsModel = new SettingsModel();
            _settingsScene = new SettingsScene(settingsModel, settingsView);
        }

        /// <summary>
        /// Gets or sets the game language.
        /// </summary>
        internal static Language Language { get; set; } = Language.English;

        /// <summary>
        /// Changes the current scene to the specified one
        /// and recalculates all the elements.
        /// </summary>
        /// <param name="newScene">
        /// The new scene to be set as the current one.
        /// </param>
        private void ChangeCurrentScene(IPrimaryController newScene)
        {
            _currentScene = newScene;
            _currentScene.RecalculateAll();
        }

        /// <summary>
        /// Initializes the game.
        /// </summary>
        protected override void Initialize()
        {
            ScreenController.Initialize(_graphics);
            ScreenController.ChangeResolution(1280, 720, false);
            ScreenController.ApplyChanges();

            GameSettings.Players.Add(new PlayerModel("Player1", "Red", PlayerType.Local));
            GameSettings.Players.Add(new PlayerModel("Player2", "Blue"));
            GameSettings.Players.Add(new PlayerModel("Player3", "Green"));
            GameSettings.Players.Add(new PlayerModel("Player4", "Yellow"));

            _menuScene.Initialize();
            var newGameButton = _menuScene.Model.GetController<NewGameButtonController>();
            newGameButton.OnButtonClicked += () => ChangeCurrentScene(_lobbyScene);

            var quitButton = _menuScene.Model.GetController<QuitButtonController>();
            quitButton.OnButtonClicked += Exit;

            _lobbyScene.Initialize();
            var returnButton = _lobbyScene.Model.GetController<ReturnButtonController>();
            returnButton.OnButtonClicked += () => ChangeCurrentScene(_menuScene);
            var startGameButton = _lobbyScene.Model.GetController<StartGameButtonController>();
            startGameButton.OnButtonClicked += () =>
            {
                ChangeCurrentScene(_gameScene);
                _gameScene.StartGame();
            };

            _gameScene.Initialize();

            ChangeCurrentScene(_menuScene);
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
            _lobbyScene.LoadAll(Content);
            _gameScene.LoadAll(Content);
            _settingsScene.LoadAll(Content);

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
            GraphicsDevice.Clear(Color.White);
            
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