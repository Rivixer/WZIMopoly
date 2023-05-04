using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a 'Start' tile. <br/>
    /// The tile from which the game is started. <br/><br/>
    /// After each circuit, each player receives the amount of ECTS indicated on the tile after passing through the 'Start' tile.<br/>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Go">'Go'</see> in Monopoly.
    /// </summary>
    internal class Start : Tile, ICrossable
    {
        private readonly int _reward;

        internal Start(XmlNode node) : base(node)
        {
            _reward = int.Parse(node.SelectSingleNode("reward").InnerText);
        }

        public void OnCross(Player player)
        {
            throw new System.NotImplementedException();
        }

        internal override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
        
    }
}
