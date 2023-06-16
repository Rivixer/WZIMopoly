using Microsoft.Xna.Framework;
using WZIMopoly.Engine;

namespace WZIMopoly.Utils.PositionExtensions
{
    /// <summary>
    /// Provides extension methods for <see cref="Rectangle"/> and <see cref="Vector2"/>.
    /// </summary>
    internal static class PositionExtensions
    {
        /// <summary>
        /// Converts the rectangle to the current resolution.
        /// </summary>
        /// <param name="rectangle">
        /// The rectangle to convert.
        /// </param>
        /// <param name="defaultWidth">
        /// The width of the screen for which the rectangle is specified.
        /// Defaults to 1920.
        /// </param>
        /// <param name="defaultHeight">
        /// The height of the screen for which the rectangle is specified.
        /// Defaults to 1080.
        /// </param>
        /// <returns>
        /// The converted rectangle.
        /// </returns>
        public static Rectangle ToCurrentResolution(this Rectangle rectangle, int defaultWidth = 1920, int defaultHeight = 1080)
        {
            var x = rectangle.X * ScreenController.Width / defaultWidth;
            var y = rectangle.Y * ScreenController.Height / defaultHeight;
            var width = rectangle.Width * ScreenController.Width / defaultWidth;
            var height = rectangle.Height * ScreenController.Height / defaultHeight;
            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Converts the vector to the current resolution.
        /// </summary>
        /// <param name="vector">
        /// The vector to convert.
        /// </param>
        /// <param name="defaultWidth">
        /// The width of the screen for which the vector is specified.
        /// </param>
        /// <param name="defaultHeight">
        /// The height of the screen for which the vector is specified.
        /// </param>
        /// <returns>
        /// The converted vector.
        /// </returns>
        public static Vector2 ToCurrentResolution(this Vector2 vector, int defaultWidth = 1920, int defaultHeight = 1080)
        {
            var x = vector.X * ScreenController.Width / defaultWidth;
            var y = vector.Y * ScreenController.Height / defaultHeight;
            return new Vector2(x, y);
        }

        /// <summary>
        /// Converts the point to the current resolution.
        /// </summary>
        /// <param name="point">
        /// The point to convert.
        /// </param>
        /// <param name="defaultWidth">
        /// The width of the screen for which the point is specified.
        /// </param>
        /// <param name="defaultHeight">
        /// The height of the screen for which the point is specified.
        /// </param>
        /// <returns>
        /// The converted point.
        /// </returns>
        public static Point ToCurrentResolution(this Point point, int defaultWidth = 1920, int defaultHeight = 1080)
        {
            var x = point.X * ScreenController.Width / defaultWidth;
            var y = point.Y * ScreenController.Height / defaultHeight;
            return new Point(x, y);
        }
    }
}
