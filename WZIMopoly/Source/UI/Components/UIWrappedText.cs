using Microsoft.Xna.Framework;
using System.Text;

namespace WZIMopoly.UI;

internal class UIWrappedText : UIText
{
    private string _unwrappedText;

    public UIWrappedText(string text, Color color, string? fontPath = null)
        : base(text, color, fontPath)
    {
        _unwrappedText = text;
        OnParentChange += UIWrappedText_OnParentChange;
    }

    public override string Text
    {
        set
        {
            _unwrappedText = value;
            WrapText();
        }
    }

    private void ParentTransform_OnRecalculate(object? sender, System.EventArgs e)
    {
        WrapText();
    }

    private void UIWrappedText_OnParentChange(object? sender, ParentChangeEventArgs e)
    {
        if (e.OldParent is { } oldParent)
        {
            oldParent.Transform.OnRecalculate -= ParentTransform_OnRecalculate;
        }

        if (e.NewParent is { } newParent)
        {
            newParent.Transform.OnRecalculate += ParentTransform_OnRecalculate;
        }
    }

    private void WrapText()
    {
        // TODO: Add the ability to adjust text for alignment
        Rectangle reference = Transform.UnscaledDestinationRectangle;
        StringBuilder result = new();
        StringBuilder currentLine = new();
        int i = 0;
        float currentWidth = 0.0f;
        while (i < _unwrappedText.Length)
        {
            StringBuilder word = new();
            char whiteCharacter = default;
            while (i < _unwrappedText.Length)
            {
                char character = _unwrappedText[i++];
                if (char.IsWhiteSpace(character))
                {
                    whiteCharacter = character;
                    break;
                }
                word.Append(character);
            }
            float wordWidth = Font.MeasureString(word).X * Size;
            if (reference.Width - currentWidth < wordWidth)
            {
                // TODO: Split the word if it's too long
                result.Append(currentLine.Length > 0 ? currentLine : word);
                result.Append('\n');
                currentLine.Clear();
                currentWidth = 0.0f;
            }
            currentLine.Append(word);
            currentWidth += wordWidth;
            if (whiteCharacter != default)
            {
                currentLine.Append(whiteCharacter);
                currentWidth += Font.MeasureString(whiteCharacter.ToString()).X * Size;
            }
        }

        if (currentLine.Length > 0)
        {
            result.Append(currentLine);
        }

        base.Text = result.ToString();
        Transform.Recalculate();
    }
}
