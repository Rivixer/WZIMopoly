using Microsoft.Xna.Framework;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Models.JoinScene;
using WZIMopoly.Utils.PositionExtensions;

namespace WZIMopoly.GUI.JoinScene
{
    /// <summary>
    /// The view for the player nick.
    /// </summary>
    internal class GUIPlayerNick : GUIEditableText
    {
        /// <summary>
        /// The model of the player nick.
        /// </summary>
        private readonly PlayerNickModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIPlayerNick"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the player nick.
        /// </param>
        public GUIPlayerNick(PlayerNickModel model)
            : base("Fonts/WZIMFont", new Vector2(960, 261), Color.Black, GUIStartPoint.Center, "", 0.65f, 10)
        {
            _model = model;
        }

#pragma warning disable CA1822 // Mark members as static
        /// <summary>
        /// Gets whether the player box is hovered.
        /// </summary>
        // TODO: Make the rectangle non-static.
        public bool IsHovered => MouseController.IsHover(new Rectangle(764, 224, 392, 73).ToCurrentResolution());
#pragma warning restore CA1822 // Mark members as static

        /// <inheritdoc/>
        public override void Update()
        {
            if (!IsSelected)
            {
                Text = _model.PlayerNick;
            }
        }
    }
}
