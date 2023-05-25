using System;
using System.Collections.Generic;
using System.Xml;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a base class for a tile model.
    /// </summary>
    internal abstract class TileModel : Model, IComparable<TileModel>
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
        /// Compares the current instance with another one
        /// and returns an integer that indicates whether the current instance
        /// has a greater <see cref="Id"/> than the other one.
        /// </summary>
        /// <param name="other">
        /// The other instance of the <see cref="TileModel"/> class.
        /// </param>
        /// <returns>
        /// Less than zero if the current instance has
        /// a greater <see cref="Id"/> than the other one,<br/>
        /// more than zero if the current instance has
        /// a smaller <see cref="Id"/> than the other one,<br/>
        /// equal to zero if the current instance has
        /// the same <see cref="Id"/> as the other one.
        /// </returns>
        public int CompareTo(TileModel other)
        {
            return Id - other.Id;
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
