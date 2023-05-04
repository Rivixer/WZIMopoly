using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents an 'Elevator' tile.<br/>
    /// </summary>
    internal class Elevator : Tile
    {
        internal Elevator(XmlNode node) : base(node) { }

        internal override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }   
    }
}
