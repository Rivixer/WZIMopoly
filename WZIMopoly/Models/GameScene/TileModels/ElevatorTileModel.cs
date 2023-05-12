using System;
using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    internal class ElevatorTileModel : TileModel
    {
        internal ElevatorTileModel(XmlNode node) : base(node) { }

        internal override void OnStand(PlayerModel player)
        {
            throw new NotImplementedException();
        }
    }
}
