using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Elevator tile model.
    /// </summary>
    internal class ElevatorTileModel : TileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElevatorTileModel"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node of the chance tile.
        /// </param>
        internal ElevatorTileModel(XmlNode node) : base(node) { }
    }
}
