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
        private readonly GUIText _timeText;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUITimeButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the time button.
        /// </param>
        public GUITimeButton(TimeButtonModel model)
            : base(model, new Rectangle(1050, 640, 304, 140), GUIStartPoint.TopLeft, false, false)
        {
            _timeText = new GUIText("Fonts/WZIMFont", new Vector2(1205, 732), Color.Black, GUIStartPoint.Center, scale: 0.55f);
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (GameSettings.MaxGameTime is not null)
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
            base.Update();
            if (GameSettings.MaxGameTime is not null)
            {
                if (GameSettings.MaxGameTime >= 60)
                {
                    _timeText.Text = $"{GameSettings.MaxGameTime / 60}:{(GameSettings.MaxGameTime % 60)}:00";
                }
                else
                {
                    _timeText.Text = $"{GameSettings.MaxGameTime}:00";
                }
            }
        }
    }
}
