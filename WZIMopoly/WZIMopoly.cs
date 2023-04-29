using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WZIMopoly.Engine;
using WZIMopoly.GUI;
using WZIMopoly.Controllers;

#if DEBUG
using WZIMopoly.DebugUtils;
#endif



namespace WZIMopoly
{
    public class WZIMopoly : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IPrimaryController _currentScene;

        public WZIMopoly()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ScreenController.Initialize(_graphics);
            ScreenController.ChangeResolution(1280, 720, false);

            _currentScene = new GameScene();
            _currentScene.RecalculateAll();
            (_currentScene as GameScene)?.StartGame();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentScene.LoadAll(Content);

            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
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
        }

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