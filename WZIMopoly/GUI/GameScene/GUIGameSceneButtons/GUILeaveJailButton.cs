using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the leave jail button.
    /// </summary>
    internal class GUILeaveJailButton : GUIButton<LeaveJailButtonModel>, IGUIGameUpdate
    {
        /// <summary>
        /// The auxiliary text informing the player about the situation in the jail.
        /// </summary>
        private readonly GUIText _text;

        /// <summary>
        /// The player that is currently taking their turn.
        /// </summary>
        private PlayerModel _currentPlayer;

        /// <summary>
        /// The tile that the player is currently on.
        /// </summary>
        private TileModel _currentTile;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUILeaveJailButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the leave jail button.
        /// </param>
        public GUILeaveJailButton(LeaveJailButtonModel model)
            : base(model, new Rectangle(860, 923, 256, 88), GUIStartPoint.Right)
        {
            SetButtonHoverArea(5, 0.75f);
            _text = new GUIText("Fonts/WZIMFont", new Vector2(960, 720), Color.Black, GUIStartPoint.Center, scale: 0.3f);
        }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            _currentPlayer = player;
            _currentTile = tile;
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_currentTile is MandatoryLectureTileModel t)
            {
                if (t.IsPrisoner(_currentPlayer))
                {
                    base.Draw(spriteBatch);
                }
                _text.Draw(spriteBatch);
            }
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            base.Load(content);
            _text.Load(content);
        }

        /// <remarks>
        /// <para>
        /// This method is called one per frame.
        /// </para>
        /// <para>
        /// Sets the auxiliary text informing the player about the situation in the jail
        /// based on the cursor position and the player's status.
        /// </para>
        /// </remarks>
        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            if (_currentPlayer.PlayerStatus == PlayerStatus.BeforeRollingDice && _currentTile is MandatoryLectureTileModel t)
            {
                if (IsHovered && t.IsPrisoner(_currentPlayer))
                {
                    if (!t.CanPrisonerPayForRelease(_currentPlayer))
                    {
                        _text.Text = WZIMopoly.Language switch
                        {
                            Language.Polish => $"Biednego studenta nie stać, potrzebujesz {t.PayForLeave}ECTS.",
                            Language.English => $"You cannot afford the deposit, you need {t.PayForLeave}ECTS.",
                            _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                        };
                    }
                    else
                    {
                        _text.Text = WZIMopoly.Language switch
                        {
                            Language.Polish => $"Zapłać {t.PayForLeave}ECTS, aby wymknaąć się z wykładu.",
                            Language.English => $"Pay {t.PayForLeave}ECTS to get out of mandatory lecture.",
                            _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                        };
                    }
                }
                else if (t.IsPrisoner(_currentPlayer))
                {
                    var turnsLeft = t.GetRemainingTurns(_currentPlayer);
                    _text.Text = WZIMopoly.Language switch
                    {
                        Language.Polish => turnsLeft == 1 ? "Została 1 kolejka czekania." : $"Zostały {turnsLeft} kolejki czekania.",
                        Language.English => $"{turnsLeft} turns left.",
                        _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                    };
                    _text.Text += WZIMopoly.Language switch
                    {
                        Language.Polish => " Wyrzuć dublet, aby wyjść wcześniej.",
                        Language.English => " Roll a double to leave early.",
                        _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                    };
                }
                else
                {
                    _text.Text = WZIMopoly.Language switch
                    {
                        Language.Polish => "Koniec wykładu!",
                        Language.English => $"The lecture has been ended!",
                        _ => string.Empty,
                    };
                }
            }
            else
            {
                _text.Text = string.Empty;
            }
        }
    }
}
