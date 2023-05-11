using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents a 'Start' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The tile from which the game is started.
    /// </para>
    /// <para>
    /// After each circuit, each player receives
    /// the amount of ECTS indicated on the tile
    /// after passing through the 'Start' tile.
    /// </para>
    /// <para>
    /// Equivalent to the
    /// <see href="https://monopoly.fandom.com/wiki/Go">'Go'</see>
    /// in Monopoly.
    /// </para>
    /// </remarks>
    internal class StartTileController : TileController, ICrossable
    {
        /// <summary>
        /// The amount of ECTS points that the player receives
        /// after passing through the tile.
        /// </summary>
        private readonly int _reward;

        /// <summary>
        /// Initializes a new instance of the <see  cref="StartTileController"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        internal StartTileController(XmlNode node) : base(node)
        {
            _reward = int.Parse(node.SelectSingleNode("reward").InnerText);      
        }

        /// <inheritdoc/>
        void ICrossable.OnCross(PlayerModel player)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player)
        {
            throw new System.NotImplementedException();
        }
        
    }
}
