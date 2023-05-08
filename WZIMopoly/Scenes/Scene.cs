using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
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
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
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
        public Scene(_M model, _V view) : base(model, view) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
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
        /// <param name="children">
        /// The list of children of controller.
        /// </param>
        public Scene(_M model, _V view, List<IControllerable> children)
            : base(model, view, children) { }
        #endregion

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


