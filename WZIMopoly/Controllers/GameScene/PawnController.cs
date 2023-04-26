using Microsoft.Xna.Framework;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a controller of the pawn.
    /// </summary>
    internal class PawnController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PawnController"/> class.
        /// </summary>
        /// <param name="view">
        /// The view of the pawn.
        /// </param>
        /// <param name="model">
        /// The model of the pawn.
        /// </param>
        internal PawnController(GUIPawn view, PawnModel model)
            : base(view, model, false) { }

        /// <summary>
        /// Gets the view of the pawn.
        /// </summary>
        internal new GUIPawn View => (GUIPawn)base.View;

        /// <summary>
        /// Updates the position of the pawn.
        /// </summary>
        /// <param name="position">
        /// The new position of the pawn.<br/>
        /// It refers to the top-left corner.
        /// </param>
        internal void UpdatePosition(Point position)
        {
            View.UpdatePosition(position);
        }
    }
}
