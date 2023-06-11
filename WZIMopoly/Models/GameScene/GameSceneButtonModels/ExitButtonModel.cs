namespace WZIMopoly.Models.GameScene.GameSceneButtonModels
{
    /// <summary>
    /// Represents the exit button model.
    /// </summary>
    internal class ExitButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExitButtonModel"/> class.
        /// </summary>
        public ExitButtonModel()
            : base("Exit")
        {
            IsActive = true;
        }
    }
}
