using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    internal class PawnController : Controller
    {
        internal PawnController(GUIPawn view, PawnModel model)
    : base(view, model, false) { }

        internal new GUIPawn View => (GUIPawn)base.View;

        internal void UpdatePosition(Point position)
        {
            View.UpdatePosition(position);
        }
    }
}
