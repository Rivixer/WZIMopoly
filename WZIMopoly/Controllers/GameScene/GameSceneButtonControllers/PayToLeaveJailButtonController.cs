using WZIMopoly.Attributes;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameSceneButtonControllers
{
    /// <summary>
    /// Represents the controller of the pay to leave jail button.
    /// </summary>
    [UpdatesNetwork]
    internal class PayToLeaveJailButtonController : ButtonController<PayToLeaveJailButtonModel, GUIPayToLeaveJailButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayToLeaveJailButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the pay to leave jail button.
        /// </param>
        /// <param name="view">
        /// The view of the pay to leave jail button.
        /// </param>
        public PayToLeaveJailButtonController(PayToLeaveJailButtonModel model, GUIPayToLeaveJailButton view)
            : base(model, view) { }
    }
}
