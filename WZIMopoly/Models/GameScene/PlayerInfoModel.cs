using Microsoft.Xna.Framework;
using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a player model.
    /// </summary>
    internal class PlayerInfoModel : Model
    {
        /// <summary>
        /// Gets or privately sets the player model.
        /// </summary>
        internal Player Player { get; private set; }

        /// <summary>
        /// Gets or privately sets the default rectangle of the player information texture.
        /// </summary>
        internal Rectangle DefRectangle { get; private set; }

        /// <summary>
        /// Gets or privately sets the GUI start point of the player information texture.
        /// </summary>
        /// <remarks>
        /// Used to determine the place for which <see cref="DefRectangle"/> is specified.
        /// </remarks>
        internal GUIStartPoint StartPoint { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInfoModel"/> class.
        /// </summary>
        /// <param name="player">
        /// The player model.
        /// </param>
        /// <param name="defRect">
        /// The default rectangle of the player information texture.
        /// </param>
        /// <param name="startPoint">
        /// The start point that determines the place for which <paramref name="defRect"> is specified.
        /// </param>
        internal PlayerInfoModel(Player player, Rectangle defRect, GUIStartPoint startPoint)
        {
            Player = player;
            DefRectangle = defRect;
            StartPoint = startPoint;
        }
    }
}
