using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using WZIMopoly.Exceptions;
using WZIMopoly.GUI;


namespace WZIMopoly
{
    // Inside because otherwise it gets
    // Model from Microsoft.Xna.Framework.Graphics
    using Models;

    /// <summary>
    /// Represents a controller in MVC pattern.
    /// </summary>
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
        /// <param name="child">
        /// The controller to be added.
        /// </param>
        protected void AddChild<M, V>(Controller<M, V> child)
            where M : Models.Model
            where V : GUIElement
        {
            _children.Add(child);
        }

        internal C InitializeChild<M, V, C>(object[] modelArgs = null, object[] viewArgs = null)
            where M : Models.Model
            where V : GUIElement
            where C : Controller<M, V>
        {
            M model;
            V view;
            if (modelArgs is null)
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

            if (viewArgs is null)
            {
                view = (V)Activator.CreateInstance(typeof(V), nonPublic: true);
            }
            else
            {
                view = (V)Activator.CreateInstance(
                    type: typeof(V),
                    bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                    binder: null,
                    args: viewArgs,
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

        internal C GetController<C>()
        {
            foreach(var child in _children)
            {
                if (child is C result)
                {
                    return result;
                }
            }
            return default;
        }
        #endregion

        #region Load Methods
        /// <summary>
        /// Loads the content for this controller.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
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
        /// <summary>
        /// Updates this controller.
        /// </summary>
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
        /// <summary>
        /// Draws the view of this controller.
        /// </summary>
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
        /// <summary>
        /// Recalculates the view of this controller.
        /// </summary>
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
    }
}


