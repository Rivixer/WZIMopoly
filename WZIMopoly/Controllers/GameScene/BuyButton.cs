using System.Diagnostics;
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
        /// <param name="view">
        /// The view of the buy button controller.
        /// </param>
        /// <param name="model">
        /// The model of the buy button controller.
        /// </param>
        internal BuyButton(GUIButton view, ButtonModel model)
            : base(view, model) { }

        /// <inheritdoc/>
        protected override void OnClick()
        {
            Debug.WriteLine($"{Model.Name} has been clicked.");
        }
    }
}
