#region Using Statements
using Microsoft.Xna.Framework.Content;
#endregion

namespace WZIMopoly.GUI
{
    internal interface IGUILoadable
    {
        /// <summary>
        /// Loads the content of the screen.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
        public abstract void Load(ContentManager content);
    }
}
