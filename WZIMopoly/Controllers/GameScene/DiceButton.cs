using WZIMopoly.Models;
using WZIMopoly.GUI;
using Microsoft.Xna.Framework.Content;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a dice button controller.
    /// </summary>
    internal sealed class DiceButton : ButtonController<GUIDiceButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiceButton"/> class.
        /// </summary>
        /// <param name="view">
        /// The view of the dice button controller.
        /// </param>
        /// <param name="model">
        /// The model of the dice button controller.
        /// </param>
        internal DiceButton(ButtonModel model, GUIDiceButton view)
            : base(model, view) { }

        /// <inheritdoc/>
        protected override void Load(ContentManager content)
        {
            base.Load(content);
        }
    }
}
