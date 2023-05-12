using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    internal class MandatoryLectureTileModel : TileModel
    {
        internal MandatoryLectureTileModel(XmlNode node) : base(node) { }

        internal override void OnStand(PlayerModel player)
        {
            throw new System.NotImplementedException();
        }
    }
}
