﻿using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.MenuScene;

namespace WZIMopoly.GUI.MenuScene
{
    /// <summary>
    /// Represents the settings button view.
    /// </summary>
    internal class GUISettingsButton : GUIButton<SettingsButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUISettingsButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the settings button.
        /// </param>
        public GUISettingsButton(SettingsButtonModel model)
            : base(model, new Rectangle(660, 780, 125, 125), GUIStartPoint.Left)
        {
            SetButtonHoverArea(5, 0.75f);
        }
    }
}
