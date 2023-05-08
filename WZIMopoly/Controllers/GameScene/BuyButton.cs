using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a buy button controller.
    /// </summary>
    internal class BuyButton : ButtonController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuyButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the buy button controller.
        /// </param>
        /// <param name="view">
        /// The view of the buy button controller.
        /// </param>
        internal BuyButton(ButtonModel model, GUIButton view)
            : base(model, view) { }
    }
}
