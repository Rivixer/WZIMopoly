using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WZIMopoly.UI;

// TODO: Consider implementing IDisposable
[SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable")]
internal class UIImage : UIComponent
{
    // We use a reference counter to keep track of how many times a texture is used.
    // This way, we can dispose of the texture when it is no longer used anywhere.
    private static readonly Dictionary<string, int> s_textureRefereceCounter = new();

    // We cache the pixels of the textures so that we don't have to load them every time we need them.
    private static readonly Dictionary<string, Lazy<Color[]>> s_cachedTexturePixels = new();

    private readonly string? _path;
    private readonly Color? _color;

    private float _opacity = 1.0f;
    private Texture2D? _texture;
    private readonly Lazy<Color[]>? _texturePixels;

    public UIImage(string path)
        : base()
    {
        _path = path;
        _texturePixels = new Lazy<Color[]>(LoadImagePixels);
    }

    public UIImage(Color color)
        : base()
    {
        _color = color;
        _texture = new Texture2D(ScreenSystem.GraphicsDevice, 1, 1);
        _texture.SetData(new[] { color });
        _opacity = color.A / 255.0f;
    }

    public Texture2D Texture
    {
        get
        {
            if (_texture is null)
            {
                LoadTexture();
            }
            return _texture!;
        }
    }

    public Color[]? TexturePixels
    {
        get { return _texturePixels?.Value; }
    }

    public Color? Color
    {
        get { return _color; }
    }

    public float Opacity
    {
        get { return _opacity; }
        set { _opacity = value; }
    }

    public override void Draw(GameTime gameTime)
    {
        ContentSystem.SpriteBatch.Draw(
            texture: Texture,
            destinationRectangle: Transform.DestinationRectangle,
            color: Microsoft.Xna.Framework.Color.White * _opacity);
        base.Draw(gameTime);
    }

    public override void Destroy()
    {
        if (_path is not null)
        {
            s_textureRefereceCounter[_path]--;
            if (s_textureRefereceCounter[_path] == 0)
            {
                s_textureRefereceCounter.Remove(_path);
                s_cachedTexturePixels.Remove(_path);
                _texture?.Dispose();
            }
        }
        else
        {
            _texture?.Dispose();
        }
        base.Destroy();
    }

    private void LoadTexture()
    {
        _texture = ContentSystem.Content.Load<Texture2D>(_path!);
        if (s_textureRefereceCounter.ContainsKey(_path!))
        {
            s_textureRefereceCounter[_path!]++;
        }
        else
        {
            s_textureRefereceCounter.Add(_path!, 1);
        }
    }

    private Color[] LoadImagePixels()
    {
        if (_path is not null && s_cachedTexturePixels.ContainsKey(_path))
        {
            return s_cachedTexturePixels[_path].Value;
        }

        var pixels = new Color[Texture.Width * Texture.Height];
        Texture.GetData(pixels);

        if (_path is not null)
        {
            s_cachedTexturePixels.Add(_path, new Lazy<Color[]>(() => pixels));
        }

        return pixels;
    }
}
