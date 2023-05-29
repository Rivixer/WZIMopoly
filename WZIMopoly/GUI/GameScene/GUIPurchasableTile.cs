using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Utils;
using WZIMopoly.Utils.PositionExtensions;

namespace WZIMopoly.GUI.GameScene
{
    internal class GUIPurchasableTile : GUITile
    {
        private GUITexture _card;

        internal GUIPurchasableTile(XmlNode node, TileModel model)
            : base(node, model)
        {
            var fileName = NamingConverter.ConvertShitToFileNames(model.EnName);
            _card = new GUITexture($"Images/Cards/{fileName}", new(0, 0, 550, 900));
            _card.IsVisible = false;
        }

        /// <summary>
        /// Whether the mouse cursor is in the tile's area.
        /// </summary>
        public bool IsHovered => MouseController.IsHover(Position.ToCurrentResolution());

        public void DrawCard()
        {
            var rect = new Rectangle(MouseController.Position.X, MouseController.Position.Y, 275, 450);

            if (rect.X + rect.Width <= ScreenController.Width)
            {
                if (rect.Y + rect.Height <= ScreenController.Height)
                {
                    _card.SetNewDefDstRectangle(rect, GUIStartPoint.TopLeft);
                }
                else
                {
                    _card.SetNewDefDstRectangle(rect, GUIStartPoint.BottomLeft);
                }
            }
            else
            {
                if (rect.Y + rect.Height <= ScreenController.Height)
                {
                    _card.SetNewDefDstRectangle(rect, GUIStartPoint.TopRight);
                }
                else
                {
                    _card.SetNewDefDstRectangle(rect, GUIStartPoint.BottomRight);
                }
            }
            _card.IsVisible = true;
        }

        public override void Load(ContentManager content)
        {
            _card.Load(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _card.Draw(spriteBatch);
        }

        public override void Recalculate()
        {
            _card.Recalculate();
        }
    }
}
