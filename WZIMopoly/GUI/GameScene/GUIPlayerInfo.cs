using Microsoft.Xna.Framework;
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
    /// Represents a player info view.
    /// </summary>
    /// <remarks>
    /// A player info view contains the player's
    /// nickname and amount of money.
    /// </remarks>
    internal class GUIPlayerInfo : GUIElement
    {
        #region Fields
        /// <summary>
        /// A dictionary that contains shift directions for each
        /// of the four corner GUI start points.
        /// </summary>
        /// <remarks>
        /// The shift directions are used to determine the offset for certain
        /// GUI elements based on the position of the GUI start point.
        /// </remarks>
        private static readonly Dictionary<GUIStartPoint, (int X, int Y)> ShiftDirections = new()
        {
            { GUIStartPoint.TopLeft, new(-1, -1) },
            { GUIStartPoint.TopRight, new(1, -1) },
            { GUIStartPoint.BottomLeft, new(-1, 1) },
            { GUIStartPoint.BottomRight, new(1, 1) },
        };

        /// <summary>
        /// The view of the flag texture.
        /// </summary>
        /// <remarks>
        /// It is the background of the player's nickname.
        /// </remarks>
        private readonly GUITexture _guiFlag;

        /// <summary>
        /// The view of the flag texture during player's round.
        /// </summary>
        /// <remarks>
        /// It is also the background of the player's nickname.
        /// </remarks>
        private readonly GUITexture _guiFlagHovered;

        /// <summary>
        /// The view of the box texture.
        /// </summary>
        /// <remarks>
        /// It is the background of the amount of money, near the flag texture.
        /// </remarks>
        private readonly GUITexture _guiBox;

        /// <summary>
        /// The view of the player's nickname text.
        /// </summary>
        private readonly GUIText _guiNick;

        /// <summary>
        /// The view of the player's amount of money text.
        /// </summary>
        private readonly GUIText _guiMoney;

        /// <summary>
        /// The function that returns the current player.
        /// </summary>
        private readonly Func<PlayerModel> _getCurrentPlayer;

        /// <summary>
        /// The model of the player info.
        /// </summary>
        private readonly PlayerInfoModel _playerInfoModel;

        /// <summary>
        /// The default destination rectangle of the button.
        /// </summary>
        /// <remarks>
        /// It specifies the position and size of the button.<br/>
        /// The X and Y coordinates refer to the top-left corner of the button.
        /// </remarks>
        private readonly Rectangle _defDstRect;

        /// <summary>
        /// The place where <see cref="_defDstRect"/> has been specified.
        /// </summary>
        private readonly GUIStartPoint _startPoint;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIPlayerInfo"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the player info.
        /// </param>
        /// <param name="getCurrentPlayer">
        /// The function that returns the current player.
        /// </param>
        internal GUIPlayerInfo(PlayerInfoModel model, Func<PlayerModel> getCurrentPlayer, Rectangle defDstRect, GUIStartPoint startPoint)
        {
            _playerInfoModel = model;
            _getCurrentPlayer = getCurrentPlayer;
            _defDstRect = defDstRect;
            _startPoint = startPoint;

            PlayerModel player = model.Player;

            _guiFlag = new GUITexture($"Images/PlayerFlag{player.Color}", _defDstRect, _startPoint);
            _guiFlagHovered = new GUITexture($"Images/PlayerFlag{player.Color}Hovered", _defDstRect, _startPoint);

            var boxRectangle = GetBoxRectangle(_guiFlag, _startPoint);
            _guiBox = new GUITexture($"Images/PlayerBox{player.Color}", boxRectangle, GUIStartPoint.Center);

            var nickPosition = GetPositionOfText(_guiFlag.UnscaledDestinationRect, _startPoint, 62, 33);
            _guiNick = new GUIText("Fonts/DebugFont", nickPosition, GUIStartPoint.Center, player.Nick, 2f);

            var moneyPosition = GetPositionOfText(_guiBox.UnscaledDestinationRect, _startPoint, 2, 0);
            _guiMoney = new GUIText("Fonts/DebugFont", moneyPosition, Color.Black, GUIStartPoint.Center, $"{player.Money} ECTS", 1.5f);
        }

        /// <inheritdoc/>
        internal override void Draw(SpriteBatch spriteBatch)
        {
            if (_getCurrentPlayer() == _playerInfoModel.Player)
            {
                _guiFlagHovered.Draw(spriteBatch);
            }
            else
            {
                _guiFlag.Draw(spriteBatch);
            }

            _guiBox.Draw(spriteBatch);
            _guiMoney.Draw(spriteBatch);
            _guiNick.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        internal override void Load(ContentManager content)
        {
            var elements = new List<GUIElement>() { _guiBox, _guiMoney, _guiNick, _guiFlag, _guiFlagHovered };
            elements.ForEach(x => x.Load(content));
        }

        /// <inheritdoc/>
        internal override void Recalculate()
        {
            var elements = new List<GUIElement>() { _guiBox, _guiMoney, _guiNick, _guiFlag, _guiFlagHovered };
            elements.ForEach(x => x.Recalculate());
        }

        /// <summary>
        /// Returns the rectangle of the box texture shifted from the center of the flag texture.
        /// </summary>
        /// <param name="guiFlag">
        /// The view of the flag texture.
        /// </param>
        /// <param name="startPoint">
        /// The start point of this view that determines the direction of the shift.
        /// </param>
        /// <returns>
        /// The rectangle shifted from the center of the flag texture.
        /// </returns>
        private static Rectangle GetBoxRectangle(GUITexture guiFlag, GUIStartPoint startPoint)
        {
            var offsetX = 140;
            var offsetY = -45;
            var rectangle = new Rectangle(guiFlag.UnscaledDestinationRect.Center.X, guiFlag.UnscaledDestinationRect.Center.Y, 220, 50);
            rectangle.X += ShiftDirections[startPoint].X * offsetX;
            rectangle.Y += ShiftDirections[startPoint].Y * offsetY;
            return rectangle;
        }

        /// <summary>
        /// Returns the position of the text shifted from another element.
        /// </summary>
        /// <param name="rect">
        /// The rectangle from which the text will be shifted from its center.
        /// </param>
        /// <param name="startPoint">
        /// The start point of this view that determines the direction of the shift.
        /// </param>
        /// <param name="offsetX">
        /// The horizontal offset from the center of the rectangle.
        /// </param>
        /// <param name="offsetY">
        /// The vertical offset from the center of the rectangle.
        /// </param>
        /// <returns>
        /// The position of the text shifted from the center of the rectangle.
        /// </returns>
        private static Vector2 GetPositionOfText(Rectangle rect, GUIStartPoint startPoint, int offsetX, int offsetY)
        {
            var position = new Vector2(rect.Center.X, rect.Center.Y);
            position.X += ShiftDirections[startPoint].X * offsetX;
            position.Y += ShiftDirections[startPoint].Y * offsetY;
            return position;
        }
    }
}
