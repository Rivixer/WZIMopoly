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
    /// Represents the mortgage view.
    /// </summary>
    internal class GUIMortgage : GUIElement, IGUIGameUpdate
    {
        /// <summary>
        /// The model of the mortgage.
        /// </summary>
        private readonly MortgageModel _model;

        /// <summary>
        /// The textures of all tiles.
        /// </summary>
        /// <remarks>
        /// Contains the black textures with opacity 0.5f,
        /// localized at the position of the tile.
        /// </remarks>
        private readonly List<GUITexture> _tileTextures;

        /// <summary>
        /// The textures of the mortgaged tiles.
        /// </summary>
        /// <remarks>
        /// Contains the red textures with opacity 0.5f,
        /// localized at the position of the tile.
        /// </remarks>
        private readonly List<GUITexture> _mortgagedTileTextures;

        /// <summary>
        /// The auxiliary text informing the player about
        /// the tile mortgage or the sale of its grades.
        /// </summary>
        /// <remarks>
        /// The text refers to the tile that the mouse is hovering over.
        /// </remarks>
        private readonly GUIText _text;

        /// <summary>
        /// The list of tile ids that the player cannot
        /// mortgage or sell their grades.
        /// </summary>
        private List<int> _nonMortgageableTileIds;

        /// <summary>
        /// The list of tile ids that the player has mortgaged.
        /// </summary>
        private List<int> _mortgagedTileIds;

        /// <summary>
        /// The player that is currently mortgaging
        /// the fields or selling their grades.
        /// </summary>
        private PlayerModel? _player;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIMortgage"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the mortgage.
        /// </param>
        public GUIMortgage(MortgageModel model)
        {
            _model = model;

            _tileTextures = new List<GUITexture>();
            _mortgagedTileTextures = new List<GUITexture>();

            _nonMortgageableTileIds = new List<int>();
            _mortgagedTileIds = new List<int>();

            foreach (TileController tile in _model.TileControllers)
            {
                var pos = tile.View.Position;
                var rect = new Rectangle(pos.X, pos.Y - 1, pos.Width + 1, pos.Height + 1);

                var blackTexture = new GUITexture("Images/Black", rect, opacity: 0.5f);
                _tileTextures.Add(blackTexture);

                var redTexture = new GUITexture("Images/Red", rect, opacity: 0.5f);
                _mortgagedTileTextures.Add(redTexture);
            }

            _text = new GUIText("Fonts/WZIMFont", new Vector2(960, 720), Color.Black, GUIStartPoint.Center, scale: 0.3f);
        }

        /// <summary>
        /// Updates the list of the tiles that the player
        /// cannot mortgage or sell thier grades.
        /// </summary>
        /// <remarks>
        /// Based on this list, a mask is applied
        /// to the fields that the player 
        /// cannot mortgage or sell thier grades.
        /// </remarks>
        public void UpdateMask()
        {
            _nonMortgageableTileIds = _model.GetTileIdsThatPlayerCannotMortgage(_model.CurrentPlayer).ToList();
            _mortgagedTileIds = _model.CurrentPlayer.MortgagedTiles.Select(x => x.Id).ToList();
            _nonMortgageableTileIds.RemoveAll(x => _mortgagedTileIds.Contains(x));
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_player?.PlayerStatus == PlayerStatus.MortgagingTiles)
            {
                _nonMortgageableTileIds.ForEach(x => _tileTextures[x].Draw(spriteBatch));
                _mortgagedTileIds.ForEach(x => _mortgagedTileTextures[x].Draw(spriteBatch));
                _text.Draw(spriteBatch);
            }
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _tileTextures.ForEach(x => x.Load(content));
            _mortgagedTileTextures.ForEach(x => x.Load(content));
            _text.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _tileTextures.ForEach(x => x.Recalculate());
            _mortgagedTileTextures.ForEach(x => x.Recalculate());
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
            if (_player?.PlayerStatus == PlayerStatus.MortgagingTiles)
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
                    text = "Wybierz pole do zastawienia lub sprzedania oceny.";
                }
                else if (!player.PurchasedTiles.Contains(t))
                {
                    text = $"Nie jestes wlascicielem pola {t.EnName}.";
                }
                else if (t.CanUnmortgage(player))
                {
                    text = $"Odkup {t.EnName} za {t.MortgagePrice}ECTS.";
                }
                else if (t.CanSellGrade(player))
                {
                    var grade = t.Grade;
                    SubjectGrade lowerGrade = grade - 1;
                    // TODO: Convert SubjectGrade to a number
                    var sellPrice = t.SellGradePrice;
                    text = $"Obniz ocene {t.EnName} z {grade} do {lowerGrade} i zyskaj {sellPrice}ECTS.";
                }
                else if (t.CanMortgage(player))
                {
                    text = $"Zastaw {t.EnName} za {t.MortgagePrice}ECTS.";
                }
                else
                {
                    text = $"Nie stac Cie na odkupienie pola {t.EnName}. (koszt {t.MortgagePrice}ECTS)";
                }
                _text.Text = text;
            }
        }
    }
}
