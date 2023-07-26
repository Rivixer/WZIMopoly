using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal abstract class UIButton : UIComponent
{
    public static bool WasClickedInThisFrame { get; set; }

    public delegate void OnClickedEventHandler(object sender, EventArgs eventArgs);
    public event OnClickedEventHandler? OnClicked;

    public bool IsEnabled { get; set; } = true;

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

        if (IsEnabled
            && !WasClickedInThisFrame
            && MouseSystem.WasLeftButtonClicked()
            && IsHovered)
        {
            WasClickedInThisFrame = true;
            OnClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
