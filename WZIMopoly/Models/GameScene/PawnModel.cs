namespace WZIMopoly.Models.GameScene
{
    internal class PawnModel : Model
    {
        /// <summary>
        /// The color of pawn
        /// </summary>
        internal readonly string Color;

        internal PawnModel(string Color)
        {
            this.Color = Color; 
        }
    }
}
