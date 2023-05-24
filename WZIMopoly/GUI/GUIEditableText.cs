using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using WZIMopoly.Engine;
using WZIMopoly.Enums;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a text that can be edited.
    /// </summary>
    internal class GUIEditableText : GUIText
    {
        #region Fields
        /// <summary>
        /// The start position of the cursor.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The position of cursor at start.
        /// </para>
        /// <para>
        /// Specified for 1920x1080 resolution.
        /// </para>
        /// </remarks>
        private readonly Vector2 _cursorStartPos;

        /// <summary>
        /// The place where <see cref="_cursorStartPos"/> has been specified.
        /// </summary>
        private readonly GUIStartPoint _cursorStartPoint;

        /// <summary>
        /// The view of the cursor.
        /// </summary>
        private readonly GUITexture _cursor;

        /// <summary>
        /// The maximum number of characters that can be entered.
        /// </summary>
        private readonly int _maxChars;

        /// <summary>
        /// The index of the cursor position in the text.
        /// </summary>
        private int _cursorPosition = 0;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIEditableText"/> class.
        /// </summary>
        /// <param name="fontPath">
        /// The path to the font that will be used to display the text.
        /// </param>
        /// <param name="defPosition">
        /// The vector that represents position specified for 1920x1080 resolution.
        /// </param>
        /// <param name="startPoint">
        /// The place where <paramref name="defPosition"/> has been specified.
        /// </param>
        /// <param name="text">
        /// The text to display.<br/>
        /// Defaults to empty string.
        /// </param>
        /// <param name="scale">
        /// The scale of the text.<br/>
        /// Defaults to 1.0f.
        /// </param>
        /// <param name="maxChars">
        /// The maximum number of characters that can be entered.
        /// </param>
        /// <remarks>
        /// The color of the text is set to white.
        /// </remarks>
        public GUIEditableText(string fontPath, Vector2 defPosition, GUIStartPoint startPoint = GUIStartPoint.TopLeft, string text = "", float scale = 1, int maxChars = int.MaxValue)
            : this(fontPath, defPosition, Color.White, startPoint, text, scale, maxChars) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIEditableText"/> class.
        /// </summary>
        /// <param name="fontPath">
        /// The path to the font that will be used to display the text.
        /// </param>
        /// <param name="defPosition">
        /// The vector that represents position specified for 1920x1080 resolution.
        /// </param>
        /// <param name="color">
        /// The color of the element.
        /// </param>
        /// <param name="startPoint">
        /// The place where <paramref name="defPosition"/> has been specified.
        /// </param>
        /// <param name="text">
        /// The text to display.<br/>
        /// Defaults to empty string.
        /// </param>
        /// <param name="scale">
        /// The scale of the text.<br/>
        /// Defaults to 1.0f.
        /// </param>
        /// <param name="maxChars">
        /// The maximum number of characters that can be entered.
        /// </param>
        /// <remarks>
        /// The color of the text is set to white.
        /// </remarks>
        public GUIEditableText(string fontPath, Vector2 defPosition, Color color, GUIStartPoint startPoint = GUIStartPoint.TopLeft, string text = "", float scale = 1, int maxChars = int.MaxValue)
            : base(fontPath, defPosition, color, startPoint, text, scale)
        {
            _cursor = new GUITexture("Images/TextCursor", Rectangle.Empty, GUIStartPoint.Center);
            _cursorStartPos = defPosition;
            _cursorStartPoint = startPoint;
            _maxChars = maxChars;
            ChangeCursorRect();
        }
        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether the text is selected.
        /// </summary>
        /// <remarks>
        /// The cursor is visible only when the text is selected.
        /// </remarks>
        public bool IsSelected { get; set; } = false;

        #region GUIElement Methods
        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (IsSelected)
            {
                if (DateTime.Now.Millisecond / 500 == 0)
                {
                    _cursor.Draw(spriteBatch);
                }
            }
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            base.Load(content);
            _cursor.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            base.Recalculate();
            // Idk why question mark must be here, but otherwise it doesn't work
            _cursor?.Recalculate();
        }
        #endregion

        /// <summary>
        /// Moves the cursor one character to the right.
        /// </summary>
        public void MoveCursorRight()
        {
            if (_cursorPosition != Text.Length)
            {
                _cursorPosition++;
                ChangeCursorRect();
            }
        }

        /// <summary>
        /// Moves the cursor one character to the left.
        /// </summary>
        public void MoveCursorLeft()
        {
            if (_cursorPosition != 0)
            {
                _cursorPosition--;
                ChangeCursorRect();
            }
        }

        /// <summary>
        /// Moves the cursor to the end of the text.
        /// </summary>
        public void MoveCursorToEnd()
        {
            _cursorPosition = Text.Length;
            ChangeCursorRect();
        }

        /// <summary>
        /// Moves the cursor to the beginning of the text.
        /// </summary>
        public void MoveCursorToHome()
        {
            _cursorPosition = 0;
            ChangeCursorRect();
        }

        /// <summary>
        /// Adds a character to the text.
        /// </summary>
        /// <remarks>
        /// The character is added at the position of the cursor.
        /// </remarks>
        /// <param name="c">
        /// The character to add.
        /// </param>
        public void AddChar(char c)
        {
            if (Text.Length < _maxChars)
            {
                TextBuilder.Insert(_cursorPosition, c);
                _cursorPosition++;
                ChangeCursorRect();
            }
        }

        /// <summary>
        /// Removes the character before the cursor.
        /// </summary>
        public void RemovePreviousChar()
        {
            if (_cursorPosition != 0)
            {
                TextBuilder.Remove(_cursorPosition - 1, 1);
                _cursorPosition--;
                if (_cursorPosition < 0)
                {
                    _cursorPosition = 0;
                }
            }
            ChangeCursorRect();
        }

        /// <summary>
        /// Removes the character after the cursor.
        /// </summary>
        public void RemoveNextChar()
        {
            if (_cursorPosition != Text.Length)
            {
                TextBuilder.Remove(_cursorPosition, 1);
                ChangeCursorRect();
            }
        }

        /// <summary>
        /// Updates the cursor rectangle.
        /// </summary>
        private void ChangeCursorRect()
        {
            int offset = 0;
            for (int i = 0; i < _cursorPosition; i++)
            {
                offset += GetTextCharLenght(i);
            }
            Vector2 pos = _cursorStartPos;
            var newRect = new Rectangle((int)pos.X + offset, (int)pos.Y, (int)(5 * Scale), (int)(100 * Scale));
            _cursor.SetNewDefDstRectangle(newRect, _cursorStartPoint);
        }

        /// <summary>
        /// Returns the length of the character at the specified index.
        /// </summary>
        /// <param name="index">
        /// The index of the character.
        /// </param>
        /// <returns>
        /// The length of the character in pixels, scaled to the current resolution.
        /// </returns>
        private int GetTextCharLenght(int index)
        {
            return (int)(Font.MeasureString(Text[index].ToString()).X * Scale * 1920) / ScreenController.Width;
        }
    }
}
