namespace WZIMopoly.Models.GameScene.GameSceneButtonModels
{
    /// <summary>
    /// Represents the settings button model.
    /// </summary>
    internal sealed class SettingsButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsButtonModel"/> class.
        /// </summary>
        internal SettingsButtonModel()
            : base("Settings")
        {
            IsActive = true;
        }
    }
}
