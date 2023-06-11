using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using WZIMopoly.GUI;
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
        /// Gets the sound effect of the money.
        /// </summary>
        // It is a temporary solution, because the deadline is coming
        // and code is not adapted for playing sound effects.
        public static SoundEffect MoneySound { get; private set; }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            base.Load(content);
            MoneySound ??= content.Load<SoundEffect>("Sounds/Money");
        }
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

    /// <summary>
    /// Represents a base class for a tile controller
    /// with a specific model and view type.
    /// </summary>
    /// <typeparam name="M">
    /// The model type.
    /// </typeparam>
    /// <typeparam name="V">
    /// The view type.
    /// </typeparam>
    internal abstract class TileController<M, V> : TileController
        where M : TileModel
        where V : GUITile
    {
        /// <summary>
        /// Gets the model of the tile.
        /// </summary>
        /// <value>
        /// The specified model of the tile.
        /// </value>
        internal new M Model => (M)base.Model;

        /// <summary>
        /// Gets the view of the tile.
        /// </summary>
        /// <value>
        /// The specidfied view of the tile.
        /// </value>
        internal new V View => (V)base.View;

        /// <summary>
        /// Initializes a new instance of the <see cref="TileController{M}"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the tile.
        /// </param>
        /// <param name="view">
        /// The view of the tile.
        /// </param>
        protected TileController(M model, V view)
            : base(model, view) { }
    }
}
