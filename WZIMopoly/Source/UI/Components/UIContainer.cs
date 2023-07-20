using Microsoft.Xna.Framework;

namespace WZIMopoly.UI;

internal class UIContainer : UIComponent
{
    public UIContainer(UIBaseComponent parent)
        : base(parent) { }

    public UIImage? Background { get; private set; }

    public void SetBackground(Color color)
    {
        Background = new UIImage(this, color);
    }

    public void SetBackground(UILazyComponent<UIImage> image)
    {
        Background = image.Initialize(this);
    }
    
    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
}
