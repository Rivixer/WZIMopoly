using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Mandatory Lecture tile model.
    /// </summary>
    internal class MandatoryLectureTileModel : TileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MandatoryLectureTileModel"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node of the chance tile.
        /// </param>
        internal MandatoryLectureTileModel(XmlNode node) : base(node) { }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player)
        {
            throw new System.NotImplementedException();
        }
    }
}
