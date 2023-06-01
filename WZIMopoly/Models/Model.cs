using System;
using System.Collections.Generic;
using System.Reflection;
using WZIMopoly.Controllers;
using WZIMopoly.GUI;

namespace WZIMopoly.Models
{
    /// <summary>
    /// Represents a base class for models.
    /// </summary>
    /// <remarks>
    /// Contains methods for managing children.
    /// </remarks>
    internal abstract partial class Model : IModelable
    {
        /// <summary>
        /// The list of children.
        /// </summary>
        private readonly List<IControllerable> _children = new();

        /// <inheritdoc/>
        public List<IControllerable> Children => new(_children);

        /// <inheritdoc/>
        public virtual void BeforeUpdate() { }

        /// <inheritdoc/>
        public virtual void Update() { }

        /// <inheritdoc/>
        public virtual void AfterUpdate() { }

        /// <inheritdoc/>
        public void AddChild(IControllerable child)
        {
            _children.Add(child);
        }

        /// <inheritdoc/>
        public C InitializeChild<M, V, C>(params object[] modelArgs)
            where M : class, IModelable
            where V : class, IGUIable
            where C : class, IControllerable
        {
            M model = InitializeObject<M>(modelArgs);
            V view;
            try
            {
                view = InitializeObject<V>();
            }
            catch (MissingMethodException)
            {
                view = InitializeObject<V>(model);
            }
            C controller = InitializeObject<C>(model, view);

            AddChild(controller);
            return controller;
        }

        /// <inheritdoc/>
        public C InitializeChild<M, V, C>(object[] modelArgs, object[] viewArgs)
            where M : class, IModelable
            where V : class, IGUIable
            where C : class, IControllerable
        {
            M model = InitializeObject<M>(modelArgs);
            V view = InitializeObject<V>(viewArgs);
            C controller = InitializeObject<C>(model, view);
            AddChild(controller);
            return controller;
        }

        /// <summary>
        /// Initializes an object of type T with the given arguments.
        /// </summary>
        /// <typeparam name="T">
        /// The type of object to initialize.
        /// </typeparam>
        /// <param name="args">
        /// The arguments to pass to the constructor.
        /// </param>
        /// <returns>
        /// The initialized object.
        /// </returns>
        private static T InitializeObject<T>(params object[] args)
            where T : class
        {
            // TODO: Remove try-catch after changing all constructors to public.
            try
            {
                return args.Length == 0
                ? (T)Activator.CreateInstance(typeof(T), nonPublic: true)
                : (T)Activator.CreateInstance(
                    type: typeof(T),
                    bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                    binder: null,
                    args: args,
                    culture: null
                );
            }
            catch (MissingMethodException)
            {
                return args.Length == 0
                ? (T)Activator.CreateInstance(typeof(T))
                : (T)Activator.CreateInstance(
                    type: typeof(T),
                    bindingAttr: BindingFlags.Instance | BindingFlags.Public,
                    binder: null,
                    args: args,
                    culture: null
                );
            }
        }

        /// <inheritdoc/>
        public void AddChildBefore<T>(IControllerable child)
            where T : IControllerable
        {
            int index = _children.FindIndex(x => x is T);
            if (index == -1)
            {
                AddChild(child);
            }
            else
            {
                _children.Insert(index, child);
            }
        }
    }
}
