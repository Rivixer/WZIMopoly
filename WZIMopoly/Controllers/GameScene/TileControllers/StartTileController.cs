using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents a 'Start' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The tile from which the game is started.
    /// </para>
    /// <para>
    /// After each circuit, each player receives
    /// the amount of ECTS indicated on the tile
    /// after passing through the 'Start' tile.
    /// </para>
    /// <para>
    /// Equivalent to the
    /// <see href="https://monopoly.fandom.com/wiki/Go">'Go'</see>
    /// in Monopoly.
    /// </para>
    /// </remarks>
    internal class StartTileController : TileController<StartTileModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see  cref="StartTileController"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        internal StartTileController(StartTileModel model, GUITile view) 
            : base(model, view) { }
    }
}
