using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    internal class PurchasableTileController : TileController<PurchasableTileModel, GUIPurchasableTile>
    {
        public PurchasableTileController(PurchasableTileModel model, GUIPurchasableTile view) 
            : base(model, view) { }
    }
}
