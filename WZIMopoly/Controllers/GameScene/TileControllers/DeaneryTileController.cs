using System.Xml;
using WZIMopoly.Models;

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
    internal class DeaneryTileController : TileController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeaneryTileController"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        internal DeaneryTileController(XmlNode node) : base(node) { }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player)
        {
            throw new System.NotImplementedException();
        }
    }
}
