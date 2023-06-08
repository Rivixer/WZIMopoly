using WZIMopoly.Attributes;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameSceneButtonControllers
{
    /// <summary>
    /// Represents the controller of the leave jail button.
    /// </summary>
    [UpdatesNetwork]
    internal class LeaveJailButtonController : ButtonController<LeaveJailButtonModel, GUILeaveJailButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveJailButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the leave jail button.
        /// </param>
        /// <param name="view">
        /// The view of the leave jail button.
        /// </param>
        public LeaveJailButtonController(LeaveJailButtonModel model, GUILeaveJailButton view)
            : base(model, view) { }
    }
}
