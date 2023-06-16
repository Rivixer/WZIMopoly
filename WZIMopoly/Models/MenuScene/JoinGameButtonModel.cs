namespace WZIMopoly.Models.MenuScene
{
    /// <summary>
    /// Represents the join game button model.
    /// </summary>
    internal class JoinGameButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinGameButtonModel"/> class.
        /// </summary>
        public JoinGameButtonModel()
            : base("MenuJoin") { }

        public override void Update()
        {
            IsActive = NetworkService.Type == ConnectionType.Root;
        }
    }
}
