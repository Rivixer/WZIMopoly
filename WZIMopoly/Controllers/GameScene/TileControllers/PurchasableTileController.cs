using WZIMopoly.Engine;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    internal class PurchasableTileController : TileController<PurchasableTileModel, GUIPurchasableTile>
    {
        public PurchasableTileController(PurchasableTileModel model, GUIPurchasableTile view) 
            : base(model, view) { }

        /// <summary>
        /// The method called when the tile is clicked.
        /// </summary>
        protected virtual void OnClick()
        {
            View.DrawCard();
        }

        public override void Update()
        {
            base.Update();

            if (Model.IsActive && MouseController.WasLeftBtnClicked() && View.IsHovered)
            {
                OnClick();
            }
        }
    }
}
