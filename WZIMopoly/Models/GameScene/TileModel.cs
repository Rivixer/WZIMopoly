using System.Collections.Generic;
using System.Xml;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a base class for a tile model.
    /// </summary>
    internal abstract class TileModel : Model
    {
        #region Fields
        /// <summary>
        /// The name of the tile in English.
        /// </summary>
        public readonly string EnName;

        /// <summary>
        /// The name of the tile in Polish.
        /// </summary>
        public readonly string PlName;

        /// <summary>
        /// The id of the tile.
        /// </summary>
        public readonly int Id;

        ///<summary>
        ///The list of players.
        ///</summary>
        public readonly List<PlayerModel> Players = new();
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TileModel"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node that contains the tile data.
        /// </param>
        protected TileModel(XmlNode node)
        {
            EnName = node.SelectSingleNode("en_name").InnerText;
            PlName = node.SelectSingleNode("pl_name").InnerText;
            Id = int.Parse(node.Attributes["id"].Value);
        }

        /// <summary>
        /// The action that should be performed when the player lands on the tile.
        /// </summary>
        /// <param name="player">
        /// The player that landed on the tile.
        /// </param>
        public abstract void OnStand(PlayerModel player);
    }
}
