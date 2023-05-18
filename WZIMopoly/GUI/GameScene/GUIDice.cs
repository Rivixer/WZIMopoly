﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents the dice GUI element.
    /// </summary>
    /// <remarks>
    /// Shows the last roll of the dice on the screen.
    /// </remarks>
    internal class GUIDice : GUIElement, IGUIGameUpdate
    {
        /// <summary>
        /// List of textures for the first dice.
        /// </summary>
        private readonly List<GUITexture> _firstDiceTextures;

        /// <summary>
        /// List of textures for the second dice.
        /// </summary>
        private readonly List<GUITexture> _secondDiceTextures;

        /// <summary>
        /// The model of the dice.
        /// </summary>
        private readonly DiceModel _model;

        /// <summary>
        /// The player who is now taking a turn.
        /// </summary>
        private PlayerModel _currentPlayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIDice"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the dice.
        /// </param>
        internal GUIDice(DiceModel model)
        {
            _model = model;
            _firstDiceTextures = new List<GUITexture>();
            _secondDiceTextures = new List<GUITexture>();

            var rect1 = new Rectangle(660, 540, 160, 160);
            var rect2 = new Rectangle(1260, 540, 160, 160);
            for (int i = 0; i < 6; i++)
            {
                var text = $"Images\\Dice{i + 1}";
                _firstDiceTextures.Add(new GUITexture(text, rect1, GUIStartPoint.Center));
                _secondDiceTextures.Add(new GUITexture(text, rect2, GUIStartPoint.Center));
            }
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_currentPlayer.PlayerStatus == PlayerStatus.AfterRollingDice)
            {
                _firstDiceTextures[_model.LastRoll.Item1 - 1].Draw(spriteBatch);
                _secondDiceTextures[_model.LastRoll.Item2 - 1].Draw(spriteBatch);
            }
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _firstDiceTextures.ForEach(x => x.Load(content));
            _secondDiceTextures.ForEach(x => x.Load(content));
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _firstDiceTextures.ForEach(x => x.Recalculate());
            _secondDiceTextures.ForEach(x => x.Recalculate());
        }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            _currentPlayer = player;
        }
    }
}
