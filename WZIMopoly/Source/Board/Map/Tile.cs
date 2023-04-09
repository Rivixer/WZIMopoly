#region Using Statements
using System;
using System.Xml;
using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
#endregion

namespace WZIMopoly.Board
{

    public abstract class Tile
    {
        public readonly string EnName;
        public readonly string PlName;
        public int Id;

        protected readonly TileOrientation Orientation;
        protected Rectangle Position;

        public abstract void OnStand(Player player);

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
    }
}
