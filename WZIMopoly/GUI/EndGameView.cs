using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using WZIMopoly.Enums;
using WZIMopoly.Models;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents the end game view.
    /// </summary>
    internal class EndGameView : GUIElement
    {
        /// <summary>
        /// The X position of the nick texts.
        /// </summary>
        private static readonly int s_nickPostionX = 666;

        /// <summary>
        /// The X position of the money texts.
        /// </summary>
        private static readonly int s_moneyPositionX = 1316;

        /// <summary>
        /// The list of Y positions for each row.
        /// </summary>
        private static readonly int[] s_linePositionYs = new int[4] { 314, 385, 456, 527 };

        /// <summary>
        /// The background of the end game scene.
        /// </summary>
        private readonly GUITexture _background;

        /// <summary>
        /// The texture with summary table of the game.
        /// </summary>
        private readonly GUITexture _summaryTable;

        /// <summary>
        /// The list of player nicks.
        /// </summary>
        private readonly List<GUIText> _playerNicks = new();

        /// <summary>
        /// The list of player values;
        /// </summary>
        private readonly List<GUIText> _playerValues = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="EndGameView"/> class.
        /// </summary>
        public EndGameView()
        {
            _background = new GUITexture("Images/SettingsBackgroundInGame", new Rectangle(0, 0, 1920, 1080));
            _summaryTable = new GUITexture("Images/Finished", new Rectangle(0, 0, 1920, 1080));

            for (int i = 0; i < 4; i++)
            {
                var nickText = new GUIText("Fonts/WZIMFont", new Vector2(s_nickPostionX, s_linePositionYs[i]), Color.Black, GUIStartPoint.Left, scale: i == 0 ? 0.5f : 0.45f);
                _playerNicks.Add(nickText);

                var moneyText = new GUIText("Fonts/WZIMFont", new Vector2(s_moneyPositionX, s_linePositionYs[i]), Color.Black, GUIStartPoint.Right, scale: i == 0 ? 0.5f : 0.45f);
                _playerValues.Add(moneyText);
            }
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            _summaryTable.Draw(spriteBatch);
            _playerNicks.ForEach(x => x.Draw(spriteBatch));
            _playerValues.ForEach(x => x.Draw(spriteBatch));
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _background.Load(content);
            _summaryTable.Load(content);
            _playerNicks.ForEach(x => x.Load(content));
            _playerValues.ForEach(x => x.Load(content));
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _background.Recalculate();
            _summaryTable.Recalculate();
            _playerNicks.ForEach(x => x.Recalculate());
            _playerValues.ForEach(x => x.Recalculate());
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            var players = new List<PlayerModel>(GameSettings.Players);
            players.Sort(new ComparePlayerValues());
            for (int i = 0; i < 4; i++)
            {
                var player = players[i];
                var nickSB = new StringBuilder($"{i + 1}. ");
                if (player.PlayerType == PlayerType.None)
                {
                    nickSB.Append('-');
                    _playerValues[i].Text = "-";
                }
                else
                {
                    nickSB.Append(player.Nick);
                    if (player.PlayerStatus == PlayerStatus.Bankrupt)
                    {
                        _playerValues[i].Text = WZIMopoly.Language switch
                        {
                            Language.Polish => "BANKRUT",
                            Language.English => "BANKRUPT",
                            _ => throw new NotImplementedException($"Language not implemented: {WZIMopoly.Language}")
                        };
                    }
                    else
                    {
                        _playerValues[i].Text = player.PlayerValue.ToString();
                    }
                }
                _playerNicks[i].Text = nickSB.ToString();
            }
        }
    }

    /// <summary>
    /// Represents a comparer for player values.
    /// </summary>
    internal class ComparePlayerValues : IComparer<PlayerModel>
    {
        /// <summary>
        /// Compares two player values.
        /// </summary>
        /// <param name="x">
        /// The first player.
        /// </param>
        /// <param name="y">
        /// The second player.
        /// </param>
        /// <returns>
        /// More than 0 if the first player has a higher value than the second,
        /// less than 0 if the first player has a lower value than the second,
        /// 0 if both players have the same value.
        /// </returns>
        int IComparer<PlayerModel>.Compare(PlayerModel x, PlayerModel y)
        {
            if (x.PlayerType == PlayerType.None && y.PlayerType == PlayerType.None)
            {
                return 0;
            }
            else if (x.PlayerType == PlayerType.None)
            {
                return 1;
            }
            else if (y.PlayerType == PlayerType.None)
            {
                return -1;
            }

            if (x.PlayerStatus == PlayerStatus.Bankrupt && y.PlayerStatus == PlayerStatus.Bankrupt)
            {
                return (int)(y.BankruptTime.Value.Ticks - x.BankruptTime.Value.Ticks);
            }
            else if (x.PlayerStatus == PlayerStatus.Bankrupt)
            {
                return 1;
            }
            else if (y.PlayerStatus == PlayerStatus.Bankrupt)
            {
                return -1;
            }

            return y.PlayerValue - x.PlayerValue;
        }
    }
}
