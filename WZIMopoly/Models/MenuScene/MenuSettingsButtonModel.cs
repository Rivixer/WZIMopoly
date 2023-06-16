namespace WZIMopoly.Models.MenuScene
{
    /// <summary>
    /// Represents the settings button model.
    /// </summary>
    internal class MenuSettingsButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuSettingsButtonModel"/> class.
        /// </summary>
        public MenuSettingsButtonModel()
            : base("MenuSettings")
        {
            IsActive = true;
        }
    }
}
