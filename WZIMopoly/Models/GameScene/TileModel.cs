using System;
using System.Collections.Generic;
using System.Xml;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a base class for a tile model.
    /// </summary>
    [Serializable]
    internal abstract class TileModel : Model, IComparable<TileModel>
    {
        #region Fields
        /// <summary>
        /// The name of the tile in English.
        /// </summary>
        [NonSerialized]
        internal string EnName;

        /// <summary>
        /// The name of the tile in Polish.
        /// </summary>
        [NonSerialized]
        internal string PlName;

        /// <summary>
        /// The id of the tile.
        /// </summary>

        internal readonly int Id;

        ///<summary>
        ///The list of players.
        ///</summary>
        public readonly List<PlayerModel> Players = new();
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
        /// The delegate for the <see cref="OnStand"/> event.
        /// </summary>
        /// <param name="player">
        /// The player that stands on the tile.
        /// </param>
        public delegate void OnStandHandler(PlayerModel player);

        /// <summary>
        /// The event that is invoked when a player stands on the tile.
        /// </summary>
        [field: NonSerialized]
        public event OnStandHandler OnStand;

        /// <summary>
        /// Compares the current instance with another one
        /// and returns an integer that indicates whether the current instance
        /// has a greater <see cref="Id"/> than the other one.
        /// </summary>
        /// <param name="other">
        /// The other instance of the <see cref="TileModel"/> class.
        /// </param>
        /// <returns>
        /// More than zero if the current instance has
        /// a greater <see cref="Id"/> than the other one,<br/>
        /// less than zero if the current instance has
        /// a smaller <see cref="Id"/> than the other one,<br/>
        /// equal to zero if the current instance has
        /// the same <see cref="Id"/> as the other one.
        /// </returns>
        public int CompareTo(TileModel other)
        {
            return Id - other.Id;
        }

        /// <summary>
        /// Gets all the tiles.
        /// </summary>
        public List<TileModel> AllTiles { get; private set; }

        /// <summary>
        /// Activates <see cref="OnStand"/> event.
        /// </summary>
        /// <param name="player">
        /// The player that stands on the tile.
        /// </param>
        public void OnPlayerStand(PlayerModel player)
        {
            OnStand?.Invoke(player);
        }

        /// <summary>
        /// Gives this class access to all tiles.
        /// </summary>
        /// <param name="tiles">
        /// The list of all tiles.
        /// </param>
        public void SetAllTiles(List<TileModel> tiles)
        {
            AllTiles = tiles;
        }

        /// <summary>
        /// Updates the model based on the data from the other model.
        /// </summary>
        /// <param name="model">
        /// The model to update from.
        /// </param>
        /// <remarks>
        /// Sets the players list to the one from the other model.
        /// </remarks>
        public virtual void Update(TileModel model)
        {
            Players.Clear();
            Players.AddRange(model.Players);
        }
    }
}
