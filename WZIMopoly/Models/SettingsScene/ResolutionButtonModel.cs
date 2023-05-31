using WZIMopoly.Engine;

namespace WZIMopoly.Models.SettingsScene
{
    /// <summary>
    /// Represents the resolution settings button model.
    /// </summary>
    internal class ResolutionButtonModel : ButtonModel
    {
        /// <summary>
        /// The width of the resolution settings.
        /// </summary>
        private readonly int _width;

        /// <summary>
        /// The height of the resolution settings.
        /// </summary>
        private readonly int _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionButtonModel"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the resolution settings button.
        /// </param>
        /// <param name="width">
        /// The width of the resolution settings.
        /// </param>
        /// <param name="height">
        /// The height of the resolution settings.
        /// </param>
        public ResolutionButtonModel(string name, int width, int height)
            : base(name)
        {
            _width = width;
            _height = height;
        }

        /// <inheritdoc/>
        public override void Update()
        {
            IsActive = ScreenController.Width == _width && ScreenController.Height == _height;
        }
    }
}
