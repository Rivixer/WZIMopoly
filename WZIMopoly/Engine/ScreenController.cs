using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
        private static GraphicsDeviceManager s_graphics;

        /// <summary>
        /// The width of the screen.
        /// </summary>
        private static int s_width;

        /// <summary>
        /// The height of the screen.
        /// </summary>
        private static int s_height;

        /// <summary>
        /// Whether the screen is in fullscreen mode.
        /// </summary>
        private static bool s_fullScreen;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the width of the screen.
        /// </summary>
        public static int Width => s_width;

        /// <summary>
        /// Gets the height of the screen.
        /// </summary>
        public static int Height => s_height;

        /// <summary>
        /// Gets whether the screen is in fullscreen mode.
        /// </summary>
        public static bool IsFullScreen => s_fullScreen;
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
            s_graphics = graphics;
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
            s_width = width;
            s_height = height;
            s_fullScreen = fullscreen;
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
            s_graphics.PreferredBackBufferWidth = s_width;
            s_graphics.PreferredBackBufferHeight = s_height;
            s_graphics.IsFullScreen = s_fullScreen;
            s_graphics.ApplyChanges();
        }

        /// <summary>
        /// Updates the controller.
        /// </summary>
        /// <remarks>
        /// Checks if the <c>F</c> key has been pressed and changes the resolution
        /// from 1280x720 to 1920x1080(fullscreen) and vice versa.
        /// </remarks>
        public static void Update()
        {
            if (KeyboardController.WasClicked(Keys.F))
            {
                if (s_fullScreen)
                {
                    ChangeResolution(1280, 720, false);
                }
                else
                {
                    ChangeResolution(1920, 1080, true);
                }
                ApplyChanges();
            }
        }
    }
}
