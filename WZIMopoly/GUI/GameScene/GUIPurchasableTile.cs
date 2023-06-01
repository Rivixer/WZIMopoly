using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.TileModels;
using WZIMopoly.Utils;
using WZIMopoly.Utils.PositionExtensions;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a view of the purchasable tile.
    /// </summary>
    internal class GUIPurchasableTile : GUITile
    {
        /// <summary>
        /// The info card texture.
        /// </summary>
        private readonly GUITexture _card;

        /// <summary>
        /// The text on card which represents the owner of the tile.
        /// </summary>
        private GUIText _ownerOnCard;

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
            var fileName = NamingConverter.ConvertXMLNamesToFileNames(model.EnName);
            _card = new GUITexture($"Images/Cards/{fileName}", new(0, 0, 550, 900));

            _ownerOnCard = new GUIText("Fonts/WZIMFont", new Vector2(0), Color.Black, GUIStartPoint.Center, scale: 0.3f);
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
            _ownerOnCard.Load(content);
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (InfoVisible)
            {
                _card.Draw(spriteBatch);
                _ownerOnCard.Draw(spriteBatch);
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
                UpdateOwnerOnCard();
                var pos = new Vector2(_card.UnscaledDestinationRect.Center.X, _card.UnscaledDestinationRect.Bottom - 35);
                _ownerOnCard.SetNewDefPosition(pos, GUIStartPoint.Center);
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
            _ownerOnCard.Recalculate();
        }

        /// <summary>
        /// Updates the card position.
        /// </summary>
        private void UpdateCardPosition()
        {
            var size = new Point(275, 450).ToCurrentResolution();
            var rect = new Rectangle(MouseController.Position, size);
            var startPoint = (Model.Id / 10) switch
            {
                0 => GUIStartPoint.BottomLeft,
                1 => GUIStartPoint.TopLeft,
                2 => GUIStartPoint.TopRight,
                3 => GUIStartPoint.BottomRight,
                _ => GUIStartPoint.Center,
            };
            _card.SetNewDstRectangle(rect, startPoint);
            _card.Recalculate();
        }

        /// <summary>
        /// Updates the text in <see cref="_ownerOnCard"/> which represents an owner name.
        /// </summary>
        private void UpdateOwnerOnCard()
        {
            if ((Model as PurchasableTileModel).Owner is not null)
            {
                _ownerOnCard.Text = WZIMopoly.Language switch
                {
                    Language.Polish => $"Wlasciciel: {(Model as PurchasableTileModel).Owner.Nick}",
                    Language.English => $"Owner: {(Model as PurchasableTileModel).Owner.Nick}",
                    _ => throw new ArgumentException($"{WZIMopoly.Language} language is not implemented for card.")
                };
            }
            else
                _ownerOnCard.Text = "";
        }
    }
}
