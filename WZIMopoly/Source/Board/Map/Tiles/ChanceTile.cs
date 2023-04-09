#region Using Statements
using System.Xml;
using WZIMopoly;
#endregion

namespace WZIMopoly.Board
{
    /// <summary>
    /// The base class for the <see href="https://monopoly.fandom.com/wiki/Chance">'Chance'</see> in Monopoly.<br/>
    /// The class from which <see cref="VendingMachine"/> and <see cref="Canteen"/> classes inherits. <br/>
    /// </summary>
    abstract class ChanceTile : Tile
    {
      public ChanceCard Draw()
        {
            throw new System.NotImplementedException(); 
        }
      public ChanceTile(XmlNode node) : base(node) { }

    }
}
