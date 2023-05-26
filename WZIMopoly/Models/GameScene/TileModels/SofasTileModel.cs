using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Sofas tile model.
    /// </summary>
    internal class SofasTileModel : TileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SofasTileModel"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node of the chance tile.
        /// </param>
        internal SofasTileModel(XmlNode node) : base(node) { }
    }
}
