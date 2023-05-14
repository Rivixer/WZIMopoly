using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameButtonControllers
{
    /// <summary>
    /// Represents the mortgage button controller.
    /// </summary>
    internal sealed class MortgageButtonController : ButtonController<MortgageButtonModel, GUIMortgageButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MortgageButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the mortgage button controller.
        /// </param>
        /// <param name="view">
        /// The view of the mortgage button controller.
        /// </param>
        internal MortgageButtonController(MortgageButtonModel model, GUIMortgageButton view)
            : base(model, view) { }
    }
}
