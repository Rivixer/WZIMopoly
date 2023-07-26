using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WZIMopoly.UI;

internal class UIImage : UIComponent
{
    private static readonly Dictionary<string, Texture2D> _cachedTextures = new();
    private readonly string? _path;
    private Texture2D? _texture;
    private readonly bool _useCache;

    public UIImage(string path, bool useCache = false)
        : base()
    {
        _path = path;
        _useCache = useCache;
    }

    public UIImage(Color color)
    {
        _texture = new Texture2D(ScreenSystem.GraphicsDevice, 1, 1);
        _texture.SetData(new[] { color });
        _useCache = false;
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

    public static UILazyComponent<UIImage> ToLazy(string path, bool useCache = false)
    {
        return new UILazyComponent<UIImage>(path, useCache);
    }

    public static UILazyComponent<UIImage> ToLazy(Color color)
    {
        return new UILazyComponent<UIImage>(color);
    }

    public override void Draw(GameTime gameTime)
    {
        ContentSystem.SpriteBatch.Draw(Texture, Transform.DestinationRectangle, Color.White);
        base.Draw(gameTime);
    }

    private void LoadTexture()
    {
        if (_useCache && _cachedTextures.ContainsKey(_path!))
        {
            _texture = _cachedTextures[_path!];
        }
        else
        {
            _texture = ContentSystem.Content.Load<Texture2D>(_path!);
            if (!_cachedTextures.ContainsKey(_path!))
            {
                _cachedTextures.Add(_path!, _texture);
            }
        }
    }
}
