using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WZIMopoly.UI;

internal class UIBaseComponent
{
    private readonly List<UIComponent> _children = new();

    public UIBaseComponent(UITransform? transform = null)
    {
        // If this is the base component, create default transform if none exists
        if (transform is null && GetType() == typeof(UIBaseComponent))
        {
            transform = new(this, new Rectangle(Point.Zero, ScreenSystem.DefaultSize));
        }
        Transform ??= transform!;
    }

    public IEnumerable<UIComponent> Children => _children;

    public int ChildCount => _children.Count;

    public UITransform Transform { get; protected set; }

    public static bool operator ==(UIBaseComponent? left, UIBaseComponent? right)
    {
        return left?.Equals(right) ?? false;
    }

    public static bool operator !=(UIBaseComponent? left, UIBaseComponent? right)
    {
        return !(left == right);
    }

    public virtual void AddChild(UIComponent child)
    {
        _children.Add(child);
    }

    public virtual void RemoveChild(UIComponent child)
    {
        _children.Remove(child);
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
    /// Returns the first child of this component
    /// that is of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the child to get.</typeparam>
    /// <param name="predicate">The predicate to use to filter the children.</param>
    /// <returns>
    /// The first child of this component
    /// that is of type <typeparamref name="T"/>.
    /// </returns>
    public T? GetChild<T>(Predicate<T>? predicate = null)
        where T : UIComponent
    {
        return Children.FirstOrDefault(c => c is T t && (predicate?.Invoke(t) ?? true)) as T;
    }

    /// <summary>
    /// Returns the first descendant of this component
    /// that is of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the descendant to get.</typeparam>
    /// <param name="predicate">The predicate to use to filter the descendants.</param>
    /// <returns>
    /// The first descendant of this component
    /// that is of type <typeparamref name="T"/>.
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
    /// Returns all children of this component
    /// that are of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the children to get.</typeparam>
    /// <param name="predicate">The predicate to use to filter the children.</param>
    /// <returns>
    /// An enumerable of all children of this component
    /// that are of type <typeparamref name="T"/>.
    /// </returns>
    public IEnumerable<T> GetAllChildren<T>(Predicate<T>? predicate = null)
        where T : UIComponent
    {
        return Children.OfType<T>().Where(c => predicate?.Invoke(c) ?? true);
    }

    /// <summary>
    /// Returns all descendants of this component
    /// that are of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the descendants to get.</typeparam>
    /// <param name="predicate">The predicate to use to filter the descendants.</param>
    /// <returns>
    /// An enumerable of all descendants of this
    /// component that are of type <typeparamref name="T"/>.
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

    public bool IsAncestorOf(UIComponent component)
    {
        return GetAllDescendants<UIComponent>().Contains(component);
    }

    public bool HasChildOfType<T>()
        where T : UIComponent
    {
        return GetAllChildren<T>().Any();
    }

    public bool HasDescendantOfType<T>()
        where T : UIComponent
    {
        return GetAllDescendants<T>().Any();
    }

    public override bool Equals(object? obj)
    {
        return obj is UIBaseComponent component
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
