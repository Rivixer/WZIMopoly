#region Using Statements
using System.Xml;
using WZIMopoly.Models;
#endregion

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a 'Deanery' tile.<br/>
    /// </summary>
    /// <para>
    /// After stopping on this tile, the player enters a <see cref="Jail"/>.<br/>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Go_to_Jail_(space)">
    /// 'Go To Jail'</see> tile in Monopoly.
    /// </para>
    
    class Deanery : Tile
    {
        public Deanery(XmlNode node) : base(node)
        {
        /// <summary>
        /// Initializes a new instance of the <see cref="Deanery"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
