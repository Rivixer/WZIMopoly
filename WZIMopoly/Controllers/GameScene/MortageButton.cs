﻿using WZIMopoly.GUI;
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
        /// <param name="model">
        /// The model of the mortgage button controller.
        /// </param>
        /// <param name="view">
        /// The view of the mortgage button controller.
        /// </param>
        internal MortgageButton(ButtonModel model, GUIButton view)
            : base(model, view) { }
    }
}
