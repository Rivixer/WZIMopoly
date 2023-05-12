using System;
using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    internal class StartTileModel : TileModel, ICrossable
    {
        /// <summary>
        /// The amount of ECTS points that the player receives
        /// after passing through the tile.
        /// </summary>
        private readonly int _reward;

        internal StartTileModel(XmlNode node) : base(node)
        {
            _reward = int.Parse(node.SelectSingleNode("reward").InnerText);
        }

        internal override void OnStand(PlayerModel player)
        {
            throw new NotImplementedException();
        }

        void ICrossable.OnCross(PlayerModel player)
        {
            throw new NotImplementedException();
        }
    }
}
