using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a view of the tile.
    /// </summary>
    internal class GUITile : GUIElement
    {
        /// <summary>
        /// The model of the tile.
        /// </summary>
        private readonly TileModel _model;

        /// <summary>
        /// The orientation of the tile.
        /// </summary>
        private readonly TileOrientation _orientation;

        /// <summary>
        /// The position of the tile.
        /// </summary>
        private readonly Rectangle _position;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUITile"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node that contains the tile data.
        /// </param>
        /// <param name="model">
        /// The model of the tile.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The XML tile data is invalid.
        /// </exception>
        public GUITile(XmlNode node, TileModel model)
        {
            _model = model;

            XmlNode position = node.SelectSingleNode("position");
            if (!Enum.TryParse(position.Attributes["orientation"].Value, true, out _orientation))
            {
                throw new ArgumentException($"Invalid value of orientation attribute in position node " +
                    $"in tile node with {_model.Id} id");
            }

            int x1 = int.Parse(position.Attributes["x1"].Value);
            int y1 = int.Parse(position.Attributes["y1"].Value);
            int width = int.Parse(position.Attributes["x2"].Value) - x1;
            int height = int.Parse(position.Attributes["y2"].Value) - y1;
            _position = new Rectangle(x1, y1, width, height);
        }

        /// <summary>
        /// Returns the list of points where the pawns should be placed on the tile.
        /// </summary>
        /// <remarks>
        /// The points refer to the center of the pawns.
        /// </remarks>
        /// <returns>
        /// The list of points where the pawns should be placed.
        /// </returns>
        public List<Point> GetPawnPositions()
        {
            List<Point> positions = new();
            switch (_model.Players.Count)
            {
                case 1:
                    positions.Add(_position.Center);
                    break;
                case 2:
                    switch (_orientation)
                    {
                        case TileOrientation.Square:
                            positions.Add(new(_position.Center.X + 30, _position.Center.Y));
                            positions.Add(new(_position.Center.X - 30, _position.Center.Y));
                            break;
                        case TileOrientation.HorizontalLeft:
                            positions.Add(new(_position.Center.X + 10, _position.Center.Y));
                            positions.Add(new(_position.Center.X - 35, _position.Center.Y));
                            break;
                        case TileOrientation.HorizontalRight:
                            positions.Add(new(_position.Center.X - 10, _position.Center.Y));
                            positions.Add(new(_position.Center.X + 35, _position.Center.Y));
                            break;
                        case TileOrientation.Vertical:
                            positions.Add(new(_position.Center.X, _position.Center.Y - 10));
                            positions.Add(new(_position.Center.X, _position.Center.Y + 35));
                            break;
                    }
                    break;
                case 3:
                    switch (_orientation)
                    {
                        case TileOrientation.Square:
                            positions.Add(new(_position.Center.X + 30, _position.Center.Y - 25));
                            positions.Add(new(_position.Center.X - 30, _position.Center.Y - 25));
                            positions.Add(new(_position.Center.X, _position.Center.Y + 25));
                            break;
                        case TileOrientation.HorizontalLeft:
                            positions.Add(new(_position.Center.X + 10, _position.Center.Y - 15));
                            positions.Add(new(_position.Center.X - 35, _position.Center.Y - 15));
                            positions.Add(new(_position.Center.X - 13, _position.Center.Y + 15));
                            break;
                        case TileOrientation.HorizontalRight:
                            positions.Add(new(_position.Center.X - 10, _position.Center.Y + 15));
                            positions.Add(new(_position.Center.X + 35, _position.Center.Y + 15));
                            positions.Add(new(_position.Center.X + 13, _position.Center.Y - 15));
                            break;
                        case TileOrientation.Vertical:
                            positions.Add(new(_position.Center.X - 15, _position.Center.Y - 10));
                            positions.Add(new(_position.Center.X - 15, _position.Center.Y + 35));
                            positions.Add(new(_position.Center.X + 15, _position.Center.Y + 13));
                            break;
                    }
                    break;
                case 4:
                    switch (_orientation)
                    {
                        case TileOrientation.Square:
                            positions.Add(new(_position.Center.X - 30, _position.Center.Y - 30));
                            positions.Add(new(_position.Center.X + 30, _position.Center.Y + 30));
                            positions.Add(new(_position.Center.X - 30, _position.Center.Y + 30));
                            positions.Add(new(_position.Center.X + 30, _position.Center.Y - 30));
                            break;
                        case TileOrientation.HorizontalLeft:
                            positions.Add(new(_position.Center.X + 2, _position.Center.Y - 18));
                            positions.Add(new(_position.Center.X - 33, _position.Center.Y - 18));
                            positions.Add(new(_position.Center.X - 33, _position.Center.Y + 18));
                            positions.Add(new(_position.Center.X + 2, _position.Center.Y + 18));
                            break;
                        case TileOrientation.HorizontalRight:
                            positions.Add(new(_position.Center.X - 2, _position.Center.Y + 18));
                            positions.Add(new(_position.Center.X + 33, _position.Center.Y + 18));
                            positions.Add(new(_position.Center.X + 33, _position.Center.Y - 18));
                            positions.Add(new(_position.Center.X - 2, _position.Center.Y - 18));
                            break;
                        case TileOrientation.Vertical:
                            positions.Add(new(_position.Center.X - 18, _position.Center.Y - 2));
                            positions.Add(new(_position.Center.X - 18, _position.Center.Y + 33));
                            positions.Add(new(_position.Center.X + 18, _position.Center.Y + 33));
                            positions.Add(new(_position.Center.X + 18, _position.Center.Y - 2));
                            break;
                    }
                    break;
            }
            return positions;
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch) { }

        /// <inheritdoc/>
        public override void Load(ContentManager content) { }

        /// <inheritdoc/>
        public override void Recalculate() { }
    }
}
