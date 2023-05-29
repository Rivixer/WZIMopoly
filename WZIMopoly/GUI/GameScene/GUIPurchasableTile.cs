using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
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
        /// <summary>
        /// The card info texture.
        /// </summary>
        private readonly GUITexture _card;

#nullable enable
        /// <summary>
        /// The time since the tile is hovered.
        /// </summary>
        private DateTime? _hoverTime;
#nullable disable

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIPurchasableTile"/> class.
        /// </summary>
        /// <inheritdoc/>
        internal GUIPurchasableTile(XmlNode node, TileModel model)
            : base(node, model)
        {
            var fileName = NamingConverter.ConvertShitToFileNames(model.EnName);
            _card = new GUITexture($"Images/Cards/{fileName}", new(0, 0, 550, 900));
        }

        /// <summary>
        /// Gets the value whether the tile is hovered.
        /// </summary>
        private bool IsHovered => MouseController.IsHover(Position.ToCurrentResolution());

        /// <summary>
        /// Gets the value whether the info card should be visible.
        /// </summary>
        /// <remarks>
        /// It is visible when <see cref="_hoverTime"/> has been set more than half a second ago.
        /// </remarks>
        private bool InfoVisible => _hoverTime + TimeSpan.FromSeconds(0.5) < DateTime.Now;

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _card.Load(content);
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (InfoVisible)
            {
                _card.Draw(spriteBatch);
            }
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();

            if (IsHovered)
            {
                _hoverTime ??= DateTime.Now;
                UpdateCardPosition();
            }
            else
            {
                _hoverTime = null;
            }
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _card.Recalculate();
        }

        /// <summary>
        /// Updates the card position.
        /// </summary>
        private void UpdateCardPosition()
        {
            var size = new Point(275, 450).ToCurrentResolution();
            var rect = new Rectangle(MouseController.Position, size);
            var startPoint = (_model.Id / 10) switch
            {
                0 => GUIStartPoint.BottomLeft,
                1 => GUIStartPoint.TopLeft,
                2 => GUIStartPoint.TopRight,
                3 => GUIStartPoint.BottomRight,
                _ => GUIStartPoint.Center,
            };
            _card.SetNewDstRectangle(rect, startPoint);
        }
    }
}
