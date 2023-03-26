#region Using Statements
using System.Xml;
#endregion

namespace WindowsWZIMpoly.Source.Board.Map.Tiles
{
    class Elevator: Tile
    {
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
        public Elevator(XmlNode node):base(node)
        {

        }
    }
}
