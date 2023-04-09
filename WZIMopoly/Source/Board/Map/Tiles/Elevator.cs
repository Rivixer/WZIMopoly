#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Board
{
    /// <summary>
    /// Represents an 'Elevator' tile.<br/>
    /// </summary>
    class Elevator : Tile
    {
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
        public Elevator(XmlNode node) : base(node)
        {

        }
    }
}
