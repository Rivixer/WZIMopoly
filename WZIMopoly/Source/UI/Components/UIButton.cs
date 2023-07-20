using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal abstract class UIButton : UIComponent
{
    protected UIButton(UIComponent parent)
        : base(parent) { }

    public static bool WasClickedInThisFrame { get; set; }

    public delegate void OnClickedEventHandler(object sender, EventArgs eventArgs);
    public delegate void OnPressedEventHandler(object sender, EventArgs eventArgs);
    public delegate void OnReleasedEventHandler(object sender, EventArgs eventArgs);

    public event OnClickedEventHandler? OnClicked;
    public event OnPressedEventHandler? OnPressed;
    public event OnReleasedEventHandler? OnReleased;

    public bool IsEnabled { get; set; }

    public virtual bool IsHovered
    {
        get
        {
            Point cursorPostion = MouseSystem.Position;
            return Transform.DestinationRectangle.Contains(cursorPostion);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!WasClickedInThisFrame
            && MouseSystem.WasLeftButtonClicked()
            && IsHovered)
        {
            WasClickedInThisFrame = true;
            OnClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
