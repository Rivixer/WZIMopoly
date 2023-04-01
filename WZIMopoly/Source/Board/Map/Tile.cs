#region Using Statements
using System.Xml;
using Microsoft.Xna.Framework;
#endregion

namespace WZIMopoly.Source.Board.Map
{
    /// <summary>
    /// Represent tile orientation
    /// </summary>
    public enum TileOrientation
    {
        Vertical,
        Horizontal,
        Square
    }
    public abstract class Tile
    {
        public readonly string EnName;
        public readonly string PlName;
        public int Id;
        protected readonly TileOrientation TileOrientation;
        protected Rectangle Position;
        public abstract void OnStand(Player player);
        protected Tile(XmlNode node) { }
    }
}
