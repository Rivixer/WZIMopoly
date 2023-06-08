using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameButtonControllers
{
    /// <summary>
    /// Represents the unmortgage button controller.
    /// </summary>
    internal sealed class UnmortgageButtonController : ButtonController<UnmortgageButtonModel, GUIUnmortgageButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnmortgageButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the unmortgage button controller.
        /// </param>
        /// <param name="view">
        /// The view of the unmortgage button controller.
        /// </param>
        internal UnmortgageButtonController(UnmortgageButtonModel model, GUIUnmortgageButton view)
            : base(model, view) { }
    }
}
