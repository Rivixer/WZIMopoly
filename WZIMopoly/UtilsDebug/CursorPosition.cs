using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace WZIMopoly.DebugUtils
{
    static class ShowCursorPosition
    {
        // Change if you want to show the cursor position
        public static ShowPlace ShowCursorPos = ShowPlace.None;

        private readonly static List<DrawString> s_toDraw = new();
        private static SpriteFont s_font;

        private static Vector2 GetInfoPosition()
        {
            return new Vector2(10, 10 + 30 * s_toDraw.Count);
        }

        public static void Draw(SpriteBatch spriteBatch, ContentManager content)
        {
            s_font ??= content.Load<SpriteFont>("Fonts/DebugFont");

            if (ShowCursorPos != ShowPlace.None)
            {
                var cursorPosition = Mouse.GetState().Position;
                var info = $"Cursor position: {cursorPosition}";
                if (ShowCursorPos.HasFlag(ShowPlace.Console))
                {
                    Debug.WriteLine(info);
                }
                if (ShowCursorPos.HasFlag(ShowPlace.Screen))
                {
                    var cls = new DrawString(s_font, info, GetInfoPosition(), Color.White);
                    s_toDraw.Add(cls);
                }
            }
            foreach (var cls in s_toDraw)
            {
                cls.Draw(spriteBatch);
            }
            s_toDraw.Clear();
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
