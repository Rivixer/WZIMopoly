using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents the chance card controller.
    /// </summary>
    internal class ChanceCardController : Controller<ChanceCardModel, GUIChanceCard>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChanceCardController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the chance card controller.
        /// </param>
        /// <param name="view">
        /// The view of the chance card controller.
        /// </param>
        public ChanceCardController(ChanceCardModel model, GUIChanceCard view)
            : base(model, view) { }
    }
}
