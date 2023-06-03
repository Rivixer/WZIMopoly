using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    internal class GUISubjectTile : GUIPurchasableTile
    {
        /// <summary>
        /// The grade on the tile.
        /// </summary>
        private GUIText _grade;

        internal GUISubjectTile(XmlNode node, TileModel model) : base(node, model)
        {
            _grade = _orientation switch
            {
                TileOrientation.Vertical => new GUIText("Fonts/WZIMFont", new Vector2(Position.Center.X, Position.Top + 11), Color.Black, GUIStartPoint.Center, "3", 0.3f),
                TileOrientation.HorizontalLeft => new GUIText("Fonts/WZIMFont", new Vector2(Position.Right - 12, Position.Center.Y), Color.Black, GUIStartPoint.Center, "3", 0.3f, 1.57f),
                TileOrientation.HorizontalRight => new GUIText("Fonts/WZIMFont", new Vector2(Position.Left + 14, Position.Center.Y), Color.Black, GUIStartPoint.Center, "3", 0.3f, 1.57f),
                _ => throw new ArgumentException($"Displaying a grade is implemented only for vertical and horizontal tiles.")
            };
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _grade.Load(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            _grade.Draw(spriteBatch);
        }

        public override void Recalculate()
        {
            base.Recalculate();
            _grade.Recalculate();
        }
    }
}
