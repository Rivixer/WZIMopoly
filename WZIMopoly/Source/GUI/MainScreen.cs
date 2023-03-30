#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace WZIMopoly
{
    public static class MainScreen
    {
        private static _Resolution _resolution;
        public static _Resolution Resolution => _resolution;
        public abstract class _Resolution
        {
            public readonly int Width;
            public readonly int Height;
            protected _Resolution(GraphicsDeviceManager graphics, int width, int height)
            {

                Width = width;
                Height = height;
                graphics.PreferredBackBufferWidth = Width;
                graphics.PreferredBackBufferHeight = Height;
            }
        }
        private class Windowed : _Resolution
        {
            public Windowed(GraphicsDeviceManager graphics) : base(graphics, 1280, 720)
            {
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();
            }
        } 
        private class FullScreen : _Resolution
        {
            public FullScreen(GraphicsDeviceManager graphics) : base(graphics, 1920, 1080)
            {
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
            }
        }
        public static void Initialize(GraphicsDeviceManager graphics)
        {
            _resolution = new Windowed(graphics);
        }
        public static void Update(GraphicsDeviceManager graphics)
        {
            if (KeyboardController.WasClicked(Keys.F))
            {
                if (Resolution.GetType() == typeof(Windowed))
                {
                    _resolution = new FullScreen(graphics);
                }
                else
                {
                    _resolution = new Windowed(graphics);
                }
            }
        }
    }
}
