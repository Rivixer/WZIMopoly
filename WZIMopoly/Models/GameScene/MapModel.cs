using System.Collections.Generic;
using WZIMopoly.Controllers.GameScene;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a map model.
    /// </summary>
    internal sealed class MapModel : Model
    {
        /// <summary>
        /// Gets or sets the list of tiles.
        /// </summary>
        internal List<Tile> Tiles { get; set; } = new();
    }
}
