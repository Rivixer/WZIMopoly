using Microsoft.Xna.Framework;

namespace WZIMopoly.Engine
{
    /// <summary>
    /// Represents a controller for the screen.
    /// </summary>
    public static class ScreenController
    {
        #region Fields
        /// <summary>
        /// The graphics device manager provided by MonoGame.
        /// </summary>
        private static GraphicsDeviceManager _graphics;

        /// <summary>
        /// The width of the screen.
        /// </summary>
        private static int _width;

        /// <summary>
        /// The height of the screen.
        /// </summary>
        private static int _height;

        /// <summary>
        /// Whether the screen is in fullscreen mode.
        /// </summary>
        private static bool _fullScreen;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the width of the screen.
        /// </summary>
        public static int Width => _width;

        /// <summary>
        /// Gets the height of the screen.
        /// </summary>
        public static int Height => _height;

        /// <summary>
        /// Gets whether the screen is in fullscreen mode.
        /// </summary>
        public static bool IsFullScreen => _fullScreen;
        #endregion

        /// <summary>
        /// Initializes the screen controller.
        /// </summary>
        /// <remarks>
        /// Should be called once and before any other method.
        /// </remarks>
        /// <param name="graphics"></param>
        public static void Initialize(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
        }

        /// <summary>
        /// Changes the screen resolution parameters.
        /// </summary>
        /// <param name="width">
        /// The new width of the screen.
        /// </param>
        /// <param name="height">
        /// The new height of the screen.
        /// </param>
        /// <param name="fullscreen">
        /// Whether the screen should be in fullscreen mode.
        /// </param>
        public static void ChangeResolution(int width, int height, bool fullscreen)
        {
            _width = width;
            _height = height;
            _fullScreen = fullscreen;
        }

        /// <summary>
        /// Applies the screen settings to the main window.
        /// </summary>
        /// <remarks>
        /// To change the screen settings, use
        /// <see cref="ChangeResolution"/> method.
        /// </remarks>
        public static void ApplyChanges()
        {
            _graphics.PreferredBackBufferWidth = _width;
            _graphics.PreferredBackBufferHeight = _height;
            _graphics.IsFullScreen = _fullScreen;
            _graphics.ApplyChanges();
        }
    }
}
