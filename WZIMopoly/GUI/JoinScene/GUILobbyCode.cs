using Microsoft.Xna.Framework;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Models.JoinScene;
using WZIMopoly.Utils.PositionExtensions;

namespace WZIMopoly.GUI.JoinScene
{
    /// <summary>
    /// Represents a view for the lobby code.
    /// </summary>
    internal class GUILobbyCode : GUIEditableText
    {
        /// <summary>
        /// The model of the lobby code.
        /// </summary>
        private readonly LobbyCodeModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUILobbyCode"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the lobby code.
        /// </param>
        public GUILobbyCode(LobbyCodeModel model)
            : base("Fonts/WZIMFont", new Vector2(960, 413), Color.Black, GUIStartPoint.Center, "", 0.65f, 6)
        {
            _model = model;
        }

#pragma warning disable CA1822 // Mark members as static
        /// <summary>
        /// Gets whether the player box is hovered.
        /// </summary>
        // TODO: Make the rectangle non-static.
        public bool IsHovered => MouseController.IsHover(new Rectangle(764, 376, 392, 73).ToCurrentResolution());
#pragma warning restore CA1822 // Mark members as static

        /// <inheritdoc/>
        public override void Update()
        {
            if (!IsSelected)
            {
                Text = _model.LobbyCode;
            }
        }
    }
}
