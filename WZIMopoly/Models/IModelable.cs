using System;
using System.Collections.Generic;
using WZIMopoly.Controllers;
using WZIMopoly.GUI;

namespace WZIMopoly.Models
{
    /// <summary>
    /// Provides methods and properties for models.
    /// </summary>
    internal interface IModelable
    {
        /// <summary>
        /// Gets the list of child controllers.
        /// </summary>
        List<IControllerable> Children { get; }

        /// <summary>
        /// Updates the model before the main update loop.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame,
        /// before the <see cref="Update"/> method.
        /// </remarks>
        virtual void BeforeUpdate() { }

        /// <summary>
        /// Updates the model.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame.
        /// </remarks>
        virtual void Update() { }

        /// <summary>
        /// Updates the model after the main update loop.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame, 
        /// after the <see cref="Update"/> method.
        /// </remarks>
        virtual void AfterUpdate() { }

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
        void AddChild(IControllerable child);

        #region InitializeChild Methods
        /// <summary>
        /// Initializes a child controller and
        /// adds it to the list of children.
        /// <example>
        /// For example:
        /// <code>
        /// IControllerable controller = 
        /// InitializeChild&lt;PawnController, PawnModel, GUIPawn&gt;("Red");
        /// </code>
        /// </example>
        /// </summary>
        /// <remarks>
        /// <para>
        /// The model of the child controller is
        /// initialized by calling the constructor
        /// matching the arguments sent in
        /// <paramref name="modelArgs"/>.
        /// </para>        
        /// <para>
        /// The view of the child controller is
        /// initialized by calling the default
        /// constructor or the constructor with
        /// the model as the only argument if
        /// the default constructor is not available.
        /// </para>
        /// <para>
        /// The initialized child controller
        /// is added to the list of children.
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
        C InitializeChild<M, V, C>(params object[] modelArgs)
            where M : class, IModelable
            where V : class, IGUIable
            where C : class, IControllerable;

        /// <summary>
        /// Initializes a child controller and adds it to the list of children.
        /// <example>
        /// For example:
        /// <code>
        /// IControllerable controller = 
        /// InitializeChild&lt;ControllerType, ModelType, ViewType&gt;
        /// (new object[] { "name" }, new object[] { "static" });
        /// </code>
        /// </example>
        /// </summary>
        /// <remarks>
        /// <para>
        /// The model of the child controller is
        /// initialized by calling the constructor
        /// matching the arguments sent in
        /// <paramref name="modelArgs"/>.
        /// </para> 
        /// <para>
        /// The view of the child controller is
        /// initialized by calling the constructor
        /// matching the arguments sent in
        /// <paramref name="viewArgs"/>.
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
        C InitializeChild<M, V, C>(object[] modelArgs, object[] viewArgs)
            where M : class, IModelable
            where V : class, IGUIable
            where C : class, IControllerable;
        #endregion

        #region GetController Methods
        /// <summary>
        /// Returns the first child controller of
        /// type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child controller to retrieve.
        /// </typeparam>
        /// <returns>
        /// The first matching child controller, or
        /// <see langword="null"/> if none is found.
        /// </returns>
        /// <remarks>
        /// This method searches only the direct
        /// children of the current controller.
        /// </remarks>
        T GetController<T>()
            where T : IControllerable;

        /// <summary>
        /// Returns the first child controller of
        /// type <typeparamref name="T"/>
        /// that satisfies the specified <paramref name="condition"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child controller to retrieve.
        /// </typeparam>
        /// <param name="condition">
        /// A predicate that specifies the condition
        /// that the child controller must satisfy.
        /// </param>
        /// <returns>
        /// The first matching child controller,
        /// or <see langword="null"/> if none is found.
        /// </returns>
        /// <remarks>
        /// This method searches only the direct
        /// children of the current controller.
        /// </remarks>
        T GetController<T>(Predicate<T> condition)
            where T : IControllerable;

        /// <summary>
        /// Returns the first child controller of type
        /// <typeparamref name="T"/> by recursively
        /// searching through all descendants.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child controller to retrieve.
        /// </typeparam>
        /// <returns>
        /// The first matching child controller, or
        /// <see langword="null"/> if none is found.
        /// </returns>
        /// <remarks>
        /// This method searches through all
        /// descendants of the current controller.
        /// </remarks>
        T GetControllerRecursively<T>()
            where T : IControllerable;

        /// <summary>
        /// Returns the first child controller of type <typeparamref name="T"/>
        /// that satisfies the specified <paramref name="condition"/>
        /// by recursively searching through all descendants.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child controller to retrieve.
        /// </typeparam>
        /// <param name="condition">
        /// A predicate that specifies the condition
        /// that the child controller must satisfy.
        /// </param>
        /// <returns>
        /// The first matching child controller,
        /// or <see langword="null"/> if none is found.
        /// </returns>
        /// <remarks>
        /// This method searches through all
        /// descendants of the current controller.
        /// </remarks>
        T GetControllerRecursively<T>(Predicate<T> condition)
            where T : IControllerable;

