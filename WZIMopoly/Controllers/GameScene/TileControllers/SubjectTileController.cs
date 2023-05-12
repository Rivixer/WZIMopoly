using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents a subject tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is a purchasable tile.
    /// </para>
    /// <para>
    /// In order to place grades or an exam exemptions,
    /// there is a need to buy all the tiles of a given 
    /// section. To place another element on the tile,
    /// it is required to have one on each of the tiles
    /// of that section, and so on. Scores and exemptions
    /// can be placed before player's move.
    /// </para>
    /// <para>
    /// If someone step on this tile, they have to pay
    /// a rent to the person who owns this tile.
    /// </para>
    /// <para>
    /// In case of having all of the tiles in
    /// the section, the rent is double.
    /// </para>
    /// <para>
    /// Player can decide to take a retake froman owned
    /// subject. In this case there is an obligation to
    /// sell the grades and exemption from the exam
    /// to the bank for half of the price and pawn the card
    /// in the bank for a certain amount of the ECTS points.
    /// Moreover, money from someone entering this tile
    /// is no rewarded during this stage. In the future,
    /// player can buy this card again.
    /// </para>
    /// <para>
    /// Equivalent to the
    /// <see href="https://monopoly.fandom.com/wiki/Street">'Street'</see>
    /// in Monopoly. 
    /// </para>
    /// </remarks>
    internal class SubjectTileController : TileController<SubjectTileModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectTileController"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        internal SubjectTileController(SubjectTileModel model, GUITile view) 
            : base(model, view) { }
    }
}
