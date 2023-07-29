using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace WZIMopoly.UI;

internal class UIText : UIComponent
{
    private static readonly Dictionary<string, SpriteFont> s_cachedFonts = new();
    private readonly string _fontPath;
    private SpriteFont? _font;

    public UIText(string text, Color color, string? fontPath = null)
    {
        _fontPath = fontPath ?? "Fonts/DebugFont";
        Color = color;
        Text = text;
        Transform.TransformType = TransformType.Relative;
        Transform.Alignment = Alignment.Center;
    }

    public bool UseCache { get; set; } = true;

    public string Text { get; set; } = string.Empty;

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
        ContentSystem.SpriteBatch.DrawString(
            spriteFont: Font,
            text: Text,
            position: Transform.DestinationRectangle.Location.ToVector2(),
            color: Color,
            rotation: 0.0f,
            origin: Vector2.Zero,
            scale: ScreenSystem.Scale * Size,
            effects: SpriteEffects.None,
            layerDepth: 0.0f);
        base.Draw(gameTime);
    }

    private void LoadFont()
    {
        if (UseCache && s_cachedFonts.ContainsKey(_fontPath))
        { 
            _font = s_cachedFonts[_fontPath];
        }
        else
        {
            _font = ContentSystem.Content.Load<SpriteFont>(_fontPath);
            if (UseCache && !s_cachedFonts.ContainsKey(_fontPath))
            {
                s_cachedFonts.Add(_fontPath, _font);
            }
        }
    }
}
