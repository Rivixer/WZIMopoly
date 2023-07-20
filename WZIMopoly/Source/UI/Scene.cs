using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace WZIMopoly.UI;

internal abstract class Scene
{
    private readonly List<UIBaseComponent> _baseComponents = new();

    protected IEnumerable<UIBaseComponent> BaseComponents => _baseComponents;

    public abstract void Create();

    public virtual void Update(GameTime gameTime)
    {
        foreach (UIBaseComponent component in BaseComponents)
        {
            component.Update(gameTime);
        }
    }

    public virtual void Draw(GameTime gameTime)
    {
        foreach (UIBaseComponent component in BaseComponents)
        {
            component.Draw(gameTime);
        }
    }
    protected UIBaseComponent CreateBaseComponent()
    {
        UIBaseComponent component = new();
        _baseComponents.Add(component);
        return component;
    }
}
