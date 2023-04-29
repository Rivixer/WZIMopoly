using System.Diagnostics;
using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a mortgage button controller.
    /// </summary>
    internal class MortgageButton : ButtonController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MortgageButton"/> class.
        /// </summary>
        /// <param name="view">
        /// The view of the mortgage button controller.
        /// </param>
        /// <param name="model">
        /// The model of the mortgage button controller.
        /// </param>
        internal MortgageButton(GUIButton view, ButtonModel model)
            : base(view, model) { }

        /// <inheritdoc/>
        protected override void OnClick()
        {
            Debug.WriteLine($"{Model.Name} has been clicked.");
        }
    }
}
