using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using WZIMopoly.Enums;
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
        private readonly GUIText _guiTime;

        /// <summary>
        /// The model of the timer.
        /// </summary>
        private readonly TimerModel _timerModel;

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

            _guiTime = new GUIText("Fonts/DebugFont", new Vector2(model.DefRectangle.X, 50), Color.Black, GUIStartPoint.Center, model.Time.ToString(), 2f);
        }

        /// <inheritdoc/>
        internal override void Update()
        {
            _guiTime.Text = _timerModel.Time.ToString(@"mm\:ss");
            base.Update();
        }

        /// <inheritdoc/>
        internal override void Draw(SpriteBatch spriteBatch)
        {
            _guiBackground.Draw(spriteBatch);
            _guiTime.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        internal override void Load(ContentManager content)
        {
            var elements = new List<GUIElement>() { _guiBackground, _guiTime };
            elements.ForEach(x => x.Load(content));
        }

        /// <inheritdoc/>
        internal override void Recalculate()
        {
            var elements = new List<GUIElement>() { _guiBackground, _guiTime };
            elements.ForEach(x => x.Recalculate());
        }
    }
}