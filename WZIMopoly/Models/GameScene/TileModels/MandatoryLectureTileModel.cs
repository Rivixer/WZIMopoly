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
        internal MandatoryLectureTileModel() : base() { }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player) { }
    }
}
