using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents an 'Elevator' tile.<br/>
    /// </summary>
    class Elevator : Tile
    {
        public Elevator(XmlNode node) : base(node)
        {

        }

        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
        
    }
}
