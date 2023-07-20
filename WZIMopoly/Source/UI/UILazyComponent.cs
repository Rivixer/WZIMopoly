using System;
using System.Collections.Generic;
using System.Reflection;

namespace WZIMopoly.UI;

internal class UILazyComponent<T>
    where T : UIComponent
{
    private readonly object?[] _args;

    public UILazyComponent(params object?[] args)
    {
        _args = args;
    }

    public T Initialize(UIComponent parent)
    {
        List<object?> parameters = new() { parent };
        parameters.AddRange(_args);
        return (T)Activator.CreateInstance(
            typeof(T), BindingFlags.Public | BindingFlags.Instance, null, parameters.ToArray(), null)!;
    }
}