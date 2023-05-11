using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WZIMopoly.Controllers;
using WZIMopoly.GUI;

namespace WZIMopoly.Models
{
    /// <summary>
    /// Represents a model.
    /// </summary>
    internal abstract class Model
    {
        /// <summary>
        /// The list of children.
        /// </summary>
        private readonly List<IControllerable> _children;

        /// <summary>
        /// Gets the copy of the list of children.
        /// </summary>
        internal List<IControllerable> Children => new(_children);

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
        internal void AddChild<M, V>(Controller<M, V> child)
            where M : Model
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
            where M : Model
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
                args: new object[] { model, view },
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
    }
}
