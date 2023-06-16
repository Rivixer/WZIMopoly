namespace WZIMopoly.Models.MenuScene
{
    /// <summary>
    /// Represents the new game button model.
    /// </summary>
    internal class NewGameButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewGameButtonModel"/> class.
        /// </summary>
        public NewGameButtonModel()
            : base("MenuNew")
        {
            IsActive = true;
        }
    }
}
