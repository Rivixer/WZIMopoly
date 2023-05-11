using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a base class for a tile controller.
    /// </summary>
    internal abstract class TileController<M> : Controller<M, GUITile>
        where M : TileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TileController{M}"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the tile.
        /// </param>
        /// <param name="view">
        /// The view of the tile.
        /// </param>
        protected TileController(M model, GUITile view)
            : base(model, view) { }
    }
}
