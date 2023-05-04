using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a controller for player information.
    /// </summary>
    /// <remarks>
    /// Used to display player information such as
    /// nickname or amount of money in the game scene.
    /// </remarks>
    internal class PlayerInfo : Controller<PlayerInfoModel, GUIPlayerInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInfo"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the player information.
        /// </param>
        /// <param name="view">
        /// The view of the player information.
        /// </param>
        internal PlayerInfo(PlayerInfoModel model, GUIPlayerInfo view)
            : base (model, view) { }
    }
}
