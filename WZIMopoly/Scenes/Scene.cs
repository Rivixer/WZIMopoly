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
        where _V : GUIElement
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

        /// <summary>
        /// Initializes the scene.
        /// </summary>
        /// <remarks>
        /// This method is called once when the application starts.
        /// </remarks>
        public abstract void Initialize();

        #region IPrimaryController Implementation
        /// <inheritdoc/>
        public virtual void LoadAll(ContentManager content) => LoadAll(content, this);

        /// <inheritdoc/>
        public virtual void BeforeUpdateAll() => BeforeUpdateAll(this);

        /// <inheritdoc/>
        public virtual void UpdateAll() => UpdateAll(this);

        /// <inheritdoc/>
        public virtual void AfterUpdateAll() => AfterUpdateAll(this);

        /// <inheritdoc/>
        public virtual void DrawAll(SpriteBatch spriteBatch) => DrawAll(spriteBatch, this);

        /// <inheritdoc/>
        public virtual void RecalculateAll() => RecalculateAll(this);
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
            controller.Model.Children.ForEach(child => LoadAll(content, child));
            (controller as ICoverableScene)?.SecondScene?.LoadAll(content);
        }

        /// <summary>
        /// Updates the controller and all its chlidren
        /// before the main update.
        /// </summary>
        /// <param name="controller">
        /// The controller to be updated.
        /// </param>
        private static void BeforeUpdateAll(IControllerable controller)
        {
            controller.BeforeUpdate();
            controller.Model.Children.ForEach(child => BeforeUpdateAll(child));
            (controller as ICoverableScene)?.SecondScene?.BeforeUpdateAll();
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
            controller.Model.Children.ForEach(child => UpdateAll(child));
            (controller as ICoverableScene)?.SecondScene?.UpdateAll();
        }

        /// <summary>
        /// Updates the controller and all its chlidren.
        /// </summary>
        /// <param name="controller">
        /// The controller to be updated.
        /// </param>
        private static void AfterUpdateAll(IControllerable controller)
        {
            controller.AfterUpdate();
            controller.Model.Children.ForEach(child => AfterUpdateAll(child));
            (controller as ICoverableScene)?.SecondScene?.AfterUpdateAll();
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
            controller.Model.Children.ForEach(child => DrawAll(spriteBatch, child));
            (controller as ICoverableScene)?.SecondScene?.DrawAll(spriteBatch);
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
            controller.Model.Children.ForEach(child => RecalculateAll(child));
            (controller as ICoverableScene)?.SecondScene?.RecalculateAll();
        }
        #endregion
    }
}
