using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Start tile model.
    /// </summary>
    /// <remarks>
    /// If the player crossed that tile will receive money 
    /// which amount is specified in xml file.
    /// </remarks>
    internal class StartTileModel : TileModel, ICrossable
    {
        /// <summary>
        /// The amount of ECTS points that the player receives
        /// after passing through the tile.
        /// </summary>
        private readonly int _reward;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartTileModel"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node of the chance tile.
        /// </param>
        internal StartTileModel(XmlNode node) : base(node)
        {
            _reward = int.Parse(node.SelectSingleNode("reward").InnerText);
        }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player)
        {
            player.ReceiveMoney(_reward);
        }

        /// <inheritdoc/>
        void ICrossable.OnCross(PlayerModel player)
        {
            player.ReceiveMoney(_reward);
        }
    }
}
