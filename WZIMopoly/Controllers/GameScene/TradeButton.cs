using System.Diagnostics;
using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a trade button controller.
    /// </summary>
    internal sealed class TradeButton : ButtonController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the trade button controller.
        /// </param>
        /// <param name="view">
        /// The view of the trade button controller.
        /// </param>
        internal TradeButton(ButtonModel model, GUIButton view)
            : base(model, view) { }

        /// <inheritdoc/>
        protected override void OnClick()
        {
            Debug.WriteLine($"{Model.Name} has been clicked.");
        }
    }
}
