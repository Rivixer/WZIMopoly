#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WZIMopoly.GUI;
#endregion

#region Debug Using Statements
#if DEBUG
using WZIMopoly.DebugUtils;
#endif
#endregion



namespace WZIMopoly
{
    public class WZIMopoly : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Screen _screen;

        public WZIMopoly()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            MainScreen.Initialize(_graphics);
            MainScreen.ChangeResolution(1280, 720, false);
            _screen = new GameScreen();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _screen.Load(Content);

            // TODO: use this.Content to load your game content here

            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardController.Update();
            MouseController.Update();
            MainScreen.Update();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _screen.Draw(_spriteBatch);

#if DEBUG
            ShowCursorPosition.Draw(_spriteBatch, Content);
#endif

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}