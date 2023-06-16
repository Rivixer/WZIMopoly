namespace WZIMopoly.Models.MenuScene
{
    /// <summary>
    /// Represents the quit button model.
    /// </summary>
    internal class QuitButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuitButtonModel"/> class.
        /// </summary>
        public QuitButtonModel()
            : base("MenuQuit")
        {
            IsActive = true;
        }
    }
}
