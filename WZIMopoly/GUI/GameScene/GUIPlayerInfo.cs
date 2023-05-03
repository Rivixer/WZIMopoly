using Microsoft.Xna.Framework;
using System.Collections.Generic;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    internal class GUIPlayerInfo : GUIElement
    {
        private readonly GUITexture _guiFlag;
        private readonly GUITexture _guiBox;
        private readonly GUIText _guiNick;
        private readonly GUIText _guiMoney;

        private static readonly Dictionary<GUIStartPoint, (int X, int Y)> ShiftDirections = new()
        {
            { GUIStartPoint.TopLeft, new(1, 1) },
            { GUIStartPoint.TopRight, new(-1, 1) },
            { GUIStartPoint.BottomLeft, new(1, -1) },
            { GUIStartPoint.BottomRight, new(-1, -1) },
        };

        internal GUIPlayerInfo(PlayerInfoModel model)
        {
            Player player = model.Player;

            _guiFlag = new GUITexture($"Images/PlayerFlag{player.Color}", model.DefRectangle, model.StartPoint);
            AddChild(_guiFlag);

            var boxRectangle = GetBoxRectangle(_guiFlag, model.StartPoint);
            _guiBox = new GUITexture($"Images/PlayerBox{player.Color}", boxRectangle, GUIStartPoint.Center);
            InsertChild(_guiBox, 0);

            var nickPosition = GetPositionOfText(_guiFlag.UnscaledDestinationRect, model.StartPoint, -62, -33);
            _guiNick = new GUIText("Fonts/DebugFont", nickPosition, GUIStartPoint.Center, player.Nick, 2f);
            AddChild(_guiNick);

            var moneyPosition = GetPositionOfText(_guiBox.UnscaledDestinationRect, model.StartPoint, -2, 0);
            _guiMoney = new GUIText("Fonts/DebugFont", moneyPosition, Color.Black, GUIStartPoint.Center, $"{player.Money} ECTS", 1.5f);
            AddChild(_guiMoney);
        }

        /// <summary>
        /// Represents position of text.
        /// </summary>
        /// <param name="guiFlag">
        /// Represents image of player's flag.
        /// </param>
        /// <param name="startPoint">
        /// Represents startPoint for box Rectangle
        /// </param>
        private static Rectangle GetBoxRectangle(GUITexture guiFlag, GUIStartPoint startPoint)
        {
            var offsetX = -140;
            var offsetY = 42;
            var rectangle = new Rectangle(guiFlag.UnscaledDestinationRect.Center.X, guiFlag.UnscaledDestinationRect.Center.Y, 220, 50);
            rectangle.X += ShiftDirections[startPoint].X * offsetX;
            rectangle.Y += ShiftDirections[startPoint].Y * offsetY;
            return rectangle;
        }

        /// <summary>
        /// Represents position of text.
        /// </summary>
        /// <param name="rect">
        /// Represents current screen rectangle.
        /// </param>
        /// <param name="startPoint">
        /// Represents startPoint for text
        /// </param>
        /// <param name="offsetX">
        /// The X position parameter for text.
        /// </param>
        /// <param name="offsetY">
        /// The Y position parameter for text.
        /// </param>
        private static Vector2 GetPositionOfText(Rectangle rect, GUIStartPoint startPoint, int offsetX, int offsetX)
        {
            var position = new Vector2(rect.Center.X, rect.Center.Y);
            position.X += ShiftDirections[startPoint].X * offsetX;
            position.Y += ShiftDirections[startPoint].Y * offsetY;
            return position;
        }
    }
}
