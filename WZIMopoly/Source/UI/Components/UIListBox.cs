using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace WZIMopoly.UI;

internal class UIListBox : UIComponent
{
    private readonly List<UIComponent> _elements = new();
    private bool _needsRecalculate;

    public void AddElement(UIComponent element)
    {
        element.Parent = this;
        element.Alignment = Alignment.TopLeft;
        element.Transform.OnRecalculate += (s, e) => _needsRecalculate = true;
        _elements.Add(element);
        _needsRecalculate = true;
    }

    public void RemoveElement(UIComponent element)
    {
        _elements.Remove(element);
        _needsRecalculate = true;
    }

    public override void Update(GameTime gameTime)
    {
        if (_needsRecalculate)
        {
            RecalculateElements();
            base.Update(gameTime);
            _needsRecalculate = false;
        }
        
    }

    private void RecalculateElements()
    {
        int unscaledCurrentOffset = 0;
        foreach(UIComponent element in _elements)
        {
            element.Transform.RelativeOffset = new(0.0f, unscaledCurrentOffset / (float)element.Transform.UnscaledDestinationRectangle.Height);
            if (element is UIText uiText)
            {
                unscaledCurrentOffset += (int)(uiText.MeasureDimensions().Y);
            }
            else
            {
                unscaledCurrentOffset += element.UnscaledDestinationRectangle.Height;
            }
            unscaledCurrentOffset += 8;
        }
    }
}
