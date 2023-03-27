#region Using Statements
using System.Xml;
#endregion

namespace WindowsWZIMpoly.Source.Board.Map.Tiles
{
    class Deanery : Tile
    {
        public Deanery(XmlNode node) : base(node)
        {

        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
