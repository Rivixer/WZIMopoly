﻿using System;
using System.Xml;
using WZIMopoly.Controllers.GameScene;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents a base controller for a chance tile model.
    /// </summary>
    /// <remarks>
    /// A chance tile is a tile when the player lands on it, they draw a chance card.
    /// </remarks>
    internal class ChanceTileModel : TileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChanceTileModel"/> class.
        /// </summary>
        internal ChanceTileModel() : base() { }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player) { }

        /// <summary>
        /// Draws a chance card.
        /// </summary>
        /// <returns>
        /// The chance card drawn.
        /// </returns>
        internal ChanceCard Draw()
        {
            throw new NotImplementedException();
        }
    }
}
