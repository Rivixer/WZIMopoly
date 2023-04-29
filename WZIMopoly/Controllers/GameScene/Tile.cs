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

        /// <summary>
        /// Returns the list of points where the pawns should be placed on the tile.
        /// </summary>
        /// <remarks>
        /// The points refer to the center of the pawns.
        /// </remarks>
        /// <returns>
        /// The list of points where the pawns should be placed.
        /// </returns>
        internal List<Point> GetPawnPositions()
        {
            List<Point> positions = new();
            switch (Players.Count)
            {
                case 1:
                    positions.Add(Position.Center);
                    break;
                case 2:
                    switch (Orientation)
                    {
                        case TileOrientation.Square:
                            positions.Add(new(Position.Center.X + 30, Position.Center.Y));
                            positions.Add(new(Position.Center.X - 30, Position.Center.Y));
                            break;
                        case TileOrientation.HorizontalLeft:
                            positions.Add(new(Position.Center.X + 10, Position.Center.Y));
                            positions.Add(new(Position.Center.X - 35, Position.Center.Y));
                            break;
                        case TileOrientation.HorizontalRight:
                            positions.Add(new(Position.Center.X - 10, Position.Center.Y));
                            positions.Add(new(Position.Center.X + 35, Position.Center.Y));
                            break;
                        case TileOrientation.Vertical:
                            positions.Add(new(Position.Center.X, Position.Center.Y - 10));
                            positions.Add(new(Position.Center.X, Position.Center.Y + 35));
                            break;
                    }
                    break;
                case 3:
                    switch (Orientation)
                    {
                        case TileOrientation.Square:
                            positions.Add(new(Position.Center.X + 30, Position.Center.Y - 25));
                            positions.Add(new(Position.Center.X - 30, Position.Center.Y - 25));
                            positions.Add(new(Position.Center.X, Position.Center.Y + 25));
                            break;
                        case TileOrientation.HorizontalLeft:
                            positions.Add(new(Position.Center.X + 10, Position.Center.Y - 15));
                            positions.Add(new(Position.Center.X - 35, Position.Center.Y - 15));
                            positions.Add(new(Position.Center.X - 13, Position.Center.Y + 15));
                            break;
                        case TileOrientation.HorizontalRight:
                            positions.Add(new(Position.Center.X - 10, Position.Center.Y + 15));
                            positions.Add(new(Position.Center.X + 35, Position.Center.Y + 15));
                            positions.Add(new(Position.Center.X + 13, Position.Center.Y - 15));
                            break;
                        case TileOrientation.Vertical:
                            positions.Add(new(Position.Center.X - 15, Position.Center.Y - 10));
                            positions.Add(new(Position.Center.X - 15, Position.Center.Y + 35));
                            positions.Add(new(Position.Center.X + 15, Position.Center.Y + 13));
                            break;
                    }
                    break;
                case 4:
                    switch (Orientation)
                    {
                        case TileOrientation.Square:
                            positions.Add(new(Position.Center.X - 30, Position.Center.Y - 30));
                            positions.Add(new(Position.Center.X + 30, Position.Center.Y + 30));
                            positions.Add(new(Position.Center.X - 30, Position.Center.Y + 30));
                            positions.Add(new(Position.Center.X + 30, Position.Center.Y - 30));
                            break;
                        case TileOrientation.HorizontalLeft:
                            positions.Add(new(Position.Center.X + 2, Position.Center.Y - 18));
                            positions.Add(new(Position.Center.X - 33, Position.Center.Y - 18));
                            positions.Add(new(Position.Center.X - 33, Position.Center.Y + 18));
                            positions.Add(new(Position.Center.X + 2, Position.Center.Y + 18));
                            break;
                        case TileOrientation.HorizontalRight:
                            positions.Add(new(Position.Center.X - 2, Position.Center.Y + 18));
                            positions.Add(new(Position.Center.X + 33, Position.Center.Y + 18));
                            positions.Add(new(Position.Center.X + 33, Position.Center.Y - 18));
                            positions.Add(new(Position.Center.X - 2, Position.Center.Y - 18));
                            break;
                        case TileOrientation.Vertical:
                            positions.Add(new(Position.Center.X - 18, Position.Center.Y - 2));
                            positions.Add(new(Position.Center.X - 18, Position.Center.Y + 33));
                            positions.Add(new(Position.Center.X + 18, Position.Center.Y + 33));
                            positions.Add(new(Position.Center.X + 18, Position.Center.Y - 2));
                            break;
                    }
                    break;
            }
            return positions;
        }
    }
}
