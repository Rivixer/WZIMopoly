using Microsoft.Xna.Framework;

namespace WZIMopoly.UI;
internal class UITextButton : UIButton
{
    public UITextButton(UIComponent parent, Color backgroundColor, UILazyComponent<UIText> text)
        : base(parent)
    {
        Background = new UIImage(this, backgroundColor);
        Text = text.Initialize(this);
    }

    public UITextButton(UIComponent parent, UILazyComponent<UIImage> background, UILazyComponent<UIText> text)
        : base(parent) 
    {
        Background = background.Initialize(this);
        Text = text.Initialize(this);
    }

    public UIImage Background { get; set; }
    public UIText Text { get; set; }

    public override void Draw(GameTime gameTime)
    {
        Background.Draw(gameTime);
        Text.Draw(gameTime);
    }
}
