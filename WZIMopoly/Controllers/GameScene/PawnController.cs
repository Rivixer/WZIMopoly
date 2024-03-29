﻿using Microsoft.Xna.Framework;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a controller of the pawn.
    /// </summary>
    internal sealed class PawnController : Controller<PawnModel, GUIPawn>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PawnController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the pawn.
        /// </param>
        /// <param name="view">
        /// The view of the pawn.
        /// </param>
        internal PawnController(PawnModel model, GUIPawn view)
            : base(model, view) { }
    }
}
