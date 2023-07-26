using Microsoft.Xna.Framework;

namespace WZIMopoly.UI;

internal class UITextButton : UIButton
{
    private UIText? _text;
    private UIImage? _background;

    public UIImage? Background
    {
        get { return _background; }
        set {
            _background = value;
            if (_background is not null)
            {
                _background.Parent = this;
                _background.TransformType = TransformType.Relative;
                Transform.Ratio = (_background.Texture.Width / (float)_background.Texture.Height).ToRatio();
            }
        }
    }

    public UIText? Text
    {
        get { return _text; }
        set
        {
            _text = value;
            if (_text is not null)
            {
                _text.Parent = this;
                _text.TransformType = TransformType.Relative;
                (_text.Transform as UITransformRelative)!.Alignment = Alignment.Center;
            }
        }
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
}
