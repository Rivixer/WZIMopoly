using System;
using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Start tile model.
    /// </summary>
    [Serializable]
    internal class StartTileModel : TileModel, ICrossable
    {
        /// <summary>
        /// The amount of ECTS points that the player receives
        /// after passing through the tile.
        /// </summary>
        [NonSerialized]
        private readonly int _reward;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartTileModel"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        /// <param name="reward">
        /// The amount of ECTS points that the player receives
        /// after passing through the start line.
        /// </param>
        public StartTileModel(int id, int reward) : base(id)
        {
            _reward = reward;
            OnStand += (player) => player.Money += _reward;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="StartTileModel"/> instance.
        /// </returns>
        public static StartTileModel LoadFromXml(XmlNode node)
        {
            int id = int.Parse(node.Attributes["id"].InnerText);
            int reward = int.Parse(node.SelectSingleNode("reward").InnerText);
            var tile = new StartTileModel(id, reward);
            tile.LoadNamesFromXml(node);
            return tile;
        }

        /// <inheritdoc/>
        void ICrossable.OnCross(PlayerModel player)
        {
            player.Money += _reward;
        }
    }
}
