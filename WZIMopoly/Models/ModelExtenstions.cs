using System.Collections.Generic;
using System.Linq;
using System;
using WZIMopoly.Controllers;
using WZIMopoly.GUI;

namespace WZIMopoly.Models
{
    /// <summary>
    /// Extends the <see cref="Model"/> class.
    /// </summary>
    /// <remarks>
    /// Contains methods for getting children and their models and views.
    /// </remarks>
    internal abstract partial class Model : IModelable
    {
        #region Get Controller Methods
        /// <inheritdoc/>
        public T GetController<T>()
            where T : IControllerable => GetController<T>((x) => true);

        /// <inheritdoc/>
        public T GetController<T>(Predicate<T> condition)
            where T : IControllerable
        {
            return (T)_children.FirstOrDefault(x => x is T controller && condition(controller));
        }

        /// <inheritdoc/>
        public T GetControllerRecursively<T>()
            where T : IControllerable => GetControllerRecursively<T>((x) => true);

        /// <inheritdoc/>
        public T GetControllerRecursively<T>(Predicate<T> condition)
            where T : IControllerable
        {
            T result = GetController(condition);
            foreach (var child in Children)
            {
                if (result != null)
                {
                    break;
                }
                result = child.Model.GetControllerRecursively(condition);
            }
            return result;
        }

        /// <inheritdoc/> 
        public List<T> GetAllControllers<T>()
            where T : IControllerable => GetAllControllers<T>((x) => true);

        /// <inheritdoc/>
        public List<T> GetAllControllers<T>(Predicate<T> condition)
            where T : IControllerable
        {
            return _children.Where(x => x is T controller && condition(controller)).Cast<T>().ToList();
        }

        /// <inheritdoc/>
        public List<T> GetAllControllersRecursively<T>()
            where T : IControllerable => GetAllControllersRecursively<T>((x) => true);

        /// <inheritdoc/>
        public List<T> GetAllControllersRecursively<T>(Predicate<T> condition)
            where T : IControllerable
        {
            List<T> result = GetAllControllers(condition);
            Children.ForEach(x => result.AddRange(x.Model.GetAllControllersRecursively(condition)));
            return result;
        }
        #endregion

        #region Get Model Methods
        /// <inheritdoc/>
        public T GetModel<T>()
            where T : IModelable => GetModel<T>((x) => true);

        /// <inheritdoc/>
        public T GetModel<T>(Predicate<T> condition)
            where T : IModelable
        {
            return (T)_children.FirstOrDefault(x => x.Model is T model && condition(model))?.Model;
        }

        /// <inheritdoc/>
        public T GetModelRecursively<T>()
            where T : IModelable => GetModelRecursively<T>((x) => true);

        /// <inheritdoc/>
        public T GetModelRecursively<T>(Predicate<T> condition)
            where T : IModelable
        {
            T result = GetModel(condition);
            foreach (var child in Children)
            {
                if (result != null)
                {
                    break;
                }
                result = child.Model.GetModelRecursively(condition);
            }
            return result;
        }

        /// <inheritdoc/> 
        public List<T> GetAllModels<T>()
            where T : IModelable => GetAllModels<T>((x) => true);

        /// <inheritdoc/>
        public List<T> GetAllModels<T>(Predicate<T> condition)
            where T : IModelable
        {
            return _children.Where(x => x.Model is T model && condition(model))
                .Select(x => x.Model).Cast<T>().ToList();
        }

        /// <inheritdoc/>
        public List<T> GetAllModelsRecursively<T>()
            where T : IModelable => GetAllModelsRecursively<T>((x) => true);

        /// <inheritdoc/>
        public List<T> GetAllModelsRecursively<T>(Predicate<T> condition)
            where T : IModelable
        {
            List<T> result = GetAllModels(condition);
            Children.ForEach(x => result.AddRange(x.Model.GetAllModels(condition)));
            return result;
        }
        #endregion

        #region Get View Methods
        /// <inheritdoc/>
        public T GetView<T>()
            where T : IGUIable => GetView<T>((x) => true);

        /// <inheritdoc/>
        public T GetView<T>(Predicate<T> condition)
            where T : IGUIable
        {
            return (T)_children.FirstOrDefault(x => x.View is T view && condition(view))?.View;
        }

        /// <inheritdoc/>
        public T GetViewRecursively<T>()
            where T : IGUIable => GetViewRecursively<T>((x) => true);

        /// <inheritdoc/>
        public T GetViewRecursively<T>(Predicate<T> condition)
            where T : IGUIable
        {
            T result = GetView(condition);
            foreach (var child in Children)
            {
                if (result != null)
                {
                    break;
                }
                result = child.Model.GetViewRecursively(condition);
            }
            return result;
        }

        /// <inheritdoc/> 
        public List<T> GetAllViews<T>()
            where T : IGUIable => GetAllViews<T>((x) => true);

        /// <inheritdoc/>
        public List<T> GetAllViews<T>(Predicate<T> condition)
            where T : IGUIable
        {
            return _children.Where(x => x.View is T view && condition(view))
                .Select(x => x.View).Cast<T>().ToList();
        }

        /// <inheritdoc/>
        public List<T> GetAllViewsRecursively<T>()
            where T : IGUIable => GetAllViewsRecursively<T>((x) => true);

        /// <inheritdoc/>
        public List<T> GetAllViewsRecursively<T>(Predicate<T> condition)
            where T : IGUIable
        {
            List<T> result = GetAllViews(condition);
            Children.ForEach(x => result.AddRange(x.Model.GetAllViews(condition)));
            return result;
        }
        #endregion
    }
}
