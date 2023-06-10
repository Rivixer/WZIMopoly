using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents the view of the trade.
    /// </summary>
    internal class GUITrade : GUIElement
    {
        /// <summary>
        /// The model of the trade.
        /// </summary>
        private readonly TradeModel _model;

        /// <summary>
        /// The list of red textures.
        /// </summary>
        /// <remarks>
        /// Contains the red textures with opacity 0.35f,
        /// localized at the position of the tile.
        /// </remarks>
        private readonly List<GUITexture> _redTileTextures = new();

        /// <summary>
        /// The list of blue textures.
        /// </summary>
        /// <remarks>
        /// Contains the blue textures with opacity 0.35f,
        /// localized at the position of the tile.
        /// </remarks>
        private readonly List<GUITexture> _blueTileTextures = new();

        /// <summary>
        /// The list of green textures.
        /// </summary>
        /// <remarks>
        /// Contains the green textures with opacity 0.35f,
        /// localized at the position of the tile.
        /// </remarks>
        private readonly List<GUITexture> _greenTileTextures = new();

        /// <summary>
        /// The list of yellow textures.
        /// </summary>
        /// <remarks>
        /// Contains the yellow textures with opacity 0.35f,
        /// localized at the position of the tile.
        /// </remarks>
        private readonly List<GUITexture> _yellowTileTextures = new();

        /// <summary>
        /// The list of yellow textures.
        /// </summary>
        /// <remarks>
        /// Contains the yellow textures with opacity 0.5f,
        /// localized at the position of the tile.
        /// </remarks>
        private readonly List<GUITexture> _blackTileTextures = new();

        /// <summary>
        /// The auxiliary text informing the player about the trade.
        /// </summary>
        private readonly GUIText _text;

        /// <summary>
        /// The auxiliary text informing the player
        /// about the value of the offeror.
        /// </summary>
        private readonly GUIText _offerorValueText;

        /// <summary>
        /// The auxiliary text informing the player
        /// about the value of the recipient.
        /// </summary>
        private readonly GUIText _recipientValueText;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUITrade"/> class.
        /// </summary>
        /// <param name="model"></param>
        public GUITrade(TradeModel model)
        {
            _model = model;
            _text = new GUIText("Fonts/WZIMFont", new Vector2(960, 720), Color.Black, GUIStartPoint.Center, scale: 0.3f);
            _offerorValueText = new GUIText("Fonts/WZIMFont", new Vector2(430, 360), Color.Black, GUIStartPoint.Left, scale: 0.3f);
            _recipientValueText = new GUIText("Fonts/WZIMFont", new Vector2(1490, 360), Color.Black, GUIStartPoint.Right, scale: 0.3f);

            foreach (TileController tile in _model.TileControllers)
            {
                var pos = tile.View.Position;
                var rect = new Rectangle(pos.X, pos.Y, pos.Width, pos.Height);

                var redTexture = new GUITexture("Images/Red", rect, opacity: 0.35f);
                _redTileTextures.Add(redTexture);

                var blueTexture = new GUITexture("Images/Blue", rect, opacity: 0.35f);
                _blueTileTextures.Add(blueTexture);

                var greenTexture = new GUITexture("Images/Green", rect, opacity: 0.35f);
                _greenTileTextures.Add(greenTexture);

                var yellowTexture = new GUITexture("Images/Yellow", rect, opacity: 0.35f);
                _yellowTileTextures.Add(yellowTexture);

                var blackTexture = new GUITexture("Images/Black", rect, opacity: 0.5f);
                _blackTileTextures.Add(blackTexture);
            }
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            var player = GameSettings.CurrentPlayer;
            if (player.PlayerStatus == PlayerStatus.Trading
                || player.PlayerStatus == PlayerStatus.ReceivingTrade)
            {
                if (WZIMopoly.GameType == GameType.Local
                   || WZIMopoly.GameType == GameType.Online && player.Equals(GameSettings.Client))
                {
                    var colored = new List<int>();
                    var offerorTilesMask = ColorTextures(_model.Offeror);
                    
                    foreach (var tile in _model.Offeror.PurchasedTiles)
                    {
                        if (_model.ChosenOfferorTiles.Select(x => x.Id).ToList().Contains(tile.Id))
                        {
                            Debug.WriteLine("EEE");
                            offerorTilesMask[tile.Id].Draw(spriteBatch);
                            offerorTilesMask[tile.Id].Draw(spriteBatch);
                            offerorTilesMask[tile.Id].Draw(spriteBatch);
                            if (!colored.Contains(tile.Id))
                            {
                                colored.Add(tile.Id);
                            }
                        }
                        if (player.PlayerStatus == PlayerStatus.Trading)
                        {
                            offerorTilesMask[tile.Id].Draw(spriteBatch);
                            if (!colored.Contains(tile.Id))
                            {
                                colored.Add(tile.Id);
                            }
                        }
                    }
                    if (_model.Recipient is not null)
                    {
                        var recipientTilesMask = ColorTextures(_model.Recipient);
                        foreach (var tile in _model.Recipient.PurchasedTiles)
                        {
                            if (_model.ChosenRecipientTiles.Select(x => x.Id).ToList().Contains(tile.Id))
                            {
                                recipientTilesMask[tile.Id].Draw(spriteBatch);
                                recipientTilesMask[tile.Id].Draw(spriteBatch);
                                recipientTilesMask[tile.Id].Draw(spriteBatch);
                                if (!colored.Contains(tile.Id))
                                {
                                    colored.Add(tile.Id);
                                }
                            }
                            if (player.PlayerStatus == PlayerStatus.Trading)
                            {
                                recipientTilesMask[tile.Id].Draw(spriteBatch);
                                if (!colored.Contains(tile.Id))
                                {
                                    colored.Add(tile.Id);
                                }
                            }
                        }
                        _offerorValueText.Draw(spriteBatch);
                        _recipientValueText.Draw(spriteBatch);
                    }
                    foreach (var tile in _model.TileControllers)
                    {
                        if (!colored.Contains(tile.Model.Id))
                        {
                            _blackTileTextures[tile.Model.Id].Draw(spriteBatch);
                        }
                    }
                }
                _text.Draw(spriteBatch);
            }
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            var player = GameSettings.CurrentPlayer;
            if (player.PlayerStatus != PlayerStatus.Trading && player.PlayerStatus != PlayerStatus.ReceivingTrade)
            {
                _text.Text = string.Empty;
                return;
            }
            if (WZIMopoly.GameType == GameType.Local
                   || WZIMopoly.GameType == GameType.Online && player.Equals(GameSettings.Client))
            {
                switch (player.PlayerStatus)
                {
                    case PlayerStatus.Trading:

                        if (_model.Recipient is null)
                        {
                            _text.Text = WZIMopoly.Language switch
                            {
                                Language.Polish => $"Wybierz gracza do wymiany. (kliknij jego wstążkę z nickiem)",
                                Language.English => $"Select a player to trade with. (click on their flag with nickname)",
                                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                            };
                        }
                        else
                        {
                            _text.Text = WZIMopoly.Language switch
                            {
                                Language.Polish => $"Wybierz pola do wymiany z {_model.Recipient.Nick}.",
                                Language.English => $"Choose tiles to trade with {_model.Recipient.Nick}.",
                                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                            };
                            _offerorValueText.Text = WZIMopoly.Language switch
                            {
                                Language.Polish => $"Twoja oferowana wartość: {_model.ChosenOfferorTilesValue} ECTS.",
                                Language.English => $"Your offered value: {_model.ChosenOfferorTilesValue} ECTS.",
                                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                            };
                            _recipientValueText.Text = WZIMopoly.Language switch
                            {
                                Language.Polish => $"Oferowana wartość {_model.Recipient.Nick}: {_model.ChosenRecipientTilesValue} ECTS.",
                                Language.English => $"{_model.Recipient.Nick} value offered: {_model.ChosenRecipientTilesValue} ECTS.",
                                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                            };
                        }
                        break;
                    case PlayerStatus.ReceivingTrade:
                        _text.Text = WZIMopoly.Language switch
                        {
                            Language.Polish => $"{_model.Offeror.Nick} oferuję ci wymianę. Twoim kolorem są zaznaczone pola, które chce otrzymać.",
                            Language.English => $"{_model.Offeror.Nick} offers you a trade. The tiles they want to get are marked with your color.",
                            _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                        };
                        _recipientValueText.Text = WZIMopoly.Language switch
                        {
                            Language.Polish => $"Twoja oferowana wartość: {_model.ChosenRecipientTilesValue} ECTS.",
                            Language.English => $"Your offered value: {_model.ChosenRecipientTilesValue} ECTS.",
                            _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                        };
                        _offerorValueText.Text = WZIMopoly.Language switch
                        {
                            Language.Polish => $"Oferowana wartość {_model.Offeror.Nick}: {_model.ChosenOfferorTilesValue} ECTS.",
                            Language.English => $"{_model.Offeror.Nick} value offered: {_model.ChosenOfferorTilesValue} ECTS.",
                            _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                        };
                        break;
                    default:
                        _text.Text = string.Empty;
                        _offerorValueText.Text = string.Empty;
                        _recipientValueText.Text = string.Empty;
                        break;
                }
            }
            else
            {
                _text.Text = WZIMopoly.Language switch
                {
                    Language.Polish => $"{player.Nick} myśli nad wymianą...",
                    Language.English => $"{player.Nick} is thinking about a trade...",
                    _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                };
            }
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _redTileTextures.ForEach(x => x.Load(content));
            _blueTileTextures.ForEach(x => x.Load(content));
            _greenTileTextures.ForEach(x => x.Load(content));
            _yellowTileTextures.ForEach(x => x.Load(content));
            _blackTileTextures.ForEach(x => x.Load(content));
            _text.Load(content);
            _offerorValueText.Load(content);
            _recipientValueText.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _redTileTextures.ForEach(x => x.Recalculate());
            _blueTileTextures.ForEach(x => x.Recalculate());
            _greenTileTextures.ForEach(x => x.Recalculate());
            _yellowTileTextures.ForEach(x => x.Recalculate());
            _blackTileTextures.ForEach(x => x.Recalculate());
            _text.Recalculate();
            _offerorValueText.Recalculate();
            _recipientValueText.Recalculate();
        }

        /// <summary>
        /// Returns the textures for the given player's color.
        /// </summary>
        /// <param name="player">
        /// The player to get the textures for.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// The player's color is invalid.
        /// </exception>
        private List<GUITexture> ColorTextures(PlayerModel player)
        {
            return player.Color switch
            {
                "Red" => _redTileTextures,
                "Blue" => _blueTileTextures,
                "Green" => _greenTileTextures,
                "Yellow" => _yellowTileTextures,
                _ => throw new ArgumentException("Invalid color")
            };
        }
    }
}
