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

        #region IPrimaryController Methods
        /// <inheritdoc/>
        void IPrimaryController.DrawAll(SpriteBatch spriteBatch) => DrawAll(spriteBatch);

        /// <inheritdoc/>
        void IPrimaryController.LoadAll(ContentManager content) => LoadAll(content);

        /// <inheritdoc/>
        void IPrimaryController.RecalculateAll() => RecalculateAll();

        /// <inheritdoc/>
        void IPrimaryController.UpdateAll() => UpdateAll();
        #endregion
    }
}


