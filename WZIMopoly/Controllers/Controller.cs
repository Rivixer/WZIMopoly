using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Controllers;
using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly
{
    /// <summary>
    /// Represents a controller in MVC pattern.
    /// </summary>
    /// <remakrs>
    /// Each controller has a model and a view.
    /// </remakrs>
    /// <typeparam name="_M">
    /// The type of the model.
    /// </typeparam>
    /// <typeparam name="_V">
    /// The type of the view.
    /// </typeparam>
    internal abstract class Controller<_M, _V> : IControllerable<_M, _V>
        where _M : IModelable
        where _V : IGUIable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="view">
        /// The view of the controller.
        /// </param>
        /// <param name="model">
        /// The model of the controller.
        /// </param>
        protected Controller(_M model, _V view)
        {
            Model = model;
            View = view;
        }

        /// <inheritdoc/>
        public _M Model { get; private set; }

        /// <inheritdoc/>
        public _V View { get; private set; }

        /// <inheritdoc/>
        public virtual void Load(ContentManager content)
        {
            View.Load(content);
        }

        /// <inheritdoc/>
        public virtual void BeforeUpdate()
        {
            Model.BeforeUpdate();
            View.BeforeUpdate();
        }

        /// <inheritdoc/>
        public virtual void Update() 
        {
            Model.Update();
            View.Update();
        }

        /// <inheritdoc/>
        public virtual void AfterUpdate()
        {
            Model.AfterUpdate();
            View.AfterUpdate();
        }

        /// <inheritdoc/>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            View.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        public virtual void Recalculate()
        {
            View.Recalculate();
        }
    }
}
