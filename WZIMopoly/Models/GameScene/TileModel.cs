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
        internal readonly string EnName;

        /// <summary>
        /// The name of the tile in Polish.
        /// </summary>
        internal readonly string PlName;

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
        protected TileModel()
        {
            EnName = MapModel.XmlNode.SelectSingleNode("en_name").InnerText;
            PlName = MapModel.XmlNode.SelectSingleNode("pl_name").InnerText;
            Id = int.Parse(MapModel.XmlNode.Attributes["id"].Value);
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
