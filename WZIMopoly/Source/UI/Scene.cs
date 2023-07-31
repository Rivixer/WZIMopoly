using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace WZIMopoly.UI;

internal abstract class Scene
{
    private readonly List<UIComponent> _components = new();

    protected IEnumerable<UIComponent> Components => _components;

    public abstract void Create();

    public virtual void Update(GameTime gameTime)
    {
        Debug.WriteLine(Components.First().Transform.DestinationRectangle);
        foreach (UIComponent component in Components)
        {
            component.Update(gameTime);
        }
    }

    public virtual void Draw(GameTime gameTime)
    {
        foreach (UIComponent component in Components)
        {
            component.Draw(gameTime);
        }
    }

    protected void AddComponent(UIComponent component)
    {
        _components.Add(component);
    }

    protected void RemoveComponent(UIComponent component)
    {
        _components.Remove(component);
    }
}
