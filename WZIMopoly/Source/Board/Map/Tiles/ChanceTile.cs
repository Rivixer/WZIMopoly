#region Using Statements
using System.Xml;
using WZIMopoly.Source.Items;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
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
