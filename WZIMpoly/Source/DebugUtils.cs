#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
#endregion

namespace WZIMpoly.DebugUtils
{
    static class DebugUtils
    {
        // Change if you want to show the cursor position
        public static ShowPlace ShowCursorPosition = ShowPlace.None;

        private readonly static List<DrawString> _toDraw = new();
        private static SpriteFont _font;

        private static Vector2 GetInfoPosition()
        {
            return new Vector2(10, 10 + 30 * _toDraw.Count);
        }

        public static void Draw(SpriteBatch spriteBatch, ContentManager content)
        {
            _font ??= content.Load<SpriteFont>("Fonts/DebugFont");

            if (ShowCursorPosition != ShowPlace.None)
            {
                var cursorPosition = Mouse.GetState().Position;
                var info = $"Cursor position: {cursorPosition}";
                if (ShowCursorPosition.HasFlag(ShowPlace.Console))
                {
                    Debug.WriteLine(info);
                }
                if (ShowCursorPosition.HasFlag(ShowPlace.Screen))
                { 
                    var cls = new DrawString(_font, info, GetInfoPosition(), Color.White);
                    _toDraw.Add(cls);
                }
            }
            foreach(var cls in _toDraw)
            {
                cls.Draw(spriteBatch);
            }
            _toDraw.Clear();
        }
        [Flags]
        public enum ShowPlace
        {
            None = 0,
            Console = 1 << 0,
            Screen = 1 << 1,
            Both = Console | Screen
        }
        private class DrawString
        {
            private readonly SpriteFont _font;
            private readonly string _text;
            private readonly Vector2 _position;
            private readonly Color _color;

            internal DrawString(SpriteFont font, string text, Vector2 position, Color color)
            {
                _font = font;
                _text = text;
                _position = position;
                _color = color;
            }

            internal void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.DrawString(_font, _text, _position, _color);
            }
        }
    }
}
