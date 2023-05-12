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
        /// <remarks>
        /// Loads tiles from a xml file calling <see cref="LoadTiles()"/> method.
        /// </remarks>
        internal MapController(MapModel model, GUIMap view)
            : base(model, view) { }
    }
}
