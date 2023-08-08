using System;

namespace WZIMopoly.UI;

internal class ParentChangeEventArgs : EventArgs
{
    public ParentChangeEventArgs(UIComponent? newParent, UIComponent? oldParent)
    {
        NewParent = newParent;
        OldParent = oldParent;
    }
    public UIComponent? NewParent { get; }
    public UIComponent? OldParent { get; }
}