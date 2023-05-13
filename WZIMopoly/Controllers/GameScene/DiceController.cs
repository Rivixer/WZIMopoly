using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents the dice controller.
    /// </summary>
    internal class DiceController : Controller<DiceModel, GUIDice>
    {
        /// <summary>
        /// Initilizes a new instance of the <see cref="DiceController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the dice controller.
        /// </param>
        /// <param name="view">
        /// The view of the dice controller.
        /// </param>
        internal DiceController(DiceModel model, GUIDice view) 
            : base(model, view) { }
    }
}
