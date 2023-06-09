using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents the jail controller.
    /// </summary>
    internal class JailController : Controller<JailModel, GUIJail>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JailController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the jail controller.
        /// </param>
        /// <param name="view">
        /// The view of the jail controller.
        /// </param>
        public JailController(JailModel model, GUIJail view)
            : base(model, view) { }
    }
}
