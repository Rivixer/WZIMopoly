using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal class UIFrame : UIComponent
{
    private readonly UIImage[] _lines = new UIImage[4];
    private readonly UIContainer _innerRectangle;

    private int _thickness;
    private Color _color;

    public UIFrame(int thickness, Color color)
        : base()
    {
        _thickness = thickness;
        _color = color;

        _innerRectangle = new UIContainer()
        {
            Parent = this,
            TransformType = TransformType.Absolute,
        };

        for (int i = 0; i < _lines.Length; i++)
        {
            if (_lines[i] is null)
            {
                _lines[i] = new UIImage(_color)
                {
                    Parent = this,
                    TransformType = TransformType.Absolute
                };
            }
        }

        Transform.OnRecalculate += Transform_OnRecalculate;
        OnChildAdd += Component_OnChildAdd;
    }

    public int Thickness
    {
        get { return _thickness; }
        set
        {
            if (_thickness == value)
            {
                return;
            }
            _thickness = value;
            UpdateLines();
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    private void UpdateLines()
    {
        Point scaledThickness = new Point(_thickness).Scale(ScreenSystem.Scale);
        Rectangle referenceRect = Transform.DestinationRectangle;

        for (int i = 0; i < _lines.Length; i++)
        {
            Rectangle rect = Transform.DestinationRectangle;
            switch (i)
            {
                case 0: // Left line
                    rect.Width = (int)scaledThickness.X;
                    rect.Height -= 2 * (int)scaledThickness.Y;
                    rect.Y += (int)scaledThickness.Y;
                    break;
                case 1: // Top line
                    rect.Height = (int)scaledThickness.X;
                    break;
                case 2: // Right line
                    rect.X += rect.Width - (int)scaledThickness.X;
                    rect.Y += (int)scaledThickness.Y;
                    rect.Width = (int)scaledThickness.X;
                    rect.Height -= 2 * (int)scaledThickness.Y;
                    break;
                case 3: // Bottom line
                    rect.Y += rect.Height - (int)scaledThickness.Y;
                    rect.Height = (int)scaledThickness.Y;
                    break;
            }
            _lines[i].Transform.ScaledLocation = rect.Location;
            _lines[i].Transform.ScaledSize = rect.Size;
        }

        _innerRectangle.Transform.ScaledLocation = new Point(
            referenceRect.X + scaledThickness.X,
            referenceRect.Y + scaledThickness.Y);

        _innerRectangle.Transform.ScaledSize = new(
            referenceRect.Width - 2 * scaledThickness.X,
            referenceRect.Height - 2 * scaledThickness.Y);


        /*_lines[0].Transform.ScaledLocation = Transform.DestinationRectangle.Location;
        _lines[0].Transform.ScaledSize = new(
            _thickness,
            Transform.DestinationRectangle.Height);

        _lines[1].Transform.ScaledLocation = Transform.DestinationRectangle.Location;
        _lines[1].Transform.ScaledSize = new(
                       Transform.DestinationRectangle.Width,
                                  _thickness);

        _lines[2].Transform.ScaledLocation = new(
            Transform.DestinationRectangle.X + Transform.DestinationRectangle.Width - _thickness,
                       Transform.DestinationRectangle.Y);

        _lines[2].Transform.ScaledSize = new(
            _thickness,
                       Transform.DestinationRectangle.Height);

        _lines[3].Transform.ScaledLocation = new(
            Transform.DestinationRectangle.X,
                       Transform.DestinationRectangle.Y + Transform.DestinationRectangle.Height - _thickness);

        _lines[3].Transform.ScaledSize = new(
            Transform.DestinationRectangle.Width,
                                  _thickness);*/

        /*_lines[0].Transform.DestinationRectangle = new Rectangle(
             UnscaledDestinationRectangle.X,
             UnscaledDestinationRectangle.Y,
             _thickness,
             UnscaledDestinationRectangle.Height);

         _lines[1].UnscaledDestinationRectangle = new Rectangle(
             UnscaledDestinationRectangle.X,
             UnscaledDestinationRectangle.Y,
             UnscaledDestinationRectangle.Width,
             _thickness);

         _lines[2].UnscaledDestinationRectangle = new Rectangle(
             UnscaledDestinationRectangle.X + UnscaledDestinationRectangle.Width - _thickness,
             UnscaledDestinationRectangle.Y,
             _thickness,
             UnscaledDestinationRectangle.Height); */

        /* _lines[3].UnscaledDestinationRectangle = new Rectangle(
             UnscaledDestinationRectangle.X,
             UnscaledDestinationRectangle.Y + UnscaledDestinationRectangle.Height - _thickness,
             UnscaledDestinationRectangle.Width,
             _thickness);
         _lines[3].Transform.Recalculate();

        
        for (int i = 0; i < 4; i++)
        {
            if (_lines[i] is null)
            {
                _lines[i] = new UIImage(_color)
                {
                    Parent = this,
                    TransformType = TransformType.Absolute,
                };
            }

            // TODO: Refactor
            Rectangle rect = Transform.UnscaledDestinationRectangle;
            Vector2 thickness = new(_thickness);
            switch (i)
            {
                case 0: // Left line
                    rect.Width = (int)thickness.X;
                    rect.Height -= 2 * (int)thickness.Y;
                    rect.Y += (int)thickness.Y;
                    break;
                case 1: // Top line
                    rect.Height = (int)thickness.X;
                    break;
                case 2: // Right line
                    rect.X += rect.Width - (int)thickness.X;
                    rect.Y += (int)thickness.Y;
                    rect.Width = (int)thickness.X;
                    rect.Height -= 2 * (int)thickness.Y;
                    break;
                case 3: // Bottom line
                    rect.Y += rect.Height - (int)thickness.Y;
                    rect.Height = (int)thickness.Y;
                    break;
            }

            _lines[i].UnscaledDestinationRectangle = rect;
        }*/


    }


    /*Rectangle referenceRect = Transform.UnscaledDestinationRectangle;

    void UpdateLine(Vector2 start, Vector2 end)
    {
        if (_lines[updatedLines] is null)
        {
            _lines[updatedLines] = new UIImage(_color)
            {
                Parent = this,
                TransformType = TransformType.Absolute,
                Rotation = (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
                Origin = Vector2.Zero,
            };
        }

        float length = Vector2.Distance(start, end);
        _lines[updatedLines].Transform.UnscaledLocation = start.ToPoint();
        _lines[updatedLines++].Transform.UnscaledSize = new((int)length, _thickness);
    }

    UpdateLine( // Top line
        new(referenceRect.Left + _thickness, referenceRect.Top),
        new(referenceRect.Right - _thickness, referenceRect.Top));
    UpdateLine( // Bottom line
        new(referenceRect.Left, referenceRect.Bottom - _thickness),
        new(referenceRect.Right, referenceRect.Bottom - _thickness));
    UpdateLine( // Left line
        new(referenceRect.Left + _thickness, referenceRect.Top),
        new(referenceRect.Left + _thickness, referenceRect.Bottom - _thickness));
    UpdateLine( // Right line
        new(referenceRect.Right, referenceRect.Top),
        new(referenceRect.Right, referenceRect.Bottom - _thickness));

    _innerRectangle.Transform.UnscaledLocation = new(
        referenceRect.X + _thickness,
        referenceRect.Y + _thickness);
    _innerRectangle.Transform.UnscaledSize = new(
        referenceRect.Width - 2 * _thickness,
        referenceRect.Height - 2 * _thickness);*/


    public override void Draw(GameTime gameTime)
    {
        /*int thickness = (int)(_thickness *ScreenSystem.Scale.X);
        var sb = ContentSystem.SpriteBatch;
        Texture2D solidWhiteTexture = new UIImage(_color).Texture;
        var rect = Transform.DestinationRectangle;
        var clr = Color.White;
        float depth = 0.0f;
        Rectangle srcRect = new Rectangle(0, 0, 1, 1);
        sb.Draw(solidWhiteTexture, new Vector2(rect.X, rect.Y), null, clr, 0.0f, Vector2.Zero, new Vector2(thickness, rect.Height), SpriteEffects.None, depth);
        sb.Draw(solidWhiteTexture, new Vector2(rect.X + thickness, rect.Y), null, clr, 0.0f, Vector2.Zero, new Vector2(rect.Width - thickness, thickness), SpriteEffects.None, depth);
        sb.Draw(solidWhiteTexture, new Vector2(rect.X + thickness, rect.Bottom - thickness), null, clr, 0.0f, Vector2.Zero, new Vector2(rect.Width - thickness, thickness), SpriteEffects.None, depth);
        sb.Draw(solidWhiteTexture, new Vector2(rect.Right - thickness, rect.Y + thickness), null, clr, 0.0f, Vector2.Zero, new Vector2(thickness, rect.Height - thickness * 2f), SpriteEffects.None, depth);
        */
        base.Draw(gameTime);
    }

    private void Transform_OnRecalculate(object? sender, EventArgs e)
    {
        UpdateLines();
    }

    /// <summary>
    /// Switches the parent of an added component to <see cref="_innerRectangle"/>.
    /// </summary>
    private void Component_OnChildAdd(object? sender, ChildChangeEventArgs e)
    {
        UIComponent child = e.Child;
        child.Parent = _innerRectangle;
    }
}
