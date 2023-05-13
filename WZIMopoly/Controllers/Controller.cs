using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using WZIMopoly.Controllers;
using WZIMopoly.GUI;

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
    internal abstract class Controller<_M, _V> : IControllerable
        where _M : Models.Model
        where _V : GUIElement
    {
        #region Constructors
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
        #endregion

        #region IControllerable Implementation
        /// <inheritdoc cref="Models.Model.Children"/>
        List<IControllerable> IControllerable.Children => Model.Children;

        /// <inheritdoc/>
        void IControllerable.Load(ContentManager content) => Load(content);

        /// <inheritdoc/>
        void IControllerable.Draw(SpriteBatch spriteBatch) => Draw(spriteBatch);

        /// <inheritdoc/>
        void IControllerable.Update() => Update();

        /// <inheritdoc/>
        void IControllerable.Recalculate() => Recalculate();
        #endregion

        #region Properties
        /// <summary>
        /// Gets or privately sets the model of the controller.
        /// </summary>
        internal _M Model { get; private set; }

        /// <summary>
        /// Gets or privately sets the view of the controller.
        /// </summary>
        internal _V View { get; private set; }
        #endregion

        #region Children Methods
        /// <inheritdoc cref="Models.Model.GetController{T}"/>
        protected T GetController<T>()
        {
            return Model.GetController<T>();
        }

        /// <inheritdoc cref="Models.Model.GetController{T}(Predicate{T})"/>
        protected T GetController<T>(Predicate<T> condition)
        {
            return Model.GetController(condition);
        }

        /// <inheritdoc cref="Models.Model.GetAllControllers{T}"/>
        protected List<T> GetAllControllers<T>()
        {
            return Model.GetAllControllers<T>();
        }

        /// <inheritdoc cref="Models.Model.GetAllControllers{T}(Predicate{T})"/>
        protected List<T> GetAllControllers<T>(Predicate<T> condition)
        {
            return Model.GetAllControllers(condition);
        }

        /// <inheritdoc cref="Models.Model.AddChild{M, V}(Controller{M, V})"/>
        protected void AddChild<M, V>(Controller<M, V> controller)
            where M : Models.Model
            where V : GUIElement
        {
            Model.AddChild(controller);
        }
        #endregion

        #region IControllerable Methods
        /// <inheritdoc cref="IControllerable.Load(ContentManager)"/>
        protected virtual void Load(ContentManager content)
        {
            View.Load(content);
        }

        /// <inheritdoc cref="IControllerable.Update"/>
        protected virtual void Update() 
        {
            View.Update();
        }

        /// <inheritdoc cref="IControllerable.Draw(SpriteBatch)"/>
        protected virtual void Draw(SpriteBatch spriteBatch)
        {
            View.Draw(spriteBatch);
        }

        /// <inheritdoc cref="IControllerable.Recalculate"/>
        protected virtual void Recalculate()
        {
            View.Recalculate();
        }
        #endregion
    }
}
