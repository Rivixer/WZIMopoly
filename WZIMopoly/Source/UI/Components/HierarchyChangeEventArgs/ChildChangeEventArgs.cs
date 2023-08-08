using System;

namespace WZIMopoly.UI;

internal class ChildChangeEventArgs : EventArgs
{
    public ChildChangeEventArgs(UIComponent child)
    {
        Child = child;
    }

    public UIComponent Child { get; }
}
