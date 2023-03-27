#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion


namespace WindowsWZIMpoly.GUI
{
    internal abstract class GUIElement
    {
        internal abstract Texture Texture { get; }
        internal abstract Rectangle Rectangle { get; }
        internal abstract void Load(ContentManager content);
    }
}
