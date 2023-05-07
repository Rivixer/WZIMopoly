#region Using Statements
using System.Xml;
using WZIMopoly.Models;
#endregion

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a 'Start' tile. <br/>
    /// The tile from which the game is started. <br/>
    /// </summary>
    /// <remarks>
    /// <para>
    /// After each circuit, each player receives the amount of ECTS indicated on the tile after passing through the 'Start' tile.<br/>
    /// </para>
    /// <para>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Go">'Go'</see> in Monopoly.
    /// </para>
    /// </remarks>
    class Start : Tile, ICrossable
    {
        private readonly int _reward;
        /// <summary>
        /// Initializes a new instance of the <see  cref="Start"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        public Start(XmlNode node) : base(node)
        {
            _reward = int.Parse(node.SelectSingleNode("reward").InnerText);      
        }
        public void OnCross(Player player)
        {
            throw new System.NotImplementedException();
        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
