using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace WZIMopoly.UI;

internal class UIImage : UIComponent, IDisposable
{
    private static readonly Dictionary<string, Texture2D> s_cachedTextures = new();
    private readonly float _opacity = 1.0f;
    private readonly string? _path;
    private readonly bool _useCache;
    private Texture2D? _texture;
    private Lazy<Color[]> _texturePixels;

    public UIImage(string path, bool useCache = false)
        : base()
    {
        _path = path;
        _useCache = useCache;
        _texturePixels = new Lazy<Color[]>(LoadImagePixels);
    }

    public UIImage(Color color)
    {
        _texture = new Texture2D(ScreenSystem.GraphicsDevice, 1, 1);
        _texture.SetData(new[] { color });
        _opacity = color.A / 255.0f;
        _useCache = false;

        _texturePixels = new Lazy<Color[]>(() => {
            var colors = new Color[
            Transform.UnscaledDestinationRectangle.Size.X
            * Transform.UnscaledDestinationRectangle.Size.Y];
            Array.Fill(colors, color);
            return colors;
        });

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

    public Color[] TexturePixels => _texturePixels.Value;

    public void Dispose()
    {
#warning If a texture is cached, it should only be disposed when it is not used anywhere else.
        //_texture?.Dispose();

        if (_texturePixels.IsValueCreated)
        { 
            _texturePixels = new Lazy<Color[]>(LoadImagePixels);
        }
    }

    private Color[] LoadImagePixels()
    {
        var pixels = new Color[Texture.Width * Texture.Height];
        Texture.GetData(pixels);
        return pixels;
    }

    public override void Draw(GameTime gameTime)
    {
        ContentSystem.SpriteBatch.Draw(
            texture: Texture,
            destinationRectangle: Transform.DestinationRectangle,
            color: Color.White * _opacity);
        base.Draw(gameTime);
    }

    private void LoadTexture()
    {
        if (_useCache && s_cachedTextures.ContainsKey(_path!))
        {
            _texture = s_cachedTextures[_path!];
        }
        else
        {
            _texture = ContentSystem.Content.Load<Texture2D>(_path!);
            if (_useCache && !s_cachedTextures.ContainsKey(_path!))
            {
                s_cachedTextures.Add(_path!, _texture);
            }
        }
    }
}
