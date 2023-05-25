using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents a 'Restroom' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is a purchasable tile.
    /// </para>
    /// <para>
    /// If player steps on this field, they have to pay a rent
    /// to the person who owns this tile.
    /// </para>
    /// <para>
    /// There is a chance to pop an owned tile. It's about
    /// pawning the card in the bank for a certain amount
    /// of the ECTS points. Money from someone entering
    /// this tile is no rewarded during this stage.
    /// </para>
    /// <para>
    /// Equivalent to one of the Monopoly
    /// <see href="https://monopoly.fandom.com/wiki/Railroads">'Railroads'</see>.
    /// </para>
    /// </remarks>
    internal sealed class RestroomTileController : TileController<RestroomTileModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see  cref="RestroomTileController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the Restroom tile.
        /// </param>
        /// <param name="view">
        /// The view of the Restroom tile.
        /// </param>
        public RestroomTileController(RestroomTileModel model, GUITile view) 
            : base(model, view) { }
    }
}