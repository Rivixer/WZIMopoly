#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace WZIMopoly
{
    public static class MainScreen
    {
        private static GraphicsDeviceManager _graphics;
        private static int _width;  
        private static int _height;
        private static bool _fullScreen;

        public static int Width => _width;
        public static int Height => _height;
        public static bool IsFullScreen => _fullScreen;

        public static void ChangeResolution(int width, int height, bool fullscreen)
        {
            _width = width;
            _height = height;
            _fullScreen = fullscreen;

            _graphics.PreferredBackBufferWidth = _width;
            _graphics.PreferredBackBufferHeight = _height;
            _graphics.IsFullScreen = _fullScreen;

            _graphics.ApplyChanges();
        }
        public static void Initialize(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
        }
        public static void Update()
        {
            if (KeyboardController.WasClicked(Keys.F))
            {
                if (_fullScreen)
                {
                    ChangeResolution(1280, 720, false);
                }
                else
                {
                    ChangeResolution(1920, 1080, true);
                }
            }
        }
    }
}
