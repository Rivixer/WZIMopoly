using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameSceneButtonControllers
{
    /// <summary>
    /// Represents the end turn button controller.
    /// </summary>
    internal class EndTurnButtonController : ButtonController<EndTurnButtonModel, GUIEndTurnButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndTurnButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the end turn button controller.
        /// </param>
        /// <param name="view">
        /// The view of the end turn button controller.
        /// </param>
        public EndTurnButtonController(EndTurnButtonModel model, GUIEndTurnButton view)
            : base(model, view) { }
    }
}
