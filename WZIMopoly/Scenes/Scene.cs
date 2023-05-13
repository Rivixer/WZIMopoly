using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Controllers;
using WZIMopoly.GUI;

namespace WZIMopoly.Scenes
{
    /// <summary>
    /// Represents a scene of the program.
    /// </summary>
    internal abstract class Scene<_M, _V> : Controller<_M, _V>, IPrimaryController
        where _M : Models.Model
        where _V : GUITexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Scene{_M, _V}"/> class.
        /// </summary>
        /// <remarks>
        /// The scene is a primary controller.
        /// </remarks>
        /// <param name="view">
        /// The view of the controller.
        /// </param>
        /// <param name="model">
        /// The model of the controller.
        /// </param>
        internal Scene(_M model, _V view) : base(model, view) { }

        #region IPrimaryController Implementation       
        /// <inheritdoc/>
        void IPrimaryController.LoadAll(ContentManager content) => LoadAll(content, this);

        /// <inheritdoc/>
        void IPrimaryController.UpdateAll() => UpdateAll(this);

        /// <inheritdoc/>
        void IPrimaryController.DrawAll(SpriteBatch spriteBatch) => DrawAll(spriteBatch, this);
        
        /// <inheritdoc/>
        void IPrimaryController.RecalculateAll() => RecalculateAll(this);
        #endregion

        #region Private Static Methods
        /// <summary>
        /// Loads the content for the controller and all its children.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
        /// <param name="controller">
        /// The controller whose content is to be loaded.
        /// </param>
        private static void LoadAll(ContentManager content, IControllerable controller)
        {
            controller.Load(content);
            controller.Children.ForEach(child => LoadAll(content, child));
        }

        /// <summary>
        /// Updates the controller and all its chlidren.
        /// </summary>
        /// <param name="controller">
        /// The controller to be updated.
        /// </param>
        private static void UpdateAll(IControllerable controller)
        {
            controller.Update();
            controller.Children.ForEach(child => UpdateAll(child));
        }

        /// <summary>
        /// Draws the view of the controller.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch object used for rendering.
        /// </param>
        /// <param name="controller">
        /// The controller whose view is to be drawn.
        /// </param>
        private static void DrawAll(SpriteBatch spriteBatch, IControllerable controller)
        {
            controller.Draw(spriteBatch);
            controller.Children.ForEach(child => DrawAll(spriteBatch, child));
        }

        /// <summary>
        /// Recalculates the view of the controller.
        /// </summary>
        /// <param name="controller">
        /// The controller whose view is to be recalculated.
        /// </param>
        private static void RecalculateAll(IControllerable controller)
        {
            controller.Recalculate();
            controller.Children.ForEach(child => RecalculateAll(child));
        }
        #endregion
    }
}
