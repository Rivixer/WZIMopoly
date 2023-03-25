#region Using Statements
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion


namespace WindowsWZIMpoly.GUI
{
    abstract class Screen
    {
        public abstract void Load(ContentManager content);
        public abstract void Draw(SpriteBatch spriteBatch);
        
    }
}
