using WZIMopoly.Attributes;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameSceneButtonControllers
{
    /// <summary>
    /// Represents the use elevator button controller.
    /// </summary>
    [UpdatesNetwork]
    internal class UseElevatorButtonController : ButtonController<UseElevatorButtonModel, GUIUseElevatorButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UseElevatorButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the use elevator button.
        /// </param>
        /// <param name="view">
        /// The view of the use elevator button.
        /// </param>
        public UseElevatorButtonController(UseElevatorButtonModel model, GUIUseElevatorButton view) 
            : base(model, view) { }
    }
}
