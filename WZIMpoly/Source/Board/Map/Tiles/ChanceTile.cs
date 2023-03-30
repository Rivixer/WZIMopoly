#region Using Statements
using System.Xml;
using WZIMpoly.Source.Items;
#endregion

namespace WZIMpoly.Source.Board.Map.Tiles
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
