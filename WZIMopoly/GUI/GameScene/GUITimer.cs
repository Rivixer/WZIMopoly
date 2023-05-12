using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a timer view.
    /// </summary>
    /// <remarks>
    /// A timer view contains the game's time.
    /// </remarks>
    internal class GUITimer : GUIElement
    {
        /// <summary>
        /// The view of the timer texture.
        /// </summary>
        /// <remarks>
        /// It is the background of the timer.
        /// </remarks>
        private readonly GUITexture _guiBackground;

        /// <summary>
        /// The view of the timer.
        /// </summary>
        private readonly GUITextTimer _guiTime;

        /// <summary>
        /// The model of the timer.
        /// </summary>
        private readonly TimerModel _timerModel;

        /// <summary>
        /// Private extended <see cref="GUIText"/> to easly changing text.
        /// </summary>
        private class GUITextTimer : GUIText
        {
            /// <summary>
            /// Initializes a new instance of <see cref="GUITextTimer"/> class.
            /// </summary>
            /// <param name="fontPath">
            /// The path to the font that will be used to display the text.
            /// </param>
            /// <param name="defPosition">
            /// The position vector of the element specified for 1920x1080 resolution.
            /// </param>
            /// <param name="color">
            /// The color of the element.
            /// </param>
            /// <param name="startPoint">
            /// The starting position of the element for which <paramref name="defPosition"/> has been specified.
            /// </param>
            /// <param name="text">
            /// The text to display.<br/>
            /// Defaults to empty string.
            /// </param>
            /// <param name="scale">
            /// The scale of the text. <br/>
            /// Defaults to 1.0f.
            /// </param>
            /// 
            internal GUITextTimer(string fontPath, Vector2 defPosition, Color color, GUIStartPoint startPoint, string text, float scale = 1.0f) :base(fontPath, defPosition, startPoint, text, scale)
            {
                Text = text;
                Color = color;
            }

            /// <summary>
            /// Gets or sets the text of the element.
            /// </summary>
            /// <value>
            /// The text to display.
            /// </value>
            internal new string Text
            {
                set => base.Text = value;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GUITimer"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the timer.
        /// </param>
        internal GUITimer(TimerModel model)
        {
            _timerModel = model;
            _guiBackground = new GUITexture($"Images/Timer", model.DefRectangle, model.StartPoint);
            AddChild(_guiBackground);

            _guiTime = new GUITextTimer("Fonts/DebugFont", new Vector2(model.DefRectangle.X, 50), Color.Black, GUIStartPoint.Center, model.Time.ToString(), 2f);
            AddChild(_guiTime);
        }


        /// <inheritdoc/>
        internal override void Update()
        {
            _timerModel.UpdateTime();

            _guiTime.Text = _timerModel.Time.ToString(@"mm\:ss");
            base.Update();
        }
    }
}