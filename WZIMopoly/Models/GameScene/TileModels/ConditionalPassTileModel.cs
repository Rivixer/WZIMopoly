using System;
using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    internal class ConditionalPassTileModel : TileModel
    {
        /// <summary>
        /// The amount of ECTS to be paid.
        /// </summary>
        internal readonly int Tax;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalPassTileModel"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        internal ConditionalPassTileModel(XmlNode node) : base(node)
        {
            Tax = int.Parse(node.SelectSingleNode("tax").InnerText);
        }

        internal override void OnStand(PlayerModel player)
        {
            throw new NotImplementedException();
        }
    }
}
