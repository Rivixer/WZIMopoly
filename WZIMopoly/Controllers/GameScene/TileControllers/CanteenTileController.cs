using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary> 
    /// Represents a 'Canteen' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When a counter lands on this tile, a card is drawn.
    /// It can have a positive or negative effect on player's turn.
    /// </para>
    /// <para>
    /// Sometimes this card can be saved for later (e.g. Get out of the Must-have Lecture card).<br/>
    /// In case of taking a card from the 'Canteen' tile that moves counters,
    /// money is also rewarded for crossing the start tile in the case of moving a pawn, 
    /// unless it says 'go back'.
    /// </para>
    /// <para>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Chance">'Chance'</see> tile in Monopoly. 
    /// </para>
    /// </remarks>
    internal sealed class CanteenTileController : TileController<CanteenTileModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanteenTileController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the Canteen tile.
        /// </param>
        /// <param name="view">
        /// The view of the Canteen tile.
        /// </param>
        internal CanteenTileController(CanteenTileModel model, GUITile view) 
            : base(model, view) { }
    }
}
