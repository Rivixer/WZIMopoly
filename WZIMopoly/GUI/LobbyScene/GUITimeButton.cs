using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Enums;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.GUI.LobbyScene
{
    /// <summary>
    /// Represents the time button view.
    /// </summary>
    internal class GUITimeButton : GUIButton<TimeButtonModel>
    {
        /// <summary>
        /// The text of time.
        /// </summary>
        private GUIText _timeText;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUITimeButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the time button.
        /// </param>
        public GUITimeButton(TimeButtonModel model)
            : base(model, new Rectangle(1050, 640, 304, 140), GUIStartPoint.TopLeft, false, false)
        {
            _timeText = new GUIText("Fonts/WZIMFont", new Vector2(1205, 695), Color.Black, GUIStartPoint.Top, GameSettings.MatchDuration.ToString() + ":00", 0.8f);
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (GameSettings.MatchDuration != 0)
            {
                _timeText.Draw(spriteBatch);
            }
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            base.Load(content);
            _timeText.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            base.Recalculate();
            _timeText.Recalculate();
        }

        /// <inheritdoc/>
        public override void Update()
        {
            if (GameSettings.MatchDuration != 0)
            {
                _timeText.Text = GameSettings.MatchDuration.ToString() + ":00";
            }
        }
    }
}
