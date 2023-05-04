using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a 'Sofas' tile. <br/>
    /// Sofas tile is a safe tile in the game. It doesn't have a special function.<br/>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Free_Parking">'Free Parking'</see> tile in Monopoly.
    /// </summary>
    internal class Sofas : Tile
    {
        internal Sofas(XmlNode node) : base(node) { }

        internal override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
