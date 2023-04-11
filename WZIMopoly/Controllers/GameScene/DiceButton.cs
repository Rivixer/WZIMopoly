using WZIMopoly.Models;
using WZIMopoly.GUI;
using System.Diagnostics;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a dice button controller.
    /// </summary>
    internal sealed class DiceButton : ButtonController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiceButton"/> class.
        /// </summary>
        /// <param name="view">
        /// The view of the dice button controller.
        /// </param>
        /// <param name="model">
        /// The model of the dice button controller.
        /// </param>
        internal DiceButton(GUIButton view, ButtonModel model)
            : base(view, model) { }

        /// <inheritdoc/>
        protected override void OnClick()
        {
            Debug.WriteLine($"{Model.Name} has been clicked.");
        }
    }
}
