using System;
using System.Linq;

namespace WZIMopoly.UI;

internal abstract class UIComponent : UIBaseComponent
{
    private UIBaseComponent _parent;

    protected UIComponent(UIBaseComponent parent)
    {
        _parent = parent;
        Transform = new(this);
        parent.AddChild(this);
    }

    public UIBaseComponent Parent
    {
        get
        {
            return _parent;
        }
        set
        {
            if (_parent != value)
            {
                _parent.RemoveChild(this);
                _parent = value;
                _parent.AddChild(this);
            }
        }
    }

    public bool IsChildOf(UIBaseComponent component)
    {
        return component.Children.Contains(this);
    }

    public bool IsDescendantOf(UIBaseComponent component)
    {
        return component.Children.Contains(this)
            || component.Children.Any(c => c.IsDescendantOf(this));
    }

    public bool HasParentOfType<T>()
        where T : UIBaseComponent
    {
        return Parent is T;
    }

    public bool HasAncestorOfType<T>()
        where T : UIBaseComponent
    {
        return Parent is T
            || Parent is UIComponent c && c.HasAncestorOfType<T>();
    }

    public override bool Equals(object? obj)
    {
        return obj is UIComponent component
            && base.Equals(obj)
            // Hash code instead of Equals to avoid infinite recursion
            && Parent.GetHashCode() == component.Parent.GetHashCode();
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = base.GetHashCode();
            hash = hash * 23 + Parent.GetHashCode();
            return hash;
        }
    }
}
