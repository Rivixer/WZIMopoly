using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using WZIMopoly.Controllers;
using WZIMopoly.Exceptions;
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

        #region Properties
        /// <summary>
        /// Gets or privately sets the model of the controller.
        /// </summary>
        public _M Model { get; private set; }

        /// <summary>
        /// Gets or privately sets the view of the controller.
        /// </summary>
        public _V View { get; private set; }
        #endregion

        #region Load Methods
        /// <inheritdoc cref="IControllerable.Load(ContentManager)"/>
        public virtual void Load(ContentManager content)
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
            controller.Model.Children.ForEach(child => LoadAll(content, child));
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
        public virtual void Update() 
        {
            Model.Update();
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
            controller.Model.Children.ForEach(child => UpdateAll(child));
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
        public virtual void Draw(SpriteBatch spriteBatch)
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
        /// The controller whose view is to be drawn.
        /// </param>
        private static void DrawAll(SpriteBatch spriteBatch, IControllerable controller)
        { 
            controller.Draw(spriteBatch);
            controller.Model.Children.ForEach(child => DrawAll(spriteBatch, child));
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
        public virtual void Recalculate()
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
            controller.Model.Children.ForEach(child => RecalculateAll(child));
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
