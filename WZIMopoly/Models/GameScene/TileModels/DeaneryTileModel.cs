using System;
using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    internal class DeaneryTileModel : TileModel
    {
        internal DeaneryTileModel(XmlNode node) : base(node) { }

        internal override void OnStand(PlayerModel player)
        {
            throw new NotImplementedException();
        }
    }
}
