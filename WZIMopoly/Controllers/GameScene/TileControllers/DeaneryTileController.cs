using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents a 'Deanery' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// After stopping on this tile, the player enters a 
    /// <see cref="MandatoryLectureTileController">Jail</see>.
    /// </para>
    /// <para>
    /// Equivalent to the
    /// <see href="https://monopoly.fandom.com/wiki/Go_to_Jail_(space)">
    /// 'Go To Jail'</see> tile in Monopoly.
    /// </para>
    /// </remarks>
    internal sealed class DeaneryTileController : TileController<DeaneryTileModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeaneryTileController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the Deanery tile.
        /// </param>
        /// <param name="view">
        /// The view of the Deanery tile.
        /// </param>
        internal DeaneryTileController(DeaneryTileModel model, GUITile view) 
            : base(model, view) { }
    }
}
