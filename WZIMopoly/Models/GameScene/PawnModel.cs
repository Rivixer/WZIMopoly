namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a pawn model.
    /// </summary>
    internal class PawnModel : Model
    {
        /// <value>
        /// The color of pawn.
        /// </value>
        public string Color { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PawnModel"/> class.
        /// </summary>
        public PawnModel(string color)
        {
            Color = color;
        }
    }
}
