using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.UI;

internal class UIText : UIComponent
{
    private readonly string? _fontPath;
    private SpriteFont? _font;

    public UIText(string text, Color color, string? fontPath = null)
    {
        // TODO: Change default font
        _fontPath = fontPath ?? "Fonts/DebugFont";
        Color = color;
        Text = text;
        Alignment = Alignment.Center;
    }

    public UIText(string text, Color color, SpriteFont font)
    {
        _font = font;
        Color = color;
        Text = text;
        Alignment = Alignment.Center;
    }

    public virtual string Text { get; set; } = string.Empty;

    public Color Color { get; set; }

    public float Size { get; set; } = 1.0f;

    public SpriteFont Font
    {
        get
        {
            if (_font is null)
            {
                LoadFont();
            }
            return _font!;
        }
    }

    public Vector2 MeasureDimensions()
    {
        return Font.MeasureString(Text) * Size;
    }

    public override void Draw(GameTime gameTime)
    {
        SpriteBatch spriteBatch = ContentSystem.SpriteBatch;
        spriteBatch.End();

        RasterizerState rasterizerState = new() { ScissorTestEnable = true };
        spriteBatch.Begin(
            sortMode: SpriteSortMode.Immediate,
            blendState: BlendState.AlphaBlend,
            samplerState: null,
            depthStencilState: null,
            rasterizerState: rasterizerState);

        Rectangle currentRect = spriteBatch.GraphicsDevice.ScissorRectangle;
        if (Parent is not null)
        {
            spriteBatch.GraphicsDevice.ScissorRectangle = Parent.Transform.DestinationRectangle;
        }

        spriteBatch.DrawString(
            spriteFont: Font,
            text: Text,
            position: Transform.DestinationRectangle.Location.ToVector2(),
            color: Color,
            rotation: 0.0f,
            origin: Vector2.Zero,
            scale: Size * ScreenSystem.Scale,
            effects: SpriteEffects.None,
            layerDepth: 0.0f);

        spriteBatch.GraphicsDevice.ScissorRectangle = currentRect;
        spriteBatch.End();
        rasterizerState.Dispose();
        spriteBatch.Begin();
        base.Draw(gameTime);
    }

    private void LoadFont()
    {
        _font = ContentSystem.Content.Load<SpriteFont>(_fontPath);
    }
}
