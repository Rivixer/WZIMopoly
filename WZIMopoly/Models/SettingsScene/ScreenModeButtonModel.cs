using WZIMopoly.Engine;

namespace WZIMopoly.Models.SettingsScene
{
    /// <summary>
    /// Represents the screen mode button model.
    /// </summary>
    internal class ScreenModeButtonModel : ButtonModel
    {
        /// <summary>
        /// The fullscreen flag, which indicates whether
        /// the screen mode button is for fullscreen mode.
        /// </summary>
        private readonly bool _fullscreen;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenModeButtonModel"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the screen mode button.
        /// </param>
        /// <param name="fullscreen">
        /// The fullscreen flag, which indicates whether
        /// the screen mode button is for fullscreen mode.
        /// </param>
        public ScreenModeButtonModel(string name, bool fullscreen)
            : base(name)
        {
            _fullscreen = fullscreen;
        }

        /// <inheritdoc/>
        public override void Update()
        {
            IsActive = ScreenController.IsFullScreen == _fullscreen;
        }
    }
}
