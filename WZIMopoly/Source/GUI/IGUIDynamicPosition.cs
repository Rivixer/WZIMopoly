#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WZIMopoly.Source.GUI
{
    interface IGUIDynamicPosition
    {
        public abstract void UpdateDestinationRect(Vector2 newPosition);
        public abstract void UpdateDestinationRect(Rectangle newRectangle);
    }
}