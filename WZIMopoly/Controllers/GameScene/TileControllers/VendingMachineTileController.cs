using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents a 'Vending Machine' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When a counter lands on this tile, a card is drawn.
    /// It can have a positive or negative effect on player's
    /// turn. Sometimes this card can be saved for later
    /// (e.g. Get out of the Must-have Lecture card).
    /// </para>
    /// <para>
    /// In case of taking a card from this tile that moves counters, 
    /// money is also rewarded for crossing the start tile
    /// in the case of moving a pawn, unless it says 'go back'.
    /// </para>
    /// <para>
    /// Equivalent to the 
    /// <see href="https://monopoly.fandom.com/wiki/Chance">'Chance'</see>
    /// tile in Monopoly.
    /// </para>
    /// </remarks>
    internal sealed class VendingMachineTileController : TileController<VendingMachineTileModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VendingMachineTileController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the Vending Machine tile.
        /// </param>
        /// <param name="view">
        /// The view of the Vending Machine tile.
        /// </param>
        internal VendingMachineTileController(VendingMachineTileModel model, GUITile view) 
            : base(model, view) { }
    }
}
