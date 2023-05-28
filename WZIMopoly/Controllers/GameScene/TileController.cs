using WZIMopoly.Engine;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a base class for a tile controller
    /// with the <see cref="TileModel"/> model.
    /// </summary>
    /// <remarks>
    /// Used to get the Tile controller via <see cref="Models.Model.GetController{T}"/>.
    /// </remarks>
    internal abstract class TileController : Controller<TileModel, GUITile>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TileController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the tile.
        /// </param>
        /// <param name="view">
        /// The view of the tile.
        /// </param>
        protected TileController(TileModel model, GUITile view)
            : base(model, view) { }

        /// <summary>
        /// The method called when the tile is clicked.
        /// </summary>
        /*protected virtual void OnClick()
        {
            View.DrawCard();
            Debug.WriteLine("dupa");
        }*/

        /*public override void Update()
        {
            base.Update();       

            if (Model.IsActive && MouseController.WasLeftBtnClicked() && View.IsHovered)
            {
                OnClick();
            }
        }*/
    }

    /// <summary>
    /// Represents a base class for a tile controller
    /// with a specific model type.
    /// </summary>
    internal abstract class TileController<M> : TileController
        where M : TileModel
    {
        /// <summary>
        /// Gets the model of the tile.
        /// </summary>
        /// <value>
        /// The specified model of the tile.
        /// </value>
        internal new M Model => (M)base.Model;

        /// <summary>
        /// Initializes a new instance of the <see cref="TileController{M}"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the tile.
        /// </param>
        /// <param name="view">
        /// The view of the tile.
        /// </param>
        protected TileController(M model, GUITile view)
            : base(model, view) { }
    }
}
