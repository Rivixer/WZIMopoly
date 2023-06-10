using System;

#nullable enable

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents a base controller for a chance tile model.
    /// </summary>
    /// <remarks>
    /// A chance tile is a tile when the player lands on it, they draw a chance card.
    /// </remarks>
    [Serializable]
    internal abstract class ChanceTileModel : TileModel
    {
        /// <summary>
        /// The drawn card.
        /// </summary>
        private ChanceCardModel? _drawnCard;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChanceTileModel"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        internal ChanceTileModel(int id) : base(id) { }

        /// <summary>
        /// Gets or sets the drawn card.
        /// </summary>
        public ChanceCardModel? DrawnCard
        {
            get => _drawnCard;
            set => _drawnCard = value;
        }

        /// <inheritdoc/>
        public override void Update(TileModel model)
        {
            base.Update(model);
            if (model is ChanceTileModel t)
            {
                DrawnCard = t.DrawnCard;
            }
        }
    }
}
