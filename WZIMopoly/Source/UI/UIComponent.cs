using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WZIMopoly.UI;

internal abstract class UIComponent
{
    private readonly List<UIComponent> _children = new();
    private UIComponent? _parent;

    protected UIComponent(UITransform? transform = null)
    {
        Transform = transform ?? new UITransformAbsolute(this);
    }

    public TransformType TransformType
    {
        get
        {
            Type type = Transform.GetType();
            if (type == typeof(UITransformAbsolute))
            {
                return TransformType.Absolute;
            }
            if (type == typeof(UITransformRelative))
            {
                return TransformType.Relative;
            }
            throw new InvalidOperationException("Invalid transform type.");
        }
        set
        {
            if (value == TransformType.Absolute && TransformType != TransformType.Absolute)
            {
                Transform = new UITransformAbsolute(Transform);
            }
            else if (value == TransformType.Relative && TransformType != TransformType.Relative)
            {
                Transform = new UITransformRelative(Transform);
            }
        }
    }

    public UITransform Transform { get; protected set; }

    public IEnumerable<UIComponent> Children => _children;

    public UIComponent? Parent
    {
        get
        {
            return _parent;
        }
        set
        {
            if (_parent != value)
            {
                UIComponent? oldParent = _parent;
                _parent?._children.Remove(this);
                _parent = value;
                _parent?._children.Add(this);
                Transform.Component_OnParentChanged(Parent, oldParent);
            }
        }
    }

    public static bool operator ==(UIComponent? left, UIComponent? right)
    {
        return left?.Equals(right) ?? false;
    }

    public static bool operator !=(UIComponent? left, UIComponent? right)
    {
        return !(left == right);
    }

    public virtual void Update(GameTime gameTime)
    {
        Transform.RecalculateIfNeeded();
        foreach (UIComponent child in Children)
        {
            child.Update(gameTime);
        }
    }

    public virtual void Draw(GameTime gameTime)
    {
        foreach (UIComponent child in Children)
        {
            child.Draw(gameTime);
        }
    }

    /// <summary>
    /// Returns the first child of a component
    /// that is of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the child to get.</typeparam>
    /// <param name="predicate">
    /// An optional predicate used to filter the child components.
    /// </param>
    /// <returns>
    /// The first child of a component that is of type <typeparamref name="T"/>,
    /// or <see langword="null"/> if no child is found. 
    /// </returns>
    public T? GetChild<T>(Predicate<T>? predicate = null)
        where T : UIComponent
    {
        return Children.FirstOrDefault(c => c is T t && (predicate?.Invoke(t) ?? true)) as T;
    }

    /// <summary>
    /// Returns the first descendant of a component
    /// that is of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the descendant to get.</typeparam>
    /// <param name="predicate">
    /// An optional predicate used to filter the descendant components.</param>
    /// <returns>
    /// The first descendant of a component that is of type <typeparamref name="T"/>,
    /// or <see langword="null"/> if no descendant is found.
    /// </returns>
    public T? GetDescendant<T>(Predicate<T>? predicate = null)
        where T : UIComponent
    {
        T? descendant = GetChild(predicate);
        foreach (UIComponent child in Children)
        {
            if (descendant is not null)
            {
                break;
            }
            descendant = child.GetDescendant(predicate);
        }
        return descendant;
    }

    /// <summary>
    /// Retrieves all children of a component
    /// that are of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the children to retrieve.</typeparam>
    /// <param name="predicate">
    /// An optional predicate used to filter the children components.
    /// </param>
    /// <returns>
    /// An enumerable containing all children of a component
    /// that are of type <typeparamref name="T"/>.
    /// </returns>
    public IEnumerable<T> GetAllChildren<T>(Predicate<T>? predicate = null)
        where T : UIComponent
    {
        return Children.OfType<T>().Where(c => predicate?.Invoke(c) ?? true);
    }

    /// <summary>
    /// Retrieves all descendants of a component
    /// that are of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the descendants to retrive.</typeparam>
    /// <param name="predicate">
    /// An optional predicate to use to filter the descendants components.
    /// </param>
    /// <returns>
    /// An enumerable containing all descendants of a component
    /// that are of type <typeparamref name="T"/>.
    /// </returns>
    public IEnumerable<T> GetAllDescendants<T>(Predicate<T>? predicate = null)
        where T : UIComponent
    {
        foreach (T child in GetAllChildren(predicate))
        {
            yield return child;
        }
        foreach (UIComponent child in Children)
        {
            foreach (T descendant in child.GetAllDescendants(predicate))
            {
                yield return descendant;
            }
        }
    }

    public bool IsParentOf(UIComponent component)
    {
        return Children.Contains(component);
    }

    public bool IsChildOf(UIComponent component)
    {
        return component.Children.Contains(this);
    }

    public bool IsAncestorOf(UIComponent component)
    {
        return GetAllDescendants<UIComponent>().Contains(component);
    }

    public bool IsDescendantOf(UIComponent component)
    {
        return component.Children.Contains(this)
            || component.Children.Any(c => c.IsDescendantOf(this));
    }

    public bool HasParentOfType<T>()
        where T : UIComponent
    {
        return Parent is T;
    }

    public bool HasChildOfType<T>()
        where T : UIComponent
    {
        return GetAllChildren<T>().Any();
    }

    public bool HasAncestorOfType<T>()
        where T : UIComponent
    {
        return Parent is T
            || Parent is UIComponent c && c.HasAncestorOfType<T>();
    }

    public bool HasDescendantOfType<T>()
        where T : UIComponent
    {
        return GetAllDescendants<T>().Any();
    }

    public override bool Equals(object? obj)
    {
        return obj is UIComponent component
            && GetType() == component.GetType()
            && Children.SequenceEqual(component.Children);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + GetType().GetHashCode();
            hash = hash * 23 + Children.GetHashCode();
            return hash;
        }
    }
}
