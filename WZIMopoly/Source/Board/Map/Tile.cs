#region Using Statements
using System;
using System.Xml;
using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
#endregion

namespace WZIMopoly.Source.Board.Map
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
            Id = int.Parse(node.Attributes["id"].InnerText);

            XmlNode position = node.SelectSingleNode("position");
            if (!Enum.TryParse(position.Attributes["orientation"].InnerText, true, out Orientation))
            {
                throw new ArgumentException($"Invalid orientation in position node with {Id} id");
            }

            int x1 = int.Parse(position.Attributes["x1"].InnerText);
            int y1 = int.Parse(position.Attributes["y1"].InnerText);
            int width = int.Parse(position.Attributes["x2"].InnerText) - x1;
            int height = int.Parse(position.Attributes["y2"].InnerText) - y1;
            Position = new Rectangle(x1, y1, width, height);
        }
    }
}
