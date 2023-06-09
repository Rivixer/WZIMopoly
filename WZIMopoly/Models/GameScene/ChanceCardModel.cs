using System;
using System.Collections.Generic;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents the chance card model.
    /// </summary>
    [Serializable]
    internal class ChanceCardModel : Model
    {
        /// <summary>
        /// The id of the chance card.
        /// </summary>
        /// <remarks>
        /// Used to identify the chance card
        /// and load the corresponding image.
        /// </remarks>
        private readonly int _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChanceCardModel"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of the chance card.
        /// </param>
        /// <param name="type">
        /// The type of the chance card.
        /// </param>
        /// <param name="chanceTiles">
        /// List of all tiles from which any chance card can be drawn.
        /// </param>
        /// <remarks>
        /// The <paramref name="id"/> parameter is used to identify
        /// the chance card and load the corresponding image.
        /// </remarks>
        public ChanceCardModel(int id, ChanceCardType type, List<ChanceTileModel> chanceTiles)
        {
            _id = id;
            Type = type;
            ChanceTiles = chanceTiles;
        }

        /// <summary>
        /// The delegate for the <see cref="OnDrawn"/> event.
        /// </summary>
        /// <param name="player">
        /// The player who drew the card.
        /// </param>
        public delegate void OnDrawnHandler(PlayerModel player);

        /// <summary>
        /// The event that is invoked when the card is drawn.
        /// </summary>
        [field: NonSerialized]
        public event OnDrawnHandler OnDrawn;

        /// <summary>
        /// Gets the id of the chance card.
        /// </summary>
        /// <value>
        /// The id of the chance card used to identify
        /// the chance card and load the corresponding image.
        /// </value>
        public int Id => _id;

        /// <summary>
        /// Gets the type of the chance card.
        /// </summary>
        public ChanceCardType Type { get; private set; }

        /// <summary>
        /// Gets the list of all tiles from which any chance card can be drawn.
        /// </summary>
        public List<ChanceTileModel> ChanceTiles { get; private set; }

        /// <summary>
        /// Activates the <see cref="OnDrawn"/> event.
        /// </summary>
        /// <param name="player">
        /// The player who drew the card.
        /// </param>
        public void OnCardDrawn(PlayerModel player)
        {
            OnDrawn?.Invoke(player);
        }

        /// <summary>
        /// Compares the <see cref="ChanceCardModel"/> with the given object.
        /// </summary>
        /// <param name="obj">
        /// The object to compare with.
        /// </param>
        /// <returns>
        /// True if the <see cref="ChanceCardModel"/> is equal to
        /// the given object, false otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is ChanceCardModel model)
            {
                return model.Id == Id && model.Type == Type;
            }
            return false;
        }

        /// <summary>
        /// Returns the hash code of the <see cref="ChanceCardModel"/>.
        /// </summary>
        /// <returns>
        /// The hash code of the <see cref="ChanceCardModel"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type);
        }
    }
}
