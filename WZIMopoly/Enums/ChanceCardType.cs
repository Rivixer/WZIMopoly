using WZIMopoly.Controllers.GameScene.TileControllers;

namespace WZIMopoly.Enums
{
    /// <summary>
    /// Represents the chance card type.
    /// </summary>
    internal enum ChanceCardType
    {
        /// <summary>
        /// The chance card is drawn from
        /// <see cref="CanteenTileController"/>"
        /// </summary>
        Canteen,

        /// <summary>
        /// The chance card is drawn from
        /// <see cref="VendingMachineTileController"/>"
        /// </summary>
        VendingMachine,
    }
}
