using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a 'Deanery' tile.<br/>
    /// After stopping on this tile, the player enters a <see cref="Jail"/>.<br/>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Go_to_Jail_(space)">
    /// 'Go To Jail'</see> tile in Monopoly.
    /// </summary>
    internal class Deanery : Tile
    {
        internal Deanery(XmlNode node) : base(node) { }

        internal override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
