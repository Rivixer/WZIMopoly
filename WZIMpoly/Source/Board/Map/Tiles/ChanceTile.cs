#region Using Statements
using System.Xml;
using WindowsWZIMpoly.Source.Items;
#endregion

namespace WindowsWZIMpoly.Source.Board.Map.Tiles
{
    abstract class ChanceTile : Tile
    {
      public ChanceCard Draw()
        {
            throw new System.NotImplementedException(); 
        }
      public ChanceTile(XmlNode node) : base(node) { }

    }
}
