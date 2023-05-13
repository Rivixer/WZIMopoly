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
        /// The view of the timer text.
        /// </summary>
        private readonly GUIText _guiTime;

        /// <summary>
        /// The model of the timer controller..
        /// </summary>
        private readonly TimerModel _timerModel;

        /// <summary>
        /// The destination rectangle of the element specified for 1920x1080 resolution.
        /// </summary>
        private readonly Rectangle _defRectangle;

        /// <summary>
        /// The place where <see cref="_defRectangle"/> has been specified.
        /// </summary>
        private readonly GUIStartPoint _startPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUITimer"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the timer.
        /// </param>
        internal GUITimer(TimerModel model)
        {
            _timerModel = model;
            _defRectangle = new Rectangle(960, 0, 170, 54);
            _startPoint = GUIStartPoint.Top;

            _guiBackground = new GUITexture($"Images/Timer", _defRectangle, _startPoint);
            
            var textPosition = new Vector2(_defRectangle.X, _defRectangle.Height / 2);
            _guiTime = new GUIText("Fonts/WZIMFont", textPosition, Color.Black, GUIStartPoint.Center, model.Time.ToString(), 0.51f);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            _guiTime.Text = _timerModel.Time.ToString(@"mm\:ss");
            base.Update();
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            _guiBackground.Draw(spriteBatch);
            _guiTime.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            var elements = new List<GUIElement>() { _guiBackground, _guiTime };
            elements.ForEach(x => x.Load(content));
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            var elements = new List<GUIElement>() { _guiBackground, _guiTime };
            elements.ForEach(x => x.Recalculate());
        }
    }
}
