using Castle.Components.DictionaryAdapter.Xml;

namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the subtract time button model.
    /// </summary>
    internal class SubtractTimeButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractTimeButtonModel"/> class.
        /// </summary>
        public SubtractTimeButtonModel()
            : base("Trade")
        {
            IsActive = true;
        }
    }
}
