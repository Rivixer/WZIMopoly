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
    internal abstract class Controller
    {
        #region Fields
        /// <summary>
        /// The list of children of the controller.
        /// </summary>
        public List<Controller> Children;

        /// <summary>
        /// Whether the controller is a primary one and has no parents.
        /// </summary>
        /// <value>
        /// If true, it is possible to call <see cref="RecalculateAll()"/> method.
        /// </value>
        private readonly bool _isPrimary;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or privately sets the view of the controller.
        /// </summary>
        internal GUIElement View { get; private set; }

        /// <summary>
        /// Gets or privately sets the model of the controller.
        /// </summary>
        internal Model Model { get; private set; }
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
        /// <param name="isPrimary">
        /// Should be true if the controller has no parents.
        /// </param>
        protected Controller(GUIElement view, Model model, bool isPrimary)
            : this(view, model, isPrimary, new List<Controller>()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="view">
        /// The view of the controller.
        /// </param>
        /// <param name="model">
        /// The model of the controller.
        /// </param>
        /// <param name="isPrimary">
        /// Should be true if the controller has no parents.
        /// </param>
        /// <param name="children">
        /// The list of children.
        /// </param>
        protected Controller(GUIElement view, Model model, bool isPrimary, List<Controller> children)
        {
            View = view;
            Model = model;
            Children = children;
            _isPrimary = isPrimary;
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
        private static void LoadAll(ContentManager content, Controller controller)
        {
            controller.Load(content);
            controller.Children.ForEach(child => LoadAll(content, child));
        }

        /// <summary>
        /// Loads the content for this controller and all its children.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
        /// <exception cref="NotPrimaryException">
        /// Thrown if the controller is not primary.
        /// </exception>
        internal void LoadAll(ContentManager content)
        {
            if (!_isPrimary)
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
        protected virtual void Update() { }

        /// <summary>
        /// Updates the controller and all its chlidren.
        /// </summary>
        /// <param name="controller">
        /// The controller to be updated.
        /// </param>
        private static void UpdateAll(Controller controller)
        {
            controller.Update();
            controller.Children.ForEach(child => UpdateAll(child));
        }

        /// <summary>
        /// Updates this controller and all its children.
        /// </summary>
        /// <exception cref="NotPrimaryException">
        /// Thrown if the controller is not primary.
        /// </exception>
        internal void UpdateAll()
        {
            if (!_isPrimary)
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
        private static void DrawAll(SpriteBatch spriteBatch, Controller controller)
        {
            controller.Draw(spriteBatch);
            controller.Children.ForEach(child => DrawAll(spriteBatch, child));
        }

        /// <summary>
        /// Draws the view of this controller and all its children.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch object used for rendering.
        /// </param>
        /// <exception cref="NotPrimaryException">
        /// Thrown if the controller is not primary.
        /// </exception>
        internal void DrawAll(SpriteBatch spriteBatch)
        {
            if (!_isPrimary)
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
        private static void RecalculateAll(Controller controller)
        {
            controller.Recalculate();
            controller.Children.ForEach(child => RecalculateAll(child));
        }

        /// <summary>
        /// Recalculates the view of this controller and all its children.
        /// </summary>
        /// <exception cref="NotPrimaryException">
        /// Thrown if the controller is not primary.
        /// </exception>
        internal void RecalculateAll()
        {
            if (!_isPrimary)
            {
                throw new NotPrimaryException(
                    "Controller must be primary to recalculate all children.");
            }
            RecalculateAll(this);
        }
        #endregion
    }
}


