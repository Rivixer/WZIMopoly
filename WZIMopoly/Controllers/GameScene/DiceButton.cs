using WZIMopoly.Models;
using WZIMopoly.GUI.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a dice button controller.
    /// </summary>
    internal sealed class DiceButton : ButtonController<GUIDiceButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiceButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the dice button controller.
        /// </param>
        /// <param name="view">
        /// The view of the dice button controller.
        /// </param>
        internal DiceButton(ButtonModel model, GUIDiceButton view)
            : base(model, view) { }
    }
}
