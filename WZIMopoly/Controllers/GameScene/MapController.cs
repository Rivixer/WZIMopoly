using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a controller of the map.
    /// </summary>
    internal sealed class MapController : Controller<MapModel, GUIMap>
    {
        /// <summary>
        /// Initilizes a new instance of the <see cref="MapController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the map controller.
        /// </param>
        /// <param name="view">
        /// The view of the map controller.
        /// </param>
        public MapController(MapModel model, GUIMap view)
            : base(model, view) { }
    }
}
