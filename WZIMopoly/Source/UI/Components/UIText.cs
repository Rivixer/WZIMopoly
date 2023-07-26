using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace WZIMopoly.UI;

internal class UIText : UIComponent
{
    private static readonly Dictionary<string, SpriteFont> _cachedFonts = new();
    private readonly string _fontPath;
    private SpriteFont? _font;

    public UIText(string text, Color color, string? fontPath = null)
    {
        _fontPath = fontPath ?? "Fonts/DebugFont";
        Color = color;
        Text = text;
        //Transform.Alignment = Alignment.Center;
    }

    public bool UseCache { get; set; } = true;

    public string Text { get; set; } = string.Empty;

    public Color Color { get; set; }

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

    public static UILazyComponent<UIText> ToLazy(string text, Color color, string? fontPath = null)
    {
        return new UILazyComponent<UIText>(text, color, fontPath);
    }

    public override void Draw(GameTime gameTime)
    {
        ContentSystem.SpriteBatch.DrawString(Font, Text, Transform.DestinationRectangle.Location.ToVector2(), Color);
    }

    private void LoadFont()
    {
        if (UseCache && _cachedFonts.ContainsKey(_fontPath))
        { 
            _font = _cachedFonts[_fontPath];
        }
        else
        {
            _font = ContentSystem.Content.Load<SpriteFont>(_fontPath);
            _cachedFonts.Add(_fontPath, _font);
        }
    }
}
