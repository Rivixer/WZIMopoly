using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
            PlayerModel? player = _model.CurrentPlayer;
            if (player is not null && player.PlayerStatus == PlayerStatus.UpgradingTiles)
            {
                string text;
                if (!player.Equals(GameSettings.Client))
                {
                    text = WZIMopoly.Language switch
                    {
                        Language.Polish => $"{player.Nick} ulepsza swoje pola...",
                        Language.English => $"{player.Nick} is upgrading their tiles...",
                        _ => throw new ArgumentException($"{WZIMopoly.Language} language is not supported."),
                    };
                }
                else
                {
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
                    if (t == null)
                    {
                        text = WZIMopoly.Language switch
                        {
                            Language.Polish => "Wybierz pole do ulepszenia.",
                            Language.English => "Choose tile to upgrade.",
                            _ => throw new ArgumentException($"{WZIMopoly.Language} language is not implemented.")
                        };
                    }
                    else if (!player.PurchasedTiles.Contains(t))
                    {
                        text = WZIMopoly.Language switch
                        {
                            Language.Polish => $"Nie jesteś właścicielem pola {t.PlName}.",
                            Language.English => $"You are not an owner of {t.EnName}.",
                            _ => throw new ArgumentException($"{WZIMopoly.Language} language is not implemented.")
                        };
                    }
                    else if (t.UpgradePrice > player.Money)
                    {
                        text = WZIMopoly.Language switch
                        {
                            Language.Polish => $"Nie stać Cię na ulepszenie pola {t.PlName}. (koszt {t.UpgradePrice}ECTS)",
                            Language.English => $"You cannot afford to upgrade tile {t.EnName}. (price {t.UpgradePrice}ECTS",
                            _ => throw new ArgumentException($"{WZIMopoly.Language} language is not implemented.")
                        };
                    }
                    else if (t.IsMortgaged)
                    {
                        text = WZIMopoly.Language switch
                        {
                            Language.Polish => $"Nie możesz ulepszyc pola {t.PlName}, ponieważ jest zastawione.",
                            Language.English => $"You cannot upgrade tile {t.EnName}, because it has been pawned.",
                            _ => throw new ArgumentException($"{WZIMopoly.Language} language is not implemented.")
                        };
                    }
                    else if (!t.CanUpgrade(player))
                    {
                        text = WZIMopoly.Language switch
                        {
                            Language.Polish => $"Musisz mieć zakupione wszystkie pola koloru {t.Color}, aby ulepszyć {t.PlName}.",
                            Language.English => $"You have to buy every tile of color {t.Color} to upgrade {t.EnName}.",
                            _ => throw new ArgumentException($"{WZIMopoly.Language} language is not implemented.")
                        };
                    }
                    else
                    {
                        text = WZIMopoly.Language switch
                        {
                            Language.Polish => $"Ulepsz pole {t.PlName} (koszt {t.UpgradePrice}ECTS)",
                            Language.English => $"Upgrade tile {t.EnName} (price {t.UpgradePrice}ECTS)",
                            _ => throw new ArgumentException($"{WZIMopoly.Language} language is not implemented.")
                        };
                    }
                }
                _text.Text = text;
            }
        }
    }
}
