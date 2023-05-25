namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a pawn model.
    /// </summary>
    internal class PawnModel : Model
    {
        /// <summary>
        /// The color of pawn.
        /// </summary>
        public readonly string Color;

        /// <summary>
        /// Initializes a new instance of the <see cref="PawnModel"/> class.
        /// </summary>
        public PawnModel(string color)
        {
            Color = color;
        }
    }
}
