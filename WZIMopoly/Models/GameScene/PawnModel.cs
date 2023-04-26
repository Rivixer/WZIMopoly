namespace WZIMopoly.Models.GameScene
{
    internal class PawnModel : Model
    {
        /// <summary>
        /// The color of pawn.
        /// </summary>
        internal readonly string Color;

        /// <summary>
        /// Initializes a new instance of the <see cref="PawnModel"/> class.
        /// </summary>
        /// <param name="color">
        /// The color of pawn.
        /// </param>
        internal PawnModel(string color)
        {
            Color = color; 
        }
    }
}
