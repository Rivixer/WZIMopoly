using Microsoft.Xna.Framework;

namespace WZIMopoly.UI;

internal class UIFrame : UIComponent
{
    private readonly UIImage[] _lines = new UIImage[4];
    private int _thickness;
    private Color _color;

    public UIFrame(UIComponent parent, int thickness, Color color)
        //: base(parent)
    {
        _thickness = thickness;
        _color = color;
        CreateLines();
    }

    public int Thickness
    {
        get { return _thickness; }
        set
        {
            _thickness = value;
            CreateLines();
        }
    }

    public Color Color
    {
        get { return _color; }
        set
        {
            _color = value;
            CreateLines();
        }
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        foreach (UIImage line in _lines)
        {
            line?.Draw(gameTime);
            Debug.WriteLine(line?.Transform.DestinationRectangle);
        }
        Debug.WriteLine("");
        
    }

    private void CreateLines()
    {
        Transform.RecalculateIfNeeded();
        for (int i = 0; i < 4; i++)
        {
            //UIImage line = new(this, _color);
            Rectangle rect = Transform.NonScaledDestinationRectangle;
            switch (i)
            {
                case 0:
                    rect.Width = _thickness;
                    break;
                case 1:
                    rect.Height = _thickness;
                    break;
                case 2:
                    rect.X += rect.Width - _thickness;
                    rect.Width = _thickness;
                    break;
                case 3:
                    rect.Y += rect.Height - _thickness;
                    rect.Height = _thickness;
                    break;
            }
            //line.Transform.Parent = null!;
            //line.Transform.NonScaledDestinationRectangle = rect;
            //line.Transform.RecalculateIfNeeded();
            //_lines[i] = line;
        }
    }
}
