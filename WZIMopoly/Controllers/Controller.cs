using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using WZIMopoly.Controllers;
using WZIMopoly.Exceptions;
using WZIMopoly.GUI;
using System.Reflection;
using System.Linq;

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
        #region Fields
        /// <summary>
        /// The list of children of the controller.
        /// </summary>
        private readonly List<IControllerable> _children;
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

        /// <inheritdoc cref="_children"/>
        List<IControllerable> IControllerable.Children => _children;
        #endregion

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
            : this(model, view, new List<IControllerable>()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="view">
        /// The view of the controller.
        /// </param>
        /// <param name="model">
        /// The model of the controller.
        /// </param>
        /// <param name="children">
        /// The list of children.
        /// </param>
        protected Controller(_M model, _V view, List<IControllerable> children)
        {
            Model = model;
            View = view;
            _children = children;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a controller to the list of children.
        /// </summary>
        /// <typeparam name="M">
        /// The type of the model of the child controller.
        /// </typeparam>
        /// <typeparam name="V">
        /// The type of the view of the child controller.
        /// </typeparam>
        /// <param name="child">
        /// The controller to be added.
        /// </param>
        protected void AddChild<M, V>(Controller<M, V> child)
            where M : Models.Model
            where V : GUIElement
        {
            _children.Add(child);
        }

        /// <summary>
        /// Initializes a child controller and adds it to the list of children.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The view of the child controller is initialized by calling
        /// the default constructor or the constructor with the model
        /// as the only argument if the default constructor is not available.
        /// </para>
        /// <para>
        /// The initialized child controller is added to the list of children.
        /// </para>
        /// </remarks>
        /// <typeparam name="M">
        /// The type of the model of the child controller.
        /// </typeparam>
        /// <typeparam name="V">
        /// The type of the view of the child controller.
        /// </typeparam>
        /// <typeparam name="C">
        /// The type of the child controller.
        /// </typeparam>
        /// <param name="modelArgs">
        /// The arguments for the model constructor.
        /// </param>
        /// <returns>
        /// The initialized child controller.
        /// </returns>
        internal C InitializeChild<M, V, C>(params object[] modelArgs)
            where M : Models.Model
            where V : GUIElement
            where C : Controller<M, V>
        {
            M model;
            if (modelArgs.Length == 0)
            {
                model = (M)Activator.CreateInstance(typeof(M), nonPublic: true);
            }
            else
            {
                model = (M)Activator.CreateInstance(
                    type: typeof(M),
                    bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                    binder: null,
                    args: modelArgs,
                    culture: null
                );
            }

            V view;
            try
            {
                view = (V)Activator.CreateInstance(typeof(V), nonPublic: true);
            }
            catch (MissingMethodException)
            {
                view = (V)Activator.CreateInstance(
                type: typeof(V),
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                binder: null,
                args: new object[] { model },
                culture: null
                );
            }

            C controller = (C)Activator.CreateInstance(
                type: typeof(C),
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                binder: null,
                args: new object[] { model, view},
                culture: null
            );

            AddChild(controller);
            return controller;
        }

        /// <summary>
        /// Returns the first child controller of the specified type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child controller.
        /// </typeparam>
        /// <returns>
        /// The first child controller of the specified type if found; otherwise null.
        /// </returns>
        internal T GetController<T>()
        {
            return GetController<T>((c) => true);
        }

        /// <inheritdoc cref="GetController{C}"/>
        /// <param name="condition">
        /// A predicate used to determine whether a child controller matches the search criteria.
        /// </param>
        internal T GetController<T>(Predicate<T> condition)
        {
            return (T)_children.FirstOrDefault(c => c is T child && condition(child));
        }

        /// <summary>
        /// Returns all controllers of the specified type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child controllers.
        /// </typeparam>
        /// <returns>
        /// The list of child controllers of the specified type.
        /// </returns>
        internal List<T> GetAllControllers<T>()
        {
            return GetAllControllers<T>((c) => true);
        }

        /// <inheritdoc cref="GetAllControllers{T}"/>
        /// <param name="condition">
        /// A predicate used to determine whether a child controller matches the search criteria.
        /// </param>
        internal List<T> GetAllControllers<T>(Predicate<T> condition)
        {
            return _children.FindAll((c) => c is T result && condition(result)).Cast<T>().ToList();
        }
        #endregion

        #region Load Methods
        /// <inheritdoc cref="IControllerable.Load(ContentManager)"/>
        protected virtual void Load(ContentManager content)
        {
            View.Load(content);
        }

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

        /// <inheritdoc cref="IPrimaryController.LoadAll(ContentManager)"/>
        /// <exception cref="NotPrimaryException">
        /// Thrown if the controller is not primary.
        /// </exception>
        protected void LoadAll(ContentManager content)
        {
            if (this is not IPrimaryController)
            {
                throw new NotPrimaryException(
                    "Controller must be primary to load all children.");
            }
            LoadAll(content, this);
        }
        #endregion

        #region Update Methods
        /// <inheritdoc cref="IControllerable.Update"/>
        protected virtual void Update() 
        {
            View.Update();
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

        /// <inheritdoc cref="IPrimaryController.UpdateAll"/>
        /// <exception cref="NotPrimaryException">
        /// Thrown if the controller is not primary.
        /// </exception>
        protected void UpdateAll()
        {
            if (this is not IPrimaryController)
            {
                throw new NotPrimaryException(
                    "Controller must be primary to update all children.");
            }
            UpdateAll(this);
        }
        #endregion

        #region Draw Methods
        /// <inheritdoc cref="IControllerable.Draw(SpriteBatch)"/>
        protected virtual void Draw(SpriteBatch spriteBatch)
        {
            View.Draw(spriteBatch);
        }

        /// <summary>
        /// Draws the view of the controller.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch object used for rendering.
        /// </param>
        /// <param name="controller">
        /// The controller whose view is to be recalculated.
        /// </param>
        private static void DrawAll(SpriteBatch spriteBatch, IControllerable controller)
        { 
            controller.Draw(spriteBatch);
            controller.Children.ForEach(child => DrawAll(spriteBatch, child));
        }

        /// <inheritdoc cref="IPrimaryController.DrawAll(SpriteBatch)"/>
        /// <exception cref="NotPrimaryException">
        /// Thrown if the controller is not primary.
        /// </exception>
        protected void DrawAll(SpriteBatch spriteBatch)
        {
            if (this is not IPrimaryController)
            {
                throw new NotPrimaryException(
                    "Controller must be primary to draw all children.");
            }
            DrawAll(spriteBatch, this);
        }
        #endregion

        #region Recalculate Methods
        /// <inheritdoc cref="IControllerable.Recalculate"/>
        protected virtual void Recalculate()
        {
            View.Recalculate();
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

        /// <inheritdoc cref="IPrimaryController.RecalculateAll"/>
        /// <exception cref="NotPrimaryException">
        /// Thrown if the controller is not primary.
        /// </exception>
        protected void RecalculateAll()
        {
            if (this is not IPrimaryController)
            {
                throw new NotPrimaryException(
                    "Controller must be primary to recalculate all children.");
            }
            RecalculateAll(this);
        }
        #endregion

        #region IControllerable Methods
        /// <inheritdoc/>
        void IControllerable.Load(ContentManager content) => Load(content);

        /// <inheritdoc/>
        void IControllerable.Draw(SpriteBatch spriteBatch) => Draw(spriteBatch);

        /// <inheritdoc/>
        void IControllerable.Update() => Update();

        /// <inheritdoc/>
        void IControllerable.Recalculate() => Recalculate();
        #endregion
    }
}
