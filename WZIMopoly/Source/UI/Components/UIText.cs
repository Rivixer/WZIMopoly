using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace WZIMopoly.UI;

internal class UIText : UIComponent
{
    private readonly string? _fontPath;
    private SpriteFont? _font;

    private bool _needsRecalculation;

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

    private string _text = string.Empty;

    public virtual string Text
    {
        get { return _text; }
        set
        {
            _text = value;
            _needsRecalculation = true;
        }
    }

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

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_needsRecalculation)
        {
            Transform.Recalculate();
            _needsRecalculation = false;
        }
    }

    public override void Draw(GameTime gameTime)
    {
        ContentSystem.SpriteBatch.DrawString(
            spriteFont: Font,
            text: Text,
            position: Transform.DestinationRectangle.Location.ToVector2(),
            color: Color,
            rotation: 0.0f,
            origin: Vector2.Zero,
            scale: Size * ScreenSystem.Scale,
            effects: SpriteEffects.None,
            layerDepth: 0.0f);
        base.Draw(gameTime);
    }

    private void LoadFont()
    {
        _font = ContentSystem.Content.Load<SpriteFont>(_fontPath);
    }
}
