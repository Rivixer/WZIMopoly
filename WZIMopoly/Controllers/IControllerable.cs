using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers
{
    /// <summary>
    /// Provides methods and properties for controlling
    /// the interacton between the model and the view.
    /// </summary>
    internal interface IControllerable
    {
        /// <summary>
        /// Gets the model associated with this controller.
        /// </summary>
        IModelable Model { get; }

        /// <summary>
        /// Gets the view associated with this controller.
        /// </summary>
        IGUIable View { get; }

        /// <summary>
        /// Loads the content required by the controller.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
        void Load(ContentManager content);

        /// <summary>
        /// Draws the view associated with this controller.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch used for drawing.
        /// </param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Updates the controller and its associated model and view
        /// before the main update loop.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame,
        /// before the <see cref="Update"/> method.
        /// </remarks>
        void BeforeUpdate();

        /// <summary>
        /// Updates the controller and its associated model and view.
        /// </summary>
        void Update();

        /// <summary>
        /// Updates the controller and its associated model and view
        /// after the main update loop.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame,
        /// after the <see cref="Update"/> method.
        /// </remarks>
        void AfterUpdate();

        /// <summary>
        /// Recalculates the view associated with this controller.
        /// </summary>
        void Recalculate();
    }

    /// <summary>
    /// Provides methods and generic properties for controlling
    /// the interacton between the model and the view.
    /// </summary>
    /// <typeparam name="M">
    /// The type of the model associated with the controller.
    /// </typeparam>
    /// <typeparam name="V">
    /// The type of the view associated with the controller.
    /// </typeparam>
    internal interface IControllerable<M, V> : IControllerable
        where M : IModelable
        where V : IGUIable
    {
        /// <inheritdoc/>
        IModelable IControllerable.Model => Model;

        /// <inheritdoc/>
        IGUIable IControllerable.View => View;

        /// <inheritdoc cref="IControllerable.Model"/>
        public new M Model { get; }

        /// <inheritdoc cref="IControllerable.View"/>
        public new V View { get; }        
    }
}
