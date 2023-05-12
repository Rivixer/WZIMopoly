using System;
using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    internal class SofasTileModel : TileModel
    {
        internal SofasTileModel(XmlNode node) : base(node) { }

        internal override void OnStand(PlayerModel player)
        {
            throw new NotImplementedException();
        }
    }
}
