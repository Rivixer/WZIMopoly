using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.TileModels;
using WZIMopoly.Utils.PositionExtensions;

#nullable enable

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents the upgrade tiles view.
    /// </summary>
    internal class GUIUpgrade : GUIElement, IGUIGameUpdate
    {
        /// <summary>
        /// The model of the upgrade tiles.
        /// </summary>
        private readonly UpgradeModel _model;

        /// <summary>
        /// The textures of all tiles.
        /// </summary>
        /// <remarks>
        /// Contains the black textures with opacity 0.5f,
        /// localized at the position of the tile.
        /// </remarks>
        private readonly List<GUITexture> _tileTextures;

        /// <summary>
        /// The auxiliary text informing the player about the upgrade.
        /// </summary>
        /// <remarks>
        /// The text refers to the tile that the mouse is hovering over.
        /// </remarks>
        private readonly GUIText _text;

        /// <summary>
        /// The list of tile ids that the player cannot upgrade.
        /// </summary>
        private List<int> _nonUpgradeableTileIds;

        /// <summary>
        /// The player that is currently upgrading the fields.
        /// </summary>
        private PlayerModel? _player;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIUpgrade"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the upgrade tiles.
        /// </param>
        public GUIUpgrade(UpgradeModel model)
        {
            _model = model;
            _tileTextures = new List<GUITexture>();
            _nonUpgradeableTileIds = new List<int>();
            foreach (TileController tile in _model.TileControllers)
            {
                var pos = tile.View.Position;
                var rect = new Rectangle(pos.X, pos.Y, pos.Width, pos.Height);
                var texture = new GUITexture("Images/Black", rect, opacity: 0.5f);
                _tileTextures.Add(texture);
            }
            _text = new GUIText("Fonts/WZIMFont", new Vector2(960, 720), Color.Black, GUIStartPoint.Center, scale: 0.3f);
        }

        /// <summary>
        /// Updates the list of the tiles that the player cannot upgrade.
        /// </summary>
        /// <remarks>
        /// Based on this list, a mask is applied
        /// to the fields that the player cannot upgrade.
        /// </remarks>
        public void UpdateMask()
        {
            _nonUpgradeableTileIds = _model.GetTileIdsThatPlayerCannotUpgrade(_model.CurrentPlayer);
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_player?.PlayerStatus == PlayerStatus.UpgradingTiles)
            {
                foreach (var id in _nonUpgradeableTileIds)
                {
                    _tileTextures[id].Draw(spriteBatch);
                }
                _text.Draw(spriteBatch);
            }
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _tileTextures.ForEach(x => x.Load(content));
            _text.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _tileTextures.ForEach(x => x.Recalculate());
            _text.Recalculate();
        }

        /// <inheritdoc/>
        public override void Update()
        {
            UpdateText();
        }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            _player = player;
        }

        /// <summary>
        /// Updates the auxiliary text informing the player about the upgrade.
        /// </summary>
        private void UpdateText()
        {
            if (_player?.PlayerStatus == PlayerStatus.UpgradingTiles)
            {
                string text;
                SubjectTileModel? t = null;
                foreach (TileController tile in _model.TileControllers)
                {
                    if (tile.Model is SubjectTileModel
                        && MouseController.IsHover(tile.View.Position.ToCurrentResolution()))
                    {
                        t = tile.Model as SubjectTileModel;
                        break;
                    }
                }

                PlayerModel player = _model.CurrentPlayer;
                // TODO: Add localization
                if (t == null)
                {
                    text = "Wybierz pole do ulepszenia.";
                }
                else if (!player.PurchasedTiles.Contains(t))
                {
                    text = $"Nie jestes wlascicielem pola {t.EnName}.";
                }
                else if (t.UpgradePrice > player.Money)
                {
                    text = $"Nie stac Cie na ulepszenie pola {t.EnName}. (koszt {t.UpgradePrice}ECTS";
                }
                else if (t.IsMortgaged)
                {
                    text = $"Nie mozesz ulepszyc pola {t.EnName}, poniewaz jest zastawione.";
                }
                else if (!t.CanUpgrade(player))
                {
                    text = $"Musisz miec zakupione wszystkie pola koloru {t.Color}, aby ulepszyc {t.EnName}";
                }
                else
                {
                    text = $"Ulepsz pole {t.EnName} (koszt {t.UpgradePrice}ECTS)";
                }
                _text.Text = text;
            }
        }
    }
}
