#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace WZIMopoly.GUI
{
    interface IGUIDynamicPosition
    {
        public abstract void UpdateDestinationRect(Vector2 newPosition);
        public abstract void UpdateDestinationRect(Rectangle newRectangle);
    }
}