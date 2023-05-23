﻿using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents a 'ConditionalPass' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The player, who lands on such a tile have to pay
    /// the amount of ECTS indicated on the tile to the bank,
    /// depending on the tile on which he stood.
    /// </para>
    /// <para>
    /// Equivalent to the
    /// <see href="https://monopoly.fandom.com/wiki/Luxury_Tax">'Luxury Tax'</see>
    /// and <see href="https://monopoly.fandom.com/wiki/Income_Tax">'Income Tax'</see>
    /// tiles in Monopoly.
    /// </para>
    /// </remarks>
    internal sealed class ConditionalPassTileController : TileController<ConditionalPassTileModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalPassTileController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the Conditional Pass tile.
        /// </param>
        /// <param name="view">
        /// The view of the Conditional Pass tile.
        /// </param>
        public ConditionalPassTileController(ConditionalPassTileModel model, GUITile view) 
            : base(model, view) { }
    }
}
