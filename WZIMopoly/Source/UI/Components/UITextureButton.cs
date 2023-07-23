using Microsoft.Xna.Framework;

namespace WZIMopoly.UI;

internal class UITextureButton : UIButton
{
    private readonly Color[] _texturePixels;

    public UITextureButton(UIComponent parent, string texturePath)
        : base(parent)
    {
        Image = new UIImage(this, texturePath);
        Transform.Ratio = Image.Texture.Bounds.Size.ToRatio();
        _texturePixels = ReadTexturePixels();
    }

    public UITextureButton(UIComponent parent, UILazyComponent<UIImage> image)
        : base(parent)
    {
        Image = image.Initialize(this);
        Transform.Ratio = Image.Texture.Bounds.Size.ToRatio();
        _texturePixels = ReadTexturePixels();
    }

    public UIImage Image { get; private set; }

    public override bool IsHovered
    {
        get
        {
            if (!base.IsHovered) { return false; }

            // Button is hovered if the mouse is over a non-transparent pixel
            Point mousePoisition = MouseSystem.Position;
            Point mouseOffset = mousePoisition - Image.Transform.DestinationRectangle.Location;
            var mouseOffsetX = mouseOffset.X * Image.Texture.Width / Image.Transform.DestinationRectangle.Size.X;
            var mouseOffsetY = mouseOffset.Y * Image.Texture.Height / Image.Transform.DestinationRectangle.Size.Y;
            int index = mouseOffsetY * Image.Texture.Width + mouseOffsetX;
            return index > 0 && index < _texturePixels.Length && _texturePixels[index].A > 0;
        }
    }

    public override void Draw(GameTime gameTime)
    {
        Image.Draw(gameTime);
        base.Draw(gameTime);
    }

    private Color[] ReadTexturePixels()
    {
        var textureData = new Color[Image.Texture.Width * Image.Texture.Height];
        Image.Texture.GetData(textureData);
        return textureData;
    }
}