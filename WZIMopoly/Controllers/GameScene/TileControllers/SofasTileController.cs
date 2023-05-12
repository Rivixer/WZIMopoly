using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents a 'Sofas' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Sofas tile is a safe tile in the game.
    /// It doesn't have a special function.
    /// </para>
    /// <para>
    /// Equivalent to the
    /// <see href="https://monopoly.fandom.com/wiki/Free_Parking">'Free Parking'</see>
    /// tile in Monopoly.
    /// </para>
    /// </remarks>
    internal class SofasTileController : TileController<SofasTileModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see  cref="SofasTileController"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        internal SofasTileController(SofasTileModel model, GUITile view) 
            : base(model, view) { }
    }
}