        /// <summary>
        /// Returns a list of all child controllers
        /// of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child controllers to retrieve.
        /// </typeparam>
        /// <returns>
        /// A list of child controllers.
        /// </returns>
        /// <remarks>
        /// This method searches only the direct
        /// children of the current controller.
        /// </remarks>
        List<T> GetAllControllers<T>()
            where T : IControllerable;

        /// <summary>
        /// Returns a list of child controllers of type
        /// <typeparamref name="T"/> that satisfy
        /// the specified <paramref name="condition"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child controllers to retrieve.
        /// </typeparam>
        /// <param name="condition">
        /// A predicate that specifies the condition
        /// that the child controllers must satisfy.
        /// </param>
        /// <returns>
        /// A list of child controllers that
        /// satisfy the specified condition.
        /// </returns>
        /// <remarks>
        /// This method searches only the direct
        /// children of the current controller.
        /// </remarks>
        List<T> GetAllControllers<T>(Predicate<T> condition)
            where T : IControllerable;

        /// <summary>
        /// Returns a list of child controllers of type
        /// <typeparamref name="T"/> by recursively
        /// searching through all descendants.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child controllers to retrieve.
        /// </typeparam>
        /// <returns>
        /// A list of child controllers.
        /// </returns>
        /// <remarks>
        /// This method searches through all
        /// descendants of the current controller.
        /// </remarks>
        List<T> GetAllControllersRecursively<T>()
            where T : IControllerable;

        /// <summary>
        /// Returns a list of child controllers of type
        /// <typeparamref name="T"/> that satisfy
        /// the specified <paramref name="condition"/>
        /// by recursively searching through all descendants.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child controllers to retrieve.
        /// </typeparam>
        /// <returns>
        /// A list of child controllers
        /// that satisfy the specified condition.
        /// </returns>
        /// <remarks>
        /// This method searches through all
        /// descendants of the current controller.
        /// </remarks>
        List<T> GetAllControllersRecursively<T>(Predicate<T> condition)
            where T : IControllerable;
        #endregion

        #region GetModel Methods
        /// <summary>
        /// Returns the first child model of
        /// type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child model to retrieve.
        /// </typeparam>
        /// <returns>
        /// The first matching child model, or
        /// <see langword="null"/> if none is found.
        /// </returns>
        /// <remarks>
        /// This method searches only the direct
        /// children of the current model.
        /// </remarks>
        T GetModel<T>()
            where T : IModelable;

        /// <summary>
        /// Returns the first child model of
        /// type <typeparamref name="T"/>
        /// that satisfies the specified <paramref name="condition"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child model to retrieve.
        /// </typeparam>
        /// <param name="condition">
        /// A predicate that specifies the condition
        /// that the child model must satisfy.
        /// </param>
        /// <returns>
        /// The first matching child model,
        /// or <see langword="null"/> if none is found.
        /// </returns>
        /// <remarks>
        /// This method searches only the direct
        /// children of the current model.
        /// </remarks>
        T GetModel<T>(Predicate<T> condition)
            where T : IModelable;

        /// <summary>
        /// Returns the first child model of type
        /// <typeparamref name="T"/> by recursively
        /// searching through all descendants.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child model to retrieve.
        /// </typeparam>
        /// <returns>
        /// The first matching child model, or
        /// <see langword="null"/> if none is found.
        /// </returns>
        /// <remarks>
        /// This method searches through all
        /// descendants of the current model.
        /// </remarks>
        T GetModelRecursively<T>()
            where T : IModelable;

        /// <summary>
        /// Returns the first child model of type <typeparamref name="T"/>
        /// that satisfies the specified <paramref name="condition"/>
        /// by recursively searching through all descendants.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child model to retrieve.
        /// </typeparam>
        /// <param name="condition">
        /// A predicate that specifies the condition
        /// that the child model must satisfy.
        /// </param>
        /// <returns>
        /// The first matching child model,
        /// or <see langword="null"/> if none is found.
        /// </returns>
        /// <remarks>
        /// This method searches through all
        /// descendants of the current model.
        /// </remarks>
        T GetModelRecursively<T>(Predicate<T> condition)
            where T : IModelable;

        /// <summary>
        /// Returns a list of all child models
        /// of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child models to retrieve.
        /// </typeparam>
        /// <returns>
        /// A list of child models.
        /// </returns>
        /// <remarks>
        /// This method searches only the direct
        /// children of the current controller.
        /// </remarks>
        List<T> GetAllModels<T>()
            where T : IModelable;

        /// <summary>
        /// Returns a list of child models of type
        /// <typeparamref name="T"/> that satisfy
        /// the specified <paramref name="condition"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child models to retrieve.
        /// </typeparam>
        /// <param name="condition">
        /// A predicate that specifies the condition
        /// that the child models must satisfy.
        /// </param>
        /// <returns>
        /// A list of child models that
        /// satisfy the specified condition.
        /// </returns>
        /// <remarks>
        /// This method searches only the direct
        /// children of the current controller.
        /// </remarks>
        List<T> GetAllModels<T>(Predicate<T> condition)
            where T : IModelable;

