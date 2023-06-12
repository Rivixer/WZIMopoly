using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.TileModels;
using WZIMopoly.Utils.PositionExtensions;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a view of the purchasable tile.
    /// </summary>
    internal class GUIPurchasableTile : GUITile, IGUIGameUpdate
    {
        /// <summary>
        /// The info card texture.
        /// </summary>
        private readonly GUITileCard _card;

#nullable enable
        /// <summary>
        /// The time since the tile is hovered.
        /// </summary>
        private DateTime? _hoverTime;
#nullable disable

        /// <summary>
        /// The player that is currently playing.
        /// </summary>
        private PlayerModel _player;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIPurchasableTile"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node that contains the tile data.
        /// </param>
        /// <param name="model">
        /// The model of the tile.
        /// </param>
        /// <param name="tileCard">
        /// The card of the tile.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The XML tile data is invalid.
        /// </exception>
        internal GUIPurchasableTile(XmlNode node, PurchasableTileModel model, TileCardController tileCard)
            : base(node, model)
        {
            _card = tileCard.View;
        }

        /// <summary>
        /// Gets the purchasable tile model.
        /// </summary>
        protected new PurchasableTileModel Model => (PurchasableTileModel)base.Model;

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
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (InfoVisible 
                && _player.PlayerStatus != PlayerStatus.MortgagingTiles 
                && _player.PlayerStatus != PlayerStatus.UpgradingTiles
                && _player.PlayerStatus != PlayerStatus.Trading
                && _player.PlayerStatus != PlayerStatus.SavingFromBankruptcy)
            {
                _card.IsVisible = true;
            }
            else
            {
                _card.IsVisible = false;
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
            }
            else
            {
                _hoverTime = null;
            }
        }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            _player = player;
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
            if (Model.Owner is not null)
            {
                _card.OwnerOnCard.Text = WZIMopoly.Language switch
                {
                    Language.Polish => $"Właściciel: {Model.Owner.Nick}",
                    Language.English => $"Owner: {Model.Owner.Nick}",
                    _ => throw new ArgumentException($"{WZIMopoly.Language} language is not implemented for card.")
                };
            }
            else
                _card.OwnerOnCard.Text = "";
        }
    }
}
