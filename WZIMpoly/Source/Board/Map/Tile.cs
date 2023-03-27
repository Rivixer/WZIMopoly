#region Using Statements
using System.Xml;
using Microsoft.Xna.Framework;
#endregion

namespace WindowsWZIMpoly.Source.Board.Map
{
    abstract class Tile
    {
        public string Name;
        public int Id;
        protected Vector2 Position;
        public abstract void OnStand(Player player);
        protected Tile(XmlNode node) { }
    }
}
