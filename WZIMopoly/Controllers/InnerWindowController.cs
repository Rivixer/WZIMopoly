using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers
{
    internal class InnerWindowController : Controller<InnerWindowModel, GUIInnerWindow>
    {
        public InnerWindowController(InnerWindowModel model, GUIInnerWindow view) 
            : base(model, view)
        {
        }
    }
}
