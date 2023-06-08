using WZIMopoly.Attributes;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameButtonControllers
{
    /// <summary>
    /// Represents the dice button controller.
    /// </summary>
    [UpdatesNetwork]
    internal sealed class DiceButtonController : ButtonController<DiceButtonModel, GUIDiceButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiceButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the dice button controller.
        /// </param>
        /// <param name="view">
        /// The view of the dice button controller.
        /// </param>
        internal DiceButtonController(DiceButtonModel model, GUIDiceButton view)
            : base(model, view) { }
    }
}
