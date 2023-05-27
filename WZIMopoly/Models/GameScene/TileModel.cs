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
        internal string EnName;

        /// <summary>
        /// The name of the tile in Polish.
        /// </summary>
        internal string PlName;

        /// <summary>
        /// The id of the tile.
        /// </summary>
        internal readonly int Id;

        ///<summary>
        ///The list of players.
        ///</summary>
        internal readonly List<PlayerModel> Players = new();
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TileModel"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        protected TileModel(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="TileModel"/> instance.
        /// </returns>

        /// <summary>
        /// Loads the names from the xml node.
        /// </summary>
        /// <param name="node">
        /// The node to load names from.
        /// </param>
        protected void LoadNamesFromXml(XmlNode node)
        {
            EnName = node.SelectSingleNode("en_name").InnerText;
            PlName = node.SelectSingleNode("pl_name").InnerText;
        }

        /// <summary>
        /// The action that should be performed when the player lands on the tile.
        /// </summary>
        /// <param name="player">
        /// The player that landed on the tile.
        /// </param>
        internal abstract void OnStand(PlayerModel player);
    }
}