        /// <summary>
        /// Returns a list of child models of type
        /// <typeparamref name="T"/> by recursively
        /// searching through all descendants.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child models to retrieve.
        /// </typeparam>
        /// <returns>
        /// A list of child models.
        /// </returns>
        /// <remarks>
        /// This method searches through all
        /// descendants of the current controller.
        /// </remarks>
        List<T> GetAllModelsRecursively<T>()
            where T : IModelable;

        /// <summary>
        /// Returns a list of child models of type
        /// <typeparamref name="T"/> that satisfy
        /// the specified <paramref name="condition"/>
        /// by recursively searching through all descendants.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child models to retrieve.
        /// </typeparam>
        /// <returns>
        /// A list of child models
        /// that satisfy the specified condition.
        /// </returns>
        /// <remarks>
        /// This method searches through all
        /// descendants of the current controller.
        /// </remarks>
        List<T> GetAllModelsRecursively<T>(Predicate<T> condition)
            where T : IModelable;
        #endregion

        #region GetView Methods
        /// <summary>
        /// Returns the first child view of
        /// type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child view to retrieve.
        /// </typeparam>
        /// <returns>
        /// The first matching child view, or
        /// <see langword="null"/> if none is found.
        /// </returns>
        /// <remarks>
        /// This method searches only the direct
        /// children of the current view.
        /// </remarks>
        T GetView<T>()
            where T : IGUIable;

        /// <summary>
        /// Returns the first child view of
        /// type <typeparamref name="T"/>
        /// that satisfies the specified <paramref name="condition"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child view to retrieve.
        /// </typeparam>
        /// <param name="condition">
        /// A predicate that specifies the condition
        /// that the child view must satisfy.
        /// </param>
        /// <returns>
        /// The first matching child view,
        /// or <see langword="null"/> if none is found.
        /// </returns>
        /// <remarks>
        /// This method searches only the direct
        /// children of the current view.
        /// </remarks>
        T GetView<T>(Predicate<T> condition)
            where T : IGUIable;

        /// <summary>
        /// Returns the first child view of type
        /// <typeparamref name="T"/> by recursively
        /// searching through all descendants.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child view to retrieve.
        /// </typeparam>
        /// <returns>
        /// The first matching child view, or
        /// <see langword="null"/> if none is found.
        /// </returns>
        /// <remarks>
        /// This method searches through all
        /// descendants of the current view.
        /// </remarks>
        T GetViewRecursively<T>()
            where T : IGUIable;

        /// <summary>
        /// Returns the first child view of type <typeparamref name="T"/>
        /// that satisfies the specified <paramref name="condition"/>
        /// by recursively searching through all descendants.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child view to retrieve.
        /// </typeparam>
        /// <param name="condition">
        /// A predicate that specifies the condition
        /// that the child view must satisfy.
        /// </param>
        /// <returns>
        /// The first matching child view,
        /// or <see langword="null"/> if none is found.
        /// </returns>
        /// <remarks>
        /// This method searches through all
        /// descendants of the current view.
        /// </remarks>
        T GetViewRecursively<T>(Predicate<T> condition)
            where T : IGUIable;

        /// <summary>
        /// Returns a list of all child views
        /// of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child views to retrieve.
        /// </typeparam>
        /// <returns>
        /// A list of child views.
        /// </returns>
        /// <remarks>
        /// This method searches only the direct
        /// children of the current controller.
        /// </remarks>
        List<T> GetAllViews<T>()
            where T : IGUIable;

        /// <summary>
        /// Returns a list of child views of type
        /// <typeparamref name="T"/> that satisfy
        /// the specified <paramref name="condition"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child views to retrieve.
        /// </typeparam>
        /// <param name="condition">
        /// A predicate that specifies the condition
        /// that the child views must satisfy.
        /// </param>
        /// <returns>
        /// A list of child views that
        /// satisfy the specified condition.
        /// </returns>
        /// <remarks>
        /// This method searches only the direct
        /// children of the current controller.
        /// </remarks>
        List<T> GetAllViews<T>(Predicate<T> condition)
            where T : IGUIable;

        /// <summary>
        /// Returns a list of child views of type
        /// <typeparamref name="T"/> by recursively
        /// searching through all descendants.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child views to retrieve.
        /// </typeparam>
        /// <returns>
        /// A list of child views.
        /// </returns>
        /// <remarks>
        /// This method searches through all
        /// descendants of the current controller.
        /// </remarks>
        List<T> GetAllViewsRecursively<T>()
            where T : IGUIable;

        /// <summary>
        /// Returns a list of child views of type
        /// <typeparamref name="T"/> that satisfy
        /// the specified <paramref name="condition"/>
        /// by recursively searching through all descendants.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child views to retrieve.
        /// </typeparam>
        /// <returns>
        /// A list of child views
        /// that satisfy the specified condition.
        /// </returns>
        /// <remarks>
        /// This method searches through all
        /// descendants of the current controller.
        /// </remarks>
        List<T> GetAllViewsRecursively<T>(Predicate<T> condition)
            where T : IGUIable;
        #endregion
    }
}
