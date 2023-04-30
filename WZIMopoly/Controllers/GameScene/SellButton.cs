using System.Diagnostics;
using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a sell button controller.
    /// </summary>
    internal class SellButton : ButtonController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SellButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the sell button controller.
        /// </param>
        /// <param name="view">
        /// The view of the sell button controller.
        /// </param>
        internal SellButton(ButtonModel model, GUIButton view)
            : base(model, view) { }

        /// <inheritdoc/>
        protected override void OnClick()
        {
            Debug.WriteLine($"{Model.Name} has been clicked.");
        }
    }
}
