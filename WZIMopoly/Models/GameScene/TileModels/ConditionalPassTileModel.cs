using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Conditional Pass tile model.
    /// </summary>
    internal class ConditionalPassTileModel : TileModel
    {
        /// <summary>
        /// The amount of ECTS to be paid.
        /// </summary>
        internal readonly int Tax;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalPassTileModel"/> class.
        /// </summary>
        internal ConditionalPassTileModel() : base()
        {
            Tax = int.Parse(MapModel.XmlNode.SelectSingleNode("tax").InnerText);
        }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player) 
        {
            player.Money -= Tax;
        }
    }
}
