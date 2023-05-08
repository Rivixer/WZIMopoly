using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a 'Mandatory Lecture' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is the tile on which player stands during the mandatory lecture.
    /// </para>
    /// <para>
    /// Player can also get on this tile with a 'Canteen' or 'VendingMachine' card.<br/>
    /// There are 3 ways to go to the must-have lecture: 
    /// landing on this tile, drawing a <see cref="Canteen"/>
    /// or <see cref="VendingMachine"/> card that leads to the 
    /// mandatory lecture or throwing a double dice 3 times in a row.
    /// </para>
    /// <para>
    /// Once player is at the lecture, 
    /// there is a chance to use the 'Get Out Of The Mandatory Lecture'
    /// card or throw a double dice which means player is free
    /// and have to move by the number of pips on the dice.
    /// </para>
    /// <para>
    /// Before the first roll and the second roll there is a possibility
    /// to pay the amount of ECTS indicated on the tile and get out of the lecture.
    /// In case of not throwing a double dice in the third round, there is an
    /// obligation to pay pay the amount of ECTS indicated on the tile to be free.
    /// </para>
    /// <para>
    /// Nothing happens, in case of stepping on this tile.
    /// </para>
    /// <para>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Jail">'Jail'</see> tile in Monopoly.
    /// </para>
    /// </remarks>
    internal class Jail : Tile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Jail"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param> 
        internal Jail(XmlNode node) : base(node) { }
        
        /// <inheritdoc/>
        internal override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
