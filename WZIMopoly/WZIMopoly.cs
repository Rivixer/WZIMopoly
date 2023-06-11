using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Linq;
using System.Text;
using WZIMopoly.Engine;
using WZIMopoly.GUI;
using WZIMopoly.Models;
using WZIMopoly.Controllers;
using WZIMopoly.Scenes;
using WZIMopoly.Enums;
using WZIMopolyNetworkingLibrary;
using WZIMopoly.NetworkData;
using WZIMopoly.Controllers.MenuScene;
using WZIMopoly.Controllers.LobbyScene;
using WZIMopoly.Controllers.JoinScene;
using WZIMopoly.Controllers.GameScene.GameSceneButtonControllers;
using WZIMopoly.Models.GameScene;

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

        /// <summary>
        /// The join scene.
        /// </summary>
        private readonly JoinScene _joinScene;
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
        /// The previous scene relative to the current one.
        /// </summary>
        private IPrimaryController _previousScene;

        /// <summary>
        /// The song played in the background.
        /// </summary>
        private Song _song;

        /// <summary>
        /// Initializes a new instance of the <see cref="WZIMopoly"/> class.
        /// </summary>
        public WZIMopoly()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            GameSettings.CreatePlayers();

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

            var joinView = new JoinView();
            var joinModel = new JoinModel();
            _joinScene = new JoinScene(joinModel, joinView);
        }

        /// <summary>
        /// Gets or sets the game language.
        /// </summary>
        internal static Language Language { get; set; } = Language.Polish;

        /// <summary>
        /// Gets or sets the game type.
        /// </summary>
        internal static GameType GameType { get; set; } = GameType.Local;

        /// <summary>
        /// Changes the current scene to the specified one
        /// and recalculates all the elements.
        /// </summary>
        /// <param name="newScene">
        /// The new scene to be set as the current one.
        /// </param>
        private void ChangeCurrentScene(IPrimaryController newScene)
        {
            _previousScene = _currentScene;
            _currentScene = newScene;
            _currentScene.RecalculateAll();
        }

        /// <summary>
        /// Initializes the game.
        /// </summary>
        protected override void Initialize()
        {
            ScreenController.Initialize(_graphics);
            ScreenController.ChangeResolution(1366, 768, false);
            ScreenController.ApplyChanges();

            NetworkService.SwitchToRoot();

            InitializeMenuScene();
            InitializeLobbyScene();
            InitializeGameScene();
            InitializeSettingsScene();
            InitializeJoinScene();

            ReturnToMenu();
            base.Initialize();
        }

        /// <summary>
        /// Initializes the menu scene.
        /// </summary>
        private void InitializeMenuScene()
        {
            _menuScene.Initialize();

            var newGameBtn = _menuScene.Model.GetController<NewGameButtonController>();
            newGameBtn.OnButtonClicked += () => ChangeCurrentScene(_lobbyScene);

            var quitBtn = _menuScene.Model.GetController<QuitButtonController>();
            quitBtn.OnButtonClicked += Exit;

            var settingsBtn = _menuScene.Model.GetController<MenuSettingsButtonController>();
            settingsBtn.OnButtonClicked += () => ChangeCurrentScene(_settingsScene);

            var joinBtn = _menuScene.Model.GetController<JoinGameButtonController>();
            joinBtn.OnButtonClicked += () => ChangeCurrentScene(_joinScene);
        }

        /// <summary>
        /// Initializes the lobby scene.
        /// </summary>
        private void InitializeLobbyScene()
        {
            _lobbyScene.Initialize();

            var returnBtn = _lobbyScene.Model.GetController<Controllers.LobbyScene.ReturnButtonController>();
            returnBtn.OnButtonClicked += ReturnToMenu;

            var localModeBtn = _lobbyScene.Model.GetController<LocalModeButtonController>();
            localModeBtn.OnButtonClicked += () =>
            {
                GameSettings.Players[0].PlayerType = PlayerType.Local;
                ResetGameSettings();
            };

            var onlineModeBtn = _lobbyScene.Model.GetController<OnlineModeButtonController>();
            onlineModeBtn.OnButtonClicked += () =>
            {
                GameSettings.Players[0].PlayerType = PlayerType.OnlineHostPlayer;
                if (NetworkService.Type == ConnectionType.Root)
                {
                    NetworkService.Connection.Send(new byte[] { (byte)PacketType.NewLobby }, 0, 1);
                    NetworkService.Connection.DataReceived += (sender, e) =>
                    {
                        var code = Encoding.ASCII.GetString(e.Data);
                        var lobbyCode = _lobbyScene.Model.GetController<Controllers.LobbyScene.LobbyCodeController>();
                        lobbyCode.Model.Code = code;
                        NetworkService.SwitchToLobby(code);
                        if (NetworkService.Connection is not null)
                        {
                            NetworkService.Connection.DataReceived += (sender, e) =>
                            {
                                PacketType type = (PacketType)e.Data[0];
                                if (type == PacketType.NewPlayer)
                                {
                                    string playerNick = Encoding.UTF8.GetString(e.Data.Skip(1).ToArray());
                                    int playerIndex = GameSettings.ActivePlayers.Count;
                                    GameSettings.Players[playerIndex].Nick = playerNick;
                                    GameSettings.Players[playerIndex].PlayerType = PlayerType.OnlinePlayer;

                                    var lobbyData = new LobbyData { Players = GameSettings.ActivePlayers };
                                    byte[] data = new byte[] { (byte)PacketType.LobbyData }.Concat(lobbyData.ToByteArray()).ToArray();
                                    NetworkService.Connection.Send(data, 0, data.Length);
                                }
                                if ((PacketType)e.Data[0] == PacketType.Close)
                                {
                                    ReturnToMenu();
                                }
                                else if ((PacketType)e.Data[0] == PacketType.GameStatus)
                                {
                                    var gameData = NetData.FromByteArray<GameData>(e.Data.Skip(1).ToArray());
                                    GameSettings.UpdateGameData(gameData, _gameScene.Model);
                                };
                            };
                        }
                    };
                }

                for (int i = 1; i <= 3; i++)
                {
                    GameSettings.Players[i].PlayerType = PlayerType.None;
                    GameSettings.Players[i].ResetNick();
                };

            };

            var startGameBtn = _lobbyScene.Model.GetController<StartGameButtonController>();
            startGameBtn.OnButtonClicked += () =>
            {
                ChangeCurrentScene(_gameScene);
                _gameScene.StartGame();
                if (GameType == GameType.Online)
                {
                    var data = new byte[] { (byte)PacketType.StartGame };
                    NetworkService.Connection.Send(data, 0, 1);
                }  
            };
        }

        /// <summary>
        /// Initializes the settings scene.
        /// </summary>
        private void InitializeSettingsScene()
        {
            _settingsScene.Initialize();

            var returnBtn = _settingsScene.Model.GetController<Controllers.SettingsScene.ReturnButtonController>();
            returnBtn.OnButtonClicked += () =>
            {
                if (_previousScene is GameScene && GameType == GameType.Local)
                    _gameScene.Model.GameStatus = GameStatus.Running;

                ChangeCurrentScene(_previousScene);
            };
        }

        /// <summary>
        /// Initializes the join scene.
        /// </summary>
        private void InitializeJoinScene()
        {
            _joinScene.Initialize();
            var joinBtn = _joinScene.Model.GetController<JoinButtonController>();
            joinBtn.OnButtonClicked += () =>
            {
                var codeController = _joinScene.Model.GetController<Controllers.JoinScene.LobbyCodeController>();
                var lobbyCode = codeController.Model.LobbyCode;
                var playerNickModel = _joinScene.Model.GetController<PlayerNickController>();
                NetworkService.SwitchToLobby(lobbyCode);
                if (NetworkService.Type == ConnectionType.None)
                {
                    codeController.Model.LobbyCode = "ERROR";
                    return;
                }
                NetworkService.Connection.DataReceived += (sender, e) =>
                {
                    if ((PacketType)e.Data[0] == PacketType.Close)
                    {
                        ReturnToMenu();
                    }
                };
                var playerNick = Encoding.ASCII.GetBytes(playerNickModel.Model.PlayerNick);
                byte[] data = new byte[] { (byte)PacketType.NewPlayer }.Concat(playerNick).ToArray();
                NetworkService.Connection.Send(data, 0, data.Length);
                NetworkService.Connection.DataReceived += (sender, e) =>
                {
                    var packetType = (PacketType)e.Data[0];
                    if (packetType == PacketType.LobbyData)
                    {
                        var lobbyRawData = e.Data.Skip(1).ToArray();
                        var lobbyData = NetData.FromByteArray<LobbyData>(lobbyRawData);
                        var players = lobbyData.Players;
                        for (int i = 0; i < players.Count; i++)
                        {
                            GameSettings.Players[i].Nick = players[i].Nick;
                            GameSettings.Players[i].PlayerType = players[i].PlayerType;
                        }
                        GameSettings.ClientIndex ??= players.Count - 1;
                        GameType = GameType.Online;
                        var lobbyCodeModel = _lobbyScene.Model.GetModel<Models.LobbyScene.LobbyCodeModel>();
                        lobbyCodeModel.Code = lobbyCode;
                        ChangeCurrentScene(_lobbyScene);
                    }
                    else if (packetType == PacketType.StartGame)
                    {
                        ChangeCurrentScene(_gameScene);
                        _gameScene.StartGame();
                    }
                    else if (packetType == PacketType.GameStatus)
                    {
                        var gameData = NetData.FromByteArray<GameData>(e.Data.Skip(1).ToArray());
                        GameSettings.UpdateGameData(gameData, _gameScene.Model);
                    }
                };
            };
            var returnBtn = _joinScene.Model.GetController<Controllers.JoinScene.ReturnButtonController>();
            returnBtn.OnButtonClicked += ReturnToMenu;
        }

        /// <summary>
        /// Initializes the game scene.
        /// </summary>
        private void InitializeGameScene()
        {
            _gameScene.Initialize();

            var returnButton = _gameScene.Model.GetController<ExitButtonController>();
            returnButton.OnButtonClicked += ReturnToMenu;

            var settingsButton = _gameScene.Model.GetController<SettingsButtonController>();
            settingsButton.OnButtonClicked += () =>
            {
                if (GameType == GameType.Local)
                {
                    _gameScene.Model.GameStatus = GameStatus.Paused;
                    _gameScene.Model.Update();
                }

                ChangeCurrentScene(_settingsScene);
            };
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

            _song = Content.Load<Song>("Songs/menu_song");
            MediaPlayer.Play(_song);
            MediaPlayer.IsRepeating = true;

            _menuScene.LoadAll(Content);
            _lobbyScene.LoadAll(Content);
            _gameScene.LoadAll(Content);
            _settingsScene.LoadAll(Content);
            _joinScene.LoadAll(Content);

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

#if DEBUG
            if (KeyboardController.WasClicked(Microsoft.Xna.Framework.Input.Keys.F11))
            {
                if (ScreenController.IsFullScreen)
                    ScreenController.ChangeResolution(1366, 768, false);
                else
                    ScreenController.ChangeResolution(1920, 1080, true);

                ScreenController.ApplyChanges();
                _currentScene.RecalculateAll();
            }
#endif
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

        private void ResetGameSettings()
        {
            // Reset players
            GameSettings.ResetPlayers();
            GameSettings.ClientIndex = null;
            GameSettings.Players[0].PlayerType = PlayerType.Local;

            GameType = GameType.Local;

            // Reset join scene
            var codeJoin = _joinScene.Model.GetController<Controllers.JoinScene.LobbyCodeController>();
            codeJoin.Model.LobbyCode = "";
            var nickJoin = _joinScene.Model.GetController<PlayerNickController>();
            nickJoin.Model.PlayerNick = "";

            // Reset game scene
            var mapModel = _gameScene.Model.GetModel<MapModel>();
            mapModel.GetAllModels<TileModel>().ForEach(x => x.Reset());

            // Swtich to root
            if (NetworkService.Type != ConnectionType.Root
                && NetworkService.Type != ConnectionType.ConnectingToRoot
                && NetworkService.Type != ConnectionType.None)
            {
                NetworkService.SwitchToRoot();
            }
        }

        /// <summary>
        /// Changes the current scene to the menu scene
        /// and resets the game settings.
        /// </summary>
        private void ReturnToMenu()
        {
            ResetGameSettings();
            ChangeCurrentScene(_menuScene);
        }
    }
}