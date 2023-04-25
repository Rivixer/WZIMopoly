using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a tile on the board.
    /// </summary>
    public abstract class Tile
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
        public int Id;

        /// <summary>
        /// The orientation of the tile.
        /// </summary>
        protected readonly TileOrientation Orientation;

        /// <summary>
        /// The position of the tile.
        /// </summary>
        protected Rectangle Position;

        ///<summary>
        ///The list of players.
        ///</summary>
        internal List<Player> Players = new();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        /// <param name="node">
        /// The xml node that contains the tile data.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The tile data is invalid.
        /// </exception>
        protected Tile(XmlNode node)
        {
            EnName = node.SelectSingleNode("en_name").InnerText;
            PlName = node.SelectSingleNode("pl_name").InnerText;
            Id = int.Parse(node.Attributes["id"].Value);

            XmlNode position = node.SelectSingleNode("position");
            if (!Enum.TryParse(position.Attributes["orientation"].Value, true, out Orientation))
            {
                throw new ArgumentException($"Invalid value of orientation attribute in position node " +
                    $"in tile node with {Id} id");
            }

            int x1 = int.Parse(position.Attributes["x1"].Value);
            int y1 = int.Parse(position.Attributes["y1"].Value);
            int width = int.Parse(position.Attributes["x2"].Value) - x1;
            int height = int.Parse(position.Attributes["y2"].Value) - y1;
            Position = new Rectangle(x1, y1, width, height);
        }

        /// <summary>
        /// The action that should be performed when the player lands on the tile.
        /// </summary>
        /// <param name="player">
        /// The player that landed on the tile.
        /// </param>
        public abstract void OnStand(Player player);

        internal List<Point> GetPawnPositions()
        {
            List<Point> positions = new();
            for (int i = 0; i < Players.Count; i++)
            {
                positions.Add(Position.Center);
            }
            return positions;
        }
    }
}
