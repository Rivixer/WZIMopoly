using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a GUI element.
    /// </summary>
    public abstract class GUIElement
    {
        /// <summary>
        /// List of children of the element.
        /// </summary>
        /// <remarks>
        /// The children are drawn in the order they are added.
        /// </remarks>
        private readonly List<GUIElement> _children = new();

        /// <summary>
        /// Adds a child to the element.
        /// </summary>
        /// <param name="child">
        /// The child to be added.
        /// </param>
        protected void AddChild(GUIElement child)
        {
            _children.Add(child);
        }

        /// <summary>
        /// Inserts a child to the element at the specified index.
        /// </summary>
        /// <param name="child">
        /// The child to be added.
        /// </param>
        /// <param name="index">
        /// The index at which the child will be added.
        /// </param>
        protected void InsertChild(GUIElement child, int index)
        {
            _children.Insert(index, child);
        }

        /// <summary>
        /// Adds a child to the element before the specified type of child.
        /// </summary>
        /// <remarks>
        /// The child is added before the first child of the specified type.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the child before which the new child will be added.
        /// </typeparam>
        /// <param name="child">
        /// The child to be added.
        /// </param>
        protected void AddChildBefore<T>(GUIElement child)
            where T : GUIElement
        {
            int index = _children.FindIndex(x => x is T);
            _children.Insert(index, child);
        }

        /// <summary>
        /// Updates the element.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame.
        /// </remarks>
        internal virtual void Update()
        {
            _children.ForEach(x => x.Update());
        }

        /// <summary>
        /// Loads the content of the element.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
        internal virtual void Load(ContentManager content)
        {
            _children.ForEach(_x => _x.Load(content));
        }

        /// <summary>
        /// Draws the element.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch object used for rendering.
        /// </param>
        internal virtual void Draw(SpriteBatch spriteBatch)
        {
            _children.ForEach(x => x.Draw(spriteBatch));
        }

        /// <summary>
        /// Scales a GUI element for the current screen resolution.<br/>
        /// </summary>
        internal virtual void Recalculate()
        {
            _children.ForEach(x => x.Recalculate());
        }
    }
}
