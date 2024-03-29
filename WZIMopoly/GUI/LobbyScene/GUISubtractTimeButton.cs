﻿using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.GUI.LobbyScene
{
    /// <summary>
    /// Represents the subtract time button view.
    /// </summary>
    internal class GUISubtractTimeButton : GUIButton<SubtractTimeButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUISubtractTimeButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the subtract time button.
        /// </param>
        public GUISubtractTimeButton(SubtractTimeButtonModel model)
            : base(model, new Rectangle(1125, 734, 50, 50), GUIStartPoint.Right, false, false) { }
    }
}
